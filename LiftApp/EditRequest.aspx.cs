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
    public partial class EditRequest : System.Web.UI.Page
    {
        int initialRequestType = 1;
        int initialGroupType = 1;
        protected EncouragementRenderer encouragementRenderer = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            public_private_selected.ErrorMessage = Language.Current.REQUEST_PUBLIC_OR_PRIVATE;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

			PageAuthorized.check(Request, Response);

            submitBtn.Text = LiftDomain.Language.Current.SHARED_SUBMIT;
            LiftDomain.User U = LiftDomain.User.Current;
            encouragementRenderer = new EncouragementRenderer();

            int active = 1;

            if (IsPostBack)
            {
                string sessionCaptcha = Session["captchaValue"].ToString();
                string pageCaptcha = txtCaptcha.Text.ToString().Trim().ToUpper();

                if (txtCaptcha.Text.ToString().Trim().ToUpper() == Session["captchaValue"].ToString().Trim().ToUpper())
                {
                    //Response.Write("CAPTCHA verification succeeded");


                    LiftDomain.Request prayerRequest = new LiftDomain.Request();

                    prayerRequest.title.Value = request_title.Text;
                    prayerRequest.description.Value = request_description.Text;
                    prayerRequest.from.Value = request_from.Text;
                    prayerRequest.requesttype_id.Value = Convert.ToInt32(request_type.SelectedItem.Value);
                    prayerRequest.group_relationship_type_id.Value = Convert.ToInt32(request_group_relationship.SelectedItem.Value);
                    prayerRequest.encouragement_address.Value = request_encouragement_address.Text;
                    prayerRequest.needs_encouragement.Value = (request_encouragement_address.Text.Length > 1 ? 1 : 0);

                    prayerRequest.encouragement_phone.Value = request_encouragement_phone.Text;
                    prayerRequest.from_email.Value = request_from_email.Text;

                    prayerRequest.listed.Value = (request_is_public.Checked ? 1 : 0);

                    prayerRequest.last_action.Value = LiftTime.CurrentTime;
                    prayerRequest.post_date.Value = LiftTime.CurrentTime;
                    prayerRequest.updated_at.Value = LiftTime.CurrentTime;

                    prayerRequest.is_approved.Value = Organization.Current.default_approval.Value;  

                    prayerRequest.user_id.Value = U.id;

                    if ((id.Value == "0") || (id.Value == ""))
                    {
                        prayerRequest.created_at.Value = LiftTime.CurrentTime;
                        prayerRequest.total_requests.Value = 0;
                        prayerRequest.total_comments.Value = 0;
                        prayerRequest.total_comments_needing_approval.Value = 0;
                        prayerRequest.total_private_comments.Value = 0;
                        prayerRequest.active.Value = 1;
                    }
                    else
                    {
                        prayerRequest.id.Value = int.Parse(id.Value);
                        LiftDomain.Request savedRequest = new Request();
                        savedRequest.id.Value = prayerRequest.id.Value;
                        savedRequest = savedRequest.doSingleObjectQuery<Request>("getobject");
                        active = savedRequest.active.Value;
                    }

                    long ident = prayerRequest.doCommand("save");

                    
                    
                    try
                    {
                        Email ackEmail = new Email();
                        ackEmail.subject = "Thank you for your prayer request";
                        ackEmail.Body = "Your prayer request has been received.  If you have indicated that your request can be made public, it will appear on the prayer wall as soon as it is approved.";
                        ackEmail.addTo(prayerRequest.from_email.Value);
                        ackEmail.from = Organization.Current.getFromEmail();
                        ackEmail.send();
                    }
                    catch   // ignore any errors
                    { }
                }
                /*
                else
                {
                    // else captcha failed...
                }
                 */

                Response.Redirect("Requests.aspx?active="+active.ToString());
            }
            else
            {
                LiftDomain.Request prayerRequest = new LiftDomain.Request();
                string idStr = Request["id"];
             
                 int reqId = 0;
                 try
                 {
                     if (idStr != null)
                     {
                         if (idStr.Length > 0)
                         {
                             reqId = int.Parse(idStr);
                         }
                     }
                 }
                 catch
                 {
                 }

                 if (reqId > 0)
                 {
                     try
                     {
                         prayerRequest["id"] = reqId;
                         id.Value = idStr;
                         prayerRequest = prayerRequest.doSingleObjectQuery<LiftDomain.Request>("getobject");
                         if (!U.canEditRequest(prayerRequest.user_id.Value))
                             Response.Redirect("Requests.aspx");

                         request_title.Text                 = prayerRequest.title;
                         request_description.Text           = prayerRequest.description;
                         request_from.Text                  = prayerRequest.from;
                         initialRequestType                 = prayerRequest.requesttype_id;
                         initialGroupType                   = prayerRequest.group_relationship_type_id;
                         request_encouragement_address.Text = prayerRequest.encouragement_address;
                         request_encouragement_phone.Text   = prayerRequest.encouragement_phone;
                         request_from_email.Text            = prayerRequest.from_email;

                         if (prayerRequest.listed == 1)
                         {
                             request_is_private.Checked = false;
                             request_is_public.Checked = true;
                         }
                         else
                         {
                             request_is_private.Checked = true;
                             request_is_public.Checked = false;
                         }

                         initUserInfo(prayerRequest.user_id);

                         LiftDomain.Encouragement enc       = new LiftDomain.Encouragement();
                         enc.request_id.Value               = reqId;
                         enc["listed_threshold"]            = (U.canApproveRequests ? 0 : 1);
                         enc["approval_threshold"]          = (U.canApproveRequests ? 0 : 1);

                         DataSet encDs                      = enc.doQuery("get_updates");
                         encouragementRenderer.DataSource   = encDs;
                         encouragementRenderer.Filename     = "_updateRequest2.htm";

                     }
                     catch (Exception x)
                     {
                         Logger.log(prayerRequest, x, "Error retrieving prayer request.");

                     }
                 }
                 else
                 {
                     initUserInfo(-1);
                 }
            }
             
            initRequestTypes(initialRequestType);
            initGroupRelTypes(1);
            
            //initTimeZoneList();
            request_title.Focus();
        }

        protected void initUserInfo( int userId)
        {
            LiftDomain.User u = new User();

            if (userId == -1)
            {
                u = LiftDomain.User.Current;
            }
            else if (userId > 0)
            {
                u.id.Value = userId;
                u = u.doSingleObjectQuery<LiftDomain.User>("select");
            }

            request_from.Text = u.first_name + " " + u.last_name.Value;
            request_from_email.Text = u.email;
        }

        protected void initGroupRelTypes(int initialValue)
        {
            request_group_relationship.Items.Clear();
            GroupRelationshipType groupRelType = new GroupRelationshipType();
            List<GroupRelationshipType> grtList = groupRelType.doList("select");

            foreach (GroupRelationshipType grt in grtList)
            {
                request_group_relationship.Items.Add(new ListItem(grt.name, grt.id.Value.ToString()));
            }

            foreach (ListItem li in request_group_relationship.Items)
            {
                object val = li.Value;
                try
                {
                    if (Convert.ToInt32(val) == initialValue)
                        li.Selected = true;
                    else
                        li.Selected = false;
                }
                catch (Exception x)
                {
                    string m = x.Message;
                }
            }
        }

        protected void initRequestTypes(int initialValue)
        {
            request_type.Items.Clear();
            RequestType requestType = new RequestType();
            List<RequestType> rtList = requestType.doList("select");
            foreach (RequestType rt in rtList)
            {
                request_type.Items.Add(new ListItem(rt.title, rt.id.Value.ToString()));
            }

            foreach (ListItem li in request_type.Items)
            {
                object val = li.Value;
                try
                {
                    if (Convert.ToInt32(val) == initialValue)
                        li.Selected = true;
                    else
                        li.Selected = false;
                }
                catch (Exception x)
                {
                    string m = x.Message;
                }
            }
        }

        /*
        protected void initTimeZoneList()
        {
            timezone_list.Items.Clear();
            LiftTime lt = new LiftTime();
            DataSet tzSet = lt.doQuery("get_timezones");
            DataTable tzTable = tzSet.Tables[0];

            timezone_list.DataValueField = "id";
            timezone_list.DataTextField = "name";
            timezone_list.DataSource = tzTable;
            timezone_list.DataBind();
        }
         */

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
