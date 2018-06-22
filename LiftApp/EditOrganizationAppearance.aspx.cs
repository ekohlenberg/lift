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
    public partial class EditOrganizationAppearance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

						PageAuthorized.check(Request, Response);

            try
            {
                //-------------------------------------------------------------------------
                //-- do the language setting for the SUBMIT button here
                //-- (unable to place <%=LiftDomain.Language.Current.SHARED_SUBMIT %> in asp:Button Text field)
                //-------------------------------------------------------------------------
                this.submitBtn.Text = LiftDomain.Language.Current.SHARED_SUBMIT.Value;

                if (!IsPostBack)
                {
                    string idStr = Request["id"];

                    if (String.IsNullOrEmpty(idStr))
                    {
                        //TODO: ??? HOW DO WE NOTIFY THE USER
                        Logger.log(Logger.Level.ERROR, this, "Organization ID must be passed in the request string [EditOrganizationEmails.aspx].");
                        throw new ApplicationException("Organization ID must be passed in the request string [EditOrganizationEmails.aspx].");
                    }
                    else
                    {
                        id.Value = idStr;
                    }

                    LiftDomain.Organization thisOrganization = new LiftDomain.Organization();
                    thisOrganization.id.Value = Convert.ToInt32(id.Value);

                    //-------------------------------------------------------------------------
                    //-- query database for data for this organization
                    //-------------------------------------------------------------------------
                    thisOrganization = thisOrganization.doSingleObjectQuery<LiftDomain.Organization>("select");
                    title_label.Text = LiftDomain.Language.Current.ORGANIZATION_EDITING_ORGANIZATION.Value + " " + thisOrganization.title;
                    this.subdomain.Value = thisOrganization.subdomain;

                    string serverFileLocation = Server.MapPath("/custom/" + this.subdomain.Value + "/stylesheets/lift_custom.css");

                    if (File.Exists(serverFileLocation))
                    {
                        StreamReader sr;
                        sr = File.OpenText(serverFileLocation);
                        this.lift_custom_css.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                }
            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganizationAppearance.aspx.cs::Page_Load(): " + m);
                Logger.log("EditOrganizationAppearance.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganizationAppearance.aspx.cs::Page_Load(): " + m);
            }
            finally
            {
            }
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            string serverFileLocation = Server.MapPath(".") + "\\custom\\" + this.subdomain.Value + "\\stylesheets\\lift_custom.css";

            try
            {
                File.WriteAllText(serverFileLocation, this.lift_custom_css.Text);
                //Response.Write("The file has been updated.");
                this.status_label.Text = "The file has been updated.";
            }
            catch (Exception ex)
            {
                //Response.Write("Error: " + ex.Message);
                this.status_label.Text = "Error: " + ex.Message;
            }
        }
    }
}
