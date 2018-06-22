using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using LiftCommon;

namespace LiftDomain
{
    public class OrgEmail : DatabaseObject
    {
        public IntProperty id = new IntProperty();
        public IntProperty organization_id = new IntProperty();
        public StringProperty title = new StringProperty();
        public StringProperty smtp_server = new StringProperty();
        public StringProperty smtp_username = new StringProperty();
        public StringProperty smtp_password = new StringProperty();
        public IntProperty smtp_port = new IntProperty();
        public StringProperty email_to = new StringProperty();
        public StringProperty email_reply_to = new StringProperty();
        public StringProperty email_from = new StringProperty();
        public StringProperty webmaster_email_to = new StringProperty();
        public StringProperty contact_us_email_to = new StringProperty();
        public StringProperty encourager_email_to = new StringProperty();

        public OrgEmail()
        {
            BaseTable = "org_emails";
            AutoIdentity = false;
            PrimaryKey = "id";

            attach("id", id);
            attach("organization_id", organization_id);
            attach("title", title);
            attach("smtp_server", smtp_server);
            attach("smtp_username", smtp_username);
            attach("smtp_password", smtp_password);
            attach("smtp_port", smtp_port);
            attach("email_to", email_to);
            attach("email_reply_to", email_reply_to);
            attach("email_from", email_from);
            attach("webmaster_email_to", webmaster_email_to);
            attach("contact_us_email_to", contact_us_email_to);
            attach("encourager_email_to", encourager_email_to);
        }

    }
  }
