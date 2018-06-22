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
    public partial class EditUser : System.Web.UI.Page
    {
        protected string edit_user_fieldset_legend = string.Empty;
        protected string delete_user_id = string.Empty;
        protected string redirect_after_delete_to_page = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            PasswordRequired.Enabled = false;
            EmailValidator.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;
            PasswordValidator.ErrorMessage = LiftDomain.Language.Current.SHARED_PASSWORDS_DO_NOT_MATCH;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

						PageAuthorized.check(Request, Response);

            int initialUserStatus = 1;  //-- 1 = unconfirmed
            string initialTimeZone = "Central Standard Time";
            int initialLanguageId = 1;  //-- 1 = English
            int initialOrgId = 0;
            string saltValue = string.Empty;
            LiftDomain.RolesUser thisRolesUser;

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
                edit_user_fieldset_legend = LiftDomain.Language.Current.USER_EDIT_USER.Value;
                //this.user_roles_2.Text = LiftDomain.Language.Current.ROLES_ADMIN.Value;
                this.user_roles_7.Text = LiftDomain.Language.Current.ROLES_MODERATOR.Value;
                this.user_roles_8.Text = LiftDomain.Language.Current.ROLES_WALL_LEADER.Value;
                this.user_roles_10.Text = LiftDomain.Language.Current.ROLES_WATCHMAN.Value;
                this.user_roles_13.Text = LiftDomain.Language.Current.ROLES_SYSTEM_ADMIN.Value;
                this.user_roles_14.Text = LiftDomain.Language.Current.ROLES_ORGANIZATION_ADMIN.Value;
                //this.user_roles_11.Text = LiftDomain.Language.Current.ROLES_TESTADMIN.Value;
                //this.user_roles_12.Text = LiftDomain.Language.Current.ROLES_ADMINTEST.Value;

                LiftDomain.User thisUser = new LiftDomain.User();

                if (IsPostBack)
                {
                    //-------------------------------------------------------------------------
                    //-- get the object ID from the hidden id field on the page;
                    //-- if there is a object ID value, then we are editing an EXISTING object
                    //-------------------------------------------------------------------------
                    if (!String.IsNullOrEmpty(id.Value) && (id.Value != "0"))
                    {
                        thisUser.id.Value = int.Parse(id.Value);

                        if (!String.IsNullOrEmpty(password.Text.Trim()))
                        {
                            //TODO: ???what if passwords do not match??? // TO BE DONE IN JAVASCRIPT
                            //(user_password.Text != password_confirmation.Text)

                            thisUser.password_hash_type.Value = "md5";
                            saltValue = LiftDomain.User.generateRandomSalt();
                            thisUser.password_salt.Value = saltValue;

                            thisUser.crypted_password.Value = LiftDomain.User.hash(password.Text, saltValue);
                            thisUser.last_password_changed_date.Value = LiftTime.CurrentTime;
                        }

                    }
                    else
                    {
                        //-------------------------------------------------------------------------
                        //-- if the object ID is blank or zero (0), then set some NEW object values (NOT id)
                        //-------------------------------------------------------------------------
                        thisUser.created_at.Value = LiftTime.CurrentTime;
                        thisUser.last_logged_in_at.Value = new DateTime(2000, 1, 1, 0, 0, 0); //-- DateTime.MinValue;
                        thisUser.login_failure_count.Value = 0;
                        //thisUser.total_comments.Value = 0;
                        //thisUser.total_comments_needing_approval.Value = 0;
                        //thisUser.total_private_comments.Value = 0;

                        //TODO: ???what if password is blank??? // TO BE DONE IN JAVASCRIPT
                        if (String.IsNullOrEmpty(password.Text.Trim()))
                        {
                        }
                        else
                        {
                            //TODO: ???what if passwords do not match??? // TO BE DONE IN JAVASCRIPT
                            //(user_password.Text != password_confirmation.Text)

                            thisUser.password_hash_type.Value = "md5";
                            saltValue = LiftDomain.User.generateRandomSalt();
                            thisUser.password_salt.Value = saltValue;

                            thisUser.crypted_password.Value = LiftDomain.User.hash(password.Text, saltValue);
                            thisUser.last_password_changed_date.Value = LiftTime.CurrentTime;
                        }
                    }

                    //-------------------------------------------------------------------------
                    //-- transfer screen values to the object
                    //-------------------------------------------------------------------------
                    //TODO: ???what if data field validation fails??? // TO BE DONE IN JAVASCRIPT
                    //TODO: ???what if user login already exists??? // TO BE DONE IN JAVASCRIPT
                    thisUser.login.Value = user_login.Text;
                    thisUser.email.Value = user_email.Text;

                    thisUser.first_name.Value = user_first_name.Text;
                    thisUser.last_name.Value = user_last_name.Text;
                    thisUser.address.Value = user_address.Text;
                    thisUser.city.Value = user_city.Text;
                    thisUser.state_province.Value = user_state.Text;
                    thisUser.postal_code.Value = user_postal_code.Text;
                    thisUser.phone.Value = user_phone.Text;

                    thisUser.state.Value = Convert.ToInt32(user_status_list.SelectedItem.Value);
                    thisUser.time_zone.Value = timezone_list.SelectedItem.Value;
                    thisUser.language_id.Value = Convert.ToInt32(language_list.SelectedItem.Value);
                    thisUser.organization_id.Value = Convert.ToInt32(org_list.SelectedItem.Value);

                    thisUser.previous_increment_id.Value = 0;
                    thisUser.updated_at.Value = LiftTime.CurrentTime;

                    //thisUser.isapproved.Value = true; //TODO: ???need to fix when moderator user available

                    //-------------------------------------------------------------------------
                    //-- persist the object data to the database
                    //-------------------------------------------------------------------------
                    thisUser.OverrideAutoOrgAssignment = true;
                    thisUser.id.Value = Convert.ToInt32(thisUser.doCommand("save"));

                    //id.Value = thisUser.id.Value.ToString();

                    //-------------------------------------------------------------------------
                    //-- persist the RolesUser object data to the database
                    //-- first, delete all for this user...then insert in the selected roles
                    //-------------------------------------------------------------------------
                    thisRolesUser = new LiftDomain.RolesUser();
                    thisRolesUser.user_id.Value = thisUser.id.Value;
                    thisRolesUser.doQuery("delete_roles_users_by_user_id");

                    /*
                    if (user_roles_2.Checked)
                    {
                        thisRolesUser = new LiftDomain.RolesUser();
                        thisRolesUser.user_id.Value = thisUser.id.Value;
                        thisRolesUser.role_id.Value = 2;
                        thisRolesUser.created_at.Value = LiftTime.CurrentTime;
                        thisRolesUser.doCommand("save");
                    }
                    */
                   

                    if (user_roles_7.Checked)
                    {
                        thisRolesUser = new LiftDomain.RolesUser();
                        thisRolesUser.user_id.Value = thisUser.id.Value;
                        thisRolesUser.role_id.Value = 7;
                        thisRolesUser.created_at.Value = LiftTime.CurrentTime;
                        thisRolesUser.doCommand("save");
                    }

                    if (user_roles_8.Checked)
                    {
                        thisRolesUser = new LiftDomain.RolesUser();
                        thisRolesUser.user_id.Value = thisUser.id.Value;
                        thisRolesUser.role_id.Value = 8;
                        thisRolesUser.created_at.Value = LiftTime.CurrentTime;
                        thisRolesUser.doCommand("save");
                    }

                    if (user_roles_10.Checked)
                    {
                        thisRolesUser = new LiftDomain.RolesUser();
                        thisRolesUser.user_id.Value = thisUser.id.Value;
                        thisRolesUser.role_id.Value = 10;
                        thisRolesUser.created_at.Value = LiftTime.CurrentTime;
                        thisRolesUser.doCommand("save");
                    }

                    if (user_roles_13.Checked)
                    {
                        thisRolesUser = new LiftDomain.RolesUser();
                        thisRolesUser.user_id.Value = thisUser.id.Value;
                        thisRolesUser.role_id.Value = 13;
                        thisRolesUser.created_at.Value = LiftTime.CurrentTime;
                        thisRolesUser.doCommand("save");
                    }

                    if (user_roles_14.Checked)
                    {
                        thisRolesUser = new LiftDomain.RolesUser();
                        thisRolesUser.user_id.Value = thisUser.id.Value;
                        thisRolesUser.role_id.Value = 14;
                        thisRolesUser.created_at.Value = LiftTime.CurrentTime;
                        thisRolesUser.doCommand("save");
                    }

                    //if (user_roles_11.Checked)
                    //{
                    //    thisRolesUser = new LiftDomain.RolesUser();
                    //    thisRolesUser.user_id.Value = thisUser.id.Value;
                    //    thisRolesUser.role_id.Value = 11;
                    //    thisRolesUser.created_at.Value = LiftTime.CurrentTime;
                    //    thisRolesUser.doCommand("save");
                    //}

                    //if (user_roles_12.Checked)
                    //{
                    //    thisRolesUser = new LiftDomain.RolesUser();
                    //    thisRolesUser.user_id.Value = thisUser.id.Value;
                    //    thisRolesUser.role_id.Value = 12;
                    //    thisRolesUser.created_at.Value = LiftTime.CurrentTime;
                    //    thisRolesUser.doCommand("save");
                    //}

                    //-------------------------------------------------------------------------
                    //-- return to the User List page
                    //-------------------------------------------------------------------------
                    if (Session["last_user_list_search"] != null)
                    {
                        Response.Redirect("UserList.aspx?" + Session["last_user_list_search"]);
                    }
                    else
                    {
                        Response.Redirect("UserList.aspx");
                    }
                }
                else
                {
                    //-------------------------------------------------------------------------
                    //-- first time on this page, so get the object ID from the ASP Request cache
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

                    thisUser.id.Value = Convert.ToInt32(id.Value);

                    //-------------------------------------------------------------------------
                    //-- if this is a NEW user...
                    //-------------------------------------------------------------------------
                    if (id.Value == "0")
                    {
                        //-------------------------------------------------------------------------
                        //-- set default values
                        //-------------------------------------------------------------------------
                        initialUserStatus = 1;  //-- 1 = unconfirmed
                        initialTimeZone = LiftDomain.Organization.Current.time_zone.Value;
                        initialLanguageId = LiftDomain.Organization.Current.language_id.Value;
                        initialOrgId = LiftDomain.Organization.Current.id.Value;

                        login_label.Visible = false;
                        edit_user_fieldset_legend = LiftDomain.Language.Current.USER_CREATE_A_NEW_USER.Value;

                        bottomNavTableCellDelete.Visible = false;
                        delete_user_id = string.Empty;
                        redirect_after_delete_to_page = string.Empty;
                        PasswordRequired.Enabled = true;
                    }

                    //-------------------------------------------------------------------------
                    //-- else, if this is an EXISTING user...
                    //-------------------------------------------------------------------------
                    else
                    {
                        //-------------------------------------------------------------------------
                        //-- query database for data for this user
                        //-------------------------------------------------------------------------

                        if (LiftDomain.User.Current.IsInRole(Role.SYS_ADMIN))
                        {
                            thisUser.OverrideAutoOrgAssignment = true;
                        }

                        thisUser = thisUser.doSingleObjectQuery<LiftDomain.User>("select");

                        initialUserStatus = thisUser.state;
                        initialTimeZone = thisUser.time_zone;
                        initialLanguageId = thisUser.language_id;
                        initialOrgId = thisUser.organization_id;

                        login_label.Text = LiftDomain.Language.Current.USER_EDITING_USER.Value + " " + thisUser.login;
                        edit_user_fieldset_legend = LiftDomain.Language.Current.USER_EDIT_USER.Value;

                        bottomNavTableCellDelete.Visible = true;
                        delete_user_id = id.Value;

                        if (Session["last_user_list_search"] != null)
                        {
                            redirect_after_delete_to_page = "UserList.aspx?" + Session["last_user_list_search"];
                        }
                        else
                        {
                            redirect_after_delete_to_page = "UserList.aspx";
                        }
                    }

                    //-------------------------------------------------------------------------
                    //-- populate the screen controls
                    //-------------------------------------------------------------------------
                    user_login.Text = thisUser.login;
                    user_email.Text = thisUser.email;
                    user_first_name.Text = thisUser.first_name;
                    user_last_name.Text = thisUser.last_name;
                    user_address.Text = thisUser.address;
                    user_city.Text = thisUser.city;
                    user_state.Text = thisUser.state_province;
                    user_postal_code.Text = thisUser.postal_code;
                    user_phone.Text = thisUser.phone;

                    /*
                    thisRolesUser = new LiftDomain.RolesUser();
                    thisRolesUser.user_id.Value = thisUser.id.Value;
                    thisRolesUser.role_id.Value = 2;
                    thisRolesUser = thisRolesUser.doSingleObjectQuery<LiftDomain.RolesUser>("select");
                    user_roles_2.Checked = (thisRolesUser.id.Value > 0);
                     * */

                    thisRolesUser = new LiftDomain.RolesUser();
                    thisRolesUser.user_id.Value = thisUser.id.Value;
                    thisRolesUser.role_id.Value = 7;
                    thisRolesUser = thisRolesUser.doSingleObjectQuery<LiftDomain.RolesUser>("select");
                    user_roles_7.Checked = (thisRolesUser.id.Value > 0);

                    thisRolesUser = new LiftDomain.RolesUser();
                    thisRolesUser.user_id.Value = thisUser.id.Value;
                    thisRolesUser.role_id.Value = 8;
                    thisRolesUser = thisRolesUser.doSingleObjectQuery<LiftDomain.RolesUser>("select");
                    user_roles_8.Checked = (thisRolesUser.id.Value > 0);

                    thisRolesUser = new LiftDomain.RolesUser();
                    thisRolesUser.user_id.Value = thisUser.id.Value;
                    thisRolesUser.role_id.Value = 10;
                    thisRolesUser = thisRolesUser.doSingleObjectQuery<LiftDomain.RolesUser>("select");
                    user_roles_10.Checked = (thisRolesUser.id.Value > 0);

                    thisRolesUser = new LiftDomain.RolesUser();
                    thisRolesUser.user_id.Value = thisUser.id.Value;
                    thisRolesUser.role_id.Value = 13;
                    thisRolesUser = thisRolesUser.doSingleObjectQuery<LiftDomain.RolesUser>("select");
                    user_roles_13.Checked = (thisRolesUser.id.Value > 0);

                    thisRolesUser = new LiftDomain.RolesUser();
                    thisRolesUser.user_id.Value = thisUser.id.Value;
                    thisRolesUser.role_id.Value = 14;
                    thisRolesUser = thisRolesUser.doSingleObjectQuery<LiftDomain.RolesUser>("select");
                    user_roles_14.Checked = (thisRolesUser.id.Value > 0);

                    //thisRolesUser = new LiftDomain.RolesUser();
                    //thisRolesUser.user_id.Value = thisUser.id.Value;
                    //thisRolesUser.role_id.Value = 11;
                    //thisRolesUser = thisRolesUser.doSingleObjectQuery<LiftDomain.RolesUser>("select");
                    //user_roles_11.Checked = (thisRolesUser.id.Value > 0);

                    //thisRolesUser = new LiftDomain.RolesUser();
                    //thisRolesUser.user_id.Value = thisUser.id.Value;
                    //thisRolesUser.role_id.Value = 12;
                    //thisRolesUser = thisRolesUser.doSingleObjectQuery<LiftDomain.RolesUser>("select");
                    //user_roles_12.Checked = (thisRolesUser.id.Value > 0);

                    initUserStatusList(initialUserStatus);
                    initTimeZoneList(initialTimeZone);
                    initLanguageList(initialLanguageId);
                    initOrgList(initialOrgId);

                    enforceRoleSettings();
                    

                }
            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN EditUser.aspx.cs::Page_Load(): " + m);
                Logger.log("EditUser.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN EditUser.aspx.cs::Page_Load(): " + m);
            }
            finally
            {
            }
        }

        protected void enforceRoleSettings()
        {
            org_list.Enabled = false;
            user_roles_13.Enabled = false;
            user_roles_14.Enabled = false;
            user_roles_10.Enabled = false;
            user_roles_8.Enabled = false;
            user_roles_7.Enabled = false;

            if (LiftDomain.User.Current.IsInRole(Role.SYS_ADMIN))
            {
                org_list.Enabled = true;
                user_roles_13.Enabled = true;
                user_roles_14.Enabled = true;
                user_roles_10.Enabled = true;
                user_roles_8.Enabled = true;
                user_roles_7.Enabled = true;
            }
            else if (LiftDomain.User.Current.IsInRole(Role.ORG_ADMIN))
            {
                user_roles_14.Enabled = true;
                user_roles_10.Enabled = true;
                user_roles_8.Enabled = true;
                user_roles_7.Enabled = true;
            }
            else if (LiftDomain.User.Current.IsInRole(Role.MODERATOR))
            {
                user_roles_8.Enabled = true;
                user_roles_7.Enabled = true;
            }
            else if (LiftDomain.User.Current.IsInRole(Role.WALL_LEADER))
            {
                user_roles_7.Enabled = true;
            }
        }

        protected void initUserStatusList(int selectedValue)
        {
            user_status_list.Items.Clear();
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_STATUS_CONFIRMED.Value, ((int)UserState.confirmed).ToString()));
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_STATUS_UNCONFIRMED.Value, ((int)UserState.unconfirmed).ToString()));
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_STATUS_DELETED.Value, ((int)UserState.deleted).ToString()));
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_STATUS_LOCKED.Value, ((int)UserState.locked).ToString()));

            foreach (ListItem thisListItem in user_status_list.Items)
            {
                thisListItem.Selected = (thisListItem.Value == selectedValue.ToString());
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

        protected void initOrgList(int selectedValue)
        {
            org_list.Items.Clear();

            LiftDomain.Organization o = new Organization();
            DataSet orgSet = o.doQuery("select");
            DataTable orgTable = orgSet.Tables[0];

            org_list.DataValueField = "id";
            org_list.DataTextField = "title";
            org_list.DataSource = orgTable;
            org_list.DataBind();

            foreach (ListItem li in org_list.Items)
            {
                if (li.Value == selectedValue.ToString())
                {
                    li.Selected = true;
                }
                else
                {
                    li.Selected = false;
                }
            }

        }

    }
}
