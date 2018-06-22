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
    public class WallRenderer : PartialRenderer
    {
        protected EncouragementRenderer encouragementRenderer = null;
        protected string customPath = string.Empty;
        protected string appPath = string.Empty;
        protected string cellHtml = string.Empty;
        protected string myTimeHtml = string.Empty;
        protected ArrayList days = new ArrayList();

        public WallRenderer()
        {
            init();
        }

        public WallRenderer(DataSet ds)
        {

            mCtx = HttpContext.Current;
            mDs = ds;
            mFilename = "_Wall.htm";
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
            StringBuilder t = new StringBuilder();
            t.Append("<td id=\"cell_<%=dow%>_<%=tod%>\"  class=\"<%=cell_style%>\" rel=\"celllocator\" >\n");
            t.Append("<<%=wrapper_tag%> class=\"<%=span1_style%>\"> <%=avail_text%> <br />\n");
            t.Append("<span class=\"<%=span2_style%>\"><%=day_name%> <%=title%></span> </<%=wrapper_tag%>> <%=href%>\n");
            t.Append("</td>\n\n");

            cellHtml = t.ToString();

             StringBuilder t2 = new StringBuilder();
            t2.Append("<td id=\"cell_<%=dow%>_<%=tod%>\"  class=\"<%=cell_style%>\" rel=\"celllocator\" >\n");
            t2.Append("<div id=\"\" class=\"slot-mine-no-move userSlot\">\n");
            t2.Append("<a href=\"javascript:void(0);\" class=\"wall-remove-btn\" title=\"unsubscribe from this time\" onclick=\"updateIncrement(this,'unsubscribe_from_increment','1158')\"></a>\n");
            t2.Append("<%=wall.my_time%> <br />\n");
            t2.Append("<span class=\"userTimeInfo\"><%=day_name%> <%=title%></span>\n");
            t2.Append("</div>\n");
            t2.Append("</td>\n\n");

            myTimeHtml = t2.ToString();
        }

        public void render_helper(DataRow r, Hashtable h)
        {
            string cellStyle = "timeSlot-partial timeSlot";
            string span1Style = "partialInfo";
            string span2Style = "openTimeInfo";
            string dow = string.Empty;
            string tod = string.Empty;
            string availText = string.Empty;
            string dayName = string.Empty;
            string title = string.Empty;
            int totalWalls = 0;
            

            string my_tod = r["my_tod"].ToString();
            string my_dow = r["my_dow"].ToString();
            
            

            h["custom_path"] = customPath;
            h["app_path"] = appPath;

            totalWalls = Convert.ToInt32(r["total"]);
            tod = r["tod"].ToString();
            title = r["title"].ToString();

            // this loop assumes that data for the days of the week (dow)
            // are in the last 7 columns of the data set!
            int startDowIndex = r.Table.Columns.Count - 7;

            for (int i = 0; i < 7; i++)
            {
                string href = "<a class=\"addUser\" title=\"<%=wall.subscribe_to_this_slot%>\" onclick=\"updateIncrement(this,'subscribe_to_increment','<%=dow%>', '<%=tod%>')\" href=\"javascript:void(0);\"></a>\n";
                StringBuilder cellTemplate = new StringBuilder(cellHtml);
                string wrapperTag = "span";
                dayName = days[i].ToString();
                dow = (i+1).ToString();

                int colIndex = i + startDowIndex;
                DataColumn dc = r.Table.Columns[colIndex];
                string tag = dc.ColumnName + "_timeslot";


                if ((tod == my_tod) && (dow == my_dow))
                {
                    cellStyle = "timeSlot-partial timeSlot";
                    span1Style = "slot-mine-no-move userSlot";
                    span2Style = "userTimeInfo";
                    wrapperTag = "div";
                    availText = Language.Current.WALL_MY_TIME;
                    href = "<a href=\"javascript:void(0);\" class=\"wall-remove-btn\" title=\"<%=wall.unsubscribe_from_this_time%>\" onclick=\"updateIncrement(this,'unsubscribe_from_increment','<%=dow%>', '<%=tod%>')\"></a>\n";
                    cellTemplate = new StringBuilder(myTimeHtml);
                }
                else
                {
                    int wallsAvailable = Convert.ToInt32(r[colIndex]);

                    if (wallsAvailable >= totalWalls)  // should never be greater than
                    {
                        // TODO: Lang cvt availText
                        cellStyle = "timeSlot timeSlot";
                        span1Style = "openInfo";
                        span2Style = "openTimeInfo";
                        availText = " " + Language.Current.WALL_OPEN;

                    }
                    else if (wallsAvailable <= 0) // should never be less than 0
                    {
                        cellStyle = "timeSlot timeSlot";
                        span1Style = "slot-closed userSlot";
                        span2Style = "userTimeInfo";
                        availText = Language.Current.WALL_FULL;
                        wrapperTag = "div";
                        href = "";
                    }
                    else if ((wallsAvailable > 0) && (wallsAvailable < totalWalls))
                    {
                        cellStyle = "timeSlot-partial timeSlot";
                        span1Style = "partialInfo";
                        span2Style = "openTimeInfo";

                        if (wallsAvailable == 1)
                        {
                            availText = wallsAvailable.ToString() + " " + Language.Current.WALL_WALLS_OPEN;
                        }
                        else
                        {
                            availText = wallsAvailable.ToString() + " " + Language.Current.WALL_WALLS_OPEN;
                        }
                    }

                }
            

                replace(cellTemplate, "href", href);
                replace(cellTemplate, "dow", dow);
                replace(cellTemplate, "tod", tod);
                replace(cellTemplate, "cell_style", cellStyle);
                replace(cellTemplate, "span1_style", span1Style);
                replace(cellTemplate, "span2_style", span2Style);
                replace(cellTemplate, "day_name", dayName);
                replace(cellTemplate, "avail_text", availText);
                replace(cellTemplate, "title", title);
                replace(cellTemplate, "wrapper_tag", wrapperTag);

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
