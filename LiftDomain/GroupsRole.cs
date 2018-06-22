using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class GroupsRole : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty group_id = new IntProperty();
		public IntProperty role_id = new IntProperty();

		public GroupsRole()
		{
			BaseTable = "groups_roles";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("group_id", group_id);
			attach("role_id", role_id);
		}
	}
}
