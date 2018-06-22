using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class EditOrganizationImages : System.Web.UI.Page
    {
        static public ArrayList htmlInputFileArrayList = new ArrayList();

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
                //-- do the language settings for the buttons here
                //-- (e.g., unable to place <%=LiftDomain.Language.Current.SHARED_UPLOAD %> in asp:Button Text field)
                //-------------------------------------------------------------------------
                this.addBtn.Text = LiftDomain.Language.Current.ORGANIZATION_IMAGES_ADD_TO_LIST.Value;
                this.removeBtn.Text = LiftDomain.Language.Current.ORGANIZATION_IMAGES_REMOVE_FROM_LIST.Value;
                this.uploadBtn.Text = LiftDomain.Language.Current.ORGANIZATION_IMAGES_UPLOAD_TO_SERVER.Value;

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
                }

                //-------------------------------------------------------------------------
                //-- display list of server-side image files for this organization
                //-------------------------------------------------------------------------
                DisplayOrganizationImageList();

            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganizationImages.aspx.cs::Page_Load(): " + m);
                Logger.log("EditOrganizationImages.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganizationImages.aspx.cs::Page_Load(): " + m);
            }
            finally
            {
            }
        }

        protected void DisplayOrganizationImageList()
        {
            try
            {
                //-------------------------------------------------------------------------
                //-- display list of server-side image files for this organization
                //-------------------------------------------------------------------------
                this.organizationImageListTable.Rows.Clear();

                TableRow tr;
                TableCell tc1;
                TableCell tc2;

                tr = new TableRow();
                tc1 = new TableCell();
                tc2 = new TableCell();

                tc1.Text = "";
                tc2.Text = LiftDomain.Language.Current.ORGANIZATION_IMAGE_FILE_NAME;
                tc2.Font.Bold = true;
                tr.Cells.Add(tc1);
                tr.Cells.Add(tc2);
                this.organizationImageListTable.Rows.Add(tr);

                string serverImageDirectory = Server.MapPath("/custom/" + this.subdomain.Value + "/images/");

                DirectoryInfo di = new DirectoryInfo(serverImageDirectory);
                FileInfo[] fiArray = di.GetFiles();

                foreach (FileInfo fi in fiArray)
                {
                    tr = new TableRow();
                    tc1 = new TableCell();
                    tc2 = new TableCell();

                    tc1.Text = "<a href=\"DeleteOrganizationImage.aspx?filename=" + Server.UrlEncode(serverImageDirectory + fi.Name) + "&redirect_to_page=EditOrganizationImages.aspx?id=" + id.Value + "\" onclick=\"javascript:return confirm('" + LiftDomain.Language.Current.DELETE_ORGANIZATION_IMAGE_CONFIRMATION + "')\"" + " title=\"" + LiftDomain.Language.Current.DELETE_ORGANIZATION_IMAGE_CAPTION + "\">" + LiftDomain.Language.Current.SHARED_DELETE.Value + "</a>";
                    tc2.Text = fi.Name;
                    tr.Cells.Add(tc1);
                    tr.Cells.Add(tc2);
                    this.organizationImageListTable.Rows.Add(tr);
                }
            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganizationImages.aspx.cs::DisplayOrganizationImageList(): " + m);
                Logger.log("EditOrganizationImages.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN EditOrganizationImages.aspx.cs::DisplayOrganizationImageList(): " + m);
            }
            finally
            {
            }
        }

        protected void uploadBtn_Click(object sender, EventArgs e)
        {
            string serverFileLocation = Server.MapPath("/custom/" + this.subdomain.Value + "/images/");


            //if ((File1.PostedFile != null) & File1.PostedFile.ContentLength > 0)
            //{
            //    string basePostedFileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);

            //    try
            //    {
            //        for (int i = 0; i < this.fileListBox.Items.Count; i++)
            //        {
            //            File1.PostedFile.SaveAs(serverFileLocation);
            //        }
            //        //Response.Write("The file has been uploaded.");
            //        this.status_label.Text = "The file has been uploaded.";
            //    }
            //    catch (Exception ex)
            //    {
            //        //Response.Write("Error: " + ex.Message);
            //        this.status_label.Text = "Error: " + ex.Message;
            //    }
            //}
            //else
            //{
            //    //Response.Write("Please select a file to upload.");
            //    this.status_label.Text = "Please select a file to upload.";
            //}

            int filesUploaded = 0;
            string basePostedFileName = string.Empty;
            string statusMessage = string.Empty;

            foreach (System.Web.UI.HtmlControls.HtmlInputFile thisHtmlInputFile in htmlInputFileArrayList)
            {
                try
                {
                    basePostedFileName = System.IO.Path.GetFileName(thisHtmlInputFile.PostedFile.FileName);
                    if (!String.IsNullOrEmpty(basePostedFileName))
                    {
                        thisHtmlInputFile.PostedFile.SaveAs(serverFileLocation + basePostedFileName);
                        filesUploaded++;
                        statusMessage += basePostedFileName + "<br>";
                    }
                }
                catch (Exception err)
                {
                    this.status_label.Text = "Error saving file: " + basePostedFileName + "<br><br>" + err.ToString();
                }
            }

            if (filesUploaded == htmlInputFileArrayList.Count)
            {
                this.status_label.Text = "These " + filesUploaded + " file(s) were uploaded:<br><br>" + statusMessage;
            }

            //-------------------------------------------------------------------------
            //-- display list of server-side image files for this organization
            //-------------------------------------------------------------------------
            DisplayOrganizationImageList();

        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack == true)
            {
                if (!String.IsNullOrEmpty(File1.PostedFile.FileName))
                {
                    htmlInputFileArrayList.Add(File1);
                    fileListBox.Items.Add(File1.PostedFile.FileName);
                }
            }
        }

        protected void removeBtn_Click(object sender, EventArgs e)
        {
            if (fileListBox.Items.Count != 0)
            {
                htmlInputFileArrayList.RemoveAt(fileListBox.SelectedIndex);
                fileListBox.Items.Remove(fileListBox.SelectedItem.Text);
            }
        }

    }
}
