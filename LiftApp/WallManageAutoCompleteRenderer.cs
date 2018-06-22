using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Text;


using LiftCommon;
using LiftDomain;


namespace liftprayer
{
    public class WallManageAutoCompleteRenderer : PartialRenderer
    {
        protected string unsubscribedUser = @"<li class=""userlist""><a href=""#"" title=""add this user"" onclick=""myWalladmin.addExistingUser('<%=first_name%>','<%=last_name%>','<%=user_id%>', '<%=wall_id%>', '<%=dow%>', '<%=tod%>'); return false;""><%=first_name%> <%=last_name%></a></li>";

        protected string subscribedUser = @"<li class=""userlist""><a href=""#"" title=""add this user"" onclick=""myWalladmin.transferUser('<%=first_name%>','<%=last_name%>','<%=user_id%>', '<%=wall_id%>', '<%=dow%>', '<%=tod%>', '<%=subscriber_day%>', '<%=subscriber_wall%>'); return false;""><%=first_name%> <%=last_name%> </a></li>";


        public string wallId = string.Empty;
        public string dow = string.Empty;
        public string tod = string.Empty;

        public WallManageAutoCompleteRenderer(DataSet ds)
        {

            mCtx = HttpContext.Current;
            mDs = ds;
            mFilename = "_WallManageAutoComplete.htm";
            mRh = new RenderHelper(render_helper);
        }

        protected void render_helper(DataRow r, Hashtable h)
        {
            StringBuilder markup;
            string subscriberWall = string.Empty;
            string subscriberDay = string.Empty;

            int apptId = Convert.ToInt32(r["appt_id"]);

            if (apptId == 0)
            {
                markup = new StringBuilder(unsubscribedUser);
            }
            else
            {
                markup = new StringBuilder(subscribedUser);
                subscriberWall = r["wall_title"].ToString();
                subscriberDay = LiftDomain.Appt.getDay(r["dow"]);
            }

            replace(markup, "subscriber_day", subscriberDay);
            replace(markup, "subscriber_wall", subscriberWall);
            replace(markup, "wall_id", wallId);
            replace(markup, "dow", dow);
            replace(markup, "tod", tod);
            replace(markup, "first_name", r["first_name"]);
            replace(markup, "last_name", r["last_name"]);
            replace(markup, "email", r["email"]);
            replace(markup, "user_id", r["user_id"]);

            h["markup"] = markup;

        }
    }
}
