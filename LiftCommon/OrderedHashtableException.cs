using System;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for VectoredHashtableException.
	/// </summary>
	public class OrderedHashtableException : ChainedException 
	{
		
		public OrderedHashtableException()
		{
			
		}

		public OrderedHashtableException( object context, string message ) : base( context, message )
		{
				
		}

		public OrderedHashtableException( object context, Exception e, string message) : base( context, e, message )
		{
			
		}
	}
}
