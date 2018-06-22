using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class DeleteSubscription : System.Web.UI.Page
    {
        protected string idStr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }


						PageAuthorized.check(Request, Response);

            Subscription s = new Subscription();
            idStr = Request["id"];
            s.request_id.Value = Convert.ToInt32(idStr);
            s.user_id.Value = LiftDomain.User.Current.id.Value;
         
            s.doCommand("unsubscribe");

            Response.ContentType = "text/javascript";
        }
    }
}
