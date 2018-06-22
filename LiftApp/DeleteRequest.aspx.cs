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
    public partial class DeleteRequest : System.Web.UI.Page
    {
        public string idStr = string.Empty;
        public string actionStr = string.Empty;
        public string action = "delete";

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
                    LiftDomain.Request pr = new LiftDomain.Request();

                    actionStr = Request["action"];
                    if (!String.IsNullOrEmpty(actionStr))
                    {
                        action = actionStr;
                    }

                    pr["id"] = id;
                    pr.doCommand(action);

                    Response.ContentType = "text/javascript";
                }
                catch(Exception x)
                {
                    Logger.log(idStr, x, "Error deleting request");
                }
            }
        }
    }
}
