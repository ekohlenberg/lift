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
    public partial class EditOrganization : System.Web.UI.Page
    {
        protected string edit_organization_fieldset_legend = string.Empty;
        protected string delete_organization_id = string.Empty;
        protected string redirect_after_delete_to_page = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            PageAuthorized.check(Request, Response);

            string initialTimeZone = "Central Standard Time";
            int initialLanguageId = 1;  //-- 1 = English
            int initialStatusId = 0;  //-- 0 = Unapproved; 1 = Approved
            string saltValue = string.Empty;
            bool sendOrgIsApprovedEmail = false;

            try
            {
                //-------------------------------------------------------------------------
                //-- do the language setting for the SUBMIT button here
                //-- (unable to place <%=LiftDomain.Language.Current.SHARED_SUBMIT %> in asp:Button Text field)
                //-------------------------------------------------------------------------
                this.submitBtn.Text = LiftDomain.Language.Current.SHARED_SUBMIT.Value;

                //-------------------------------------------------------------------------
                //-- do other language settings
                //-------------------------------------------------------------------------
                edit_organization_fieldset_legend = LiftDomain.Language.Current.ORGANIZATION_EDIT_ORGANIZATION.Value;

                LiftDomain.Organization thisOrganization = new LiftDomain.Organization();

                if (IsPostBack)
                {
                    //-------------------------------------------------------------------------
                    //-- get the object ID from the hidden id field on the page;
                    //-- if there is a object ID value, then we are editing an EXISTING object
                    //-------------------------------------------------------------------------
                    if (!String.IsNullOrEmpty(id.Value) && (id.Value != "0"))
                    {
                        thisOrganization.id.Value = int.Parse(id.Value);
                    }
                    else
                    {
                        //-------------------------------------------------------------------------
                        //-- if the object ID is blank or zero (0), then set some NEW object values (NOT id)
                        //-------------------------------------------------------------------------
                        thisOrganization.created_at.Value = LiftTime.CurrentTime;
                    }

                    //-------------------------------------------------------------------------
                    //-- transfer screen values to the object
                    //-------------------------------------------------------------------------
                    //TODO: ???what if data field validation fails??? // TO BE DONE IN JAVASCRIPT
                    //TODO: ???what if org title already exists??? // TO BE DONE IN JAVASCRIPT
                    //TODO: ???what if org subdomain already exists??? // TO BE DONE IN JAVASCRIPT
                    thisOrganization.title.Value = organization_title.Text;
                    thisOrganization.user_id.Value = 0; // LiftDomain.User.Current.id.Value;
                    thisOrganization.address.Value = organization_address.Text;
                    thisOrganization.city.Value = organization_city.Text;
                    thisOrganization.state_province.Value = organization_state.Text;
                    thisOrganization.postal_code.Value = organization_postal_code.Text;
                    thisOrganization.phone.Value = organization_phone.Text;
                    thisOrganization.subdomain.Value = organization_subdomain.Text;
                    thisOrganization.time_zone.Value = timezone_list.SelectedItem.Value;
                    thisOrganization.language_id.Value = Convert.ToInt32(language_list.SelectedItem.Value);
                    thisOrganization.footer.Value = organization_footer.Text;

                    thisOrganization.default_approval.Value = (default_approved.Checked ? 1 : 0);

                    thisOrganization.default_signup_mode.Value = (new_users_require_approval.Checked ? 1 : 0);


                    //-------------------------------------------------------------------------
                    //-- if the status changes from "Unapproved" to "Approved," 
                    //-- then we want to notify the organization's webmaster
                    //-------------------------------------------------------------------------
                    if (thisOrganization.id.Value != 0)
                    {
                        LiftDomain.Organization tempOrganization = new LiftDomain.Organization();
                        tempOrganization.id.Value = thisOrganization.id.Value;
                        tempOrganization = tempOrganization.doSingleObjectQuery<LiftDomain.Organization>("select");

                        if ((tempOrganization.status == 0) && (Convert.ToInt32(language_list.SelectedItem.Value) == 1))
                        {
                            sendOrgIsApprovedEmail = true;
                        }
                    }
                    thisOrganization.status.Value = Convert.ToInt32(organization_status_list.SelectedItem.Value);

                    //-------------------------------------------------------------------------
                    //-- persist the object data to the database
                    //-------------------------------------------------------------------------
                    thisOrganization.id.Value = Convert.ToInt32(thisOrganization.doCommand("save"));

                    //id.Value = thisOrganization.id.Value.ToString();

                    //-------------------------------------------------------------------------
                    //-- send the approval email to the organization's webmaster
                    //-------------------------------------------------------------------------
                    if (sendOrgIsApprovedEmail)
                    {
                        LiftCommon.Email emailHelper = new LiftCommon.Email();
                        LiftDomain.OrgEmail thisOrgEmail = new LiftDomain.OrgEmail();
                        thisOrgEmail.organization_id.Value = thisOrganization.id.Value;
                        thisOrgEmail = thisOrgEmail.doSingleObjectQuery<LiftDomain.OrgEmail>("select");

                        //email.replyTo = thisOrgEmail.emailReplyTo;  // not supported yet

                        emailHelper.from = "admin@liftprayer.cc";

                        //TODO: ??? THIS NEEDS TO BE A VALID E-MAIL ADDRESS
                        if (LiftCommon.Email.IsValidEmailAddress(thisOrgEmail.webmaster_email_to.Value))
                        {
                            emailHelper.addTo(thisOrgEmail.webmaster_email_to.Value);
                        }
                        else
                        {
                            //TODO: ??? HOW DO WE NOTIFY THE USER
                            Logger.log(Logger.Level.ERROR, this, "E-mail address '" + thisOrgEmail.webmaster_email_to.Value + "' is not in a correct format [SignupOrganization.aspx].");
                            throw new ApplicationException("E-mail address '" + thisOrgEmail.webmaster_email_to.Value + "' is not in a correct format [SignupOrganization.aspx].");
                        }

                        emailHelper.subject = LiftDomain.Language.Current.SIGNUP_ORGANIZATION_APPROVAL_RESPONSE_SUBJECT.Value;
                        emailHelper.Body = LiftDomain.Language.Current.SIGNUP_ORGANIZATION_APPROVAL_RESPONSE_MESSAGE.Value + "  " + thisOrganization.title.Value;

                        //email.MIME = MIME.Text | MIME.HTML;  // just supposing that it supports multiple formats. May not be necessary

                        emailHelper.send();
                    }

                    if (LiftDomain.User.Current.isSysAdmin)
                    {
                        //-------------------------------------------------------------------------
                        //-- return to the Organization List page
                        //-------------------------------------------------------------------------
                        if (Session["last_org_list_search"] != null)
                        {
                            Response.Redirect("OrganizationList.aspx?" + Session["last_org_list_search"]);
                        }
                        else
                        {
                            Response.Redirect("OrganizationList.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("Admin.aspx");
                    }
                }
                else
                {
                    //-------------------------------------------------------------------------
                    //-- first time on this page, so get the organization ID from the ASP Request cache
                    //-------------------------------------------------------------------------
                    string idStr = Request["id"];

                    if (String.IsNullOrEmpty(idStr))
                    {
                        id.Value = "0";
                    }
                    else
                    {
                        id.Value = idStr;
                    }

                    thisOrganization.id.Value = Convert.ToInt32(id.Value);

                    //-------------------------------------------------------------------------
                    //-- if this is a NEW organization...
                    //-------------------------------------------------------------------------
                    if (id.Value == "0")
                    {
                        //-------------------------------------------------------------------------
                        //-- set default values
                        //-------------------------------------------------------------------------
                        initialTimeZone = LiftDomain.Organization.Current.time_zone.Value;
                        initialLanguageId = LiftDomain.Organization.Current.language_id.Value;
                        initialStatusId = 0; //-- 0 = Unapproved; 1 = Approved

                        title_label.Visible = false;
                        edit_organization_fieldset_legend = LiftDomain.Language.Current.ORGANIZATION_CREATE_A_NEW_ORGANIZATION.Value;

                        delete_organization_id = string.Empty;
                        redirect_after_delete_to_page = string.Empty;
                    }

                    //-------------------------------------------------------------------------
                    //-- else, if this is an EXISTING organization...
                    //-------------------------------------------------------------------------
                    else
                    {
                        //-------------------------------------------------------------------------
                        //-- query database for data for this organization
                        //-------------------------------------------------------------------------
                        thisOrganization = thisOrganization.doSingleObjectQuery<LiftDomain.Organization>("select");

                        initialTimeZone = thisOrganization.time_zone;
                        initialLanguageId = thisOrganization.language_id;
                        initialStatusId = thisOrganization.status;

                        title_label.Text = LiftDomain.Language.Current.ORGANIZATION_EDITING_ORGANIZATION.Value + " " + thisOrganization.title;
                        edit_organization_fieldset_legend = LiftDomain.Language.Current.ORGANIZATION_EDIT_ORGANIZATION.Value;

                        delete_organization_id = id.Value;

                        if (Session["last_org_list_search"] != null)
                        {
                            redirect_after_delete_to_page = "OrganizationList.aspx?" + Session["last_org_list_search"];
                        }
                        else
                        {
                            redirect_after_delete_to_page = "OrganizationList.aspx";
                        }
                    }

                    //-------------------------------------------------------------------------
                    //-- populate the screen controls
                    //-------------------------------------------------------------------------
                    organization_title.Text = thisOrganization.title;
                    organization_address.Text = thisOrganization.address;
                    organization_city.Text = thisOrganization.city;
                    organization_state.Text = thisOrganization.state_province;
                    organization_postal_code.Text = thisOrganization.postal_code;
                    organization_phone.Text = thisOrganization.phone;
                    organization_subdomain.Text = thisOrganization.subdomain;
                    organization_footer.Text = thisOrganization.footer;
                    if (thisOrganization.default_approval.Value == 1)
                    {
                        this.default_approved.Checked = true;
                        this.default_not_approved.Checked = false;
                    }
                    else
                    {
                        this.default_approved.Checked = false;
                        this.default_not_approved.Checked = true;
                    }

                    if (thisOrganization.default_signup_mode.Value == (int)UserSignupMode.user_create_account)
                    {
                        this.new_users_create_accounts.Checked = true;
                        this.new_users_require_approval.Checked = false;
                    }
                    else
                    {
                        this.new_users_create_accounts.Checked = false;
                        this.new_users_require_approval.Checked = true;
                    }

                    initTimeZoneList(initialTimeZone);
                    initLanguageList(initialLanguageId);
                    initOrganizationStatusList(initialStatusId);
                }
            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganization.aspx.cs::Page_Load(): " + m);
                Logger.log("EditOrganization.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganization.aspx.cs::Page_Load(): " + m);
            }
            finally
            {
            }
        }

        protected void initTimeZoneList(string selectedValue)
        {
            timezone_list.Items.Clear();
            LiftTime thisLiftTime = new LiftTime();
            DataSet timeZoneDataSet = thisLiftTime.doQuery("get_timezones");
            DataTable timeZoneDataTable = timeZoneDataSet.Tables[0];

            timezone_list.DataValueField = "id";
            timezone_list.DataTextField = "name";
            timezone_list.DataSource = timeZoneDataTable;
            timezone_list.DataBind();

            foreach (ListItem thisListItem in timezone_list.Items)
            {
                thisListItem.Selected = (thisListItem.Value == selectedValue);
            }
        }

        protected void initLanguageList(int selectedValue)
        {
            language_list.Items.Clear();
            LiftDomain.Language thisLanguage = new LiftDomain.Language();
            DataSet languageDataSet = thisLanguage.doQuery("select");
            DataTable languageDataTable = languageDataSet.Tables[0];

            language_list.DataValueField = "id";
            language_list.DataTextField = "title";
            language_list.DataSource = languageDataTable;
            language_list.DataBind();

            foreach (ListItem thisListItem in language_list.Items)
            {
                thisListItem.Selected = (thisListItem.Value == selectedValue.ToString());
            }
        }

        protected void initOrganizationStatusList(int selectedValue)
        {
            organization_status_list.Items.Clear();
            organization_status_list.Items.Insert(organization_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.ORGANIZATION_STATUS_UNAPPROVED.Value, "0"));
            organization_status_list.Items.Insert(organization_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.ORGANIZATION_STATUS_APPROVED.Value, "1"));

            foreach (ListItem thisListItem in organization_status_list.Items)
            {
                thisListItem.Selected = (thisListItem.Value == selectedValue.ToString());
            }
        }

    }
}
