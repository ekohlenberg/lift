using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected string forgot_password_fieldset_legend = string.Empty;
        protected DataSet userListSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            string randomPassword = string.Empty;
            string saltValue = string.Empty;
            int ok = 0;
            string targetEmail = string.Empty;

            EmailValidator.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            try
            {
                //-------------------------------------------------------------------------
                //-- do the language setting for the SUBMIT button here
                //-- (unable to place <%=LiftDomain.Language.Current.SHARED_SUBMIT %> in asp:Button Text field)
                //-------------------------------------------------------------------------
                this.submitBtn.Text = LiftDomain.Language.Current.SHARED_SUBMIT.Value;

                string email = Request["email"];
                if (!string.IsNullOrEmpty(email))
                {
                    user_email.Text = email;
                }

                //-------------------------------------------------------------------------
                //-- do other language settings
                //-------------------------------------------------------------------------
                forgot_password_fieldset_legend = LiftDomain.Language.Current.FORGOT_PASSWORD_INSTRUCTIONS.Value;

                if (IsPostBack)
                {
                    //TODO: ???what if CAPTCHA validation fails??? 
                    //TODO: ???should we be doing validation checking in Page_Load or submitBtn_Click??? 
                    //if (Page.IsValid && (txtCaptcha.Text.ToString() == Session["captchaValue"].ToString()))
                    if (txtCaptcha.Text.ToString().Trim().ToUpper() == Session["captchaValue"].ToString().Trim().ToUpper())
                    {
                        //Response.Write("CAPTCHA verification succeeded");


                        //-------------------------------------------------------------------------
                        //-- validate given e-mail (required, valid e-mail)
                        //-------------------------------------------------------------------------

                        //-------------------------------------------------------------------------
                        //-- determine if user exists for given e-mail
                        //-------------------------------------------------------------------------
                        LiftDomain.User thisUserList = new LiftDomain.User();
                        thisUserList["search"] = user_email.Text;
                        userListSet = thisUserList.doQuery("SearchUsersByEmail");

                        //TODO: ???what if multiple user records are found for the given email address???
                        if (userListSet.Tables[0].Rows.Count > 0)
                        {
                            LiftDomain.User thisUser = new LiftDomain.User();

                            thisUser.id.Value = Convert.ToInt32(userListSet.Tables[0].Rows[0]["id"]);
                            string username = userListSet.Tables[0].Rows[0]["username"].ToString();

                            //-------------------------------------------------------------------------
                            //-- create new random password for user
                            //-------------------------------------------------------------------------
                            randomPassword = LiftDomain.User.generatePassword();

                            //-------------------------------------------------------------------------
                            //-- update user record with new password 
                            //-------------------------------------------------------------------------
                            thisUser.password_hash_type.Value = "md5";
                            saltValue = LiftDomain.User.generateRandomSalt();
                            thisUser.password_salt.Value = saltValue;
                            thisUser.crypted_password.Value = LiftDomain.User.hash(randomPassword, saltValue);
                            thisUser.last_password_changed_date.Value = LiftTime.CurrentTime;
                            thisUser.updated_at.Value = LiftTime.CurrentTime;

                            thisUser.id.Value = Convert.ToInt32(thisUser.doCommand("save"));

                            //-------------------------------------------------------------------------
                            //-- send new randomly-generated password to the given e-mail address
                            //-------------------------------------------------------------------------
                            LiftCommon.Email emailHelper = new LiftCommon.Email();
                            //email.replyTo = thisOrgEmail.emailReplyTo;  // not supported yet

                            emailHelper.from = Organization.Current.getFromEmail();

                            if (LiftCommon.Email.IsValidEmailAddress(user_email.Text))
                            {
                                targetEmail = user_email.Text;
                                try
                                {
                                    StringBuilder body = new StringBuilder();
                                    emailHelper.addTo(targetEmail);

                                    emailHelper.subject = LiftDomain.Language.Current.FORGOT_PASSWORD_NOTIFICATION_SUBJECT.Value;
                                    body.Append(LiftDomain.Language.Current.FORGOT_PASSWORD_NOTIFICATION_MESSAGE.Value);
                                    body.Append("\r\n");
                                    body.Append(LiftDomain.Language.Current.USER_EMAIL);
                                    body.Append("\t");
                                    body.Append(targetEmail);
                                    body.Append("\r\n");
                                    body.Append(LiftDomain.Language.Current.LOGIN_THE_NEW_PASSWORD);
                                    body.Append("\t");
                                    body.Append(randomPassword);
                                    body.Append("\r\n");
                                    emailHelper.Body = body.ToString();

                                    //email.MIME = MIME.Text | MIME.HTML;  // just supposing that it supports multiple formats. May not be necessary

                                    emailHelper.send();
                                    ok = 1;
                                }
                                catch
                                {
                                    ok = 0;
                                }
                                
                            }

                        }

                    }
                    //-------------------------------------------------------------------------
                    //-- redirect to the "password has been reset, you should receive an e-mail" page
                    //-------------------------------------------------------------------------
                    Response.Redirect("PasswordReset.aspx?ok="+ok.ToString()+"&e="+targetEmail);
                }
                else
                {
                    //-------------------------------------------------------------------------
                    //-- first time on this page, so ...???
                    //-------------------------------------------------------------------------
                }
            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN ForgotPassword.aspx.cs::Page_Load(): " + m);
                Logger.log("ForgotPassword.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN ForgotPassword.aspx.cs::Page_Load(): " + m);
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

    }
}
