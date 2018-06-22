using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftDomain;

namespace liftprayer
{
    public partial class PrayerSessionGetNextRequest : System.Web.UI.Page
    {
        protected LiftDomain.Request pr = new LiftDomain.Request();
        protected int prevReqOrdinal = 0;
        protected int currentReqOrdinal = 0;

        protected string title = string.Empty;
        protected string description = string.Empty;
        protected string from = string.Empty;
        protected string requestType = string.Empty;
        protected string aboutTime = string.Empty;
        protected string currentRequestLabel = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int sessionId = Convert.ToInt32(Request["s"]);
            int n = 0;
            int count = 0;

            string r = Request["r"];
            if (r == "undefined") r = null;

            if (!string.IsNullOrEmpty(r))
            {
                int requestId = Convert.ToInt32(r);
                int viewed = Convert.ToInt32(Request["v"]);
                int idx = Convert.ToInt32(Request["i"]);
                count = Convert.ToInt32(Request["c"]);

                n = idx + 1;

                prevReqOrdinal = idx;
                currentReqOrdinal = prevReqOrdinal + 1;

                Prayersession ps = new Prayersession();
                ps["id"] = sessionId;

                if (viewed == 0)
                {
                    ps.doCommand("incr_total_requests");
                }

                ps.doCommand("update_end_time");

                pr["id"] = requestId;

                pr = pr.doSingleObjectQuery<LiftDomain.Request>("get_request");

                pr.doCommand("update_total");

                pr.get_summary_info(ref title, ref description, ref from, ref requestType, ref aboutTime);
            }
            else
            {
                description = LiftDomain.Language.Current.REQUEST_PRAYER_SESSION_COMPLETE;
                n = 60;
                count = 60;
            }
            currentRequestLabel = Language.Current.PS_CURRENTLY_VIEWING_REQUEST;
            currentRequestLabel += " ";
            currentRequestLabel += n.ToString();
            currentRequestLabel += " ";
            currentRequestLabel += Language.Current.PS_OF;
            currentRequestLabel += " ";
            currentRequestLabel += count.ToString();

            Response.ContentType = "text/plain";

        }

        
    }
}
