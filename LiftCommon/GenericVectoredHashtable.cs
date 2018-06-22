using System;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for GenericVectoredHashtable.
	/// </summary>
	public class GenericVectoredHashtable : OrderedHashtable
	{
		public GenericVectoredHashtable()
		{
			
		}

		protected override object convertToValidType(object val)
		{
			return val;
		}

	}
}
