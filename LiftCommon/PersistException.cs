using System;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for PersistException.
	/// </summary>
	[Serializable]
	public class PersistException : ChainedException
	{
		public PersistException()
		{
			
		}

		public PersistException( object context, string message ) : base( context, message )
		{
			
		}

		public PersistException( object context, Exception e, string message) : base( context, e, message )
		{
		
		}
	}
}
