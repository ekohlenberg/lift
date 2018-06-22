using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class RailmailDeliverie : BaseLiftDomain 
	{
		public StringProperty from = new StringProperty();
		public IntProperty id = new IntProperty();
		public StringProperty raw = new StringProperty();
		public UserTimeProperty read_at = new UserTimeProperty();
		public StringProperty recipients = new StringProperty();
		public UserTimeProperty sent_at = new UserTimeProperty();
		public StringProperty subject = new StringProperty();

		public RailmailDeliverie()
		{
			BaseTable = "railmail_deliveries";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("from", from);
			attach("id", id);
			attach("raw", raw);
			attach("read_at", read_at);
			attach("recipients", recipients);
			attach("sent_at", sent_at);
			attach("subject", subject);
		}
	}
}
