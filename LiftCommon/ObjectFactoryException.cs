using System;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for ObjectFactoryException.
	/// </summary>
	public class ObjectFactoryException : ChainedException
	{
		public ObjectFactoryException()
		{
			
		}

		public ObjectFactoryException( string message ) : base( message )
		{
		}

		public ObjectFactoryException( object context, string message ) : base( context, message )
		{
				
		}

		public ObjectFactoryException( object context, Exception e, string message) : base( context, e, message )
		{
			
		}
	}
}
