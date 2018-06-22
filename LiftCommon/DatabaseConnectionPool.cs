using System;
using System.Collections;
using System.Threading;
using System.Diagnostics;
namespace LiftCommon
{
	/// <summary>
	/// MOST PUBLIC METHODS SHOULD BE SYNCHRONIZED WITH LOCK().
	/// The only time a public method should not be locked is when it calls another public method
	/// that is already locked.
	/// </summary>
	public class DatabaseConnectionPool
	{
		private static DatabaseConnectionPool connectionPool = null;
		private static bool instanceFlag = false;
		private static long connectionTimeOutMillis = 30000; 
		
		private static Mutex mutex = new Mutex();

		private System.Data.ConnectionState badState = (System.Data.ConnectionState.Broken |
																				System.Data.ConnectionState.Closed |
																				System.Data.ConnectionState.Executing |
																				System.Data.ConnectionState.Fetching |
																				System.Data.ConnectionState.Connecting );

		private Hashtable connectionTable;

		public DatabaseConnectionPool()
		{			
			connectionTable = new Hashtable();
		}

		public virtual void setTimeoutMillis( long millis )
		{
			lock( this )
			{
				DatabaseConnectionPool.connectionTimeOutMillis = millis;
			}
		}

		public static DatabaseConnectionPool getConnectionPool()
		{
			mutex.WaitOne();
			try
			{
			{
				if (!instanceFlag)
				{
					connectionPool = new OleDbConnectionPool2();
					instanceFlag = true;
				}
			}
			}
			finally
			{
				mutex.ReleaseMutex();
			}
			return connectionPool;
		}

		public virtual DatabaseConnection getConnection()  // don't lock this method because it calls another locked method
		{
			string name = "defaultconnection";

			return getConnection( name );
		}


		public virtual DatabaseConnection getConnection( string name )
		{
			DatabaseConnection sc = null;
			mutex.WaitOne();

				if (connectionTable.ContainsKey( name ))
				{
					ArrayList connections = (ArrayList) connectionTable[name];

					ageOutConnections( connections );

					if (connections.Count > 0)
					{
						bool found = false;
						for ( int i = 0; i < connections.Count && !found; i++)
						{
							sc = (DatabaseConnection) connections[i];
							
							if (!sc.InUse)
							{
								sc.InUse = true;
								found = true;
							}
						}

						if (found)
						{
							connections.Remove(sc);
						}
						else
						{
							sc = null;
						}
					}
				}

				if (sc == null)
				{
					sc = new DatabaseConnection( name );
				}
			
			mutex.ReleaseMutex();

			return sc;
		}

		public virtual void releaseConnection( DatabaseConnection sc )
		{
			mutex.WaitOne();

			

			ArrayList connections = null;

			if ((sc.Connection.State & badState) == 0)
			{
				if (connectionTable.ContainsKey( sc.Name ))
				{
					connections = (ArrayList) connectionTable[sc.Name];
					connections.Add( sc );
				}
				else if ((sc.Connection.State & System.Data.ConnectionState.Open) > 0)
				{
					connections = new ArrayList();
					connections.Add( sc );
					connectionTable.Add( sc.Name, connections );
				}
			}
			else
			{
				Debug.WriteLine( "OleDbConnection.releaseConnection() Invalid Connection State being returned to pool. Disgarding connection." );
			}

			sc.InUse = false;
			mutex.ReleaseMutex();
		}

		protected virtual void ageOutConnections( ArrayList connectionList )
		{
			ArrayList deleteList = new ArrayList();

			for (int i = 0; i < connectionList.Count; i++)
			{
				DatabaseConnection sc = (DatabaseConnection) connectionList[i];

				if (sc.Age > DatabaseConnectionPool.connectionTimeOutMillis)
				{
					deleteList.Add( sc );
				}
			}

			for (int j = 0; j < deleteList.Count; j++)
			{
				DatabaseConnection toBeDeleted = (DatabaseConnection) deleteList[j];

				toBeDeleted.Connection.Close();

				connectionList.Remove( toBeDeleted );
			}
		}

		public virtual int getConnectionCount( string name )
		{
			int count = 0;

			mutex.WaitOne();

				if (connectionTable.ContainsKey(name))
				{
					ArrayList connections = (ArrayList) connectionTable[name];

					count = connections.Count;
				}

			mutex.ReleaseMutex();

			return count;
		}

	}

	public class OleDbConnectionPool2 : DatabaseConnectionPool
	{
		public OleDbConnectionPool2() 
		{
		}

		public override DatabaseConnection getConnection(string name)
		{
			return new DatabaseConnection( name );
		}

		public override void releaseConnection(DatabaseConnection sc)
		{
			sc.Connection.Close();
		}


	}
}
