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
    public partial class EditOrganizationEmails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmailValidator1.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;
            EmailValidator2.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;
            EmailValidator3.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;
            EmailValidator4.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;

						PageAuthorized.check(Request, Response);


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
                
                //-------------------------------------------------------------------------
                //-- instantiate object
                //-------------------------------------------------------------------------
                LiftDomain.OrgEmail thisOrgEmail = new LiftDomain.OrgEmail();
                organization_from_email_address.ReadOnly = true;

                if (IsPostBack)
                {
                    //-------------------------------------------------------------------------
                    //-- get the object ID from the hidden id field on the page;
                    //-- if there is a object ID value, then we are editing an EXISTING object
                    //-------------------------------------------------------------------------
                    if (!String.IsNullOrEmpty(id.Value) && (id.Value != "0"))
                    {
                        thisOrgEmail.id.Value = int.Parse(id.Value);
                    }

                    //-------------------------------------------------------------------------
                    //-- transfer screen values to the object
                    //-------------------------------------------------------------------------
                    //TODO: ???what if data field validation fails??? // TO BE DONE IN JAVASCRIPT
                    thisOrgEmail.webmaster_email_to.Value = organization_email_to_webmaster.Text;
                    thisOrgEmail.contact_us_email_to.Value = organization_email_to_contact_us.Text;
                    thisOrgEmail.encourager_email_to.Value = organization_email_to_encourager.Text;
                    //thisOrgEmail.email_from.Value = organization_from_email_address.Text;

                    //-------------------------------------------------------------------------
                    //-- persist the object data to the database
                    //-------------------------------------------------------------------------
                    thisOrgEmail.id.Value = Convert.ToInt32(thisOrgEmail.doCommand("save"));

                    //-------------------------------------------------------------------------
                    //-- return to ???
                    //-------------------------------------------------------------------------
                    //TODO: ???where to redirect after editing this page???
                    //Response.Redirect("???");
                }
                else
                {
                    //-------------------------------------------------------------------------
                    //-- first time on this page, so get the organization ID from the ASP Request cache
                    //-------------------------------------------------------------------------
                    string orgIdStr = Request["o"];

                    if (String.IsNullOrEmpty(orgIdStr))
                    {
                        //TODO: ??? HOW DO WE NOTIFY THE USER
                        Logger.log(Logger.Level.ERROR, this, "Organization ID must be passed in the request string [EditOrganizationEmails.aspx].");
                        throw new ApplicationException("Organization ID must be passed in the request string [EditOrganizationEmails.aspx].");
                    }
                    
                    else
                    {
                        orgId.Value = orgIdStr;
                    }
                    

                    LiftDomain.Organization thisOrganization = new LiftDomain.Organization();
                    thisOrganization.id.Value = Convert.ToInt32(orgIdStr);
                    
                    //-------------------------------------------------------------------------
                    //-- query database for data for this organization
                    //-------------------------------------------------------------------------
                    thisOrganization = thisOrganization.doSingleObjectQuery<LiftDomain.Organization>("select");
                    title_label.Text = LiftDomain.Language.Current.ORGANIZATION_EDITING_ORGANIZATION.Value + " " + thisOrganization.title;

                    //-------------------------------------------------------------------------
                    //-- query database for data for this organization's emails
                    //-------------------------------------------------------------------------
                    thisOrgEmail.organization_id.Value = thisOrganization.id.Value;
                    try
                    {
                        thisOrgEmail = thisOrgEmail.doSingleObjectQuery<LiftDomain.OrgEmail>("select");
                        id.Value = thisOrgEmail.id.Value.ToString();
                        //-------------------------------------------------------------------------
                        //-- populate the screen controls
                        //-------------------------------------------------------------------------
                        organization_email_to_webmaster.Text = thisOrgEmail.webmaster_email_to;
                        organization_email_to_contact_us.Text = thisOrgEmail.contact_us_email_to;
                        organization_email_to_encourager.Text = thisOrgEmail.encourager_email_to;
                    }
                    catch
                    {
                        id.Value = "0";
                    }

                    organization_from_email_address.Text = Organization.Current.getFromEmail();
                    organization_from_email_address.ReadOnly = true;
                }
            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganizationEmails.aspx.cs::Page_Load(): " + m);
                Logger.log("EditOrganizationEmails.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganizationEmails.aspx.cs::Page_Load(): " + m);
            }
            finally
            {
            }
        }

    }
}
