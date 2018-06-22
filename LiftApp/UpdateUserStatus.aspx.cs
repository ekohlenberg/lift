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
    public partial class UpdateUserStatus : System.Web.UI.Page
    {
        protected string userStatus;
        

        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);
                        Organization.setCurrent();

                        try
                        {
                            string idStr = Request["id"];
                            string userStateStr =   Request["value"];

                            int userState = Convert.ToInt32(userStateStr);

                            LiftDomain.User u = new LiftDomain.User();
                            u.id.Value = Convert.ToInt32(idStr);
                            u.state.Value = userState;

                            userStatus = LiftDomain.User.getUserStatusDescription(userState);

                            /* TODO - need a new method to change user status */
                            u.doCommand("update_status");
                        }
                        catch
                        {
                        }

        }
    }
}
