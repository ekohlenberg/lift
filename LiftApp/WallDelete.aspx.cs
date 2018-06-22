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
    public partial class WallDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);

            LiftDomain.Wall w = new LiftDomain.Wall();
            w.id.Value = Convert.ToInt32(Request["id"]);
            w.doCommand("remove");

            Response.Redirect("WallList.aspx");
        }
    }
}
