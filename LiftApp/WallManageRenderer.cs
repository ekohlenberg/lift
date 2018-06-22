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
    public class WallManageRenderer : PartialRenderer
    {
        protected EncouragementRenderer encouragementRenderer = null;
        protected string customPath = string.Empty;
        protected string appPath = string.Empty;
        protected string cellHtml = string.Empty;
        protected string openHtml = string.Empty;
        protected ArrayList days = new ArrayList();

        public WallManageRenderer()
        {
            init();
        }

        public WallManageRenderer(DataSet ds)
        {

            mCtx = HttpContext.Current;
            mDs = ds;
            mFilename = "_WallManage.htm";
            mRh = new RenderHelper(render_helper);
            init();
         
        }

        protected void init()
        {
            setCustomPath();
            setAppPath();
            setHtmlTemplate();
            setDays();
        }

        protected void setCustomPath()
        {
            Organization org = Organization.Current;
            if (org != null)
            {
                customPath = "/custom/";
                customPath += org.subdomain;
            }
        }

        protected void setDays()
        {
            days.Add(Language.Current.SHARED_SUNDAY);
            days.Add(Language.Current.SHARED_MONDAY);
            days.Add(Language.Current.SHARED_TUESDAY);
            days.Add(Language.Current.SHARED_WEDNESDAY);
            days.Add(Language.Current.SHARED_THURSDAY);
            days.Add(Language.Current.SHARED_FRIDAY);
            days.Add(Language.Current.SHARED_SATURDAY);

        }


        protected void setAppPath()
        {
            appPath = HttpContext.Current.Request.ApplicationPath;
            if (appPath.Length > 1) appPath += "/";
        }

        protected void setHtmlTemplate()
        {

            cellHtml = @"<td id=""cell_<%=wall_id%>_<%=dow%>_<%=tod%>"" class=""timeSlot"" title="""">
				<!--add button and cell day and time-->
				<span class=""timeInfo""><%=dayName%> <%=title%></span>
				<a class=""addUser"" title=""add a new user to this slot"" onclick=""myWalladmin.newUserPopup(this);"" href=""javascript:void(0);""></a>			
				<div id=""user_<%=user_id%>"" class=""slot-user userSlot"">
					<a href=""javascript:void(0);"" class=""wall-remove-btn"" title=""remove from this slot"" onclick=""myWalladmin.removeUser(this, <%=user_id%>);""></a>
					<span class=""userCardName""><%=appt%></span>
					<br />
					<span class=""userTimeInfo""><%=dayName%> <%=title%></span>
				</div>
		    </td>";

            openHtml = @"<td id=""cell_<%=wall_id%>_<%=dow%>_<%=tod%>"" class=""timeSlot"" title="""">
				        <a class=""addUser"" title=""add a new user to this slot"" onclick=""myWalladmin.newUserPopup(this);"" href=""javascript:void(0);""></a>
				        <span class=""timeInfo""><%=dayName%> <%=title%></span>
		                </td>";
        	 
        }

        public void render_helper(DataRow r, Hashtable h)
        {
            string dow = string.Empty;
            string tod = string.Empty;
            string availText = string.Empty;
            string dayName = string.Empty;
            string title = string.Empty;
            string wallId = string.Empty;

            h["custom_path"] = customPath;
            h["app_path"] = appPath;

            tod = r["tod"].ToString();
            title = r["title"].ToString();
            wallId = r["wall_id"].ToString();
       

            // this loop assumes that data for the days of the week (dow)
            // are in the last 7 columns of the data set!
            int startDowIndex = r.Table.Columns.Count - 7;

            for (int i = 0; i < 7; i++)
            {
                StringBuilder cellTemplate;
                int colIndex = i + startDowIndex;
                string subscriber = r[colIndex].ToString();

                if (string.IsNullOrEmpty(subscriber))
                {
                    cellTemplate = new StringBuilder(openHtml);
                }
                else
                {
                    cellTemplate = new StringBuilder(cellHtml);
                }

                dayName = days[i].ToString();
                dow = (i+1).ToString();

               
                DataColumn dc = r.Table.Columns[colIndex];
                string tag = dc.ColumnName + "_timeslot";
                string userIdCol = dc.ColumnName + "_user_id";

                replace(cellTemplate, "wall_id", wallId);
                replace(cellTemplate, "appt", subscriber);
                replace(cellTemplate, "user_id", r[userIdCol]);

                replace(cellTemplate, "dow", dow);
                replace(cellTemplate, "tod", tod);
                replace(cellTemplate, "title", title);
                replace(cellTemplate, "dayName", dayName);

                replace(cellTemplate, "wall.my_time", Language.Current.WALL_MY_TIME);
                replace(cellTemplate, "wall.already_subscribed", Language.Current.WALL_ALREADY_SUBSCRIBED);
                replace(cellTemplate, "wall.already_subscribed_text", Language.Current.WALL_ALREADY_SUBSCRIBED_TEXT);
                replace(cellTemplate, "wall.unsubscribe_text", Language.Current.WALL_UNSUBSCRIBE_TEXT);
                replace(cellTemplate, "wall.yes", Language.Current.WALL_YES);
                replace(cellTemplate, "wall.no", Language.Current.WALL_NO);
                replace(cellTemplate, "wall.unsubscribe_from_this_time", Language.Current.WALL_UNSUBSCRIBE_FROM_THIS_TIME);
                replace(cellTemplate, "wall.walls_open", Language.Current.WALL_WALLS_OPEN);
                replace(cellTemplate, "wall.subscribe_to_this_slot", Language.Current.WALL_SUBSCRIBE_TO_THIS_SLOT);


                h[tag] = cellTemplate.ToString();

            }


        }

    }
}
