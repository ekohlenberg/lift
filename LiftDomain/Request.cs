using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Web;

using LiftCommon;

namespace LiftDomain
{
    public class Request : BaseLiftDomain
    {
        public StringProperty age = new StringProperty();
        public UserTimeProperty created_at = new UserTimeProperty();
        public StringProperty description = new StringProperty();
        public StringProperty encouragement_address = new StringProperty();
        public StringProperty encouragement_email = new StringProperty();
        public StringProperty encouragement_phone = new StringProperty();
        public StringProperty from = new StringProperty();
        public StringProperty from_email = new StringProperty();
        public IntProperty group_relationship_type_id = new IntProperty();
        public IntProperty id = new IntProperty();
        public IntProperty is_approved = new IntProperty();
        public StringProperty is_for = new StringProperty();
        public UserTimeProperty last_action = new UserTimeProperty();
        public IntProperty listed = new IntProperty();
        public IntProperty organization_id = new IntProperty();
        public UserTimeProperty post_date = new UserTimeProperty();
        public IntProperty requesttype_id = new IntProperty();
        public StringProperty title = new StringProperty();
        public IntProperty total_comments = new IntProperty();
        public IntProperty total_comments_needing_approval = new IntProperty();
        public IntProperty total_private_comments = new IntProperty();
        public FloatProperty total_requests = new FloatProperty();
        public UserTimeProperty updated_at = new UserTimeProperty();
        public IntProperty user_id = new IntProperty();
        public IntProperty needs_encouragement = new IntProperty();
        public IntProperty active = new IntProperty();



        public Request()
        {
            init();
        }

        
        protected void init()
        {
            BaseTable = "requests";
            AutoIdentity = true;
            PrimaryKey = "id";

            attach("age", age);
            attach("created_at", created_at);
            attach("description", description);
            attach("encouragement_address", encouragement_address);
            attach("encouragement_email", encouragement_email);
            attach("encouragement_phone", encouragement_phone);
            attach("from", from);
            attach("from_email", from_email);
            attach("group_relationship_type_id", group_relationship_type_id);
            attach("id", id);
            attach("is_approved", is_approved);
            attach("is_for", is_for);
            attach("last_action", last_action);
            attach("listed", listed);
            attach("organization_id", organization_id);
            attach("post_date", post_date);
            attach("requesttype_id", requesttype_id);
            attach("title", title);
            attach("total_comments", total_comments);
            attach("total_comments_needing_approval", total_comments_needing_approval);
            attach("total_private_comments", total_private_comments);
            attach("total_requests", total_requests);
            attach("updated_at", updated_at);
            attach("user_id", user_id);
            attach("needs_encouragement", needs_encouragement);
            attach("active", active);

            orgProperty = organization_id;
        }

        public long update_total()
        {
            long result = 0;
            try
            {
                string sqlUpdateTemplate = "update requests set total_requests=total_requests+1 where id=${id}";
                string sqlSelectTemplate = "select total_requests from requests where id=${id}";

                doCommandTemplate(sqlUpdateTemplate);

                DataSet totalSet = doQueryTemplate(sqlSelectTemplate);

                result = Convert.ToInt64(totalSet.Tables[0].Rows[0][0]);
            }
            catch
            {
            }


            return result;
        }

        public void my_account_request_helper(DataRow r, Hashtable h)
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            if (appPath.Length > 1) appPath += "/";
            h.Add("app_path", appPath);

            r["requesttype_title"] = LiftDomain.Language.Current.phrase(r["requesttype_title"].ToString());

            if (r["description"].ToString().Length > 50)
            {
                h["display_description"] = r["description"].ToString().Substring(0, 47) + "...";
            }
            else
            {
                h["display_description"] = r["description"].ToString();
            }

            int total_requests = Convert.ToInt32(r["total_requests"]);
            if (total_requests == 1)
            {
                h["prayers"] = LiftDomain.Language.Current.REQUEST_PRAYER.Value;
            }
            else
            {
                h["prayers"] = LiftDomain.Language.Current.REQUEST_PRAYERS.Value;
            }

            h["delete_request_confirmation"] = LiftDomain.Language.Current.MY_ACCOUNT_DELETE_REQUEST_CONFIRMATION.Value;
            h["delete_request_caption"] = LiftDomain.Language.Current.MY_ACCOUNT_DELETE_REQUEST_CAPTION.Value;
        }


        public void get_summary_info(ref string _title, ref string _description, ref string _from, ref string _requestType, ref string _aboutTime)
        {
            _title = title;
            _description = description;
            _from = from;
            
            RequestType rt = new RequestType();
            rt.id.Value = requesttype_id.Value;
            rt = rt.doSingleObjectQuery<RequestType>("select");


            _requestType = rt.title;

            _aboutTime = LiftTime.aboutTime(last_action);

        }

