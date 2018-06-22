using System;
using System.Collections;
using System.Configuration;


namespace LiftCommon
{
	/// <summary>
	/// Summary description for ConfigReader.
	/// </summary>
	public class ConfigReader
	{
		protected static Hashtable properties = new Hashtable();
		protected static object sync = new object();
		protected static AppSettingsReader settingsReader = null;
		public ConfigReader()
		{

		}

		public static string getString( string property, string defaultValue )
		{
			string result = defaultValue;

			lock( sync )
			{
				if (properties.ContainsKey( property ))
				{
					result = (string) properties[ property ];
				}
				else
				{
					try
					{
						if (settingsReader == null) settingsReader = new AppSettingsReader();
						object o = settingsReader.GetValue(property, typeof(System.Object));
						if (o != null)
						{
							result = o.ToString();
						}
						else
						{
							result = defaultValue;
						}

						properties.Add( property, result );
					}
					catch
					{
						Logger.log( Logger.Level.WARNING, "Missing config property: " + property );
						properties.Add( property, defaultValue );
					}
				}
			}

			return result;
		}



		public static int getInt( string property, int defaultValue )
		{
			int result = defaultValue;

			lock( sync )
			{
				if (properties.ContainsKey( property ))
				{
					result = (int) properties[ property ];
				}
				else
				{
					try
					{
						if (settingsReader == null) settingsReader = new AppSettingsReader();
						result = (int) settingsReader.GetValue(property, typeof(int));
						properties.Add( property, result );
					}
					catch
					{
						Logger.log( Logger.Level.WARNING, "Missing config property: " + property  );
						properties.Add( property, defaultValue );
					}
				}
			}

			return result;
		}
	}
}
