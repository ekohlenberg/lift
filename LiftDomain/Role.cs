using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class Role : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty id = new IntProperty();
		public StringProperty title = new StringProperty();
		public UserTimeProperty updated_at = new UserTimeProperty();

        public static string WATCHMAN = "Watchman";
        public static string WALL_LEADER = "Wall Leader";
        public static string MODERATOR = "Moderator";
        public static string ORG_ADMIN = "Organization Admin";
        public static string SYS_ADMIN = "System Admin";


		public Role()
		{
			BaseTable = "roles";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("id", id);
			attach("title", title);
			attach("updated_at", updated_at);
		}
	}
}
