using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using LiftDomain;

namespace liftprayer
{
    public partial class PrayerSessionUtil : System.Web.UI.Page
    {
        protected long currentSessionId;
        protected string currentRequestLabel;
        protected string currentRequestOf;

        protected ArrayList requests = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            User U = LiftDomain.User.Current;

            Response.ContentType = "text/javascript";

            currentSessionId = Convert.ToInt64(Request["sessionId"]);

            LiftDomain.Request prayerRequest = new LiftDomain.Request();

            prayerRequest["listed_threshold"] = (U.canSeePrivateRequests ? 0 : 1);

            prayerRequest["approval_threshold"] = 1;
            prayerRequest["active"] = 1;

            int tf = -90; // last 90 days
            int rt = 0; // all types

            prayerRequest["timeframe"] = tf;
            prayerRequest["requesttype"] = rt;
            prayerRequest["search"] = "";

            DataSet requestSet = prayerRequest.doQuery("get_requests");

            if (requestSet != null)
            {
                if (requestSet.Tables.Count > 0)
                {
                    for (int i = 0; i < Math.Min(requestSet.Tables[0].Rows.Count, 60); i++)
                    {
                        DataRow r = requestSet.Tables[0].Rows[i];
                        requests.Add(Convert.ToInt32(r["id"]));
                    }
                }
            }

            currentRequestLabel = Language.Current.PS_CURRENTLY_VIEWING_REQUEST;
            currentRequestOf    = Language.Current.PS_OF;

        }
    }
}
