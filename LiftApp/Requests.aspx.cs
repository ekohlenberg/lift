using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class Requests : System.Web.UI.Page
    {
        protected DataSet requestSet;
        protected RequestRenderer requestRenderer;
        protected int initialTimeframe = -7;
        protected int initialRequestType = 0;
        protected string customPath = string.Empty;
        protected int active = 1;
        protected string duringTheLast = string.Empty;
        protected string viewingRequestsFor = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            Organization org = Organization.Current;
            if (org != null)
            {
                customPath = "/custom/";
                customPath += org.subdomain;
                if (org.subdomain == "upward")
                {
                    initialTimeframe = -100;
                    duringTheLast = "&nbsp;";
                    requesttype.Visible = false;
                    timeframe.Visible = false;
                    searchBtn.Visible = false;
                    liveSearchBox.Visible = false;
                    viewingRequestsFor = string.Empty;
                }
                else
                {
                    initialTimeframe = -7;
                    duringTheLast = LiftDomain.Language.Current.REQUESTS_DURING_THE_LAST;
                    viewingRequestsFor = LiftDomain.Language.Current.REQUESTS_VISUALIZE_PRAYER_REQUESTS;
                }

            }

            PageAuthorized.check(Request, Response);

            User U = LiftDomain.User.Current;

            searchBtn.Text = LiftDomain.Language.Current.SHARED_SEARCH;

            LiftDomain.Request prayerRequest = new LiftDomain.Request();

            prayerRequest["listed_threshold"] = (U.canSeePrivateRequests ? 0 : 1);

            prayerRequest["approval_threshold"] = ( U.canApproveRequests ? 0 : 1);

            string strActive = Request["active"];

            if (!string.IsNullOrEmpty(strActive))
            {
                try
                {
                    active = int.Parse(strActive);
                }
                catch
                {
                    active = 1;
                }
            }

            active = (U.canApproveRequests ? active : 1);

            prayerRequest["active"] = active;

            if (IsPostBack)
            {
                int tf = Convert.ToInt32(timeframe.SelectedItem.Value);
                int rt = Convert.ToInt32(requesttype.SelectedItem.Value);
                string search = liveSearchBox.Text;
                prayerRequest["timeframe"] = tf;
                prayerRequest["requesttype"] = rt;
                prayerRequest["search"] = search;
                initTimeframe(tf);
                initRequestTypes(rt);
            }
            else
            {
                initRequestTypes(initialRequestType);
                initTimeframe(initialTimeframe);
                prayerRequest["search"] = "";
                prayerRequest["timeframe"] = initialTimeframe;
                prayerRequest["requesttype"] = initialRequestType;
            }

            requestSet                  = prayerRequest.doQuery("get_requests");
            requestRenderer             = new RequestRenderer(requestSet);
            requestRenderer.ShowUpdates = true;
            requestRenderer.ShowActive = (active == 1);
           
        }

        protected void initRequestTypes(int initialValue)
        {
            requesttype.Items.Clear();
            RequestType requestType = new RequestType();
            List<RequestType> rtList = requestType.doList("select_sorted");

            foreach (RequestType rt in rtList)
            {
                requesttype.Items.Add(new ListItem(rt.title, rt.id.Value.ToString()));
            }

            requesttype.Items.Insert(0, new ListItem(Language.Current.REQUESTTYPES_ALL, "0"));

            foreach( ListItem li in requesttype.Items)
            {
                object val = li.Value;
                try
                {
                    if (Convert.ToInt32(val) == initialValue)
                        li.Selected = true;
                    else
                        li.Selected = false;
                }
                catch (Exception x)
                {
                    string m = x.Message;
                }
            }
        }

        protected void initTimeframe(int initialValue)
        {
            Organization org = Organization.Current;

            timeframe.Items.Clear();
            
            timeframe.Items.Add(new ListItem(Language.Current.TIMEFRAME_DAY, "-1"));
            timeframe.Items.Add(new ListItem(Language.Current.TIMEFRAME_WEEK, "-7"));
            timeframe.Items.Add(new ListItem(Language.Current.TIMEFRAME_MONTH, "-31"));

            if (org != null)
            {
                if (org.subdomain == "upward")
                {
                    timeframe.Items.Add(new ListItem(Language.Current.TIMEFRAME_100DAYS, "-100"));
                }
            }

            timeframe.Items.Add(new ListItem(Language.Current.TIMEFRAME_YEAR, "-365"));

            foreach (ListItem li in timeframe.Items)
            {
                if (Convert.ToInt32(li.Value) == initialValue)
                    li.Selected = true;
                else
                    li.Selected = false;
            }
        }

    

    }
}
