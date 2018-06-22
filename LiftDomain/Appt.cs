using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using LiftCommon;

namespace LiftDomain
{
    public class Appt : BaseLiftDomain 
    {
        public IntProperty id = new IntProperty();
        public IntProperty user_id = new IntProperty();
        public IntProperty wall_id = new IntProperty();
        public UserTimeProperty tod_utc = new UserTimeProperty();

        protected string headerMsg = 
@"Thank you for being a watchman on the prayer wall!
Your Prayer Time is:
<%=day_name%> <%=time%>

";

        protected string unsubscribeHeaderMsg =
@"Thank you for being a watchman on the prayer wall!
You have been unscribed from:
<%=day_name%> <%=time%>

";

        protected string recvWatchMsg = 
@"You will be receiving your watch from
<%=prev_first_name%> <%=prev_last_name%>
<%=prev_email%>
<%=prev_phone%>

";

        protected string giveWatchMsg =
@"You will be giving your watch to
<%=next_first_name%> <%=next_last_name%>
<%=next_email%>
<%=next_phone%>

";


        protected string noLongerRecvWatchMsg =
@"You will no longer be receiving your watch from
<%=prev_first_name%> <%=prev_last_name%>
<%=prev_email%>
<%=prev_phone%>

";

        protected string noLongerGiveWatchMsg =
@"You will no longer be giving your watch to
<%=next_first_name%> <%=next_last_name%>
<%=next_email%>
<%=next_phone%>

";
        protected string footerMsg =
@"
Thank you for being faithful in your watch!
";
        public Appt()
		{
			BaseTable = "appts";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("id", id);
			attach("user_id", user_id);
            attach("wall_id", wall_id);
            attach("tod_utc", tod_utc);
                        
		}

        public DataSet get_stats()
        {
            DataSet result = new DataSet();

            Appt a = new Appt();
            a["organization_id"] = Organization.Current.id.Value;
            a["tzoffset"] = LiftTime.UserTzOffset;
            a["user_id"] = User.Current.id.Value;

            result = a.doQuery("get_stats_internal");


            return result;
        }

        public static string ApptTime
        {
            get
            {
                string result = string.Empty;

                User u = User.Current;

                if (u.id.Value > 0)
                {
                    Appt a = new Appt();
                    a["user_id"] = u.id.Value;
                    try
                    {
                        // TODO - Lang neutralize this section
                        ModelObject timeSlot = a.doSingleObjectQuery(typeof(ModelObject), "get_for_user");
                        string apptTime = Language.Current.WALL_MY_PRAYER_TIME;
                        apptTime += ": <b>";
                        apptTime +=timeSlot.getString("wall_title");
                        apptTime += ", ";
                        DateTime tod = LiftTime.toUserTime(timeSlot.getDateTime("tod_utc"));
                        string phraseLabel = "shared.";
                        phraseLabel += tod.ToString("dddd").ToLower();
                        apptTime += Language.Current.phrase(phraseLabel);
                        apptTime += ", ";
                        apptTime += tod.ToString("t");
                        apptTime += "</b>";
                        result = apptTime;
                    }
                    catch (ModelObjectException)
                    {
                    }
                    catch (Exception x)
                    {
                        Logger.log(a, x, "Error while retrieving appt time.");
                    }

                }

                return result;
            }
        }

        public long subscribe()
        {
            long remainingWalls = -1;
            DataSet availableWalls = new DataSet();
            int currentWallId = 0;

            Appt a = new Appt();
            a["organization_id"] = Organization.Current.id.Value;
            a["tzoffset"] = LiftTime.UserTzOffset;
            a["user_id"] = getInt("user_id");
            a["tod"] = getInt("tod");
            a["dow"] = getInt("dow");

            if (ContainsKey("wall_id"))
             
            {
                currentWallId = getInt("wall_id");
            }
            else
            {
                availableWalls = a.doQuery("get_available_walls");
                if (availableWalls.Tables.Count > 0)
                {
                    if (availableWalls.Tables[0].Rows.Count > 0)
                    {
                        remainingWalls = availableWalls.Tables[0].Rows.Count - 1;
                        currentWallId = Convert.ToInt32(availableWalls.Tables[0].Rows[0][0]);

                
                    }
                }
            }

            if (currentWallId > 0)
            {
                Appt timeSlot = new Appt();

                DateTime todUtcTime = new DateTime(2009, 3, getInt("dow"), getInt("tod"), 0, 0);

                timeSlot.user_id.Value = getInt("user_id");
                timeSlot.tod_utc.Value = todUtcTime;
                timeSlot.wall_id.Value = currentWallId;
                timeSlot.doCommand("insert");

                notifyAdjacent(currentWallId, getInt("user_id"), getInt("dow"), getInt("tod"), true);
            }
            
           
            return remainingWalls;
        }

