using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Text;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for OleDbPersist.
	/// </summary>
		
	public delegate void DoCommandDelegate( string action, Hashtable param );
    
	public class DatabasePersist : Persist, IPersistable
	{
		public ArrayList  parameters = null;
		public new DatabaseObject domainObject;

		public static Hashtable dbFormatterMap = new Hashtable();
		public static bool initializedDbFormatter = false;

		public static event DoCommandDelegate DoCommandTrigger;
        	
		public DatabasePersist( DatabaseObject domainObject ) : base( domainObject )
		{
			initializeDBFormatterMap();

			this.domainObject = domainObject;
			parameters = new ArrayList();			
		}

		protected virtual void initializeDBFormatterMap()
		{
			lock( dbFormatterMap )
			{
				if (initializedDbFormatter == false)
				{
					dbFormatterMap.Add( "dbase", new DBaseFormatter() );
					dbFormatterMap.Add( "sqlserver", new SqlServerFormatter() );
					initializedDbFormatter = true;
				}
			}
		}

		public virtual DBFormatter getDBFormatter( string dbTypeName )
		{
			DBFormatter dbFormatter = null;

			lock( dbFormatterMap )
			{

				if (dbFormatterMap.ContainsKey( dbTypeName ))
				{
					dbFormatter = (DBFormatter) dbFormatterMap[ dbTypeName ];
				}
				else
				{
					dbFormatter = new DBFormatter();
				}
			}

			return dbFormatter;
		}
        
		protected override string getResourceString(System.Resources.ResourceManager rm, string action)
		{
			string content = null;

			string candidateAction = domainObject.DBTypeName + "_" + action;

            try
            {
                content = rm.GetString(candidateAction);
            }
            catch
            {
                content = null;
            }

            try
            {
                if (content == null)
                {
                    content = rm.GetString(action);
                }
            }
            catch
            {
                content = null;
            }

			return content;
		}
        
		public override long doCommand( string action )
		{
			long result = 0;
			result = base.doCommand (action);

			ModelObject param  = new ModelObject();
			param.copyFrom( this.domainObject );
			param["ACTION"] = action;
			
			if (DoCommandTrigger != null)
			{
				foreach( DoCommandDelegate dc in DoCommandTrigger.GetInvocationList())
				{
					dc( this.domainObject.GetType().ToString (), param );
				}
			}
						

			return result;
		}
		
		public override DataSet handleQuery( string action )
		{
			
			DataSet dataSet = null;
		
			try
			{
				string sqlTemplate = contentFromAction( action );

				dataSet = executeQueryTemplate( sqlTemplate );
			}
			catch( Exception e)
			{
				string m = string.Format("OleDbPersist.handleQuery(): Error occurred while processing template {0}", action );

				throw new PersistException( this, e, m);
			}
		
			return dataSet;
		}

		public virtual DataSet executeQueryTemplate( string sqlTemplate )
		{
			DataSet dataSet;

			string sqlQuery = processSql( sqlTemplate );

			dataSet = executeQuery( sqlQuery );

			return dataSet;
		}
        
		public virtual string processSql( string _sqlTemplate )
		{
			string sql = "";

			string sqlTemplate = _sqlTemplate.Replace( "&amp;", "&" );
			sqlTemplate = sqlTemplate.Replace( "&lt;", "<" );
			sqlTemplate = sqlTemplate.Replace( "&gt;", ">" );

			parameters.Clear();

			// discern parameters
			int currentIndex = sqlTemplate.IndexOf("${") ;
			while( currentIndex != -1 )
			{
				currentIndex += 2;
				string parameter = "";

				while( sqlTemplate[currentIndex] != '}')
				{
					parameter += sqlTemplate[currentIndex];
					currentIndex++;
				}

				parameters.Add( parameter );

				currentIndex = sqlTemplate.IndexOf("${", currentIndex ) ;
			}

			// make template OLEDB-compliant by replacing named parameters with question marks
			sql = sqlTemplate;
			for (int iParam = 0; iParam < parameters.Count; iParam++)
			{
				string parameter  = (string) parameters[iParam];
				sql = sql.Replace( "${" + parameter + "}", "?" );
				parameters[iParam] = parameter.ToLower();
			}
			
			return sql;
		}

		public override long handleCommand( string action )
		{
		
			long result = 0;
		
			try
			{
				string sqlTemplate = contentFromAction( action );

				string sqlCommand = processSql( sqlTemplate );	
			
				result = executeCommand( sqlCommand );
			}
			catch( Exception e)
			{
				Logger.log( this, e, e.Message );
				throw;
			}
			
			return result;
		}

		public virtual DataSet executeQuery( string sqlQuery )
		{
			System.Data.DataSet  dataSet = null;
			DatabaseConnection sc = null;

			try
			{

                Logger.log(Logger.Level.SQL, sqlQuery);

				sc = DatabaseConnectionPool.getConnectionPool().getConnection( domainObject.ConnectionName );
						
				System.Data.OleDb.OleDbDataAdapter queryAdapter = new OleDbDataAdapter( sqlQuery, sc.Connection );

				bindParameters( queryAdapter.SelectCommand );

				dataSet = new DataSet();
				queryAdapter.Fill( dataSet );
				queryAdapter.Dispose();

				DatabaseConnectionPool.getConnectionPool().releaseConnection( sc );
			}
			catch( Exception e)
			{
			//	throw new PersistException( this, e, string.Format("OleDbPersist.handleQuery(): Error occurred while processing sql {0}", sqlQuery ));
				Logger.log( this, e, e.Message );
				throw;
			}
			finally
			{
				if (sc != null)
				{
					DatabaseConnectionPool.getConnectionPool().releaseConnection( sc );
				}
				parameters.Clear();
			}

			return dataSet;
		}

		public virtual long executeCommand( string commandSql )
		{
			DatabaseConnection sc = null;
			long result = 0;
			
			try
			{
                Logger.log(Logger.Level.SQL, commandSql);

				sc = DatabaseConnectionPool.getConnectionPool().getConnection( domainObject.ConnectionName );
				OleDbCommand oledbCommand = new OleDbCommand();
				oledbCommand.Connection = sc.Connection;
				oledbCommand.CommandType = CommandType.Text;
				oledbCommand.CommandText = commandSql; 

				
				bindParameters( oledbCommand );
				
				result = (long) oledbCommand.ExecuteNonQuery();


				DatabaseConnectionPool.getConnectionPool().releaseConnection(sc);
			}
			catch( Exception e)
			{
				StringBuilder sb = new StringBuilder();

				foreach( string p in parameters)
				{
					string name = p;
					object o = domainObject[p];
					string type = o.GetType().ToString();

					string line = name + ":" + type + ">" + o.ToString() + "<\r\n";
					sb.Append( line );
				}


				throw new PersistException( this, e, string.Format("OleDbPersist.executeCommand(): Error occurred while processing SQL {0}. \r\nParameter(s):\r\n", commandSql ) + sb.ToString());
			}
			finally
			{
				if (sc != null)
				{
					DatabaseConnectionPool.getConnectionPool().releaseConnection(sc);
				}

				parameters.Clear();
			}

			return result;
		}
        
		public virtual void bindParameters( OleDbCommand oledbCommand )
		{
			int iParam = 0;
			
			try
			{
				for (iParam = 0; iParam < parameters.Count; iParam++)
				{
					string paramName = parameters[iParam].ToString();
					object o = domainObject[paramName];
					string oleDBParam = "@" +  paramName + iParam.ToString();
                    OleDbParameter oparam = null;
					if (o.GetType() == typeof( System.DateTime))
					{
						string s = o.ToString();
						oparam = oledbCommand.Parameters.Add( oleDBParam, oledbTypeof( typeof(string)));
						oparam.Value = s;						
					}
					else
					{
						oparam = oledbCommand.Parameters.Add( oleDBParam, oledbTypeof( o.GetType()));
						oparam.Value = o;
					}

                    if (Logger.isMode( Logger.Level.SQL))
                    Logger.log(Logger.Level.SQL, new object[] { oleDBParam, ":", oparam.DbType.ToString(), "=",oparam.Value.ToString() });
				}
			}
			catch( System.IndexOutOfRangeException re)
			{
					throw new PersistException( this, re, string.Format("OleDbPersist.bindParameters(): Invalid parameter name error while binding parameter index={0} name={1}.  Check that the parameter name is spelled correctly in the in SQL template and the DomainObject has been initialized properly.", iParam.ToString(), (string) parameters[iParam]));
			}
			catch( Exception e)
			{
				throw new PersistException( this, e, string.Format("OleDbPersist.bindParameters(): Error while binding parameter index={0} name={1}", iParam.ToString(), (string) parameters[iParam]));
			}

		}

		public virtual OleDbType oledbTypeof( Type objType )
		{
			OleDbType oType = OleDbType.Char;

			if ( objType == typeof( string ))
			{
				oType = OleDbType.VarChar;
			}
			else if (objType == typeof( int ))
			{
				oType = OleDbType.Integer;
			}
            else if (objType == typeof(bool))
            {
                oType = OleDbType.Boolean;
            }
			else if (objType == typeof( long ))
			{
				oType = OleDbType.BigInt;				
			}
			else if (objType == typeof( System.Decimal))
			{
				oType = OleDbType.Decimal;
			}
			else if (objType == typeof( System.Double))
			{
				oType = OleDbType.Decimal;
			}
			else if (objType == typeof( System.Single))
			{
				oType = OleDbType.Decimal;
			}
			else if (objType == typeof( System.DateTime ))
			{
				oType = OleDbType.DBTimeStamp;
			}
			else if (objType == typeof( System.Guid))
			{
				return  OleDbType.Guid;
			}
			else
			{
				throw new PersistException( this, string.Format( "OleDbPersist.oledbTypeof(): Cannot convert {0} to oledbType.", objType.ToString() ));
			}

			return oType;
		}
        
		public virtual long getNextIdentity()
		{
			long identity = 1;

			if (domainObject.PrimaryKey == null) return -1;
			if (domainObject.PrimaryKey.Length == 0) return -1;

			string sql = "select max(" + domainObject.PrimaryKey + ")+1 from " + domainObject.BaseTable;

			try
			{
				DataSet dataSet = executeQuery( sql );

				if (dataSet.Tables.Count > 0)
				{
					if (dataSet.Tables[0].Rows.Count > 0)
					{
						object o = dataSet.Tables[0].Rows[0][0];

						if (o.GetType() != typeof( System.DBNull))
						{
							identity = long.Parse(o.ToString());
						}
					}
				}
			}
			catch( Exception e )
			{
				throw new PersistException( domainObject, e, string.Format("OleDbPersist.getNextIdentity(): Error occurred getting next identity using SQL {0}.", sql ));
			}

			return identity;
		}
        
		public override DataSet doQueryRelation(string relation)
		{
			string sqlGet = "";
			DataSet ds = null;

			try 
			{
				Hashtable relationMap = domainObject.getRelation( relation );

				Type childObjType = (Type) relationMap["type"];
				string column = (string) relationMap["column"];

				DatabaseObject childObject = (DatabaseObject) childObjType.Assembly.CreateInstance(childObjType.ToString());

				string childTable = childObject.BaseTable;

				if ( null == childTable )
				{
					throw new PersistException( childObject, "OleDbPersist.doQueryRelation(): Cannot get object because child DomainObject has not defined BaseTable.");
				}

				if (childTable.Length == 0)
				{	
					throw new PersistException( childObject, "OleDbPersist.doQueryRelation(): Cannot get object  because child DomainObject has not defined BaseTable.");
				}

				string primaryKey = domainObject.PrimaryKey;

				if (primaryKey !=  null)
				{
					if (!domainObject.ContainsKey(primaryKey))
					{
						throw new PersistException( domainObject, "OleDbPersist.doQueryRelation(): Cannot get object  because DomainObject has not set primary key value.");
					}
				}
				else // primaryKey == null
				{
						throw new PersistException( domainObject, "OleDbPersist.doQueryRelation(): Cannot get object  because DomainObject has set primary key value.");
				}

				sqlGet = "select * from " + childTable + " where " + column + "=?";

				parameters.Add( primaryKey );

				ds = executeQuery( sqlGet );
			}
			catch( PersistException pe )
			{
				throw pe;
			}
			catch( Exception e)
			{
				throw new PersistException( domainObject, e, "OleDbPersist.doQueryRelation(): Error occurred while trying to retrieve child objects.");
			}

			return ds;
		}
        
		public virtual DataSet getobject()
		{
			string sqlGet = "";
			DataSet ds = null;
			try
			{
				string baseTable = domainObject.BaseTable;

				if ( null == baseTable )
				{
					throw new PersistException( domainObject, "OleDbPersist.getobject(): Cannot get object because DomainObject has not defined BaseTable.");
				}

				if (baseTable.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.getobject(): Cannot get object  because DomainObject has not defined BaseTable.");
				}

				string primaryKey = domainObject.PrimaryKey;

				if (primaryKey ==  null)
				{
					throw new PersistException( domainObject, "OleDbPersist.getobject(): Cannot get object  because DomainObject has set primary key value.");
				}


				if (primaryKey !=  null)
				{
					if (!domainObject.ContainsKey(primaryKey))
					{
						throw new PersistException( domainObject, "OleDbPersist.getobject(): Cannot get object  because DomainObject has set primary key value.");
					}
				}

				sqlGet = "select * from " + bracket(baseTable)  + " where " + bracket(primaryKey) + "=?";
				parameters.Add(primaryKey);

				ds = executeQuery( sqlGet );
			}
			catch( PersistException pe)
			{
				throw pe;
			}
			catch( Exception e)
			{
				throw new PersistException( domainObject, e, string.Format("OleDbPersist.getobject(): Error occurred while attempting to run select statement {0}", sqlGet ));
			}

			return ds;
	

		}
        
		public virtual DataSet select()
		{
			StringBuilder sqlSelect = new StringBuilder( string.Empty );
			DataSet ds = null;
			try
			{
				string baseTable = domainObject.BaseTable;

				if ( null == baseTable )
				{
					throw new PersistException( domainObject, "OleDbPersist.select(): Cannot get object because DomainObject has not defined BaseTable.");
				}

				if (baseTable.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.select(): Cannot get object  because DomainObject has not defined BaseTable.");
				}

				sqlSelect.Append( "SELECT * FROM " );
				sqlSelect.Append( bracket(domainObject.BaseTable) );

				
				bool firstTimeThru = true;
				if (domainObject.Count > 0)
				{
					StringBuilder whereClause = new StringBuilder( " WHERE " );

					for( int i = 0; i < domainObject.Count; i++)
					{
						if (!firstTimeThru)
						{
							whereClause.Append( " AND " );
						}

						string name = (string) domainObject.Names[i];
						whereClause.Append( bracket(name) );
						whereClause.Append( "=${" );
						whereClause.Append(  name );
						whereClause.Append(  "}" );

						firstTimeThru = false;
					}

					sqlSelect.Append( whereClause );	
				}

				
				ds = executeQuery( processSql( sqlSelect.ToString() ) );

			}
			catch( Exception e)
			{
				throw new PersistException( domainObject, e, string.Format("OleDbPersist.select(): Error occurred while attempting to run select statement {0}", sqlSelect ));
			}
		
			return ds;
		}

		public virtual DataSet select_all()
		{
			string sqlSelect = "";
			DataSet ds = null;
			try
			{
				string baseTable = domainObject.BaseTable;

				if ( null == baseTable )
				{
					throw new PersistException( domainObject, "OleDbPersist.select(): Cannot get object because DomainObject has not defined BaseTable.");
				}

				if ( baseTable.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.select(): Cannot get object  because DomainObject has not defined BaseTable.");
				}

				sqlSelect  = "SELECT * FROM " + bracket(domainObject.BaseTable) + "";

				

				ds = executeQuery( processSql( sqlSelect ) );

			}
			catch( PersistException pe)
			{
				throw pe;
			}
			catch( Exception e)
			{
				throw new PersistException( domainObject, e, string.Format("OleDbPersist.select(): Error occurred while attempting to run select statement {0}", sqlSelect ));
			}

			return ds;
		}
        
		public virtual DataSet getall()
		{
			string sqlGetAll = "";
			DataSet ds = null;
			try
			{
				string baseTable = domainObject.BaseTable;

				if ( null == baseTable )
				{
					throw new PersistException( domainObject, "OleDbPersist.getall(): Cannot get object because DomainObject has not defined BaseTable.");
				}

				if (baseTable.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.getall(): Cannot get object  because DomainObject has not defined BaseTable.");
				}

				string paramNameCol = domainObject.ParamNameCol;

				if ( null == paramNameCol )
				{
					throw new PersistException( domainObject, "OleDbPersist.getall(): Cannot get object because DomainObject has not defined paramNameCol.");
				}

				if ( paramNameCol.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.getall(): Cannot get object  because DomainObject has not defined paramNameCol.");
				}

				string paramValueCol = domainObject.ParamValueCol;

				if ( null == paramValueCol )
				{
					throw new PersistException( domainObject, "OleDbPersist.getall(): Cannot get object because DomainObject has not defined paramValueCol.");
				}

				if (paramValueCol.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.getall(): Cannot get object  because DomainObject has not defined paramValueCol.");
				}

				string primaryKeyCol = domainObject.PrimaryKey;

				if ( null ==  primaryKeyCol )
				{
					throw new PersistException( domainObject, "OleDbPersist.getall(): Cannot get object because DomainObject has not defined primaryKeyCol.");
				}

				if (primaryKeyCol.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.getall(): Cannot get object  because DomainObject has not defined primaryKeyCol.");
				}

				sqlGetAll = "select " + bracket(paramNameCol) + " as PARAM_NAME, " + bracket(paramValueCol) + " as PARAM_VALUE, " + bracket(primaryKeyCol) + " from " + bracket(baseTable) ;
				
				ds = executeQuery( sqlGetAll );
			}
			catch( PersistException pe)
			{
				throw pe;
			}
			catch( Exception e)
			{
				throw new PersistException( domainObject, e, string.Format("OleDbPersist.getobject(): Error occurred while attempting to run select statement {0}", sqlGetAll ));
			}

			return ds;
	

		}

		protected virtual string bracket( string s )
		{
			string result = string.Empty;
			if (!s.StartsWith("[")) result = "[" + s + "]";
			else result = s;

			return result;
		}
        
		public virtual long insert()
		{
			
			StringBuilder sqlInsert = new StringBuilder( string.Empty );
			long identity = 0;
			try
			{
				string baseTable = domainObject.BaseTable;

				if ( null == baseTable )
				{
					throw new PersistException( domainObject, "OleDbPersist.insert(): Cannot insert because DomainObject has not defined BaseTable.");
				}

				if (baseTable.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.insert(): Cannot insert because DomainObject has not defined BaseTable.");
				}

				string primaryKey = domainObject.PrimaryKey;

				if (!domainObject.AutoIdentity)
				{
					if (primaryKey !=  null)
					{		
						if (domainObject.ContainsKey(primaryKey))
						{
							string pkValue = domainObject[primaryKey].ToString ();
							if(( pkValue == "0") || (pkValue.Length == 0) )
							{
								identity = getNextIdentity();
								domainObject[domainObject.PrimaryKey] = identity;							
							}
							else
							{
								identity = domainObject.getLong( primaryKey );
							}
						}
						else
						{
							identity = getNextIdentity();
							domainObject.Add( domainObject.PrimaryKey, identity );	
						}
					}
				}
					

				StringBuilder insertClause =  new StringBuilder("insert into ");
				insertClause.Append( bracket(baseTable) );
				insertClause.Append( " ( " ) ;

				StringBuilder valueClause = new StringBuilder( " values ( ");

				for (int i = 0; i < domainObject.Count; i++)
				{
					insertClause.Append(  bracket((string ) domainObject.Names[i]) );

					//valueClause += toQuotedSql( domainObject[i] );
					
					valueClause.Append (" ? ");
					parameters.Add( domainObject.Names[i] );
					
					if (i <= domainObject.Count -2 )
					{
						insertClause.Append(",");
						valueClause.Append( ", " );
					}
				}

				insertClause.Append( ")" );
				valueClause.Append( ")" );

				sqlInsert =new StringBuilder( insertClause.ToString() );
				sqlInsert.Append( valueClause.ToString() );



                if (domainObject.AutoIdentity)
                {
                    if (primaryKey != null)
                    {
                        identity = insertWithAutoIdentity(sqlInsert);
                    }
                }
                else
                {
                    executeCommand(sqlInsert.ToString());
                }
			}
			catch( PersistException pe)
			{
				throw pe;
			}
			catch( Exception e)
			{
				throw new PersistException( domainObject, e, string.Format("OleDbPersist.insert(): Error occurred while attempting to run insert statement {0}", sqlInsert ));
			}

			
			
			return identity;
		}

		public virtual long insertWithAutoIdentity(StringBuilder insertSql)
		{
			long identity = 1;
            string identSql = "select @@identity as [ident] ";
            insertSql.Append(identSql);

			try
			{
				DataSet dataSet = executeQuery( insertSql.ToString() );

				if (dataSet.Tables.Count > 0)
				{
					if (dataSet.Tables[0].Rows.Count > 0)
					{
						object o = dataSet.Tables[0].Rows[0][0];

						if (o.GetType() != typeof( System.DBNull))
						{
							identity = long.Parse(o.ToString());
						}
					}
				}
			}
			catch( Exception e )
			{
				throw new PersistException( domainObject, e, string.Format("OleDbPersist.getNextIdentity(): Error occurred getting next identity using SQL {0}.", insertSql.ToString() ));
			}

			return identity;
		}

		public virtual long update()
		{
			long result = 0;
			StringBuilder sqlUpdate = new StringBuilder( string.Empty );

			try
			{
				string baseTable = domainObject.BaseTable;

				if ( null == baseTable )
				{
					throw new PersistException( domainObject, "OleDbPersist.update(): Cannot update because DomainObject has not defined BaseTable.");
				}

				if ( baseTable.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.update(): Cannot update because DomainObject has not defined BaseTable.");
				}

				string primaryKey = domainObject.PrimaryKey;

				if ( null == primaryKey )
				{
					throw new PersistException( domainObject, "OleDbPersist.update(): Cannot update because DomainObject has not defined PrimaryKey.");
				}

				if (primaryKey.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.update(): Cannot update because DomainObject has not defined PrimaryKey.");
				}

				sqlUpdate.Append( "update " );
				sqlUpdate.Append( bracket(baseTable) );
				sqlUpdate.Append( " set " );

				StringBuilder setClause = new StringBuilder( string.Empty );

				bool firstTimeThru = true;
				for (int i = 0; i < domainObject.Count; i++)
				{
					string currentCol = (string) domainObject.Names[i];	

					if (String.Compare( currentCol, primaryKey) != 0 )
					{
						parameters.Add( currentCol );
						
						if (!firstTimeThru)
						{
							setClause.Append(", ");
						}

						setClause.Append( bracket(currentCol) );
						setClause.Append( " = ?" );


						firstTimeThru = false;
					}
				}

				parameters.Add( primaryKey );

				sqlUpdate.Append( setClause );
				sqlUpdate.Append( " where " );
				sqlUpdate.Append( bracket(primaryKey) );
				sqlUpdate.Append( " = ?" );
				

				result = executeCommand( sqlUpdate.ToString() );

			}
			catch( PersistException pe)
			{
				throw pe;
			}
			catch( Exception e)
			{
				throw new PersistException( domainObject, e, string.Format("OleDbPersist.update(): Error occurred while attempting to run update statement {0}.", sqlUpdate ));
			}

			return result;			
		
		}

		public virtual long delete()
		{
			long result = 0;
			string sqlDelete = "";

			try
			{
				string baseTable = domainObject.BaseTable;

				if ( null == baseTable )
				{
					throw new PersistException( domainObject, "OleDbPersist.delete(): Cannot delete because DomainObject has not defined BaseTable.");
				}

				if (baseTable.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.delete(): Cannot delete because DomainObject has not defined BaseTable.");
				}

				string primaryKey = domainObject.PrimaryKey;

				if ( null == primaryKey )
				{
					throw new PersistException( domainObject, "OleDbPersist.delete(): Cannot delete because DomainObject has not defined PrimaryKey.");
				}

				if (primaryKey.Length == 0)
				{	
					throw new PersistException( domainObject, "OleDbPersist.delete(): Cannot delete because DomainObject has not defined PrimaryKey.");
				}

				sqlDelete = "delete from " + baseTable + " where " + primaryKey + "= ?";

				parameters.Add( primaryKey );

				result = executeCommand( sqlDelete );
			}
			catch( Exception e)
			{
				throw new PersistException( domainObject, e, string.Format( "OleDbPersist.delete(): Error ocurred while attempting to run delete statement {0}.", sqlDelete ));
			}

			return result;
		}
        
		public virtual long save()
		{
			long result;

			bool doUpdate = false;

			string primaryKey = domainObject.PrimaryKey;

			if ( null == primaryKey )
			{
				throw new PersistException( domainObject, "OleDbPersist.save(): Cannot save because DomainObject has not defined PrimaryKey.");
			}

			if (primaryKey.Length == 0)
			{	
				throw new PersistException( domainObject, "OleDbPersist.save(): Cannot save because DomainObject has not defined PrimaryKey.");
			}


			if (domainObject.ContainsKey( domainObject.PrimaryKey ))
			{
				ArrayList objects = domainObject.doQueryObjects("getobject");

				if (objects.Count > 0)
				{
					doUpdate = true;		
				}
			}

			if (doUpdate)
			{
				domainObject.doCommand("update");
				domainObject.getLong(domainObject.PrimaryKey);
				result = domainObject.getPrimaryKey();
			}
			else
			{
				result = domainObject.doCommand("insert");
			}

			return result;
		}
        
		public virtual DataSet getfromrwk()
		{
			if (domainObject.RWK.Count == 0)
			{
				throw new PersistException( domainObject, "OleDbPersist.getfromrwk():Cannot get via RWK- no key columns defined.");
			}

			string baseTable = domainObject.BaseTable;

			if ( null == baseTable )
			{
				throw new PersistException( domainObject, "OleDbPersist.getfromrwk(): Cannot get via RWK because DomainObject has not defined BaseTable.");
			}

			if ( baseTable.Length == 0)
			{	
				throw new PersistException( domainObject, "OleDbPersist.getfromrwk(): Cannot get via RWK because DomainObject has not defined BaseTable.");
			}
			

			StringBuilder sql = new StringBuilder( "SELECT * FROM " );
			sql.Append(  domainObject.BaseTable );
			sql.Append( " WHERE " );

			StringBuilder where = new StringBuilder( string.Empty );

			bool firstTimeThru = true;
			foreach(string rwk in domainObject.RWK)
			{
				parameters.Add( rwk );

				if (!firstTimeThru )
				{
					where.Append(  " AND " );
				}

				where.Append( rwk );
				where.Append( "=? " );
				firstTimeThru = false;
			}

			sql.Append( where.ToString() );
			DataSet ds = executeQuery( sql.ToString() );

			return ds;

		
		}

		public virtual long put()
		{
			string primaryKey = domainObject.PrimaryKey;

			if ( null == primaryKey )
			{
				throw new PersistException( domainObject, "OleDbPersist.put(): Cannot put because DomainObject has not defined PrimaryKey.");
			}

			if (primaryKey.Length == 0)
			{	
				throw new PersistException( domainObject, "OleDbPersist.put(): Cannot put because DomainObject has not defined PrimaryKey.");
			}
			
			DatabaseObject tmpObject = (DatabaseObject) domainObject.copy();

			DataSet existingDS = tmpObject.doQuery("getfromrwk");

			long pk_id = 0;

			if (existingDS.Tables.Count > 0)
			{
				if (existingDS.Tables[0].Rows.Count > 0)
				{
					pk_id = long.Parse(existingDS.Tables[0].Rows[0][primaryKey].ToString());
				}
			}

			domainObject.Add( primaryKey, pk_id );

			return domainObject.doCommand("save");
		}

		public virtual long conditional_put()
		{
			long result = 0;

			string primaryKey = domainObject.PrimaryKey;

			if ( null == primaryKey )
			{
				throw new PersistException( domainObject, "OleDbPersist.put(): Cannot put because DomainObject has not defined PrimaryKey.");
			}

			if (primaryKey.Length == 0)
			{	
				throw new PersistException( domainObject, "OleDbPersist.put(): Cannot put because DomainObject has not defined PrimaryKey.");
			}
			
			DatabaseObject tmpObject = (DatabaseObject) domainObject.copy();

			DataSet existingDS = tmpObject.doQuery("getfromrwk");

			long pk_id = 0;

			if (existingDS.Tables.Count > 0)
			{
				if (existingDS.Tables[0].Rows.Count > 0)
				{
					pk_id = long.Parse(existingDS.Tables[0].Rows[0][primaryKey].ToString());
					
				}
			}

			if (pk_id <= 0)
			{
				domainObject.Add( primaryKey, pk_id );
				result = domainObject.doCommand("save");
			}
			else
			{
				result = pk_id;
			}

			return result;
	
		}

        public static bool hasData(DataSet ds)
        {
            bool result = false;

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
	}
}
