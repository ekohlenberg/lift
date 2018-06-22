using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class Day : BaseLiftDomain 
	{
		public IntProperty id = new IntProperty();
		public StringProperty title = new StringProperty();
		public IntProperty user_id = new IntProperty();
		public IntProperty wall_id = new IntProperty();

		public Day()
		{
			BaseTable = "days";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("id", id);
			attach("title", title);
			attach("user_id", user_id);
			attach("wall_id", wall_id);
		}
	}
}