        public void notifyAdjacent(int currentWallId, int userId, int dow, int tod, bool subscribe)
        {
            string header = headerMsg;
            string recvMsg = string.Empty;
            string giveMsg = string.Empty;
            string subj = string.Empty;
            string thisUserHeader = string.Empty;

            if (subscribe)
            {
                recvMsg = recvWatchMsg;
                giveMsg = giveWatchMsg;
                subj = "New Watchman Notification";
                thisUserHeader = headerMsg;
            }
            else
            {
                recvMsg = noLongerRecvWatchMsg;
                giveMsg = noLongerGiveWatchMsg;
                subj = "Watchman Change Notifcation";
                thisUserHeader = unsubscribeHeaderMsg;
            }


            OrgEmail oe = new OrgEmail();
            oe.organization_id.Value = Organization.Current.id.Value;
            oe = oe.doSingleObjectQuery<OrgEmail>("select");

            int prevDow = 0;
            int prevTod = 0;
            int nextDow = 0;
            int nextTod = 0;

            calcPrev(dow, tod, ref prevDow, ref prevTod);
            calcNext(dow, tod, ref nextDow, ref nextTod);

            Appt adj = new Appt();
            adj["wall_id"] = currentWallId;
            adj["next_tod"] = nextTod;
            adj["next_dow"] = nextDow;
            adj["prev_tod"] = prevTod;
            adj["prev_dow"] = prevDow;

            adj["tzoffset"] = LiftTime.UserTzOffset;

            DataSet neighbors = adj.doQuery("get_adjacent");
            User thisUser = new User();
            thisUser.id.Value = userId;
            thisUser = thisUser.doSingleObjectQuery<User>("getobject");

            User prior = null;
            User next = null;
            
            if (DatabasePersist.hasData(neighbors))
            {
                foreach (DataRow neighbor in neighbors.Tables[0].Rows)
                {
                    string rel = neighbor["rel"].ToString();

                    if (rel == "before")
                    {
                        prior = new User();
                        prior.id.Value = Convert.ToInt32(neighbor["user_id"]);
                        prior = prior.doSingleObjectQuery<User>("getobject");
                    }

                    if (rel == "after")
                    {
                        next = new User();
                        next.id.Value = Convert.ToInt32(neighbor["user_id"]);
                        next = next.doSingleObjectQuery<User>("getobject");
                    }
                }
            }

            StringBuilder currentBody = new StringBuilder(thisUserHeader);
            replace(currentBody, "day_name", getDay(dow));
            replace(currentBody, "time", getTime(tod));
            
            if (prior != null)
            {
                StringBuilder priorBody = new StringBuilder(header);
                replace(priorBody, "day_name", getDay(prevDow));
                replace(priorBody, "time", getTime(prevTod));

                priorBody.Append(giveMsg);
                replace(priorBody, "next_first_name", thisUser.first_name.Value);
                replace(priorBody, "next_last_name", thisUser.last_name.Value);
                replace(priorBody, "next_email", thisUser.email.Value);
                replace(priorBody, "next_phone", thisUser.phone.Value);

                currentBody.Append(recvMsg);
                replace(currentBody, "prev_first_name", prior.first_name.Value);
                replace(currentBody, "prev_last_name", prior.last_name.Value);
                replace(currentBody, "prev_email", prior.email.Value);
                replace(currentBody, "prev_phone", prior.phone.Value);

                priorBody.Append(footerMsg);

                Email priorEmail = new Email();
                priorEmail.subject = subj;
                priorEmail.Body = priorBody.ToString();
                priorEmail.addTo(prior.email.Value);
                priorEmail.from = Organization.Current.getFromEmail();
                priorEmail.send();
            }

            if (next != null)
            {
                StringBuilder nextBody = new StringBuilder(header);
                replace(nextBody, "day_name", getDay(nextDow));
                replace(nextBody, "time", getTime(nextTod));

                nextBody.Append(recvMsg);
                replace(nextBody, "prev_first_name", thisUser.first_name.Value);
                replace(nextBody, "prev_last_name", thisUser.last_name.Value);
                replace(nextBody, "prev_email", thisUser.email.Value);
                replace(nextBody, "prev_phone", thisUser.phone.Value);

                currentBody.Append(giveMsg);
                replace(currentBody, "next_first_name", next.first_name.Value);
                replace(currentBody, "next_last_name", next.last_name.Value);
                replace(currentBody, "next_email", next.email.Value);
                replace(currentBody, "next_phone", next.phone.Value);

                nextBody.Append(footerMsg);

                Email nextEmail = new Email();
                nextEmail.subject = subj;
                nextEmail.Body = nextBody.ToString();
                nextEmail.addTo(next.email.Value);
                nextEmail.from = Organization.Current.getFromEmail();
                nextEmail.send();
            }

            currentBody.Append(footerMsg);

            Email thisEmail = new Email();
            thisEmail.subject = subj;
            thisEmail.Body = currentBody.ToString();
            thisEmail.addTo(thisUser.email.Value);
            thisEmail.from = Organization.Current.getFromEmail();
            thisEmail.send();

        }

      

