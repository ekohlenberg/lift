using System;
using System.Collections;
using System.Globalization;
using System.Data;
using System.Reflection;
using System.Text;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for DomainObject.
	/// </summary>
	[Serializable]
	public  class ModelObject : TypedHashtable
	{
		
		public Persist persistence = null;
		protected StringBuilder stringBuilder;
	
		public ModelObject()
		{	
			stringBuilder = new StringBuilder();
	
		}


		public void setPersistence( Persist p )
		{
			this.persistence = p;
		}

	
		
	

		public virtual MethodInfo getMethod( string action )
		{
			MethodInfo m = this.GetType().GetMethod( action );
			return m;
		}

		public virtual void assignRandomData( Random r, int index )
		{
			object o;

			if (index < this.Count)
			{
				Type t = (Type) this.Types2[index];
				
				if (t == typeof(System.String))
				{
					int len = r.Next(1, 12);
					string s = "";
					for (int i = 0; i < len; i++)
					{
						char ch = (char) ((int) 'a' + r.Next(26));
						s += ch;
					}
					o = s;
				}
				else if (t == typeof(System.DateTime))
				{
					DateTime d = new DateTime( r.Next( 1990, 2004), r.Next( 1, 12), r.Next(1,28), r.Next( 0, 23), r.Next( 0, 59), r.Next( 0, 59 ));
					o = d;
				}
				else
				{
					o = r.Next(1000);
				}

				this[index] = o;
			}

		}

		public virtual DataSet doQuery( string action )
		{
			DataSet dataSet = null;

			if (persistence != null)
			{
				try
				{
					dataSet = persistence.doQuery( action );
				}
				catch( Exception e)
				{
					Logger.log( this, e, e.Message );
					throw new ModelObjectException( this, e, string.Format("DomainObject.doQuery(): Error occurred while executing action {0}", action ));
				}
			}
			else
			{
				throw new ModelObjectException( this, "DomainObject.doQuery(): Persistence mechanism has not been assigned.");
			}

			return dataSet;
		}

		

		public virtual ArrayList doQueryObjects( string action  )
		{
			ArrayList domainObjects = doQueryObjects( this.GetType(), action );

			return domainObjects;
		}

		public virtual ModelObject doSingleObjectQuery( Type objType, string action )
		{
			ModelObject result = null;
			ArrayList objects = doQueryObjects(objType, action);

			if (objects.Count == 1)
			{
				result = (ModelObject) objects[0];
			}
			else
			{
				throw new ModelObjectException( this, "DomainObject.doSingleObjectQuery(" + objType.ToString() + ", " + action + ") returned " + objects.Count.ToString() + " objects." ); 
			}

			return result;
		}



		public virtual ArrayList doQueryObjects( Type t, string action  )
		{
			ArrayList domainObjects = null;
		
			System.Data.DataSet dataSet = this.doQuery( action ); 

			domainObjects = toObjectArray( t, dataSet );

			return domainObjects;
		}

		public virtual ArrayList toObjectArray( Type t, DataSet dataSet )
		{
			ArrayList domainObjects = new ArrayList();

			foreach (System.Data.DataTable table in dataSet.Tables )
			{
				foreach( System.Data.DataRow row in table.Rows )
				{	
					ModelObject domainObject = null;

					domainObject = toDomainObject( t, row );
		
					domainObjects.Add( domainObject );
				}
			}

			return domainObjects;
		}

		public virtual ModelObject toDomainObject( Type t, DataRow row )
		{
			ModelObject domainObject = (ModelObject) t.Assembly.CreateInstance( t.ToString());

			DataTable table = row.Table;
			
			for (int col = 0; col < table.Columns.Count; col++)
			{
				domainObject.Add( table.Columns[col].ColumnName, row[col] );
			}

			return domainObject;

		}

		public virtual ModelObject toDomainObject( DataRow row )
		{
			return toDomainObject( GetType() , row );
		}


		public virtual long doCommand( string action )
		{
			long result = 0;

			if (persistence != null)
			{
				try
				{
				result = persistence.doCommand( action );
				}
				catch( Exception e)
				{
					throw new ModelObjectException( this, e, string.Format("DomainObject.doCommand(): Error occurred while executing action {0}", action ));
				}
			}
			else
			{
				MethodInfo actionMethod = null;
			
				actionMethod = getMethod(action);

				if ( actionMethod != null )
				{
					result = (long) actionMethod.Invoke( this, null );
				}
				else
				{
					throw new ModelObjectException( this, "DomainObject.doCommand(): Persistence mechanism has not been assigned and action method is not defined for action '" + action + "'");
				}
			}

			return result;
		}

		public virtual long doCommand( string action, string resultAttr )
		{
			long result = 0;

			result = doCommand( action );

			this.Add( resultAttr, result );

			return result;

		}

		public virtual long doCommand( Type objType, string action )
		{		
			ModelObject domainObject = (ModelObject) objType.Assembly.CreateInstance(objType.ToString());
			domainObject.copyFrom( this );
			return domainObject.doCommand(action);
		}

		
		public virtual int getInt( string name )
		{
			int result = 0;
			try
			{
				object o = this[name];

				long lresult = 0;
				convert( o, ref lresult );

				result = (int) lresult;
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format( "DomainObject.getInt(string): Problem accessing attribute {0}.", name ));
			}
			return result;
		}

		public virtual int getInt( int index )
		{
			int result = 0;

			try
			{
				object o = this[index];

				long lresult = 0;
				convert( o, ref lresult );

				result = (int) lresult;
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format( "DomainObject.getInt(int): Problem accessing index {0}.", index ));
			}

			return result;
		}

		

	
		public virtual string getString( string name )
		{
			string result = "";
	
			try
			{
				object o = this[name];
				convert( o, ref result );
			}
			catch( Exception e)
			{
				throw new ModelObjectException(this, e, string.Format("DomainObject.getString(string) Problem accessing attribute {0}.", name ));
			}

			return result;
		}

		public virtual string getString( int index )
		{
			string result = "";

			try
			{
				object o = this[index];
				convert( o, ref result );
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format( "DomainObject.getString(int) problem accessing index {0}.", index ));
			}

			return result;
		}

		public virtual long getLong( string name )
		{
			long result = 0;

			try
			{
				object o = this[name];
				convert( o, ref result );
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format("DomainObject.getLong(string) problem accessing attribute {0}.", name ));
			}

			return result;
		}

		public virtual long getLong( int index )
		{
			long result = 0;

			try
			{
				object o = this[index];
				convert( o, ref result );
			}
			catch( Exception e)
			{
				throw new ModelObjectException(this, e, string.Format("DomainObject.getLong(int) Problem accessing index {0}.", index ));
			}

			return result;
		}

		public virtual Decimal getDecimal( string name )
		{
			Decimal result = (Decimal) 0.0;

			try
			{
				object o = this[name];
				convert( o, ref result );
			}
			catch (Exception e)
			{
				throw new ModelObjectException( this, e, string.Format( "DomainObject.getDouble(name) Problem accessing attribute {0}.", name ));
			}

			return result;
		}

		

		public virtual Decimal getDecimal( int index )
		{
			Decimal result = (Decimal)0.0;

			try
			{
				object o = this[index];
				convert( o, ref result );
			}
			catch (Exception e)
			{
				throw new ModelObjectException(this, e, string.Format( "DomainObject.getDouble(int) Problem accessing index {0}.", index ));
			}

			return result;
		}

		public virtual double getDouble( int index )
		{
			double result = (double)0.0;

			try
			{
				object o = this[index];
				convert( o, ref result );
			}
			catch (Exception e)
			{
				throw new ModelObjectException(this, e, string.Format( "DomainObject.getDouble(int) Problem accessing index {0}.", index ));
			}

			return result;
		}

		public virtual double getDouble( string name )
		{
			double result = (double) 0.0;

			try
			{
				object o = this[name];
				convert( o, ref result );
			}
			catch (Exception e)
			{
				throw new ModelObjectException( this, e, string.Format( "DomainObject.getDouble(name) Problem accessing attribute {0}.", name ));
			}

			return result;
		}


		public virtual float getFloat( int index )
		{
			float result = (float)0.0;

			try
			{
				object o = this[index];
				convert( o, ref result );
			}
			catch (Exception e)
			{
				throw new ModelObjectException(this, e, string.Format( "DomainObject.getDouble(int) Problem accessing index {0}.", index ));
			}

			return result;
		}

		public virtual float getFloat( string name )
		{
			float result = (float) 0.0;

			try
			{
				object o = this[name];
				convert( o, ref result );
			}
			catch (Exception e)
			{
				throw new ModelObjectException( this, e, string.Format( "DomainObject.getDouble(name) Problem accessing attribute {0}.", name ));
			}

			return result;
		}

		public virtual DateTime getDateTime( string name )
		{
			DateTime result = DateTime.Now;

			try
			{
			object o = this[name];
			convert( o, ref result );
			}
			catch( Exception e)
			{
				throw new ModelObjectException(this, e, string.Format("DomainObject.getDateTime(string) exception getting attribute {0}", name));
			}

			return result;
		}

		
		public virtual DateTime getDateTime( int index )
		{
			DateTime result = DateTime.Now;
			
			try
			{
				object o = this[index];
				convert( o, ref result );
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format("DomainObject.getDateTime(int) exception getting index {0}", index));
			}

			return result;
		}

		public virtual DateTime getDateTime( int index, string format )
		{
			DateTime result = DateTime.Now;

			try
			{
				object o = this[index];
				convert( o, ref result, format );
			}
			catch( Exception e)
			{
				throw new ModelObjectException(this, e, string.Format("DomainObject.getDateTime(int, string) exception getting index {0}", index));
			}

			return result;
		}

		public virtual DateTime getDateTime( string name, string format )
		{
			DateTime result = DateTime.Now;

			try
			{
				object o = this[name];
				convert( o, ref result, format );
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format("DomainObject.getDateTime(string, string) exception getting attribute {0}", name));
			}

			return result;
		}

		protected virtual void convert( object o, ref DateTime result )
		{
			string s = "";
			if (o.GetType().ToString() == "System.DateTime")
			{
				result = (DateTime) o;
			}
			else if (o.GetType().ToString() == s.GetType().ToString())
			{
				throw new ModelObjectException(this, "Must use getDateTime( string name, string format ) for converting strings to dates.");
			}
			else
			{
				throw new ModelObjectException( this, "Cannot convert " + o.GetType().ToString() + " to System.Date");
			}
		}

		protected virtual void convert( object o, ref DateTime result, string format )
		{
			string s = "";
			
			if (o.GetType().ToString() == s.GetType().ToString())
			{
				result = DateTime.ParseExact( o.ToString(), format, new DateTimeFormatInfo() );
			}
			else
			{
				throw new ModelObjectException( this, "DomainObject.getDateTime( string name, string format ) requires that underlying object be a string type. The object is of type " + o.GetType().ToString() + ".");
			}
		}
		
		protected virtual void convert( object o, ref string result )
		{
			if (o == null)
			{
				result = null;
			}
			else if (o.GetType() == typeof(System.DBNull))
			{
				result = null;
			}
			else
			{
				result = o.ToString();
			}
		}

		protected virtual void convert( object o, ref long result )
		{
			try
			{
				if (o == null)
				{
					result = 0;
				}
				else if (o.GetType() == typeof( System.DBNull ))
				{
					result = 0;
				}
				else
				{
					string strNumber = numericString( o.ToString() );	

					result = Convert.ToInt64( strNumber );
	
					
				}
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format("DomainObject.convert( object, long ) converting {0}.", o.ToString() ));
			}
		}

		protected virtual string numericString( string s)
		{
			 stringBuilder.Remove( 0, stringBuilder.Length );
							
			for (int i = 0; i < s.Length; i++)
			{
				if ((s[i] != '$') && (s[i] != ','))
				{
					stringBuilder.Append( s[i] );
				}
			}

			return stringBuilder.ToString();
		}

		protected virtual void convert( object o, ref Decimal result )
		{
			try
			{
				string strNumber = numericString( o.ToString() );
				result = Decimal.Parse( strNumber );
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format("DomainObject.convert( object, Decimal ) converting {0}.", o.ToString() ));
			}
		}

		protected virtual void convert( object o, ref double result )
		{
			try
			{
				string strNumber = numericString( o.ToString() );
				result = Convert.ToDouble( strNumber );
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format("DomainObject.convert( object, double ) converting {0}.", o.ToString() ));
			}
		}
	

		protected virtual void convert( object o, ref float result )
		{
			try
			{
				string strNumber = numericString( o.ToString() );
				result = Convert.ToSingle( strNumber );
			}
			catch( Exception e)
			{
				throw new ModelObjectException( this, e, string.Format("DomainObject.convert( object, double ) converting {0}.", o.ToString() ));
			}
		}
	
		public override object this[int index]
		{
			get
			{
				object o = null;
				try
				{
						o = base[index];
				}
				catch( Exception e)
				{
					throw new ModelObjectException( this, e, string.Format( "this[index] get(): Index {0} out of range.  There are {1} attributes.", index, this.Count ));
				}
				return o;
			}
			set
			{
				try
				{
					base[index] = value;
				}
				catch( Exception e)
				{
					throw new ModelObjectException( this, e, string.Format( "this[index] set(): Index {0} out of range.  There are {1} attributes.", index, this.Count ));
				}
			}
		}

		public override object this[object key]
		{
			get
			{
				object o = null;
				if (!ContainsKey( key ))
				{
					throw new ModelObjectException( this, string.Format( "this[key] get(): Key {0} out of range.  There are {1} attributes.", key, this.Count ));	
				}
				try
				{
					
					o = base[key];
				}
				catch( Exception e)
				{
					throw new ModelObjectException( this, e, string.Format( "this[key] get(): Key {0} out of range. ", key ));	
				}
				return o;
			}
			set
			{
				try
				{
					base[key] = value;
				}
				catch( Exception e)
				{
					throw new ModelObjectException( this, e, string.Format( "this[key] set(): Key {0} out of range.  There are {1} attributes.", key, this.Count ));	
				}
			}
		}

		public virtual bool IsEqual(ModelObject rhs)
		{
			bool isEqual = false;

			if (this.Count == rhs.Count)
			{
				bool stillEqual = true;

				for (int i = 0; i < this.Count && stillEqual; i++)
				{
					string lhsName = (string) this.Names[i];
					string rhsName = (string) rhs.Names[i];

					if (lhsName == rhsName)
					{
						object lhsValue = this[i];
						object rhsValue = rhs[i];

						if (lhsValue.GetType().ToString() == rhsValue.GetType().ToString())
						{
							if (lhsValue.ToString() == rhsValue.ToString())
							{
								stillEqual = true;
							}
							else
							{
								stillEqual = false;
							}
						}
						else
						{
							stillEqual = false;
						}
					}
					isEqual = stillEqual;

				}
			}

			return isEqual;
		}

		public virtual DataSet toDataSet( string tableName )
		{
			DataSet ds = new DataSet();

			DataTable t = ds.Tables.Add(tableName);
			
			for (int i = 0; i < this.Count; i++)
			{
				Type type = this[i].GetType();

				if (type == typeof(System.DBNull))
				{
					type = typeof(System.String);
				}

				t.Columns.Add( (string) Names[i], type );
			}

			t.Rows.Add( this.toArray() );

			return ds;
		}

		public virtual object[] toArray()
		{
			object[] objects = new object[this.Count];

			for (int i = 0; i < this.Count; i++)
			{
				objects[i] = this[i];
			}

			return objects;
		}


		public virtual DateTime convertDBFDateTime( DateTime datePart, long seconds )
		{
			long hours = (long) ( seconds / 3600 );

			long sec = (seconds - (hours*3600));

			long minutes = (long) (sec / 60);

			sec = sec - (minutes*60);

			DateTime dt = new DateTime( datePart.Year, datePart.Month, datePart.Day, (int) hours, (int) minutes, (int) sec );

			return dt;

		}
			
	
		public virtual void convertDateTimeDBF( DateTime dateTime, ref DateTime datePart, ref long seconds )
		{
			datePart = new System.DateTime( dateTime.Year, dateTime.Month, dateTime.Day );

			seconds = (dateTime.Hour * 3600) + (dateTime.Minute*60) + dateTime.Second;
		}


		public virtual object safeConvert( string keyName, Type newType )
		{
			object result;

			try
			{
				if ( ContainsKey( keyName ))
				{
					result = safeConvert( this[keyName], newType );
				}
				else
				{
					result = ObjectFactory.create( newType );
				}
			}
			catch
			{
				result = ObjectFactory.create( newType );
			}


			return result;

		}


		public virtual object safeConvert( object o, Type newType )
		{
			object result;
			bool defaultConvert = false;


			try
			{

				if (o == null)
				{
					defaultConvert = true;
				}
				else
				{
					if (o.GetType().Equals( typeof (System.DBNull)))
					{
						defaultConvert = true;
					}
					else if (o.ToString() == "NULL" )
					{
						defaultConvert = true;
					}
				}

				if (defaultConvert)
				{
					result = ObjectFactory.create( newType );
				}
				else
				{
					result = Convert.ChangeType( o, newType );
				}
			}
			catch
			{
				string s = (o != null ? o.ToString() : "null" );
				if (Logger.isDebugMode() ) Logger.log( Logger.Level.DEBUG, "Error converting " + s + " to " + newType.ToString() );

				result = ObjectFactory.create( newType );
				
			}
			return result;
		}
			

		public void combineDataTable( DataSet target, DataSet src, int  srcTableIndex, string tableName )
		{
			if (src == null) throw new ModelObjectException( "Source DataSet is null.");
			if (src.Tables.Count == 0) throw new ModelObjectException( "Source DataSet table count is 0.");
			if (target == null) throw new ModelObjectException( "Target DataSet is null.");;

			if (srcTableIndex > src.Tables.Count) throw new ModelObjectException( "Source table index greater than the number of tables.");

			DataTable srcTable = src.Tables[srcTableIndex];
		
			DataTable targetTable = null;
			if (target.Tables.Contains( tableName))
			{
				targetTable = target.Tables[ tableName ];

				if (srcTable.Columns.Count != targetTable.Columns.Count) throw new ModelObjectException( "Source table does not have same number of columns as existing table.");
			}
			else
			{
			
				targetTable = new DataTable( tableName );

				foreach( DataColumn c in srcTable.Columns)
				{
					targetTable.Columns.Add( c.ColumnName, c.DataType );
				}

				target.Tables.Add( targetTable );
			}

			if (srcTable.Rows.Count == 0) return;

			foreach( DataRow r in srcTable.Rows)
			{
				DataRow newRow = targetTable.NewRow();

				foreach( DataColumn c in srcTable.Columns)
				{
					newRow[c.ColumnName] = r[c.ColumnName];
				}

				targetTable.Rows.Add( newRow );
			}
			
			
		}
		
		public static ModelObject buildParamTable( DataSet ds )
		{

			if (ds == null) return new ModelObject();
			if (ds.Tables.Count != 1) return new ModelObject();;
			
			return buildParamTable( ds.Tables[0] );
		}

		public static ModelObject buildParamTable( DataTable dt )
		{
			ModelObject result = new ModelObject();
			if (dt == null) return result;
			if (dt.Rows.Count == 0) return result;
			

			foreach( System.Data.DataRow r in dt.Rows )
			{	
				string paramkey = Convert.ToString(r["PARAMNAME"]);
				object paramvalue  = r["PARAMVALUE"];
				
				result[paramkey] = paramvalue;					
			}

			return result;
		}


	}
}
