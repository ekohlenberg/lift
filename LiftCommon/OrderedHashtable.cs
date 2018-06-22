using System;
using System.Collections;
using System.Data;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for VectoredHashtable.
	/// </summary>
	public class OrderedHashtable : Hashtable
	{
		private		ArrayList	columns;
		

		public OrderedHashtable()
		{
			columns = new ArrayList();	
		}

		public override void Add(object key, object val)
		{
			
			try
			{
				object o = convertToValidType( val );

				if (base.ContainsKey( key ))
				{
					base.Remove( key );
				}
				else
				{
					columns.Add( key );
				}
	
				if ( val == null)
				{
					base.Add (key, null);
				}
				else
				{
					base.Add( key, o);
				}
			}
			catch( Exception e)
			{
				throw new OrderedHashtableException( val, e, string.Format("VectoredHashtable.Add() Error occurred adding key {0} with value {1}.", key, val.GetType().ToString()) );
		
			}
		}

		public override void Clear()
		{
			columns.Clear();
			base.Clear ();
		}


		public override object this[object key]
		{
			get
			{
				return base[key];
			}
			set
			{
				Add(key,value);
			}
		}

		public virtual object this[ int index ]
		{
			get
			{
				object key = columns[index];
				return base[key];
			}
			set
			{
				object key = columns[index];
				base[key] = value;
			}
		}

		public override void Remove(object key)
		{
			columns.Remove( key );
			base.Remove (key);
		}

		public virtual ArrayList Names
		{
			get
			{
				return columns;
			}
		}

		
		public override string ToString()
		{
			string s = this.GetType().ToString();
			s += "\r\n";
			object o = null;

			for (int i = 0; i < this.Count; i++)
			{
				o = this[i];

				s += (string) this.Names[i];
				
				
				if (o != null)
				{
					s += "\t\t";
					s += o.ToString();
				}
				else
				{
					s += "\t\t";
					s += "null";
				}

				s += "\r\n";
			}

			return s;
		}


		protected virtual object convertToValidType( object val )
		{
			object o = null;

			if (val != null)
			{
				o = val;
				Type t = val.GetType();
			
				if (t == typeof( double))
				{
					o = Convert.ToDecimal( (double) val );
				}
			}
			else
			{
				o = new object();
			}

			return o;
		}

		public virtual void copyFrom( Hashtable src )
		{
			this.Clear();

			if (src == null)
			{
				return;
			}

			IDictionaryEnumerator de = src.GetEnumerator();
			while( de.MoveNext() )
			{
				this.Add( de.Key, de.Value );
			}
		}

		public virtual void copyFrom( OrderedHashtable src)
		{
			this.Clear();

			if (src == null)
			{
				return;
			}

			for (int i = 0; i < src.Count; i++)
			{
				this.Add( src.Names[i], src[i] );
			}
		}
			
		public virtual void copyFrom( DataTable t, DataRow r)
		{
			this.Clear();

			if ((t == null) || (r == null))
			{
				return;
			}

			for (int i = 0; i < t.Columns.Count; i++)
			{
				this.Add( t.Columns[i].ColumnName, r[i] );
			}
		}

		public virtual void safeAssign( string targetKey, OrderedHashtable srcObj, string srcKey )
		{
			if (srcObj.ContainsKey(srcKey))
			{
				this[targetKey] = srcObj[srcKey];
			}
		}

		public virtual OrderedHashtable copy()
		{
			Type t = this.GetType();
			object o = t.Assembly.CreateInstance( t.ToString());

			OrderedHashtable dest = (OrderedHashtable) o;

			foreach(string name in Names)
			{
				dest.Add( name, this[name] );
			}

			return dest;
		}

		public virtual void sort()
		{
			Names.Sort();
		}
			
	}
}