        protected void calcPrev(int dow, int tod, ref int prevDow, ref int prevTod)
        {
            prevTod = tod - 1;
            prevDow = dow;
            if (prevTod < 0)
            {
                prevTod = 23;
                prevDow = prevDow - 1;
                if (prevDow < 0)
                {
                    prevDow = 7;
                }
            }
        }

        protected void calcNext(int dow, int tod, ref int nextDow, ref int nextTod)
        {
            nextTod = tod + 1;
            nextDow = dow;

            if (nextTod > 23)
            {
                nextTod = 0;
                nextDow = nextDow + 1;
                if (nextDow > 7)
                {
                    nextDow = 1;
                }
            }
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


        public long unsubscribe()
        {
            Appt a = new Appt();
            a.user_id.Value = getInt("user_id");

            Appt currentAppt = a.doSingleObjectQuery<Appt>("select");
            int currentWallId = currentAppt.wall_id.Value;
            notifyAdjacent(currentWallId, getInt("user_id"), getInt("dow"), getInt("tod"), false);

            a.doCommand("delete_appt");

            Appt avail = new Appt();
            avail["organization_id"] = Organization.Current.id.Value;
            avail["tzoffset"] = LiftTime.UserTzOffset;
            avail["tod"] = getInt("tod");
            avail["dow"] = getInt("dow");

            DataSet availableWallSet = avail.doQuery("get_available_walls");

            int availableWalls = 0;
            if (availableWallSet != null)
            {
                if (availableWallSet.Tables.Count > 0)
                {
                    availableWalls = availableWallSet.Tables[0].Rows.Count;
                }
            }

            return availableWalls;
        }

        public static string getDay(object dow)
        {
            string dayName = string.Empty;
            DateTime dt = new DateTime(2009, 3, Convert.ToInt32(dow), 0, 0, 0);
            dayName = dt.ToString("dddd");
            string phraseLabel = "shared.";
            phraseLabel += dayName.ToLower();
            return Language.Current.phrase(phraseLabel);
        }

        public static string getTime(object tod)
        {
            string time = string.Empty;
            DateTime dt = new DateTime(2009, 3, 1, Convert.ToInt32(tod), 0, 0);
            time = dt.ToString("t");
            return time;
        }

        public DataSet get_my_time()
        {
            Appt a = new Appt();
            a["user_id"] = User.Current.id.Value;
            a["tzoffset"] = LiftTime.UserTzOffset;
            return a.doQuery("get_my_time_internal");
        }

        public DataSet get_user_time()
        {
            Appt a = new Appt();
            a["user_id"] = getInt("user_id");
            a["tzoffset"] = LiftTime.UserTzOffset;
            return a.doQuery("get_my_time_internal");
        }
    }
}
