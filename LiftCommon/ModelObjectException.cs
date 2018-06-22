using System;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for DomainObjectException.
	/// </summary>
	[Serializable]
	public class ModelObjectException : ChainedException
	{
		public ModelObjectException()
		{
			
		}

		public ModelObjectException( string message ) : base( message )
		{
		}

		public ModelObjectException( object context, string message ) : base( context, message )
		{
			
		}

		public ModelObjectException( object context, Exception e, string message) : base( context, e, message )
		{
		
		}

	}

	public class NoDataException : Exception
	{
		public NoDataException( string message ) : base( message )
		{
		}

		public NoDataException( string message, Exception innerException) : base( message, innerException )
		{
		}

		public NoDataException()
		{
		}
	}


	public class TooMuchDataException : Exception
	{
		public TooMuchDataException( string message ) : base( message )
		{
		}

		public TooMuchDataException( string message, Exception innerException) : base( message, innerException )
		{
		}

		public TooMuchDataException()
		{
		}
	}



												   
	
}
