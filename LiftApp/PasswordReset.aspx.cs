using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class PasswordReset : System.Web.UI.Page
    {
        protected string isOK = string.Empty;
        protected string email = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            isOK = LiftDomain.Language.Current.LOGIN_PASSWORD_NOT_RESET;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            try
            {
                if (!string.IsNullOrEmpty(Request["ok"]))
                {
                    if (Request["ok"] == "1")
                    {
                        isOK = LiftDomain.Language.Current.FORGOT_PASSWORD_INSTRUCTIONS;
                        isOK += ":";
                        email = Request["e"];
                    }
                }

            }
            catch (Exception x)
            {
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN PasswordReset.aspx.cs::Page_Load(): " + m);
                Logger.log("PasswordReset.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN PasswordReset.aspx.cs::Page_Load(): " + m);
                //Response.Write(m);
            }
            finally
            {
            }
        }
    }
}
