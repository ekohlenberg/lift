using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Principal;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class SignupUser : System.Web.UI.Page
    {
        protected string signup_user_fieldset_legend = string.Empty;
        protected string signup_user_fieldset_legend2 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            EmailValidator.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;
            PasswordValidator.ErrorMessage = LiftDomain.Language.Current.SHARED_PASSWORDS_DO_NOT_MATCH;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            int initialUserStatus = 1;  //-- 1 = unconfirmed
            string initialTimeZone = "Central Standard Time";
            int initialLanguageId = 1;  //-- 1 = English
            string saltValue = string.Empty;


            try
            {
                //-------------------------------------------------------------------------
                //-- do the language setting for the SUBMIT button here
                //-- (unable to place <%=LiftDomain.Language.Current.SIGNUP_USER_SIGN_ME_UP %> in asp:Button Text field)
                //-------------------------------------------------------------------------
                this.submitBtn.Text = LiftDomain.Language.Current.SIGNUP_USER_SIGN_ME_UP.Value;

                //-------------------------------------------------------------------------
                //-- do other language settings
                //-------------------------------------------------------------------------
                signup_user_fieldset_legend = LiftDomain.Language.Current.SIGNUP_USER_NEW_USER_REGISTRATION.Value;
                signup_user_fieldset_legend2 = LiftDomain.Language.Current.SIGNUP_USER_ALL_FIELDS_REQUIRED.Value;

                LiftDomain.User thisUser = new LiftDomain.User();

                if (IsPostBack)
                {
                    //TODO: ???what if CAPTCHA validation fails??? 
                    //TODO: ???should we be doing validation checking in Page_Load or submitBtn_Click??? 
                    //if (Page.IsValid && (txtCaptcha.Text.ToString() == Session["captchaValue"].ToString()))
                    if (txtCaptcha.Text.ToString().Trim().ToUpper() == Session["captchaValue"].ToString().Trim().ToUpper())
                    {
                        //Response.Write("CAPTCHA verification succeeded");



                        //-------------------------------------------------------------------------
                        //-- get the user ID from the hidden id field on the page;
                        //-- if there is a user ID value, then we are editing an EXISTING user
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
                            //-- if the user ID is blank or zero (0), then set some NEW user values (NOT id)
                            //-------------------------------------------------------------------------
                            thisUser.state.Value = initialUserStatus;
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
                            }
                        }

                        //-------------------------------------------------------------------------
                        //-- transfer screen values to the object
                        //-------------------------------------------------------------------------
                        thisUser.login.Value = user_email.Text;
                        thisUser.email.Value = user_email.Text;

                        thisUser.first_name.Value = user_first_name.Text;
                        thisUser.last_name.Value = user_last_name.Text;
                        //thisUser.address.Value = user_address.Text;
                        thisUser.address.Value = "";
                        //thisUser.city.Value = user_city.Text;
                        thisUser.city.Value = "";
                        //thisUser.state_province.Value = user_state.Text;
                        thisUser.state_province.Value = "";
                        //thisUser.postal_code.Value = user_postal_code.Text;
                        thisUser.postal_code.Value = "";
                        thisUser.phone.Value = user_phone.Text;

                        //thisUser.state.Value = initialUserStatus;
                        //thisUser.time_zone.Value = timezone_list.SelectedItem.Value;
                        thisUser.time_zone.Value = Organization.Current.time_zone.Value;
                        //thisUser.language_id.Value = Convert.ToInt32(language_list.SelectedItem.Value);
                        thisUser.language_id.Value = Organization.Current.language_id.Value;

                        thisUser.previous_increment_id.Value = 0;
                        thisUser.updated_at.Value = LiftTime.CurrentTime;
                        thisUser.password_hash_type.Value = "md5";

                        //thisUser.isapproved.Value = true; //TODO: ???need to fix when moderator user available

                        bool ok = true;
                        if (LiftDomain.User.checkEmailExists(user_email.Text))
                        {
                            ok = false;
                        }

                        if (user_login.Text.Length == 0)
                        {
                            user_login.Text = user_email.Text;
                        }

                        if (LiftDomain.User.checkUsernameExists(user_login.Text))
                        {
                            ok = false;
                        }

                        if (!ok)
                        {
                            errorMsg.Text = Language.Current.SIGNUP_ACCT_EXISTS1;
                            errorMsg.Text += " ";
                            errorMsg.Text += Language.Current.SIGNUP_ACCT_EXISTS2;
                            errorMsg.Text += "<br/><br/>";
                            errorMsg.Text += " <a href=\"ForgotPassword.aspx?email=";
                            errorMsg.Text += thisUser.email.Value;
                            errorMsg.Text += "\">";
                            errorMsg.Text += LiftDomain.Language.Current.SIGNUP_RETRIEVE_YOUR_PASSWORD;
                            errorMsg.Text += "</a>";
                        }

                        if (ok)
                        {
                            //-------------------------------------------------------------------------
                            //-- persist the User object data to the database
                            //-------------------------------------------------------------------------
                            thisUser.id.Value = Convert.ToInt32(thisUser.doCommand("create_account"));

                            LiftMembershipProvider membership = new LiftMembershipProvider();

                            if (membership.ValidateUser(user_email.Text, password.Text))
                            {
                                FormsAuthentication.Initialize();

                                LiftRoleProvider roleProvider = new LiftRoleProvider();
                                roleProvider.Initialize(null, null);
                                //String strRole = membership.AssignRoles(txtUsername.Text);
                                string[] roles = roleProvider.GetRolesForUser(user_login.Text);
                                string strRole = "";
                                foreach (string role in roles)
                                {
                                    if (strRole.Length > 0)
                                    {
                                        strRole += ",";
                                    }
                                    strRole += role;
                                }

                                //FormsIdentity fi = new FormsIdentity((FormsIdentity)HttpContext.Current.User.Identity;
                                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                                    user_email.Text, DateTime.Now,
                                    DateTime.Now.AddMinutes(30), false, strRole,
                                    FormsAuthentication.FormsCookiePath);


                                FormsIdentity fi = new FormsIdentity(fat);

                                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                                    FormsAuthentication.Encrypt(fat)));


                                HttpContext.Current.User = new GenericPrincipal(fi, roles);

                                Response.Redirect(FormsAuthentication.GetRedirectUrl(user_email.Text, false));
                            }
                            else
                            {
                                Response.Redirect("SignUpThankYou.aspx");
                            }

                        }
                        

                    }
                    else
                    {
                        errorMsg.Text = Language.Current.SIGNUP_USER_USER_REGISTRATION_FAILED;
                    }
                   
                }
                else
                {
                    //-------------------------------------------------------------------------
                    //-- first time on this page, so get the user ID from the ASP Request cache
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
                    }

                    //-------------------------------------------------------------------------
                    //-- else, if this is an EXISTING user...
                    //-------------------------------------------------------------------------
                    else
                    {
                        //-------------------------------------------------------------------------
                        //-- query database for data for this user
                        //-------------------------------------------------------------------------
                        thisUser = thisUser.doSingleObjectQuery<LiftDomain.User>("select");

                        initialUserStatus = thisUser.state;
                        initialTimeZone = thisUser.time_zone;
                        initialLanguageId = thisUser.language_id;
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

                    initTimeZoneList(initialTimeZone);
                    initLanguageList(initialLanguageId);
                }
            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN SignupUser.aspx.cs::Page_Load(): " + m);
                Logger.log("SignupUser.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN SignupUser.aspx.cs::Page_Load(): " + m);
                //Response.Write(m);
            }
            finally
            {
            }
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            //if (Page.IsValid && (txtCaptcha.Text.ToString().Trim().ToUpper() == Session["captchaValue"].ToString().Trim().ToUpper()))
            //{
            //    Response.Write("CAPTCHA verification succeeded");
            //}
            //else
            //{
            //    Response.Write("CAPTCHA verification failed");
            //}
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

    }
}
