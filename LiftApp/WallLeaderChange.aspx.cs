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
    public partial class WallLeaderChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);

            LiftDomain.Wall w = new LiftDomain.Wall();

            w.user_id.Value = Convert.ToInt32(Request["user_id"]);
            w.id.Value = Convert.ToInt32(Request["wall_id"]);

            w.doCommand("update");
        }
    }
}
