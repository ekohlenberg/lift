using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class SimpleCaptchaDat : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty id = new IntProperty();
		public StringProperty key = new StringProperty();
		public UserTimeProperty updated_at = new UserTimeProperty();
		public StringProperty value = new StringProperty();

		public SimpleCaptchaDat()
		{
			BaseTable = "simple_captcha_data";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("id", id);
			attach("key", key);
			attach("updated_at", updated_at);
			attach("value", value);
		}
	}
}
