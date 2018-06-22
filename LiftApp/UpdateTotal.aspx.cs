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
    public partial class UpdateTotal : System.Web.UI.Page
    {
        public string idStr = string.Empty;
        public string newTotalStr = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }
            //TODO: ???need to impl session mgr feature to track which requests have been prayed for
            idStr = Request["id"];
            if (idStr != null)
            {
                try
                {
                    int id = int.Parse(idStr);
                    LiftDomain.Request pr = new LiftDomain.Request();
                    pr["id"] = id;
                    long newTotal = pr.doCommand("update_total");
                    newTotalStr = newTotal.ToString();
                    Response.ContentType = "text/javascript";
                }
                catch(Exception x)
                {
                    Logger.log(idStr, x, "Error updating total_requests");
                }
            }
        }
    }
}
