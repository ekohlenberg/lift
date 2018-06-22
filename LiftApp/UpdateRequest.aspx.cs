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
    public partial class UpdateRequest : System.Web.UI.Page
    {
        protected DataSet requestSet;
        protected RequestRenderer requestRenderer;

        protected DataSet encSet;
        protected EncouragementRenderer encRenderer;

        protected LiftDomain.Language L;

        protected void Page_Load(object sender, EventArgs e)
        {
            public_private_selected.ErrorMessage = Language.Current.REQUEST_PUBLIC_OR_PRIVATE;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            PageAuthorized.check(Request, Response);

            L = LiftDomain.Language.Current;
            LiftDomain.User U = LiftDomain.User.Current;

            submitBtn.Text = L.SHARED_SUBMIT;

            string idStr = Request.Params["id"];
            if (idStr != null)
            {
                if (idStr.Length > 0) request_id.Value = idStr;
            }

           
            int active = 1;

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
                    int t = Convert.ToInt32(encouragement_type.SelectedValue);
                    en.encouragement_type.Value = t;
                    en.from.Value = from.Text;
                    en.from_email.Value = from_email.Text;

                    if (request_is_public.Checked)
                    {
                        en.listed.Value = 1;
                    }
                    else
                    {
                        en.listed.Value = 0;
                    }

                    en.is_approved.Value = 1;
                    en.created_at.Value = LiftDomain.LiftTime.CurrentTime;
                    en.post_date.Value = LiftDomain.LiftTime.CurrentTime;
                    en.updated_at.Value = LiftDomain.LiftTime.CurrentTime;
                    en.user_id.Value = LiftDomain.User.Current.id;
                    en.request_id.Value = Convert.ToInt32(request_id.Value);

                    en.doCommand("save_encouragement");

                    LiftDomain.Request savedRequest = new Request();
                    savedRequest.id.Value = en.request_id.Value;
                    savedRequest = savedRequest.doSingleObjectQuery<Request>("getobject");
                    active = savedRequest.active.Value;

                    Response.Redirect("Requests.aspx?active=" + active.ToString());

                }
                else
                {
                    errMsg.Text = LiftDomain.Language.Current.REQUEST_UPDATE_NOT_SUCCESSFUL;

                }


            }
            else
            {
                initEncTypes(0);

                from.Text = LiftDomain.User.Current.FullName;
                from_email.Text = LiftDomain.User.Current.email;
            }

            note.Focus();
        }

        protected void initEncTypes(int initialValue)
        {
            encouragement_type.Items.Clear();

            encouragement_type.Items.Add(new ListItem(L.REQUEST_UPDATE, ((int) Encouragement.Update).ToString()  ));
            encouragement_type.Items.Add(new ListItem(L.REQUEST_COMMENT,((int) Encouragement.Comment).ToString()  ));
            encouragement_type.Items.Add(new ListItem(L.REQUEST_PRAISE, ((int) Encouragement.Praise).ToString()  ));

            foreach (ListItem li in encouragement_type.Items)
            {
                if (Convert.ToInt32(li.Value) == initialValue)
                    li.Selected = true;
                else
                    li.Selected = false;
            }
        }
        
        protected void public_private_selected_server_validate(object source , System.Web.UI.WebControls.ServerValidateEventArgs args ) 
        {
            // Requires that the user must actively choose public or private
            if (request_is_public.Checked || request_is_private.Checked)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

    }


}
