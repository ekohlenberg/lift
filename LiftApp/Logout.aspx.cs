using System;
using System.Web.UI;
using System.Web.Security;

namespace liftprayer
{
    
    public partial class Logout : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.DefaultUrl);
        }

    }

}
