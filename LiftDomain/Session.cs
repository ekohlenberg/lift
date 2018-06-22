using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class Session : BaseLiftDomain 
	{
		public StringProperty data = new StringProperty();
		public IntProperty id = new IntProperty();
		public StringProperty session_id = new StringProperty();
		public UserTimeProperty updated_at = new UserTimeProperty();

		public Session()
		{
			BaseTable = "sessions";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("data", data);
			attach("id", id);
			attach("session_id", session_id);
			attach("updated_at", updated_at);
		}
	}
}
