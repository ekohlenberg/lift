using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class WallSubscribe : System.Web.UI.Page
    {
        protected StringBuilder markup = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            long wallsOpen = 0;
            long wallsNowOpen = 0;
            string dayName = string.Empty;
            string action = string.Empty;
            string result = string.Empty;
            string tod = string.Empty;
            string dow = string.Empty;
            string my_tod = string.Empty;
            string my_dow = string.Empty;
            string my_time = string.Empty;
            string my_dayname = string.Empty;
            string time = string.Empty;
            string alreadySubscribedMarkup = string.Empty;
            string subscribedMarkup = string.Empty;
            string unsubscribedMarkup = string.Empty;


            alreadySubscribedMarkup =
                "myModalError.start(\"<h3><%=wall.already_subscribed%></h3><p><%=wall.already_subscribed_text%></br><%=wall.unsubscribe_text%><p><a href='#' onclick=\\\"updateIncrement('cell_<%=dow%>_<%=tod%>','unsubscribe_subscribe_to_increment','<%=dow%>', '<%=tod%>')\\\" ><%=wall.yes%></a> <a href='javascript:myModalError.end();'><%=wall.no%></a>\");";

            subscribedMarkup =
                "Element.update(\"cell_<%=dow%>_<%=tod%>\", \"<div class='slot-mine-no-move userSlot'><a href='javascript:void(0);' class='wall-remove-btn' title='<%=wall.unsubscribe_from_this_time%>' onclick=\\\"updateIncrement(this,'unsubscribe_from_increment','<%=dow%>', '<%=tod%>', '<%=my_dow%>', '<%=my_tod%>')\\\"></a><%=wall.my_time%><br /><span class='userTimeInfo'><%=dayName%> <%=time%></span></div>\");";

            unsubscribedMarkup =
                "Element.update(\"cell_<%=my_dow%>_<%=my_tod%>\", \"<span class='partialInfo'> <%=wallsNowOpen%> <%=wall.walls_open%><br /><span class='openTimeInfo'> <%=myDayName%>  <%=myTime%></span></span> <a class='addUser' title='<%=wall.subscribe_to_this_slot%>' onclick=\\\"updateIncrement(this,'subscribe_to_increment','<%=dow%>', '<%=tod%>')\\\" href='javascript:void(0);'></a>\");";

            action = Request["action"];
            tod = Request["tod"];
            dow = Request["dow"];


            Appt myTime = new Appt();

            myTime = myTime.doSingleObjectQuery<Appt>("get_my_time");
            if (myTime.Count > 0)
            {
                my_dow = myTime.getString("my_dow");
                my_tod = myTime.getString("my_tod");
            }
            else
            {
                my_dow = "-1";
                my_tod = "-1";
            }
            

            Appt a = new Appt();

            if (action == "subscribe_to_increment")
            {
                if (my_tod != "-1") 
                {
                    markup = new StringBuilder(alreadySubscribedMarkup);
                }
                else
                {
                    markup = new StringBuilder(subscribedMarkup);
                    a["dow"] = dow;
                    a["tod"] = tod;
                    a["user_id"] = LiftDomain.User.Current.id.Value;
                    wallsOpen = a.doCommand("subscribe");
                    dayName = Appt.getDay(dow);
                    time = Appt.getTime(tod);
                }
            }
            else if (action == "unsubscribe_subscribe_to_increment")
            {
                Appt unsub = new Appt();
                unsub["dow"] = my_dow;
                unsub["tod"] = my_tod;
                unsub["user_id"] = LiftDomain.User.Current.id.Value;

                markup = new StringBuilder(subscribedMarkup);
                markup.Append(unsubscribedMarkup);

                wallsNowOpen = unsub.doCommand("unsubscribe");

                Appt sub = new Appt();
                sub["dow"] = dow;
                sub["tod"] = tod;
                sub["user_id"] = LiftDomain.User.Current.id.Value;
                wallsOpen = sub.doCommand("subscribe");
                dayName = Appt.getDay(dow);
                time = Appt.getTime(tod);
                my_time = Appt.getTime(my_tod);
                my_dayname = Appt.getDay(my_dow);
            }
            else if (action == "unsubscribe_from_increment")
            {
                markup = new StringBuilder(unsubscribedMarkup);
                Appt unsub = new Appt();
                unsub["dow"] = my_dow;
                unsub["tod"] = my_tod;
                unsub["user_id"] = LiftDomain.User.Current.id.Value;
                wallsNowOpen = unsub.doCommand("unsubscribe");
                my_time = Appt.getTime(my_tod);
                my_dayname = Appt.getDay(my_dow);
            }


            
            replace(markup, "tod", tod);
            replace(markup, "dow", dow);
            replace(markup, "my_tod", my_tod);
            replace(markup, "my_dow", my_dow);
            replace(markup, "myTime", my_time);
            replace(markup, "myDayName", my_dayname);
            replace(markup, "dayName", dayName);
            replace(markup, "wallsNowOpen", wallsNowOpen);
            replace(markup, "wallsOpen", wallsOpen);
            replace(markup, "time", time);

            replace(markup, "wall.my_time", Language.Current.WALL_MY_TIME);
            replace(markup, "wall.already_subscribed", Language.Current.WALL_ALREADY_SUBSCRIBED);
            replace(markup, "wall.already_subscribed_text", Language.Current.WALL_ALREADY_SUBSCRIBED_TEXT);
            replace(markup, "wall.unsubscribe_text", Language.Current.WALL_UNSUBSCRIBE_TEXT);
            replace(markup, "wall.yes", Language.Current.WALL_YES);
            replace(markup, "wall.no", Language.Current.WALL_NO);
            replace(markup, "wall.unsubscribe_from_this_time", Language.Current.WALL_UNSUBSCRIBE_FROM_THIS_TIME);
            replace(markup, "wall.walls_open", Language.Current.WALL_WALLS_OPEN);
            replace(markup, "wall.subscribe_to_this_slot", Language.Current.WALL_SUBSCRIBE_TO_THIS_SLOT);


            Response.ContentType = "text/javascript";
        }

        protected void replace(StringBuilder s, string token, object o)
        {
            string macro = "<%=";
            macro += token;
            macro += "%>";

            string replText = "NULL";

            if (o != null)
            {
                if (!o.GetType().Equals(typeof(System.DBNull)))
                {
                    replText = o.ToString();
                }
            }

            s.Replace(macro, replText);
        }

        
    }




}
