using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class RolesStaticPermission : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty role_id = new IntProperty();
		public IntProperty static_permission_id = new IntProperty();

		public RolesStaticPermission()
		{
			BaseTable = "roles_static_permissions";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("role_id", role_id);
			attach("static_permission_id", static_permission_id);
		}
	}
}
