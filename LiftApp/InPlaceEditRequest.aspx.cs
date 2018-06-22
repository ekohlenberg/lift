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
    public partial class InPlaceEditRequest : System.Web.UI.Page
    {
        protected string valueStr;
        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);

            string idStr = Request["id"];

            valueStr = Request["value"];

            LiftDomain.Request pr = new LiftDomain.Request();

            pr.id.Value = Convert.ToInt32(idStr);
            pr.description.Value = valueStr;

            pr.doCommand("update");

        }
    }
}
