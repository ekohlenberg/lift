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
    public partial class EmailAllUsers : System.Web.UI.Page
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

                    LiftCommon.Email emailHelper = new LiftCommon.Email();
                  
                    //email.replyTo = thisOrgEmail.emailReplyTo;  // not supported yet

                    emailHelper.from = "admin@liftprayer.cc";

                    //-------------------------------------------------------------------------
                    //-- get list of all users for the current organization
                    //-------------------------------------------------------------------------
                    LiftDomain.User thisUserList = new LiftDomain.User();
                    thisUserList["search"] = currentOrganization.id.Value;
                    DataSet userListSet = thisUserList.doQuery("SearchUsersByOrg");

                    foreach (DataRow dr in userListSet.Tables[0].Rows)
                    {
                        string email = dr["email"].ToString();

                        if (!String.IsNullOrEmpty(email))
                        {
                            //TODO: ??? VALIDATE THAT THE EMAIL ADDRESS IS A VALID EMAIL ADDRESS FORMAT ???

                            emailHelper.addTo(email);
                        }
                    }

                    emailHelper.subject = email_subject.Text;   // field from the form
                    emailHelper.Body = email_message.Text;      // field from the form

                    //email.MIME = MIME.Text | MIME.HTML;  // just supposing that it supports multiple formats. May not be necessary

                    emailHelper.send();

                    //TODO: ??? WHERE DO WE REDIRECT TO ???
                    //Response.Redirect("Requests.aspx");   
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
