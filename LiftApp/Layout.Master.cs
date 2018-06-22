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
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected string customPath = string.Empty;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            Organization org = Organization.Current;
            if (org != null)
            {
                customPath = "/custom/";
                customPath += org.subdomain;
            }

            
        }
    }
}
