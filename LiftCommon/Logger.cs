using System;
using System.Web.Mail;
using System.Web.Util;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for Logger.
	/// </summary>
	public class Logger
	{
		[Flags]
		public enum Level 
		{
			ERROR = 1,
			DEBUG = 2,
			PROFILE = 4,
			DETAILED = 8,
			ACTION = 16,
			SQL = 32,
			QUEUE = 64,
			DATABASE = 128,
			SECURITY = 256,
			WARNING = 512,
			EVENTS = 1024,
			ALARMS = 2048,
			TRIGGER = 4096
		}

		public static Level level;
		protected static bool initialized = false;
		protected static bool enabled = true;
		protected static bool stacktrace = false;
		
		protected static string outputfile = @"C:\naztec.log";
		protected static string options = "ERROR";

		protected static ReaderWriterLock rwl = new ReaderWriterLock();

		protected static string  SmtpServer = "";
		protected static string  SmtpUsername = "";
		protected static string  SmtpPassword = "";
		protected static string  SmtpTo = "eric@naztec.com, amrish@naztec.com";
		protected static string  SmtpFrom = "donotreply@naztec.com";
		protected static int SmtpPort  = 25;

		protected static string mainTitle = string.Empty;

		protected static int lockTimeout = 60000;

		protected static Hashtable threadInfo = new Hashtable();
		protected static Hashtable stringBuilderTable = new Hashtable();

		public Logger()
		{
		}

		public static void setThreadInfo( string info )
		{
			int id = Thread.CurrentThread.GetHashCode();

			lock( threadInfo )
			{
				if (!threadInfo.ContainsKey(id))
				{
					threadInfo.Add( id, info );
				}
				else
				{
					threadInfo[id] = info;
				}
			}
		}

		public static string getThreadInfo()
		{
			int id = Thread.CurrentThread.GetHashCode();
			string info = string.Empty;

			lock( threadInfo)
			{
				if (threadInfo.ContainsKey( id ))
				{
					info = threadInfo[id].ToString();
				}
			}

			return info;
		}

		public static bool isDebugMode()
		{
			bool debugMode = false;
			if ((Logger.level & Level.DEBUG) == (Level.DEBUG))
			{
				debugMode = true;
			}

			return debugMode;
		}

        public static bool isMode(Logger.Level m)
        {
            return ((Logger.level & m) == m);
        }
		

		public static void logArray( Level L, Array array )
		{
			Logger.initialize();

			string[] elts = new string[ array.Length ];

			for( int i = 0; i < array.Length; i++)
			{
				elts[i] = "[" + i + "] = " + array.GetValue(i).ToString() + "\n";
			}

			Logger.log( L, elts );
			

		}

		public static void log( Level L, object[] args )
		{
			Logger.initialize();

			if (Logger.enabled)
			{
				if ((L & Logger.level) != 0)
				{
					StringBuilder sb = getStringBuilder();

					foreach( object arg in args)
					{
						sb.Append( arg.ToString() );
						sb.Append(" ");
					}

					Logger.writeLog(L, sb.ToString() );
				}
			}
		}

		public static void log( Level L, Hashtable h )
		{

			Logger.initialize();

			if (Logger.enabled)
			{
				if ((L & Logger.level) != 0)
				{
					StringBuilder sb = getStringBuilder();

					if (h != null)
					{
						IDictionaryEnumerator de = h.GetEnumerator();
						while( de.MoveNext() )
						{
							sb.Append( de.Key.ToString()  );
							sb.Append("=");
							sb.Append( de.Value != null ? de.Value.ToString() : "null" );
							sb.Append( "\r\n" );
						}
					}
					else
					{
						sb.Append( "Hashtable is null\r\n" );
					}

					Logger.writeLog(L, sb.ToString() );
				}
			}
		}
		protected static StringBuilder getStringBuilder()
		{
			StringBuilder sb = null;

			int threadId = System.Threading.Thread.CurrentThread.GetHashCode();

			lock( stringBuilderTable )
			{
				if (stringBuilderTable.ContainsKey( threadId ))
				{
					sb = (StringBuilder) stringBuilderTable[ threadId ];
				}
				else
				{
					sb = new StringBuilder();
					stringBuilderTable.Add( threadId, sb );
				}

				sb.Length = 0;
			}

			return sb;
		}

		public static void log( Level L, string message )
		{
			Logger.initialize();

			if (Logger.enabled)
			{
				if ((L & Logger.level) != 0)
				{
					Logger.writeLog(L, message );
				}
			}
		}

		public static void log( Level L, object context, string message )
		{
		
			Logger.initialize();

			if (Logger.enabled)
			{
			
				if ((L & Logger.level) != 0)
				{
					StringBuilder sb = getStringBuilder();

					sb.Append( context.GetType().ToString() );
					sb.Append("\r\n");
					sb.Append( message );

					if (context != null)
					{
						sb.Append( "\r\nContext: \r\n" );
						if (context.GetType() == typeof( System.Data.DataSet ))
						{
							sb.Append( dataSetToString( (DataSet) context ) );
						}
						else if (context.GetType() == typeof( System.Data.DataTable ))
						{
							sb.Append( dataTableToString( (DataTable) context ) );
						}
						else
						{
							sb.Append (context.ToString() );
						}
					}
					
					Logger.writeLog(L, sb.ToString() );
				}
			}

		}

		public static string dataTableToString( DataTable table )
		{
			string cr = "\r\n";
			StringBuilder sb = new StringBuilder();

			sb.Append( table.TableName );
			sb.Append( cr ); 
					
			string line1 = string.Empty;
			string line2 = string.Empty;

			foreach( DataColumn dc in table.Columns)
			{
				if (line1.Length != 0)
				{
					line1 += "\t";
					line2 += "\t";
				}

				line1 += dc.ColumnName;
				line2 += dc.DataType.ToString();
			}
						
			sb.Append( line1 );
			sb.Append( cr );
			sb.Append( line2 );
			sb.Append( cr );
			
			foreach( DataRow row in table.Rows )
			{
				line1 = string.Empty;

				for(int i = 0; i < table.Columns.Count; i++)
				{
					if (line1.Length != 0)
					{
						line1 += "\t";
					}
					line1 += row[i].ToString();
				}

				sb.Append( line1 );
				sb.Append( cr );
			}

			return sb.ToString();

		}
		public static string dataSetToString( DataSet dataSet )
		{
			StringBuilder sb = new StringBuilder();

			foreach( DataTable table in dataSet.Tables)
			{
				sb.Append( dataTableToString( table ));
			}

			return sb.ToString();
		}
				
		
						
		
		public static string toHex( long number )
		{
			ArrayList result = new ArrayList();

			double quotient;
			int remainder;

			quotient = number;

			while( quotient > 1.0)
			{
				remainder = (int) quotient % 16;
			
				result.Add( remainder );
				quotient = quotient / 16;
			}

			string s = string.Empty;
			for( int i = result.Count - 1; i >= 0; i--)
			{
				int r = (int) result[i];
				char c = (char) ( r < 10 ? (int) '0' + r : (int) 'A' + (r-10) );
				s += c;
			}

			return s;
		}

		public static string toHex( byte  number )
		{
			byte upper = 0;
			byte lower = 0;
	
			upper = (byte)( number >> (byte) 4);
			lower = number &= 15;


			string result= string.Empty;
  

			result += _toHex( upper );
			result += _toHex( lower );


			return result;
		}


		protected static char _toHex( byte r )
		{
			 char c = (char) ( r < 10 ? (int) '0' + r : (int) 'A' + (r-10) );

			return c;
		}
		

		public static void log( object context, Exception e,  string message )
		{
			Logger.initialize();

			if (Logger.enabled)
			{

				StringBuilder sb = new StringBuilder();
				sb.Append( buildMessage( e ) );

				sb.Append( ";" );
				
				if (context != null)
				{
					sb.Append(  "\r\n" );
					sb.Append( context.GetType().ToString() );
					sb.Append( "\r\n" );
					sb.Append( context.ToString() );
				}
			

				writeLog( Logger.Level.ERROR, sb.ToString() );
			}
		}

		protected static string buildMessage( Exception e)
		{
			string s = e.Message;

			if (Logger.stacktrace)
			{
				s += "\n";
				s += e.StackTrace;
			}

			if (e.InnerException != null)
			{
				s += buildMessage( e.InnerException );
			}

			return s;
		}

		protected static void stackInfo( ref string _filename, ref string _objectName, ref string _methodName, ref int _lineNumber )
		{
			string filename = string.Empty;
			string objectName = "Unknown";
			string methodName = "Unknown";
			int lineNumber = 0;

			StackFrame sf = new StackFrame( 3, true ); 

			try
			{	

			if (sf != null)
			{
				
				System.Reflection.MethodBase method =  sf.GetMethod();
				if ( method != null)
				{
					if (method.ReflectedType != null)
					{

						objectName = method.ReflectedType.ToString();
					}
					else
					{
						objectName = "Global";
					}

					methodName = method.ToString(); 
				}

				int space = methodName.IndexOf(" ");

				space++;

				methodName = methodName.Substring( space, methodName.Length - space );
			
				filename = sf.GetFileName(); 

				if (filename != null)
				{

					int lastSlash = filename.LastIndexOf(@"\");

					lastSlash++;

					filename = filename.Substring( lastSlash, filename.Length - lastSlash );
				}
						
			
				lineNumber = sf.GetFileLineNumber();
			}
			}
			catch
			{
			}

			_filename = filename;
			_objectName = objectName;
			_methodName = methodName;
			_lineNumber = lineNumber;


		}



		protected static void writeLog( Level L, string m )
		{
			string filename = "";
			string objectName = "";
			string methodName = "";
			int lineNumber = 0;

			stackInfo(ref  filename, ref objectName, ref methodName, ref lineNumber );

			if ((L & Logger.level) != 0)
			{

				try
				{
					Logger.rwl.AcquireWriterLock( lockTimeout );

					int attempts = 0;
					try
					{
					
						StreamWriter sw = null;
					
					Again:
						try
						{
							if (attempts < 3)
							{
								sw =	File.AppendText( Logger.outputfile );
							}
							else
							{
								return;
							}
						}
						catch
						{
							attempts++;
							goto Again;
						}

						string memory = string.Empty;

						if ((Logger.level & Logger.Level.PROFILE) > 0)
						{
							memory = GC.GetTotalMemory(false).ToString();
							memory += "\t";
						}

						DateTime now = DateTime.Now;

						string timeString = now.ToString("MM/dd/yyyy HH:mm:ss.ffff");
						
						string info = getThreadInfo();

						StringBuilder sb = new StringBuilder();
						sb.Append(timeString);
						sb.Append("\t-");
						sb.Append(L.ToString());
						sb.Append("-\t");
						sb.Append(memory);
						sb.Append("T[");
						sb.Append(Thread.CurrentThread.GetHashCode().ToString());
						sb.Append(" " );
						sb.Append(info);
						sb.Append("]\t");
						sb.Append(filename);
						sb.Append(":");
						sb.Append(lineNumber.ToString());
						sb.Append(" ");
						sb.Append(objectName);
						sb.Append(".");
						sb.Append(methodName);
						sb.Append(": ");
						sb.Append(m);

					
						sw.WriteLine( sb.ToString() );

						sw.Close();

					}
					catch
					{
					}
					finally
					{
						rwl.ReleaseWriterLock();
					}
				}
				catch
				{

				}
			}

		}

		public static void initialize()
		{
			Logger.rwl.AcquireWriterLock( lockTimeout );
			try
			{	
				if (!Logger.initialized)
				{
					try
					{
						if (!Directory.Exists( @"c:\log"))
						{
							Directory.CreateDirectory( @"c:\log" );
						}
					}
					catch
					{
					}

					
			
					AppSettingsReader settingsReader = new AppSettingsReader();



					Logger.options = "ERROR";

					try
					{
						string enabled = (string) settingsReader.GetValue("Logger.Enabled", typeof(System.String));
						Logger.enabled = (enabled == "true");
					}
					catch
					{
						Logger.enabled = true;
					}
					try
					{
						string enabled = (string) settingsReader.GetValue("Logger.Stacktrace", typeof(System.String));
						Logger.stacktrace = (enabled == "true");
					}
					catch
					{
						Logger.stacktrace = true;
					}
					try
					{
						Logger.outputfile = (string) settingsReader.GetValue("Logger.Outputfile", typeof(System.String));
					}
					catch
					{
						string name = "app";

						try
						{
                            name = System.AppDomain.CurrentDomain.ApplicationIdentity.FullName;
                            name += ".log";
						}
						catch
						{
						}
						
						Logger.outputfile =  name;
						
					}

					try
					{
						Logger.SmtpServer  = (string) settingsReader.GetValue("Logger.SmtpServer", typeof(System.String));
					}
					catch
					{
						Logger.SmtpServer  = "";
					}

					try
					{
						Logger.SmtpUsername  = (string) settingsReader.GetValue("Logger.SmtpUsername", typeof(System.String));
					}
					catch
					{
						Logger.SmtpUsername  = "";
					}

					try
					{
						Logger.SmtpPassword  = (string) settingsReader.GetValue("Logger.SmtpPassword", typeof(System.String));
					}
					catch
					{
						Logger.SmtpPassword  = "";
					}

					try
					{
						Logger.SmtpTo  = (string) settingsReader.GetValue("Logger.SmtpTo", typeof(System.String));
					}
					catch
					{
						Logger.SmtpTo = "eric@naztec.com, amrish@naztec.com";
					}

					try
					{
						Logger.SmtpFrom  = (string) settingsReader.GetValue("Logger.SmtpFrom", typeof(System.String));
					}
					catch
					{
						Logger.SmtpFrom = "support@naztec.com";
					}

					try
					{
						Logger.SmtpPort  = (int) settingsReader.GetValue("Logger.SmtpPort ", typeof(System.Int32));
					}
					catch
					{
						Logger.SmtpPort  = 25;
					}



					string filename = Logger.outputfile;

					if (File.Exists( filename ))
					{
						string newFilename = filename.Replace( ".log", "." + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".log" );
						File.Move( filename, newFilename );
					}


					try
					{
						Logger.options = (string) settingsReader.GetValue("Logger.Options", typeof(System.String));
					}
					catch
					{
						if (File.Exists( "Logger.Options") )
						{
							StreamReader r = File.OpenText( "Logger.Options" );

							Logger.options = r.ReadLine();

							r.Close();
						}
						else
						{
							Logger.options = "ERROR";
						}
						
					}

					initializeOptions();
					
				}

				Logger.initialized = true;
			}
			finally
			{

				Logger.rwl.ReleaseWriterLock();
				
			}
		}

		protected static void initializeOptions()
		{
			string [] names = Enum.GetNames( typeof(Logger.Level) );

			Logger.level = 0;
			foreach( string s in names)
			{
				if (Logger.options.IndexOf(s) != -1)
				{
					Logger.level |= (Logger.Level) Enum.Parse( typeof( Logger.Level ), s);
				}
			}
		}

		public static void printStackInfo(  )
		{

			for( int i = 0; i < 10; i++)
			{
				StackFrame sf = new StackFrame( i, true ); 
			
				string objectName = sf.GetMethod().ReflectedType.ToString();

				string methodName = sf.GetMethod().ToString(); 
			
				string filename = sf.GetFileName(); 
			
				int lineNumber = sf.GetFileLineNumber();

				Logger.writeLog(Logger.Level.DEBUG , objectName + "." + methodName + " - " + filename + ":" + lineNumber  );
			}

			
		}

	}

	
}
