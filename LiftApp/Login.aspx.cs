using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftDomain;

namespace liftprayer
{
    public partial class Login : System.Web.UI.Page
    {
        protected string target = "Requests.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["target"]))
                {
                    dest.Value = Request["target"];
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(dest.Value))
                {
                    target = dest.Value;
                }
            }
                

            Login1.DestinationPageUrl = target;

            Login1.LoginButtonText = LiftDomain.Language.Current.LOGIN_BUTTON_TEXT;
            Login1.UserNameRequiredErrorMessage = LiftDomain.Language.Current.LOGIN_USER_ERROR;
            Login1.PasswordRequiredErrorMessage = LiftDomain.Language.Current.LOGIN_PASSWORD_ERROR;

            SetFocus(Login1.FindControl("UserName"));
                   

        }
    }
}
