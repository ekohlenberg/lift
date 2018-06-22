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
    public partial class DeleteOrganization : System.Web.UI.Page
    {
        public string idStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);

            idStr = Request["id"];
            if (!String.IsNullOrEmpty(idStr))
            {
                try
                {
                    int id = int.Parse(idStr);
                    LiftDomain.Organization thisOrganization = new LiftDomain.Organization();
                    thisOrganization.id.Value = id;
                    thisOrganization.doCommand("delete");

                    Response.Redirect(Request["redirect_to_page"]);

                    //Response.ContentType = "text/javascript";
                }
                catch(Exception x)
                {
                    Logger.log(idStr, x, "Error deleting organization");
                }
            }
        }
    }
}
