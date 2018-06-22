using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using LiftCommon;

namespace LiftDomain
{
	public class RolesUser : BaseLiftDomain 
	{
        public IntProperty id = new IntProperty();
        public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty role_id = new IntProperty();
		public IntProperty user_id = new IntProperty();

		public RolesUser()
		{
			BaseTable = "roles_users";
            AutoIdentity = true;
			PrimaryKey = "id";

			attach("id", id);
			attach("created_at", created_at);
			attach("role_id", role_id);
			attach("user_id", user_id);
		}

        //public virtual List<RolesUser> doList(string action)
        //{
        //    return doQuery<RolesUser>(action);
        //}

    }
}
