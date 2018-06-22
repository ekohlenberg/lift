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
    public partial class InPlaceEditWallTitleUpdate : System.Web.UI.Page
    {
        protected string valueStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);

            string idStr = Request["id"];

            valueStr = Request["value"];

            LiftDomain.Wall w = new LiftDomain.Wall();

            w.id.Value = Convert.ToInt32(idStr);
            w.title.Value = valueStr;

            w.doCommand("update");
        }
    }
}
