using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class Prayersession : BaseLiftDomain 
	{
		public UserTimeProperty end_time = new UserTimeProperty();
		public IntProperty id = new IntProperty();
		public StringProperty note = new StringProperty();
		public UserTimeProperty start_time = new UserTimeProperty();
		public IntProperty total_requests = new IntProperty();
		public IntProperty user_id = new IntProperty();

		public Prayersession()
		{
			BaseTable = "prayersessions";
			AutoIdentity = true;
			PrimaryKey = "id";

			attach("end_time", end_time);
			attach("id", id);
			attach("note", note);
			attach("start_time", start_time);
			attach("total_requests", total_requests);
			attach("user_id", user_id);
		}

        public void my_account_prayer_session_helper(DataRow r, Hashtable h)
        {
            //-- format = x hours, x minutes, x seconds
            DateTime startTime = LiftTime.toUserTime(Convert.ToDateTime(r["start_time"]));
            DateTime endTime = LiftTime.toUserTime(Convert.ToDateTime(r["end_time"]));
            TimeSpan durationTimeSpan = endTime - startTime;

            h["display_start_time"] = startTime.ToString("dddd MMMM dd, yyyy h:mm tt");
            h["total_time"] = durationTimeSpan.Hours.ToString() + " hours, " + durationTimeSpan.Minutes.ToString() + " minutes, " + durationTimeSpan.Seconds.ToString() + " seconds";
        }

        public virtual long create_session()
        {
            long sessionId = 0;
            User u = User.Current;
            user_id.Value = u.id;
            start_time.Value = LiftTime.CurrentTime;
            end_time.Value = start_time.Value;
            total_requests.Value = 0;
            note.Value = string.Empty;
            sessionId = doCommand("insert");

            return sessionId;
        }


        public virtual long update_end_time()
        {
            end_time.Value = LiftTime.CurrentTime;

            return doCommand("update");
        }


        public virtual long end_session()
        {
            return doCommand("update");
        }
        

        

            
            

    }
}
