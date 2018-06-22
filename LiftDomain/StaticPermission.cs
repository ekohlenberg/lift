using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class StaticPermission : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty id = new IntProperty();
		public StringProperty title = new StringProperty();
		public UserTimeProperty updated_at = new UserTimeProperty();

		public StaticPermission()
		{
			BaseTable = "static_permissions";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("id", id);
			attach("title", title);
			attach("updated_at", updated_at);
		}
	}
}
