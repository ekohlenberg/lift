using System;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for ChainedException.
	/// </summary>
	[Serializable]
	public class ChainedException : Exception
	{
		protected Exception rootException = null;
		protected object context = null;
		protected string localMessage;
		public ChainedException()
		{
		}

		public ChainedException( object context, string message ) : base( message )
		{
			this.context = context;
			this.localMessage = message;
			
		}

		public ChainedException( string message ) : base( message )
		{
			this.context = null;
			this.localMessage = message;
		}

		public ChainedException( object context, Exception e, string message) : base( message )
		{
			this.context = context;
			this.rootException = e;
			this.localMessage = "";

			if (e.InnerException != null)
			{
				this.localMessage = e.InnerException.Message + ";";
			}

			this.localMessage = e.Message + "; " + message;
		}

		protected virtual string buildMessage( string message )
		{
			string s = "";
			
			if (context != null)
			{
				s = this.GetType().ToString() + " occurred in class " + context.GetType().ToString() + "\r\n";
				s += context.ToString();
			}
			
			s +=	message;

			if (rootException != null)
			{
				s += " Root Exception: " + rootException.GetType().ToString() + ": " + rootException.Message;

				if (rootException.InnerException != null)
				{
					s += " Root Inner Exception: " + rootException.InnerException.GetType().ToString() + ": " + rootException.InnerException.Message;
				}
			}

			if (this.InnerException != null)
			{
				s += " Inner Exception: " + this.InnerException.GetType().ToString() + ": " + this.InnerException.Message;
			}

			return s;
		}

		public override string Message
		{
			get
			{
				return buildMessage( localMessage );
				//return localMessage;
			}
		}
        
		public override string ToString()
		{
			string s = "";
			
			if (context != null)
			{
				s = this.GetType().ToString() + " occurred in class " + context.GetType().ToString() + "\r\n";
				s += context.ToString();
			}
			
			s +=	base.ToString();

			if (rootException != null)
			{
				s += "\r\nRoot Exception: " + rootException.GetType().ToString() + ": " + rootException.ToString();

				if (rootException.InnerException != null)
				{
					s += "\r\nInner Exception: " + rootException.InnerException.GetType().ToString() + ": " + rootException.InnerException.ToString();
				}
			}

			return s;
		}

		public override string StackTrace
		{
			get
			{
				string s = base.StackTrace;
				if (rootException != null)
				{
					s += "\r\nRoot Exception: " + rootException.GetType().ToString() + "\r\n";
					s += rootException.StackTrace;

					if (rootException.InnerException != null)
					{
						s += "\r\nInner Exception: " + rootException.InnerException.GetType().ToString() + "\r\n";
						s += rootException.InnerException.StackTrace;
					}
				}

				return s;
			}
		}
	}
}
