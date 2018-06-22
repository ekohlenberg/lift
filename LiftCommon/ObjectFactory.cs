using System;
using System.Data ;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for ObjectFactory.
	/// </summary>
	public class ObjectFactory
	{
		public ObjectFactory()
		{
			
		}

		public static object create( Type type )
		{
			object defaultValue;

            bool b = false;
            string s = "";
			int i = 0;
			long l = 0;
			decimal d = (decimal) 0.0;
			double n = 0.0;
			float f = 0.0f;
			System.Int16 smallInt = 0;

			if (type.Equals( typeof( string ) ))
			{
				defaultValue = s;
			}
			else if (type.Equals( typeof( int )))
			{
				defaultValue = i;
			}
			else if (type.Equals( typeof( System.Int16 )))
			{
				defaultValue = smallInt;
			}
			else if (type.Equals( typeof( long )))
			{
				defaultValue = l;
			}
			else if (type.Equals( typeof( decimal )))
			{
				defaultValue = d;
			}
			else if (type.Equals( typeof( double )))
			{
				defaultValue = n;
			}
			else if (type.Equals( typeof( float )))
			{
				defaultValue = f;
			}
            else if (type.Equals(typeof(bool)))
            {
                defaultValue = b;
            }
			else if (type.Equals( typeof( System.DateTime )))
			{
				defaultValue = System.DateTime.Now;
			}
			else if (type.Equals( typeof( System.DBNull)))
			{
				defaultValue = System.DBNull.Value;
			}
			else if (type.Equals( typeof( System.Guid)))
			{
				defaultValue = System.Guid.Empty;
			}
			else if (type.Equals(typeof(System.Byte[])))
			{
				defaultValue = new System.Byte[0];
			}
            else
            {
                throw new ObjectFactoryException("ObjectFactory.create(type): cannot create default value for type: " + type.ToString());
            }

			
			object o = defaultValue;

			return o;
		}

		public static  object create( string strType, string strValue )
		{
			string s = "";
			int i = 0;
			long l = 0;
			decimal d = (decimal) 0.0;
			object o = null;
			double n = 0.0;
			float f = 0.0f;
			System.Int16 smallInt = 0;
			System.Guid g = System.Guid.NewGuid();
			
			DateTime dt = DateTime.Now;

			if (strType == s.GetType().ToString())
			{
				o = strValue;
			}
			else if (strType == i.GetType().ToString())
			{
				strValue = (strValue == "NULL" ? "0" : strValue );
				if (strValue.Length > 0)
				{
					int I = int.Parse(strValue);
					o = I;
				}
				else
				{
					o = int.Parse("0");
				}
			}
			else if (strType == smallInt.GetType().ToString())
			{
				strValue = (strValue == "NULL" ? "0" : strValue );
				if (strValue.Length > 0)
				{
					int SI = System.Int16.Parse(strValue);
					o = SI;
				}
				else
				{
					o = System.Int16.Parse("0");
				}
			}
			else if (strType == l.GetType().ToString())
			{
				strValue = (strValue == "NULL" ? "0" : strValue );
				if (strValue.Length > 0)
				{
					long L = long.Parse(strValue);
					o = L;
				}
				else
				{
					o = long.Parse("0");
				}
			}
			else if (strType == d.GetType().ToString())
			{
				strValue = (strValue == "NULL" ? "0" : strValue );
				if (strValue.Length > 0)
				{
					decimal D = decimal.Parse(strValue);
					o = D;
				}
				else
				{
					o = decimal.Parse("0");
				}
			}
			else if (strType == n.GetType().ToString())
			{
				strValue = (strValue == "NULL" ? "0" : strValue );
				if (strValue.Length > 0)
				{
					double N = double.Parse(strValue);
					o = N;
				}
				else
				{
					o = double.Parse("0");
				}
			}
			else if (strType == f.GetType().ToString())
			{
				strValue = (strValue == "NULL" ? "0" : strValue );
				if (strValue.Length > 0)
				{
					double N = float.Parse(strValue);
					o = N;
				}
				else
				{
					o = float.Parse("0");
				}
			}
			else if (strType == dt.GetType().ToString())
			{
				if (strValue.Length > 0)
				{
					try
					{
						DateTime dateTime = Convert.ToDateTime( strValue );
						o = dateTime;
					}
					catch
					{
						o = System.DateTime.MinValue;
					}
				}
				else
				{
					o = System.DateTime.Now;
				}
			}
			else if (strType == typeof(System.DBNull).ToString())
			{
				o = "NULL";
			}
			else if (strType == g.GetType().ToString())
			{
				Guid gd = new Guid(strValue);
				o = gd;										
			}
			else if (strType == dt.GetType().ToString())
			{
				DateTime dateTime = Convert.ToDateTime( strValue );
				o = dateTime;								
			}
			else if (strType == "System.Single")
			{
				strValue = (strValue == "NULL" ? "0" : strValue );
				if (strValue.Length > 0)
				{
					decimal D = decimal.Parse(strValue);
					o = D;
				}
				else
				{
					o = decimal.Parse("0");
				}
	
			}
			else if (strType == "System.Byte[]")
			{	
				int length = strValue.Length;
				length = length / 2;

				o = new Byte[ length ];
				Byte[] a = (Byte[]) o;

				for( int j = 0; j < length; j ++)
				{
					int index = j*2;
					a[j] = Utilities.fromHex( strValue, index );
				}
			}
			else
			{
				throw new ObjectFactoryException(  string.Format("ObjectFactory: Unexpected type {0}.", strType ));
			}

			return o;
		}

        /*
		protected static string[]  split( string inputString, string delim )
		{
			StringTokenizer st = new StringTokenizer( inputString,  delim );

			string[] parts = new string[st.CountTokens()];

			int i = 0;
			while( st.HasMoreTokens())
			{
				parts[i] = st.NextToken();
				i++;
			}

			return parts;
		}
        */
		public static void InitialiseRow( ref DataRow dRow )
		{
			int colCount = dRow.Table.Columns.Count ;

			for (int i =0; i < colCount ; i++)
			{
				DataColumn column = dRow.Table.Columns[i] ;				
				dRow[column.ColumnName] = create(column.DataType);
			}
		}
	}
}
