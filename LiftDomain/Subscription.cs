using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Web;
using LiftCommon;

namespace LiftDomain
{
	public class Subscription : BaseLiftDomain 
	{
		public IntProperty id = new IntProperty();
		public IntProperty request_id = new IntProperty();
		public IntProperty user_id = new IntProperty();

		public Subscription()
		{
			BaseTable = "subscriptions";
			AutoIdentity = true;
			PrimaryKey = "id";

			attach("id", id);
			attach("request_id", request_id);
			attach("user_id", user_id);
		}

        public void my_account_prayer_request_subscription_helper(DataRow r, Hashtable h)
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            if (appPath.Length > 1) appPath += "/";
            h.Add("app_path", appPath);

            if (Convert.ToInt32(r["request_total_comments"]) == 1)
            {
                h["total_comments_label"] = "1 " + LiftDomain.Language.Current.SUBSCRIPTION_COMMENT.Value;
            }
            else
            {
                h["total_comments_label"] = r["request_total_comments"].ToString() + " " + LiftDomain.Language.Current.SUBSCRIPTION_COMMENTS.Value;
            }

            //h["last_activity_label"] = Convert.ToDateTime(r["request_last_action"]).ToString("M/d/yyyy h:mm tt");
            h["last_activity_label"] = LiftDomain.LiftTime.aboutTime(Convert.ToDateTime(r["request_last_action"]));

            h["unsubscribe_subscription_confirmation"] = LiftDomain.Language.Current.MY_ACCOUNT_UNSUBSCRIBE_SUBSCRIPTION_CONFIRMATION.Value;
            h["unsubscribe_subscription_caption"] = LiftDomain.Language.Current.MY_ACCOUNT_UNSUBSCRIBE_SUBSCRIPTION_CAPTION.Value;
            h["subscription_last_activity"] = LiftDomain.Language.Current.MY_ACCOUNT_SUBSCRIPTION_LAST_ACTIVITY.Value;
        }

        

    }
}
