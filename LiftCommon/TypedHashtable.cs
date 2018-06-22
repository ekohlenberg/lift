using System;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for TypedVectoredHashtable.
	/// </summary>
	public class TypedHashtable : OrderedHashtable
	{
		private OrderedHashtable types;
		public TypedHashtable()
		{
			types = new OrderedHashtable();
		}

		public override void Add(object key, object value)
		{
			base.Add (key, value);
			
			if (!types.ContainsKey( key))
			{
				if (value != null)
				{
					types.Add( key, value.GetType());
				}
				else
				{
					types.Add( key, typeof(System.DBNull) );
				}
			}

		}

		public override void Clear()
		{
			base.Clear ();
			types.Clear();
		}

		public override void Remove(object key)
		{
			base.Remove (key);
			types.Remove(key);
		}

		
		public OrderedHashtable Types2
		{
			get
			{
				return types;
			}
		}
		

		public virtual void copyFrom( TypedHashtable src)
		{
			this.Clear();

			for (int i = 0; i < src.Count; i++)
			{
				this.Add( src.Names[i], src[i] );
				this.types[i] = src.Types2[i];
			}
		}

		public override OrderedHashtable copy()
		{
			TypedHashtable dest = (TypedHashtable) base.copy();

			
			foreach( string key in Names)
			{
				dest.types.Add( key, types[key] );
			}
			

			return dest;
				
		}


	}
}
