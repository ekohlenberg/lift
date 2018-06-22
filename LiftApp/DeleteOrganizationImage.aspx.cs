using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class DeleteOrganizationImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileNameToDelete = string.Empty;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

						PageAuthorized.check(Request, Response);

            fileNameToDelete = Server.UrlDecode(Request["filename"]);

            if (!String.IsNullOrEmpty(fileNameToDelete) && File.Exists(fileNameToDelete))
            {
                try
                {
                    File.Delete(fileNameToDelete);

                    Response.Redirect(Request["redirect_to_page"]);

                    //Response.ContentType = "text/javascript";
                }
                catch(Exception x)
                {
                    Logger.log(fileNameToDelete, x, "Error deleting organization image");
                }
            }
        }
    }
}
