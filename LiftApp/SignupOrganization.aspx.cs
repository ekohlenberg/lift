using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class SignupOrganization : System.Web.UI.Page
    {
        //-------------------------------------------------------------------------
        //-- 1) Create new org from scratch. There should be a placeholder for a terms of use agreement.
        //-- 2) Signup user as org admin for new org. This new user's email address will be copied to the webmaster email role in the org_emails table.
        //-- 3) Create email accounts using the HostingProvider interface
        //-- 4) Create org_emails records
        //-- 5) Create the /custom/org/images and /custom/org/stylesheets folders
        //-- 6) There will be a new org status - approved and unapproved. The org will initially be created in the unapproved state.
        //-- 7) The system will send an email to admin@liftprayer.cc to notify of org requesting approval.
        //-- 8) The org list page will show the approval status of orgs.
        //-- 9) The org edit page will enable the sys admin to approve an org.
        //-- 10) The system will generate an email to the org webmaster indicating that the new org has been approved.
        //-------------------------------------------------------------------------

        protected void Page_Load(object sender, EventArgs e)
        {
            EmailValidator.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;
            
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

						PageAuthorized.check(Request, Response);

            string initialTimeZone = "Central Standard Time";
            int initialLanguageId = 1;  //-- 1 = English
            string saltValue = string.Empty;
            string thisDirectory = string.Empty;

            try
            {
                //-------------------------------------------------------------------------
                //-- do the language setting for the SUBMIT button here
                //-- (unable to place <%=LiftDomain.Language.Current.SHARED_SUBMIT %> in asp:Button Text field)
                //-------------------------------------------------------------------------
                this.submitBtn.Text = LiftDomain.Language.Current.SHARED_SUBMIT.Value;

                if (IsPostBack)
                {
                    //-------------------------------------------------------------------------
                    //-- instantiate the object
                    //-------------------------------------------------------------------------
                    LiftDomain.Organization thisOrganization = new LiftDomain.Organization();

                    //-------------------------------------------------------------------------
                    //-- transfer screen values to the object
                    //-------------------------------------------------------------------------
                    //TODO: ???what if data field validation fails??? // TO BE DONE IN JAVASCRIPT
                    //TODO: ???what if org title already exists??? // TO BE DONE IN JAVASCRIPT
                    //TODO: ???what if org subdomain already exists??? // TO BE DONE IN JAVASCRIPT
                    //TODO: ???should this be wrapped in a transaction???

                    //thisOrganization.id.Value = 0; //-- id of "0" means "new"
                    thisOrganization.title.Value = organization_title.Text;
                    thisOrganization.user_id.Value = 0; // LiftDomain.User.Current.id.Value;
                    thisOrganization.address.Value = organization_address.Text;
                    thisOrganization.city.Value = organization_city.Text;
                    thisOrganization.state_province.Value = organization_state.Text;
                    thisOrganization.postal_code.Value = organization_postal_code.Text;
                    thisOrganization.phone.Value = organization_phone.Text;
                    thisOrganization.subdomain.Value = organization_subdomain.Text;
                    thisOrganization.time_zone.Value = initialTimeZone;
                    thisOrganization.language_id.Value = initialLanguageId;
                    thisOrganization.status.Value = 0; //-- 0 = unapproved; 1 = approved
                    thisOrganization.created_at.Value = LiftTime.CurrentTime;

                    //-------------------------------------------------------------------------
                    //-- persist the object data to the database
                    //-------------------------------------------------------------------------
                    thisOrganization.id.Value = Convert.ToInt32(thisOrganization.doCommand("save"));

                    if (thisOrganization.id.Value != 0)
                    {
                        //-------------------------------------------------------------------------
                        //-- instantiate the child object
                        //-------------------------------------------------------------------------
                        //TODO: ???what if data field validation fails??? // TO BE DONE IN JAVASCRIPT
                        LiftDomain.OrgEmail thisOrgEmail = new LiftDomain.OrgEmail();

                        //thisOrgEmail.id.Value = 0; //-- id of "0" means "new"
                        thisOrgEmail.organization_id.Value = thisOrganization.id.Value;
                        thisOrgEmail.smtp_server.Value = "smtp.liftprayer.cc";
                        thisOrgEmail.smtp_username.Value = thisOrganization.subdomain.Value + "-contact@liftprayer.cc";
                        //thisOrgEmail.smtp_username.Value = "sugarcreek-contact@liftprayer.cc";
                        thisOrgEmail.smtp_password.Value = "reset-password";
                        thisOrgEmail.smtp_port.Value = 25;
                        thisOrgEmail.email_from.Value = thisOrganization.subdomain.Value + "-contact@liftprayer.cc";
                        //thisOrgEmail.email_from.Value = "sugarcreek-contact@liftprayer.cc";
                        thisOrgEmail.email_to.Value = organization_email_to_webmaster.Text;
                        thisOrgEmail.webmaster_email_to.Value = organization_email_to_webmaster.Text;

                        //-------------------------------------------------------------------------
                        //-- persist the child object data to the database
                        //-------------------------------------------------------------------------
                        thisOrgEmail.doCommand("save");

                        //*************************************************************************
                        //TODO: ???create email accounts using the HostingProvider interface???
                        //*************************************************************************

                        //-------------------------------------------------------------------------
                        //-- create organization-specific file system directories
                        //-------------------------------------------------------------------------
                        thisDirectory = Server.MapPath(".");

                        if (!Directory.Exists(thisDirectory + "\\..\\custom\\" + thisOrganization.subdomain.Value + "\\images"))
                        {
                            Directory.CreateDirectory(thisDirectory + "\\..\\custom\\" + thisOrganization.subdomain.Value + "\\images");
                        }

                        if (!File.Exists(thisDirectory + "\\..\\custom\\" + thisOrganization.subdomain.Value + "\\images\\logo.gif"))
                        {
                            //File.Copy(thisDirectory + "\\..\\custom\\standard\\images\\logo.gif", thisDirectory + "\\custom\\" + thisOrganization.subdomain.Value + "\\images\\logo.gif");
                            copyDirectory(thisDirectory + "\\..\\custom\\standard\\images", thisDirectory + "\\..\\custom\\" + thisOrganization.subdomain.Value + "\\images");
                        }

                        if (!Directory.Exists(thisDirectory + "\\..\\custom\\" + thisOrganization.subdomain.Value + "\\stylesheets"))
                        {
                            Directory.CreateDirectory(thisDirectory + "\\..\\custom\\" + thisOrganization.subdomain.Value + "\\stylesheets");
                        }

                        if (!File.Exists(thisDirectory + "\\..\\custom\\" + thisOrganization.subdomain.Value + "\\stylesheets\\lift_custom.css"))
                        {
                            //File.Copy(thisDirectory + "\\..\\custom\\standard\\stylesheets\\lift_base.css", thisDirectory + "\\custom\\" + thisOrganization.subdomain.Value + "\\stylesheets\\lift_custom.css");
                           createStylesheet(thisDirectory + "\\..\\custom\\standard\\stylesheets", thisDirectory + "\\..\\custom\\" + thisOrganization.subdomain.Value + "\\stylesheets");
                        }

                        //-------------------------------------------------------------------------
                        //-- email a request for approval to the system administrator
                        //-------------------------------------------------------------------------
                        /*
                        LiftCommon.Email emailHelper = new LiftCommon.Email();

                        emailHelper.server = ConfigReader.getString("smtp_server", ""); // thisOrgEmail.smtp_server;
                       // emailHelper.username = ConfigReader.getString("smtp_username", ""); // thisOrgEmail.smtp_username;
                        emailHelper.password = ConfigReader.getString("smtp_password", ""); // thisOrgEmail.smtp_password;
                        emailHelper.port = ConfigReader.getInt("smtp_port", 25); // thisOrgEmail.smtp_port;

                        //email.replyTo = thisOrgEmail.emailReplyTo;  // not supported yet

                        emailHelper.from = thisOrganization.getFromEmail();

                        //TODO: ??? THIS NEEDS TO BE A VALID E-MAIL ADDRESS
                        if (LiftCommon.Email.IsValidEmailAddress(thisOrgEmail.webmaster_email_to.Value))
                        {
                           
                        }
                        else
                        {
                            //TODO: ??? HOW DO WE NOTIFY THE USER
                            Logger.log(Logger.Level.ERROR, this, "E-mail address '" + thisOrgEmail.webmaster_email_to.Value + "' is not in a correct format [SignupOrganization.aspx].");
                            throw new ApplicationException("E-mail address '" + thisOrgEmail.webmaster_email_to.Value + "' is not in a correct format [SignupOrganization.aspx].");
                        }

                        emailHelper.addTo("admin@liftprayer.cc");
                        //emailHelper.addTo("jpalexa@yahoo.com");

                        emailHelper.subject = LiftDomain.Language.Current.SIGNUP_ORGANIZATION_APPROVAL_REQUEST_SUBJECT.Value;
                        emailHelper.Body = LiftDomain.Language.Current.SIGNUP_ORGANIZATION_APPROVAL_REQUEST_MESSAGE.Value + "  " + thisOrganization.title.Value;

                        //email.MIME = MIME.Text | MIME.HTML;  // just supposing that it supports multiple formats. May not be necessary

                        emailHelper.send();
                         
                         */
                    }
                    else
                    {
                        //else, org ID is zero, but try-catch did not handle it for some reason???
                    }

                    //-------------------------------------------------------------------------
                    //-- navigate to the Organization edit screen
                    //-------------------------------------------------------------------------
                    Response.Redirect("EditOrganization.aspx?id=" + thisOrganization.id.Value.ToString());
                }
                else
                {
                    organization_email_to_webmaster.Text = LiftDomain.User.Current.email.Value;
                }
            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN SignupOrganization.aspx.cs::Page_Load(): " + m);
                Logger.log("SignupOrganization.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN SignupOrganization.aspx.cs::Page_Load(): " + m);
            }
            finally
            {
            }
        }

        protected void copyDirectory( string srcDir, string targetDir )
        {
            string srcPath = srcDir;
            string targetPath = targetDir;

            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(srcDir);

                FileInfo[] files = di.GetFiles();

                foreach( FileInfo f in files)
                {
                    srcPath = srcDir + "\\";
                    srcPath += f.Name;
     
                    targetPath = targetDir + "\\";
                    targetPath += f.Name;

                    File.Copy( srcPath, targetPath);
                }
            }
            catch( Exception x)
            {
                string ctx = "Copying " + srcPath + " to " + targetPath;
                Logger.log( ctx, x, "Error copying file.");
            }
        }


        protected void createStylesheet(string srcDir, string targetDir)
        {
            string baseFile = "lift_base.css";
            string customFile = "lift_custom.css";

            string srcPath = string.Empty;
            string targetPath = string.Empty;

            srcPath = srcDir + "\\";
            srcPath += baseFile;

            targetPath = targetDir + "\\";
            targetPath += customFile;

            try
            {
                // copy the base stylesheet to the org's custom stylesheet
                File.Copy(srcPath, targetPath);

                srcPath = srcDir + "\\";
                srcPath += customFile;


                // append the custom stylesheet to the org's custom stylesheet
                StreamReader sr = File.OpenText(srcPath);
                StreamWriter sw = File.AppendText(targetPath);

                sw.WriteLine("/* --- Add customizations below this line --- */");

                sw.Write(sr.ReadToEnd());

                sw.Close();

                sr.Close();
            }
            catch (Exception x)
            {
                string ctx = "Creating stylesheet " + srcPath + " as " + targetPath;
                Logger.log(ctx, x, "Error creating file.");
            }


        }
    }
}
