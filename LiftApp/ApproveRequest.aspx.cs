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
    public partial class ApproveRequest : System.Web.UI.Page
    {
        public string idStr = string.Empty;
        public string isApprovedStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            isApprovedStr = Request["ap"];

            if (isApprovedStr == "1")
            {
                PageAuthorized.check(Request, Response);
            }

            idStr = Request["id"];
            

            if (idStr != null)
            {
                try
                {
                    int id = int.Parse(idStr);
                    LiftDomain.Request pr = new LiftDomain.Request();
                    pr.id.Value = id;
                    pr.is_approved.Value = (isApprovedStr == "1" ? 1 : 0);
                    pr.last_action.Value = LiftDomain.LiftTime.CurrentTime;
                    pr.updated_at.Value = LiftDomain.LiftTime.CurrentTime;

                    pr.doCommand("approve");

                    LiftDomain.Encouragement enc = new LiftDomain.Encouragement();
                    enc.request_id.Value= id;
                    enc.is_approved.Value = (isApprovedStr == "1" ? 1 : 0);
                    enc.approved_at.Value = LiftDomain.LiftTime.CurrentTime;
                    enc.doCommand("approve_all");

                    pr.notify(id);

                    Response.ContentType = "text/javascript";

                }
                catch (Exception x)
                {
                    Logger.log(idStr, x, "Error approving request");
                }
            }
        }
    }
}
