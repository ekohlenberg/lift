using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftDomain;

namespace liftprayer
{
    public partial class PrayerSessionGetPrevRequest : System.Web.UI.Page
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
            int requestId = Convert.ToInt32(Request["r"]);
            int viewed = Convert.ToInt32(Request["v"]);
            int idx = Convert.ToInt32(Request["i"]);
            int count = Convert.ToInt32(Request["c"]);

            prevReqOrdinal = idx;
            currentReqOrdinal = prevReqOrdinal-1;

            Prayersession ps = new Prayersession();
            ps["id"] = sessionId;

            ps.doCommand("update_end_time");

            pr["id"] = requestId;

            pr = pr.doSingleObjectQuery<LiftDomain.Request>("get_request");

            pr.get_summary_info(ref title, ref description, ref from, ref requestType, ref aboutTime);
            
            currentRequestLabel = Language.Current.PS_CURRENTLY_VIEWING_REQUEST;
            currentRequestLabel += " ";
            currentRequestLabel += idx.ToString();
            currentRequestLabel += " ";
            currentRequestLabel += Language.Current.PS_OF;
            currentRequestLabel += " ";
            currentRequestLabel += count.ToString();

            Response.ContentType = "text/plain";
        }
    }
}
