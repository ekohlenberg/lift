using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Threading;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for DBObject.
	/// </summary>
	public class DatabaseObject : ModelObject
	{
		private string baseTable = null;
		private string primaryKey = null;
		private string connectionName = "defaultconnection";
		private Hashtable relations;
		public string paramNameCol = null;
		public string paramValueCol = null;
		
		private bool autoIdentity = false;
		
		protected static Hashtable connectionDBTypeMap = new Hashtable();
		protected bool initializedDBTypeMap = false;

		protected ArrayList rwk = null;



        
		public DatabaseObject() 
		{
			rwk = new ArrayList();
			relations = new Hashtable();

			setPersistence( new DatabasePersist( this ) );
		}

			
		public virtual string BaseTable
		{
			get
			{
				return baseTable.ToUpper();
			}
			set
			{
				baseTable = value;
			}
		}

		public virtual bool AutoIdentity
		{
			get
			{
				return autoIdentity;
			}
			set
			{
				autoIdentity = value;
			}
		}

		public virtual string PrimaryKey
		{
			get
			{
				return primaryKey;
			}
			set
			{
				primaryKey = value;
			}
		}

		public virtual ArrayList RWK
		{
			get
			{
				return rwk;
			}
		}

        public virtual void attach( string name, PropertyBase property )
        {
            property.Name = name;
            property.Owner = this;
        }

        
		public override long doCommand(string action)
		{
			long result = 0;
			try
			{
				result = base.doCommand (action);
			}
			catch( Exception e)
			{
				Logger.log( this, e, e.Message );
			}
			finally
			{
			}	

			return result;
		}


		public override DataSet doQuery(string action)
		{
			DataSet result = null;

			try
			{
				result = base.doQuery (action);
			}
			catch( Exception e)
			{
				Logger.log( this, e, e.Message );
			}
			finally
			{
			}

			return result;
		}

        public virtual List<T> doQuery<T>(string action) where T : System.Collections.IDictionary, new()
        {
            List<T> result = new List<T>();
   
            DataSet dataSet = doQuery(action);

            if (dataSet.Tables.Count == 1)
            {
                foreach (DataRow r in dataSet.Tables[0].Rows)
                {
                    T o = new T();
                    foreach (DataColumn c in r.Table.Columns)
                    {
                        o.Add(c.ColumnName, r[c.ColumnName]);
                    }
                    result.Add(o);
                }
            }

            return result;

        }


        public virtual T doSingleObjectQuery<T>(string action) where T : System.Collections.IDictionary, new()
        {
            T result = new T();

            DataSet dataSet = doQuery(action);

            if (DatabasePersist.hasData(dataSet))
            {
                DataRow r = dataSet.Tables[0].Rows[0];

                foreach (DataColumn c in r.Table.Columns)
                {
                    result.Add(c.ColumnName, r[c.ColumnName]);
                }
            }

            return result;

        }

        public long doCommandTemplate(string sqlTemplate)
        {
            long result = 0;
            string sql = string.Empty;
            try
            {
                DatabasePersist d = (DatabasePersist)persistence;
                sql = d.processSql(sqlTemplate);
                result = d.executeCommand(sql);
            }
            catch (Exception x)
            {
                Logger.log(this, x, "Error processing " + sql);
            }
            return result;
        }

        public DataSet doQueryTemplate(string sqlTemplate)
        {
            DataSet result = null;
            DatabasePersist d = (DatabasePersist)persistence;
            string sql = d.processSql(sqlTemplate);
            result = d.executeQuery(sql);
            return result;
        }

		public virtual string ConnectionName
		{
			get
			{
				return this.connectionName;
			}
			set
			{
				this.connectionName = value;
			}
		}

		public virtual string DBTypeName
		{
			get
			{	
				string dbtype = "dbase";
				
				lock( connectionDBTypeMap )
				{
					if (!initializedDBTypeMap)
					{
						try
						{
							AppSettingsReader settingsReader = new AppSettingsReader();
		
							dbtype = (string) settingsReader.GetValue(connectionName + ".dbtype", typeof(System.String));

							if (!connectionDBTypeMap.ContainsKey( connectionName))
							{
								connectionDBTypeMap.Add( connectionName, dbtype );
							}
						}
						catch
						{
						}

						initializedDBTypeMap = true;
					}
				
					if (connectionDBTypeMap.Contains(connectionName))
					{
						dbtype = (string) connectionDBTypeMap[connectionName];
					}
					
				}
							
				return dbtype;
			}
		}
				
		public virtual DBFormatter DatabaseFormatter
		{
			get
			{
				string dbTypeName = this.DBTypeName;

				DatabasePersist p = (DatabasePersist) this.persistence;

				DBFormatter dbFormatter = p.getDBFormatter( dbTypeName );

				return dbFormatter;
			}
		}
				

	
		public  virtual string ParamNameCol
		{
			get
			{
				return paramNameCol;
			}
			set
			{
				paramNameCol = value;
			}
		}


		public virtual string ParamValueCol
		{
			get
			{
				return paramValueCol;
			}
			set
			{
				paramValueCol = value;
			}
		}

		public virtual long getPrimaryKey()
		{
			long pk = 0; 

			if (this.primaryKey != null)
			{
				pk = this.getLong(this.primaryKey);
			}
			else 
			{
				throw new ModelObjectException(this, "getPrimaryKey(): Primary key has not been defined.");
			}

			return pk;
		}



		public virtual  void addRelation( string name, Type t, string column )
		{
			Hashtable relationMap = new Hashtable();

			relationMap.Add( "type", t );
			relationMap.Add( "column", column );

			relations.Add( name, relationMap );

		}
		
		public virtual Hashtable getRelation( string name )
		{
			Hashtable relationMap = null;
			if (relations.ContainsKey( name ))
			{
				relationMap = (Hashtable) relations[name];
			}
			else
			{
				throw new ModelObjectException(this, string.Format("DomainObject.getRelation(): Relation '{0}' has not been defined.", name));
			}
			return relationMap;
		}

		public virtual DataSet doQueryRelation( string relation )
		{
			DataSet dataSet = null;

			if (persistence != null)
			{
				try
				{
					dataSet = persistence.doQueryRelation( relation );
				}
				catch( Exception e)
				{
					throw new ModelObjectException( this, e, string.Format("DomainObject.doQueryRelation(): Error occurred while retrieving relation '{0}'.", relation ));
				}
			}
			else
			{
				throw new ModelObjectException( this, "DomainObject.doQuery(): Persistence mechanism has not been assigned.");
			}

			return dataSet;
		}

        public virtual bool exists()
        {
            bool result = false;

            DataSet ds = doQuery("select");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

		public virtual ArrayList doQueryObjectsRelation( string relation  )
		{
			ArrayList domainObjects = new ArrayList();

			Hashtable relationMap = (Hashtable) relations[relation];

			Type t = (Type) relationMap["type"];

			System.Data.DataSet dataSet = this.doQueryRelation( relation ); 

			foreach (System.Data.DataTable table in dataSet.Tables )
			{
				foreach( System.Data.DataRow row in table.Rows )
				{	
					DatabaseObject domainObject = (DatabaseObject) t.Assembly.CreateInstance( t.ToString());

					for (int col = 0; col < table.Columns.Count; col++)
					{
						domainObject.Add( table.Columns[col].ColumnName, row[col] );
					}
		
					domainObjects.Add( domainObject );
				}
			}

			return domainObjects;
		}

		public override string ToString()
		{
			string s = this.GetType().ToString();
			s += "\r\nConnectionName=" +  ConnectionName;
			s += "\r\nBaseTable=" +  BaseTable;
			s += "\r\nPrimaryKey=" +  PrimaryKey;
			s += "\r\n";

			object o = null;
			for (int i = 0; i < this.Count; i++)
			{
				o = this[i];

				s += (string) this.Names[i];

				if (o != null)
				{
					s += ":" + this[i].GetType().ToString();
					s += "\t\t";
					s += o.ToString();
				}
				else
				{
					s += ":<undefined value>";
					s += "\t\t";
					s += "null";
				}

				s += "\r\n";
			}


			return s;
		}

		public override OrderedHashtable copy()
		{
			DatabaseObject dbObject = (DatabaseObject) base.copy ();
/*
			dbObject.primaryKey = this.primaryKey;
			dbObject.baseTable = this.baseTable;

			dbObject.RWK.Clear();

			foreach(string _rwk in RWK)
			{
				dbObject.RWK.Add( _rwk );
			}
*/
			return dbObject;

		}

        public static bool isNullOrEmpty(DataSet ds)
        {
            bool result = true;

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
	}
}
