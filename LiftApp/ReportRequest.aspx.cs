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
    public partial class ReportRequest : System.Web.UI.Page
    {
        protected DataSet requestSet;
        protected RequestRenderer requestRenderer;

        protected DataSet encSet;
        protected EncouragementRenderer encRenderer;

        protected LiftDomain.Language L;

        protected void Page_Load(object sender, EventArgs e)
        {
          
          
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            string foo = LiftDomain.Language.Current.REQUEST_FIELDS_OPTIONAL;



            PageAuthorized.check(Request, Response);

            L = LiftDomain.Language.Current;
            LiftDomain.User U = LiftDomain.User.Current;

            submitBtn.Text = L.SHARED_SUBMIT;

            string idStr = Request.Params["id"];
            if (idStr != null)
            {
                if (idStr.Length > 0) request_id.Value = idStr;
            }

            LiftDomain.Request prayerRequest = new LiftDomain.Request();
            prayerRequest.id.Value = Convert.ToInt32(request_id.Value);

            // TODO - turn off links to updates and subscriptions here
            // prayerRequest["mode"] = "update_request";
            requestSet = prayerRequest.doQuery("get_request");
            requestRenderer = new RequestRenderer(requestSet);
            requestRenderer.ShowLinks = false;

            LiftDomain.Encouragement enc = new LiftDomain.Encouragement();
            enc.request_id.Value = Convert.ToInt32(request_id.Value);
            enc["listed_threshold"] = (U.canSeePrivateRequests ? 0 : 1);
            enc["approval_threshold"] = (U.canApproveRequests ? 0 : 1);
            encSet = enc.doQuery("get_updates");

            encRenderer = new EncouragementRenderer(encSet);

            if (IsPostBack)
            {
                if (txtCaptcha.Text.ToString().Trim().ToUpper() == Session["captchaValue"].ToString().Trim().ToUpper())
                {
                    //Response.Write("CAPTCHA verification succeeded");

                    LiftDomain.Encouragement en = new LiftDomain.Encouragement();
                    en.note.Value = note.Text;
                    
                    en.encouragement_type.Value = (int)Encouragement.Report;
                    en.from.Value = from.Text;
                    en.from_email.Value = from_email.Text;

                 
                    en.listed.Value = 0;  // always make reports private
                    

                    en.is_approved.Value = 0;
                    en.created_at.Value = LiftDomain.LiftTime.CurrentTime;
                    en.post_date.Value = LiftDomain.LiftTime.CurrentTime;
                    en.updated_at.Value = LiftDomain.LiftTime.CurrentTime;
                    en.user_id.Value = LiftDomain.User.Current.id;
                    en.request_id.Value = Convert.ToInt32(request_id.Value);

                    en.doCommand("save_encouragement");

                    LiftDomain.Request pr = new LiftDomain.Request();
                    pr.id.Value = Convert.ToInt32(request_id.Value);
                    pr.is_approved.Value = 0;
                    pr.last_action.Value = LiftDomain.LiftTime.CurrentTime;
                    pr.updated_at.Value = LiftDomain.LiftTime.CurrentTime;

                    pr.doCommand("approve");

                    LiftDomain.Encouragement allEnc = new LiftDomain.Encouragement();
                    allEnc.request_id.Value = Convert.ToInt32(request_id.Value) ;
                    allEnc.is_approved.Value = 0;
                    allEnc.approved_at.Value = LiftDomain.LiftTime.CurrentTime;
                    allEnc.doCommand("approve_all");

                    Response.Redirect("Requests.aspx");

                }
                else
                {
                    errMsg.Text = LiftDomain.Language.Current.REQUEST_UPDATE_NOT_SUCCESSFUL;

                }


            }
            else
            {
                from.Text = "";
                from_email.Text = "";
            }

            this.note.Focus();
        }

  
        
       
        
    }

    
}
