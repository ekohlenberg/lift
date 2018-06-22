using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class Increment : BaseLiftDomain 
	{
		public IntProperty day_id = new IntProperty();
		public FloatProperty end_time = new FloatProperty();
		public IntProperty id = new IntProperty();
		public FloatProperty start_time = new FloatProperty();
		public StringProperty title = new StringProperty();
		public IntProperty user_id = new IntProperty();

		public Increment()
		{
			BaseTable = "increments";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("day_id", day_id);
			attach("end_time", end_time);
			attach("id", id);
			attach("start_time", start_time);
			attach("title", title);
			attach("user_id", user_id);
		}
	}
}
