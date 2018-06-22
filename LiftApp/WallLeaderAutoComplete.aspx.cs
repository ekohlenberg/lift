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
    
    public partial class WallLeaderAutoComplete : System.Web.UI.Page
    {
        protected PartialRenderer userRenderer;

        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);

            User u = new User();

            string q = Request["q"];
            u["q"] = q;

            DataSet userSet =   u.doQuery("get_users_like");

            userRenderer = new PartialRenderer(HttpContext.Current, userSet, "_WallLeaderAutoComplete.htm", null);
        }
    }
}
