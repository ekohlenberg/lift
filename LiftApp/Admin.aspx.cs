using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftDomain;

namespace liftprayer
{
    public partial class Admin : System.Web.UI.Page
    {
        protected string customImagePath = string.Empty;
        protected string manage_organizations_action_page = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            Organization org = Organization.Current;
            if (org != null)
            {
                customImagePath = "/custom/";
                customImagePath += org.subdomain;
                customImagePath += "/images";
            }

						PageAuthorized.check(Request, Response);

            
            if (LiftDomain.User.Current.IsInRole("System Admin"))
            {
                manage_organizations_action_page = "OrganizationList.aspx";
            }
            else if (LiftDomain.User.Current.IsInRole("Organization Admin"))
            {
                manage_organizations_action_page = "EditOrganization.aspx?id=" + Convert.ToString(Organization.Current.id);
            }
            else
            {
                manage_organizations_action_page = "Admin.aspx";
            }
        }
    }
}
