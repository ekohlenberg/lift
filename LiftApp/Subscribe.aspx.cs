using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class Subscribe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }
            Subscription s      = new Subscription();
            string idStr        = Request["id"];
            s.request_id.Value  = Convert.ToInt32(idStr);
            s.user_id.Value     = LiftDomain.User.Current.id.Value;

            if (!s.exists())
            {
                s.doCommand("save");
            }

        }
    }
}
