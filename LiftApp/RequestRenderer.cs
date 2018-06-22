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
    public class RequestRenderer : PartialRenderer
    {
        protected EncouragementRenderer encouragementRenderer = null;
        bool mShowLinks = true;
        bool mShowActive = true;
        protected string customPath = string.Empty;
        protected string inPlaceEditHtml = string.Empty;
        protected string normalHtml = string.Empty;
        protected string deleteHtml = string.Empty;
        protected string subscribeHtml = string.Empty;
        protected string subdomain = string.Empty;

        public RequestRenderer()
        {
            setCustomPath();
            setHtml();
        }

        public RequestRenderer(DataSet ds)
        {

            mCtx = HttpContext.Current;
            mDs = ds;
            mFilename = "_request.htm";
            mRh = new RenderHelper(render_helper);
            setCustomPath();
            setHtml();
        }

        protected void setCustomPath()
        {
            Organization org = Organization.Current;
            if (org != null)
            {
                customPath = "/custom/";
                subdomain = org.subdomain;
                customPath += subdomain;
                
            }
        }

        protected void setHtml()
        {
            inPlaceEditHtml = @"<span class=""in_place_editor_field"" id=""request_description_<%=id%>_in_place_editor""><%=description%></span><script type=""text/javascript"">
                                //<![CDATA[
                                new Ajax.InPlaceEditor('request_description_<%=id%>_in_place_editor', '<%=app_path%>InPlaceEditRequest.aspx?id=<%=id%>', {rows:10})
                                //]]>
                                </script>";

            normalHtml = "<%=description%>";

            if (mShowActive)
            {
                deleteHtml = @"<li id=""req_delete<%=id%>"" class=""delete-it"" > 
                            <a href=""#"" onclick=""if (confirm('<%=confirm_recycle%>')) { new Ajax.Request('DeleteRequest.aspx?id=<%=id%>&action=recycle', {asynchronous:true, evalScripts:true}); }; return false;""><%=recycle%></a> 
                            </li>";
            }
            else
            {
                deleteHtml = @"<li id=""req_delete<%=id%>"" class=""delete-it"" > 
                            <a href=""#"" onclick=""if (confirm('<%=confirm_restore%>')) { new Ajax.Request('DeleteRequest.aspx?id=<%=id%>&action=restore', {asynchronous:true, evalScripts:true}); }; return false;""><%=restore%></a> 
                            </li>";

                deleteHtml += @"<li id=""req_delete<%=id%>"" class=""delete-it"" > 
                            <a href=""#"" onclick=""if (confirm('<%=confirm_delete%>')) { new Ajax.Request('DeleteRequest.aspx?id=<%=id%>&action=delete', {asynchronous:true, evalScripts:true}); }; return false;""><%=delete%></a> 
                            </li>";
            }

            subscribeHtml = @"<li id=""subscribe_<%=id%>""><img src=""<%=custom_path%>/images/file_add.gif"" alt=""subscribe image"" />
                               <a href=""#"" onclick=""new Ajax.Request('<%=app_path%>Subscribe.aspx?id=<%=id%>', {asynchronous:true, evalScripts:true, onLoading:function(request){Element.replace(&quot;subscribe_<%=id%>&quot;, &quot;&lt;li&gt;&lt;img src='<%=custom_path%>/images/file_apply.gif' alt='subscribed image'/&gt;subscribed&lt;/li&gt;&quot;);}}); return false;"" title=""updates/comments for this prayer request will be sent to you via email""><%=keep_me_posted%></a>
                             </li>";
        }

        public bool ShowUpdates
        {
            get
            {
                bool result = false;
                if (encouragementRenderer != null)
                {
                    result = true;
                }
                return result;
            }
            set
            {
                if (value == true)
                {
                    encouragementRenderer = new EncouragementRenderer();
                }
                else
                {
                    encouragementRenderer = null;
                }
            }
        }

        public bool ShowLinks
        {
            get
            {
                return mShowLinks;
            }
            set
            {
                mShowLinks = value;
            }
        }

        public bool ShowActive
        {
            get
            {
                return mShowActive;
            }
            set
            {
                mShowActive = value;
                setHtml();
            }
        }

        protected override bool canRender()
        {
            bool result = true;

            Organization o = Organization.Current;
            if (o.default_signup_mode.Value == (int)UserSignupMode.user_requires_approval)
            {
                if (User.IsLoggedIn)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
        
        public void render_helper(DataRow r, Hashtable h)
        {
            StringBuilder descMarkup = null;
            StringBuilder deleteMarkup = null;
            StringBuilder subscribeMarkup = null;
            string requestTitle = string.Empty;
            string title = string.Empty;

            string authFrom = string.Empty;

            r["requesttype_title"] = Language.Current.phrase(r["requesttype_title"].ToString());

            if (r.Table.Columns.Contains("from"))
            {
                authFrom = r["from"].ToString();
            }

            title = r["title"].ToString();
            requestTitle = title + " - " + r["requesttype_title"].ToString();

            if (!LiftDomain.User.IsLoggedIn)
            {
                try
                {
                    string[] parts = authFrom.Split(new char[] { ' ' });
                    string newFrom = string.Empty;
                    if (parts.Length > 1)
                    {
                        for (int m = 0; m < parts.Length; m++)
                        {
                            if (m > 0) newFrom += " ";
                            if (m == parts.Length - 1)
                            {
                                newFrom += parts[m].Substring(0, 1);
                            }
                            else
                            {
                                newFrom += parts[m];
                            }
                        }

                        authFrom = newFrom;
                    }
                }
                catch
                {
                }

                if (subdomain == "upward")
                {
                    requestTitle = title;
                }
                else
                {
                    requestTitle = r["requesttype_title"].ToString();
                }
                
                
            }


            h["request_title"] = requestTitle;

            h["auth_from"] = authFrom;

            h["custom_path"] = customPath;
            
            h["last_updated_about"] = LiftTime.aboutTime(LiftTime.toUserTime((DateTime)r["last_action"]));


           
            h.Add("app_path", AppPath);

            h["updates_comments"] = (string)Language.Current.REQUEST_PRAISES_UPDATES_COMMENTS;
            h["keep_me_posted"] = (string)Language.Current.REQUEST_KEEP_ME_POSTED;
            h["submitted_by"] = (string)Language.Current.REQUEST_SUBMITTED_BY;

            if (Convert.ToInt32(r["listed"]) == 0)
            {
                h["private"] = "<div id=\"private\">Private</div>";
            }
            else
            {
                h["private"] = "";
            }

            int total_requests = Convert.ToInt32(r["total_requests"]);
            if (total_requests == 1)
            {
                h["prayers"] = (string)Language.Current.REQUEST_PRAYER;
            }
            else
            {
                h["prayers"] = (string)Language.Current.REQUEST_PRAYERS;
            }

            h["pray"] = (string)Language.Current.REQUEST_PRAY;

            h["prdetails_style"] = "";
            if (!mShowLinks)
            {
                h["prdetails_style"] = "style=\"display: none\"";
            }

            h["update_html"] = "";
            if (encouragementRenderer != null)
            {
                if (r.Table.Columns.Contains("enc_note"))
                {
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
                   // h["enc_type_name"] = (t == 1 ? Language.Current.REQUEST_COMMENT : Language.Current.REQUEST_UPDATE);

                    h["enc_about_time"] = LiftTime.aboutTime(LiftTime.toUserTime((DateTime)r["enc_last_updated"]));
                    string note = r["enc_note"].ToString();
                    if (note.Length > 0)
                    {
                        h["update_html"] = encouragementRenderer.render(r, h);
                    }
                }
            }

            string approveMarkup = "<li id=\"approveRequest${id}\" class=\"mark-it-approved\" > <a id=\"a${id}\" href=\"#a${id}\" onclick=\"new Ajax.Request('${app_path}ApproveRequest.aspx?id=${id}&ap=1', { asynchronous: true, evalScripts: true }); return false;\" title=\"${approve_title}\">${approve}</a> </li>";
            //string inappropriateMarkup = "<a id=\"a${id}\" href=\"#a${id}\" onclick=\"new Ajax.Request('${app_path}ApproveRequest.aspx?id=${id}&ap=0', { asynchronous: true, evalScripts: true }); return false;\" title=\"${inappropriate_title}\">${inappropriate}</a>";
            string inappropriateMarkup = " | <a id=\"a${id}\" href=\"${app_path}ReportRequest.aspx?id=${id}\" title=\"${inappropriate_title}\">${inappropriate}</a>";
            string awaitingApproval = "<strong id=\"awaitingApprovalMessage${id}\" style=\"color:#ff0000;\"> - ${awaiting_approval}</strong>";

            if ((Convert.ToInt32(r["is_approved"]) == 0) && (LiftDomain.User.Current.canApproveRequests))
            {
                approveMarkup = approveMarkup.Replace("${id}", r["id"].ToString());
                approveMarkup = approveMarkup.Replace("${app_path}", AppPath);
                approveMarkup = approveMarkup.Replace("${approve}", Language.Current.REQUEST_APPROVE);
                approveMarkup = approveMarkup.Replace("${approve_title}", Language.Current.REQUEST_APPROVE_TITLE);
                awaitingApproval = awaitingApproval.Replace("${id}", r["id"].ToString());
                awaitingApproval = awaitingApproval.Replace("${awaiting_approval}", Language.Current.REQUEST_AWAITING_APPROVAL);
               
            }
            else
            {
                approveMarkup = "";
                awaitingApproval = "";
                
            }

            if (LiftDomain.User.Current.canApproveRequests)
            {
                descMarkup = new StringBuilder(inPlaceEditHtml);
                deleteMarkup = new StringBuilder(deleteHtml);
            }
            else
            {
                descMarkup = new StringBuilder(normalHtml);
                deleteMarkup = new StringBuilder("");
            }

            if (Organization.Current.default_approval.Value == 1)
            {
                inappropriateMarkup = inappropriateMarkup.Replace("${inappropriate}", Language.Current.REQUEST_REPORT_ABUSE);
                inappropriateMarkup = inappropriateMarkup.Replace("${inappropriate_title}", Language.Current.REQUEST_INAPPROPRIATE_TITLE);
                inappropriateMarkup = inappropriateMarkup.Replace("${app_path}", AppPath);
                inappropriateMarkup = inappropriateMarkup.Replace("${id}", r["id"].ToString());
            }
            else
            {
                inappropriateMarkup = string.Empty;
            }
            

            replace(descMarkup, "id", r["id"]);
            replace(descMarkup, "app_path", AppPath);
            replace(descMarkup, "description", r["description"]);

            replace(deleteMarkup, "id", r["id"]);
            replace(deleteMarkup, "confirm_delete", Language.Current.REQUEST_CONFIRM_DELETE);
            replace(deleteMarkup, "confirm_restore", Language.Current.REQUEST_CONFIRM_RESTORE);
            replace(deleteMarkup, "confirm_recycle", Language.Current.REQUEST_CONFIRM_RECYCLE);
            replace(deleteMarkup, "restore", Language.Current.REQUEST_RESTORE);
            replace(deleteMarkup, "recycle", Language.Current.REQUEST_RECYCLE);
            replace(deleteMarkup, "delete", Language.Current.REQUEST_DELETE);

            
            h["description_html"] = descMarkup.ToString();
            h["approve_markup"] = approveMarkup;
            h["inappropriate_markup"] = inappropriateMarkup;
            h["awaiting_approval"] = awaitingApproval;
            h["delete_markup"] = deleteMarkup.ToString();
            h["active"] = (mShowActive ? "1" : "0");

            string editMarkup = "<li id=\"editRequest${id}\" class=\"edit-it\" > <a id=\"a${id}\" href=\"EditRequest.aspx?id=${id}\"  title=\"${edit_title}\">${edit_label}</a> </li>";

            if (LiftDomain.User.Current.canEditRequest(r["user_id"]))
            {
                editMarkup = editMarkup.Replace("${id}", r["id"].ToString());
                editMarkup = editMarkup.Replace("${edit_label}", Language.Current.REQUEST_EDIT);
                editMarkup = editMarkup.Replace("${edit_title}", Language.Current.REQUEST_EDIT_TITLE);
            }
            else
            {
                editMarkup = "";
            }


            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {

                subscribeMarkup = new StringBuilder(subscribeHtml);
                replace(subscribeMarkup, "id", r["id"]);
                replace(subscribeMarkup, "app_path", AppPath);
                replace(subscribeMarkup, "keep_me_posted", Language.Current.REQUEST_KEEP_ME_POSTED);
         
                h["subscribe_html"] = subscribeMarkup.ToString();
            }
            else
            {
                h["subscribe_html"] = "";
            }

            h["edit_markup"] = editMarkup;



        }

       

    }
}
