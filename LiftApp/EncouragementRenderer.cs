using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public class EncouragementRenderer  : PartialRenderer
    {
        RenderHelper mRenderHelperDelegate;

        string inPlaceEditHtml = @"
<span class=""in_place_editor_field"" id=""encouragement_Note_<%=enc_id%>_in_place_editor""><%=enc_note%></span>
				<script type=""text/javascript"">
      //<![CDATA[
     new Ajax.InPlaceEditor('encouragement_Note_<%=enc_id%>_in_place_editor', '<%=app_path%>InPlaceEditUpdate.aspx?id=<%=enc_id%>', { rows: 10 })
//]]>
</script>";

        public EncouragementRenderer()
            :base()
        {
            mRenderHelperDelegate = new RenderHelper(render_helper);
            mCtx = HttpContext.Current;
            mFilename = "_updateRequest.htm";
            mRh = mRenderHelperDelegate;
        }

        public EncouragementRenderer( DataSet ds)
        {
            mRenderHelperDelegate = new RenderHelper(render_helper);

            mCtx = HttpContext.Current;
            mDs = ds;
            mFilename = "_updateRequest.htm";
            mRh = mRenderHelperDelegate ;
        }

        public DataSet DataSource
        {
            set
            {
                mDs = value;
            }
            get
            {
                return mDs;
            }
        }

        public string Filename
        {
            set
            {
                mFilename = value;
            }
            get
            {
                return mFilename;
            }
        }

        public bool HasData
        {
            get
            {
                bool result = false;
                if (mDs != null)
                {
                    if (mDs.Tables.Count > 0)
                    {
                        if (mDs.Tables[0].Rows.Count > 0)
                        {
                            result = true;
                        }
                    }
                }
                return result;
            }
        }

        public  void render_helper(DataRow r, Hashtable h)
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            if (appPath.Length > 1) appPath += "/";

            StringBuilder descMarkup = new StringBuilder("<%=enc_note%>");

            if (!h.ContainsKey("app_path"))
            {
                h.Add("app_path", appPath);
            }

            h["enc_about_time"] = LiftTime.aboutTime(LiftTime.toUserTime((DateTime)r["enc_post_date"]));

            int encTypeNum = Convert.ToInt32(r["enc_type"]);
            EncouragementType encType = (EncouragementType)Convert.ToInt32(r["enc_type"]);
            string encTypeStr = encType.ToString().ToLower();
            h["enc_type_name"] = LiftDomain.Language.Current["request." + encType.ToString().ToLower()].ToString();
 /*
            int t = Convert.ToInt32(r["enc_type"]);
           
           
            switch (t)
            {
                case 0:
                    h["enc_type_name"] = LiftDomain.Language.Current.REQUEST_UPDATE.Value;
                    break;
                case 1:
                    h["enc_type_name"] = LiftDomain.Language.Current.REQUEST_COMMENT.Value;
                    break;
                case 2:
                    h["enc_type_name"] = LiftDomain.Language.Current.REQUEST_PRAISE.Value;
                    break;
            }
            */

            string awaitingApproval = "";

            if ((Convert.ToInt32(r["enc_is_approved"]) == 0) && (LiftDomain.User.Current.canApproveRequests))
            {
                awaitingApproval = "<p id=\"updateAwaitingApprovalMessage${enc_request_id}\" ><span class=\"update_emphasize\"><strong style=\"color:#ff0000;\">${awaiting_approval}</strong></span></p>";
                awaitingApproval = awaitingApproval.Replace("${enc_request_id}", r["enc_request_id"].ToString());
                awaitingApproval = awaitingApproval.Replace("${awaiting_approval}", LiftDomain.Language.Current.REQUEST_AWAITING_APPROVAL);
            }
            else
            {
                awaitingApproval = "";
            }

            if (LiftDomain.User.Current.canApproveRequests)
            {
                descMarkup = new StringBuilder(inPlaceEditHtml);
            }

            object encId = r["enc_id"];

            replace(descMarkup, "enc_id", encId );
            replace(descMarkup, "enc_note", r["enc_note"]);
            replace(descMarkup, "app_path", appPath);

            h["note_markup"] = descMarkup.ToString();
            h["enc_awaiting_approval"] = awaitingApproval;
        }

        public string render(DataRow r, Hashtable h)
        {
            return base.render(HttpContext.Current, r, h, "_updateRequest.htm", mRenderHelperDelegate);
            
        }
        
    }

   

  
}