        public long approve()
        {
            long result = 0;
            result = doCommand("approve_internal");

            return result;
        }

        public void notify(int requestId)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(notify_proc));

            LiftContext ctx = new LiftContext();
            ctx["request_id"] = requestId;

            thread.Start(ctx);

        }

        protected void notify_proc(object ctxObj)
        {
            LiftContext ctx = (LiftContext) ctxObj;
            ctx.setCtx();
            
            notifySubscribers(ctx);
            notifyEncouragers(ctx);

            ctx.clearCtx();
        }

        protected void notifySubscribers(LiftContext ctx )
        {
            Organization org = Organization.Current;
            
            OrgEmail oe = new OrgEmail();
            oe.organization_id.Value = org.id.Value;
            oe = oe.doSingleObjectQuery<OrgEmail>("select");

            Request r = new Request();
            r.id.Value = ctx.getInt("request_id");
            List<Request> rlist = r.doQuery<Request>("get_most_recent_update");


            StringBuilder body = new StringBuilder();

            string title = string.Empty;

            foreach (Request u in rlist)
            {
                if (title.Length == 0) title = u.title.Value;

                body.Append("From: ");
                body.Append(u.from.Value);
                body.Append(" (");
                body.Append(u.from_email.Value);
                body.Append(")");
                // body.Append(u.getDateTime("post_date").ToString("G"));  // times are in utc - needs to be converted to subscriber tz
                body.Append("\r\n\r\n");
                body.Append(u.description.Value);
                body.Append("\r\n\r\n");
            }

            Email e = new Email();
            e.from = org.getFromEmail();
            e.Body = body.ToString();

            e.subject = "Update: " + title;
            
            Subscription s = new Subscription();
            s.request_id.Value = ctx.getInt("request_id");
            DataSet subsribers = s.doQuery("get_subscribers");

            if (DatabasePersist.hasData(subsribers))
            {
                foreach (DataRow subscriber in subsribers.Tables[0].Rows)
                {
                    e.clearRecipients();
                    e.addTo(subscriber["email"].ToString());
                    e.send();
                }
            }

        }

        protected void notifyEncouragers(LiftContext ctx)
        {
            Organization org = Organization.Current;
            
            Request r = new Request();
            r.id.Value = ctx.getInt("request_id");
            r = r.doSingleObjectQuery<Request>("getobject");
            
            if (r.needs_encouragement.Value == 1)
            {
                OrgEmail oe = new OrgEmail();
                oe.organization_id.Value = org.id.Value;
                oe = oe.doSingleObjectQuery<OrgEmail>("select");

                Email e = new Email();
                e.from = org.getFromEmail();
                e.addTo(oe.encourager_email_to.Value);
                e.subject = "New Encouragement Request: ";
                e.subject += r.title.Value;

                StringBuilder body = new StringBuilder();
                if (r.listed.Value == 0)
                {
                    body.Append("*** PRIVATE ***\r\n\r\n");
                }
                body.Append("REQUEST ID: "); body.Append(r.id.Value); body.Append("\r\n\r\n");
                body.Append("TITLE\r\n"); body.Append(r.title.Value); body.Append("\r\n\r\n");
                body.Append("DESCRIPTION\r\n"); body.Append(r.description.Value); body.Append("\r\n\r\n");
                body.Append("POST DATE\r\n"); body.Append(r.post_date.Value.ToString("G")); body.Append("\r\n\r\n");
               // body.Append("IS FOR\r\n"); body.Append(r.is_for.Value); body.Append("\r\n");
               // body.Append("IS APPROVED\r\n"); body.Append(r.is_approved.Value.ToString("G")); body.Append("\r\n");
                body.Append("LAST ACTION\r\n"); body.Append(r.last_action.Value.ToString("G")); body.Append("\r\n\r\n");
                body.Append("FROM\r\n"); body.Append(r.from.Value); body.Append("\r\n\r\n");
                body.Append("FROM EMAIL\r\n"); body.Append(r.from_email.Value); body.Append("\r\n\r\n");
                body.Append("ENCOURAGEMENT ADDRESS\r\n"); body.Append(r.encouragement_address.Value); body.Append("\r\n\r\n");
                //body.Append("ENCOURAGEMENT EMAIL\r\n"); body.Append(r.encouragement_email.Value); body.Append("\r\n\r\n");
                body.Append("ENCOURAGEMENT PHONE\r\n"); body.Append(r.encouragement_phone.Value); body.Append("\r\n\r\n");

                e.Body = body.ToString();
                e.send();

                r.Clear();
                r.id.Value = id;
                r.needs_encouragement.Value = 0;
                r.doCommand("update");
            }
        }



        public void recycle()
        {
            this.active.Value = 0;

            doCommand("update");
        }

        public void restore()
        {
            this.active.Value = 1;

            doCommand("update");
        }



    }
}
