using System;
using System.Data;
using System.Reflection;
using System.Resources;
using System.Collections;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for IPersist.
	/// </summary>
	public class Persist
	{
		protected ModelObject domainObject = null;

		public Persist( ModelObject domainObject )
		{
			this.domainObject = domainObject;

		}

		public virtual string  contentFromAction( string action )
		{
			string content = null;

			Assembly assy = domainObject.GetType().Assembly;
			string assyName = assy.GetName().Name;
			string objectName = domainObject.GetType().Name;
			string resourceBaseName = assyName + "." + objectName + ".template";

			try
			{		
				ResourceManager rm = new ResourceManager( resourceBaseName, assy );

				content = getResourceString( rm, action );

				if (content == null)
				{
					throw new PersistException( this, string.Format( "Resource name {0} not found in resource {1}.resx. Check that the {2} project has the correct resource file.", action, objectName, assyName));
				}
				
			}
			catch( PersistException pe )
			{
				throw pe;
			}
			catch( Exception e)
			{
				throw new PersistException( this, e, string.Format( "Error occurred while attempting to obtain resource {0} in {1}.resx in the {2} project.", action, objectName, assyName ));
			}

			return content;
		}


		protected virtual string getResourceString( ResourceManager rm, string action )
		{
			string content = rm.GetString( action );

			return content;
		}
		
		public virtual	DataSet doQuery( string action )
		{
			
			DataSet result = null;
			MethodInfo actionMethod = null;
		
			preProcessDataSet( action );

			actionMethod = domainObject.getMethod(action);

			if ( actionMethod != null )
			{
				result = (DataSet) actionMethod.Invoke( domainObject, null );
			}
			else
			{
				actionMethod = this.getMethod( action );

				if (actionMethod != null)
				{
					result = (DataSet) actionMethod.Invoke( this, null );
				}
				else
				{
					result = handleQuery( action );
				}
			}

			postProcessDataSet( result, action );
			
			return result;
		}


		public virtual void preProcessDataSet( string action )
		{
			string preProcessMethodName = "pre_" + action;
	
			MethodInfo preProcessMethod = domainObject.GetType().GetMethod( preProcessMethodName );
			
			if (preProcessMethod != null)
			{
				preProcessMethod.Invoke( domainObject, null );
			}
		}

		public virtual void postProcessDataSet( DataSet ds, string action )
		{
			try
			{
				string postProcessTableMethodName = "post_datatable_" + action;
				string postProcessRowMethodName = "post_datarow_" + action;

				MethodInfo postProcessTableMethod = domainObject.GetType().GetMethod( postProcessTableMethodName );
				MethodInfo postProcessRowMethod = domainObject.GetType().GetMethod( postProcessRowMethodName );

				if ((postProcessTableMethod != null) || (postProcessRowMethod != null))
				{
					if (ds.Tables.Count > 0)
					{
						DataTable dt = ds.Tables[0];

						if (postProcessTableMethod != null)
						{
							object[] tableParam = new object[1];
							tableParam[0] = dt;
							postProcessTableMethod.Invoke( domainObject, tableParam );
						}

						ArrayList removeList = new ArrayList();
						if (postProcessRowMethod != null)
						{
							foreach( DataRow row in dt.Rows)
							{
								object[] rowParam = new object[1];
								rowParam[0] = row;
								bool result = (bool) postProcessRowMethod.Invoke( domainObject, rowParam );
								if (!result)
								{
									removeList.Add(row);
								}
							}
						}

						for( int i = 0; i < removeList.Count; i++)
						{
							DataRow r = (DataRow) removeList[i];
							dt.Rows.Remove(r);
						}
					}
				}
			}
			catch( Exception e)
			{
				Logger.log( this, e, e.Message );
				throw;
			}
		}

		public virtual long doCommand( string action )
		{
			long result = 0;

			MethodInfo actionMethod = null;
			
			actionMethod = domainObject.getMethod(action);

			if ( actionMethod != null )
			{
				result = (long) actionMethod.Invoke( domainObject, null );
			}
			else
			{
				actionMethod = this.getMethod( action );

				if (actionMethod != null)
				{
					result = (long) actionMethod.Invoke( this, null );
				}
				else
				{
					result = handleCommand( action );
				}
			}
			
			return result;
		}

		public virtual MethodInfo getMethod( string action )
		{
			MethodInfo m = this.GetType().GetMethod( action );
			return m;
		}

		
		public virtual DataSet handleQuery( string action )
		{
			DataSet ds = null;
			return ds;
		}

		public virtual long handleCommand( string action )
		{
			return 0;
		}

		public virtual DataSet doQueryRelation( string relation )
		{
			throw new PersistException( this, "doQueryRelation() not supported with this persistence layer.");
		}

		

	}
}
