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
    public partial class DeleteUser : System.Web.UI.Page
    {
        public string idStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

						PageAuthorized.check(Request, Response);

            idStr = Request["id"];
            if (!String.IsNullOrEmpty(idStr))
            {
                try
                {
                    int id = int.Parse(idStr);
                    LiftDomain.User thisUser = new LiftDomain.User();
                    thisUser.id.Value = id;
                    thisUser.doCommand("delete");

                    LiftDomain.RolesUser thisRolesUser = new LiftDomain.RolesUser();
                    thisRolesUser.user_id.Value = thisUser.id.Value;
                    thisRolesUser.doQuery("delete_roles_users_by_user_id");

                    Response.Redirect(Request["redirect_to_page"]);

                    //Response.ContentType = "text/javascript";
                }
                catch(Exception x)
                {
                    Logger.log(idStr, x, "Error deleting user");
                }
            }
        }
    }
}
