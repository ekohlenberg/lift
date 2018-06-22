using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class GroupsUser : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty group_id = new IntProperty();
		public IntProperty user_id = new IntProperty();

		public GroupsUser()
		{
			BaseTable = "groups_users";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("group_id", group_id);
			attach("user_id", user_id);
		}
	}
}
