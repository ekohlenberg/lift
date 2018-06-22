using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using LiftCommon;
using LiftDomain;


namespace liftprayer
{
    public partial class WallManageAutoComplete : System.Web.UI.Page
    {
        protected WallManageAutoCompleteRenderer userRenderer;
      

        protected void Page_Load(object sender, EventArgs e)
        {
			PageAuthorized.check(Request, Response);

            Appt a = new Appt();

            string q = Request["q"];
            
            a["q"] = q;
            a["tzoffset"] = LiftDomain.LiftTime.UserTzOffset;
            a["organization_id" ] = Organization.Current.id.Value;
            DataSet userSet = a.doQuery("get_users_like");

            Response.ContentType = "text/plain";

            userRenderer = new WallManageAutoCompleteRenderer( userSet );

            string cell = Request["cell"];

            string[] parts = cell.Split(new char[] { '_' });
            userRenderer.wallId = parts[0];
            userRenderer.dow = parts[1];
            userRenderer.tod = parts[2];
            
            
        }

    }
}
