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
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }
            //-------------------------------------------------------------------------
            //-- do the language setting for the SUBMIT button here
            //-- (unable to place <%=LiftDomain.Language.Current.SHARED_SUBMIT %> in asp:Button Text field)
            //-------------------------------------------------------------------------
            this.submitBtn.Text = LiftDomain.Language.Current.SHARED_SUBMIT;

            string emailMessageBody = string.Empty;

            if (IsPostBack)
            {
                try
                {
                    //TODO: ??? HOW DO WE VALIDATE THE FORM FIELD DATA (required, max length, valid e-mail address, dangerous content?, etc.)

                    //-------------------------------------------------------------------------
                    //-- get the information entered on the web form 
                    //-- and send it in an e-mail to the organization point of contact
                    //-------------------------------------------------------------------------
                    //-- (org_email and org_appearance will specify recipients and smtp settings)
                    //-------------------------------------------------------------------------

                    //YOUR NAME: = contact_from.Text;
                    //YOUR EMAIL: = contact_from_email.Text;
                    //SUBJECT: = contact_subject.Text;
                    //MESSAGE: = contact_message.Text;

                    Organization currentOrganization = Organization.Current;
                    OrgEmail thisOrgEmail = currentOrganization.getOrgEmail("email.contact_us");

                    LiftCommon.Email emailHelper = new LiftCommon.Email();

                  
                    //email.replyTo = thisOrgEmail.emailReplyTo;  // not supported yet

                    //TODO: ??? THIS NEEDS TO BE A VALID E-MAIL ADDRESS
                    if (Email.IsValidEmailAddress(contact_from_email.Text))
                    {
                        emailHelper.from = contact_from_email.Text;  // field from the form
                    }
                    else
                    {
                        //TODO: ??? HOW DO WE NOTIFY THE USER
                        Logger.log(Logger.Level.ERROR, this, "E-mail address '" + contact_from_email.Text + "' is not in a correct format [Contact.aspx].");
                        throw new ApplicationException("E-mail address '" + contact_from_email.Text + "' is not in a correct format [Contact.aspx].");
                    }

                    emailHelper.addTo(thisOrgEmail.email_to);
                    emailHelper.subject = contact_subject.Text;  // field from the form

                    emailMessageBody = LiftDomain.Language.Current.CONTACTUS_YOUR_NAME + ":  " + contact_from.Text + "\r\n";
                    emailMessageBody += LiftDomain.Language.Current.CONTACTUS_YOUR_EMAIL + ":  " + contact_from_email.Text + "\r\n";
                    emailMessageBody += LiftDomain.Language.Current.CONTACTUS_MESSAGE + ":  \r\n";
                    emailMessageBody += contact_message.Text;   // field from the form

                    emailHelper.Body = emailMessageBody;

                    //email.MIME = MIME.Text | MIME.HTML;  // just supposing that it supports multiple formats. May not be necessary

                    emailHelper.send();
                   
                    Response.Redirect("Requests.aspx");   
                }
                catch (Exception x)
                {
                    //TODO: ??? WHAT DO WE DO IF THE E-MAIL PROCESS FAILS
                    string m = x.Message;
                    System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR SENDING E-MAIL: " + m);
                }
                finally
                {
                }

            }
        }

    }
}
