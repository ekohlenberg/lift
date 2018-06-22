using System;
using System.Configuration;
using System.Data.OleDb;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for ServerConnection.
	/// </summary>
	public class DatabaseConnection
	{
		private	string					name;
		private	OleDbConnection		connection = null;
		private	string					connectionString;
		private  DateTime				createTime;
		protected bool					inUse = false;



		public DatabaseConnection( string name)
		{
			this.name = name;
			
			connectionString = ConfigurationManager.AppSettings[name];
			connection = new OleDbConnection( connectionString );
			connection.Open();
			this.InUse = true;
			createTime = DateTime.Now;
		}

		public long Age  // in millis
		{
			get
			{
				DateTime now = DateTime.Now;

				long ageMillis = (long) ((now.Ticks - createTime.Ticks) / 10000);
				return ageMillis;
			}
		}

		public OleDbConnection Connection
		{
			get
			{
				return connection;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public bool InUse
		{
			get
			{
				bool result = false;
				lock( this )
				{
					result = inUse;
				}
				return result;
			}
			set
			{
				lock( this )
				{
					inUse = value;
				}
			}
	
		}


	}
}
