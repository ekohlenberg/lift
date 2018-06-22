using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class Group : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty id = new IntProperty();
		public IntProperty parent_id = new IntProperty();
		public StringProperty title = new StringProperty();
		public UserTimeProperty updated_at = new UserTimeProperty();

		public Group()
		{
			BaseTable = "groups";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("id", id);
			attach("parent_id", parent_id);
			attach("title", title);
			attach("updated_at", updated_at);
		}
	}
}
