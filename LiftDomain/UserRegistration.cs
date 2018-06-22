using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class UserRegistration : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public UserTimeProperty expires_at = new UserTimeProperty();
		public IntProperty id = new IntProperty();
		public IntProperty  token = new IntProperty ();
		public IntProperty user_id = new IntProperty();

		public UserRegistration()
		{
			BaseTable = "user_registrations";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("expires_at", expires_at);
			attach("id", id);
			attach("token", token);
			attach("user_id", user_id);
		}
	}
}
