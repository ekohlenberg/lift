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
    public enum UserSignupMode
    {
        user_create_account = 0,
        user_requires_approval = 1
    };

	public class Organization : DatabaseObject 
	{
		public StringProperty address = new StringProperty();
		public StringProperty city = new StringProperty();
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty id = new IntProperty();
		public StringProperty phone = new StringProperty();
		public StringProperty postal_code = new StringProperty();
		public StringProperty state_province = new StringProperty();
		public StringProperty title = new StringProperty();
        public StringProperty subdomain = new StringProperty();
		public IntProperty user_id = new IntProperty();
        public IntProperty language_id = new IntProperty();
        public StringProperty time_zone = new StringProperty();
        public IntProperty status = new IntProperty();  //-- 0 = unapproved; 1 = approved
        public StringProperty footer = new StringProperty();
        public IntProperty default_approval = new IntProperty();
        public IntProperty default_signup_mode = new IntProperty();

        protected static Hashtable orgs = new Hashtable();
        protected static object orgSync = new object();

		public Organization()
		{
			BaseTable = "organizations";
            AutoIdentity = true;
			PrimaryKey = "id";

			attach("address", address);
			attach("city", city);
			attach("created_at", created_at);
			attach("id", id);
			attach("phone", phone);
			attach("postal_code", postal_code);
			attach("state_province", state_province);
			attach("title", title);
            attach("subdomain", subdomain);
			attach("user_id", user_id);
            attach("language_id", language_id);
            attach("time_zone", time_zone);
            attach("status", status);   //-- 0 = unapproved; 1 = approved
            attach("footer", footer);
            attach("default_approval", default_approval);
            attach("default_signup_mode", default_signup_mode);  

        }

        public static Organization Current
        {
            get
            {
                Organization org = null;
                HttpContext ctx = HttpContext.Current;
                if (ctx != null)
                {
                    if (ctx.Session != null)
                    {
                        string subdomain = (string)(ctx.Session["org"]);
                        if (subdomain != null)
                        {
                            if (subdomain.Length > 0)
                            {
                                org = Organization.getOrg(subdomain);
                            }
                        }
                    }
                
                }

                if (org == null)
                {
                    LiftContext lctx = LiftContext.Current;
                    if (lctx != null)
                    {
                        org = (Organization)lctx["organization"];
                    }
                }

                return org;
            }
        }

        public static bool setCurrent()
        {
            bool result             = false;
            Organization org    = null;
            HttpContext ctx      = HttpContext.Current;
            
            string subdomain    = ctx.Request["org"];

            if (subdomain == null)
            {
                object o = ctx.Session["org"];
                if (o != null) subdomain = o.ToString();
            }
            

            if (subdomain != null)
            {
                if (subdomain.Length > 0)
                {
                    org = Organization.getOrg(subdomain);

                    if (org != null)
                    {
                        ctx.Session["org"] = subdomain;
                        result = true;
                    }
                }
            }
            else // no explicit subdomain supplied
            {
                org = Current;
                if (org != null)
                    result = true;
            }

            return result;
        }

        public static bool setCurrent(string subdomain)
        {
            bool result = false;

            if (getOrg(subdomain) != null)
            {
                HttpContext ctx = HttpContext.Current;
                ctx.Session["org"] = subdomain;
                result = true;
            }

            return result;
        }

        public static bool setCurrent(int organizationId)
        {
            bool result = false;

            Organization org = getOrg(organizationId);
            if ( org != null)
            {
                HttpContext ctx = HttpContext.Current;
                ctx.Session["org"] = org.subdomain.Value;
                result = true;
            }

            return result;
        }

        public static Organization getOrg(string subdomain)
        {
            Organization org = null;

            lock (orgSync)
            {
                if (orgs.ContainsKey(subdomain))
                {
                    org = (Organization)orgs[subdomain];
                }
                else
                {
                    Organization.loadAll();
                    if (orgs.ContainsKey(subdomain))
                    {
                        org = (Organization)orgs[subdomain];
                    }
                }
            }
            return org;
        }


        public static Organization getOrg(int organizationId)
        {
            Organization org = new Organization();

            org.id.Value = organizationId;
            org = (Organization) org.doSingleObjectQuery(typeof(Organization), "select");

            if (org != null)
            {
                string subdomain = org.subdomain.Value;

                lock (orgSync)
                {
                    if (orgs.ContainsKey(subdomain))
                    {
                        org = (Organization)orgs[subdomain];
                    }
                    else
                    {
                        Organization.loadAll();
                        if (orgs.ContainsKey(subdomain))
                        {
                            org = (Organization)orgs[subdomain];
                        }
                    }
                }
            }
            return org;
        }



        protected static void loadAll()
        {
            Organization org = new Organization();
            List<Organization> orgList = org.doQuery<Organization>("select");

            orgs.Clear();

            foreach (Organization o in orgList)
            {
                orgs.Add(o.subdomain.Value, o);
            }
        }

        public virtual OrganizationList doList(string action)
        {
            return (OrganizationList) doQuery<Organization>(action);
        }

        public OrgEmail getOrgEmail(string orgEmailTitle)
        {
            OrgEmail thisOrgEmail = new OrgEmail();

            thisOrgEmail.organization_id.Value = this.id;
            thisOrgEmail.title.Value = orgEmailTitle;
            thisOrgEmail = thisOrgEmail.doSingleObjectQuery<OrgEmail>("select");



            return thisOrgEmail;
        }


        public long save()
        {
            long result = ((DatabasePersist)persistence).save();

            if (ContainsKey("id"))
            {
                Organization newOrg = new Organization();
                newOrg.id.Value = getInt("id");

                newOrg = newOrg.doSingleObjectQuery<Organization>("getobject");

                lock (orgSync)
                {
                    orgs[newOrg.subdomain.Value] = newOrg;
                }
            }

            return result;
        }


        public string getFromEmail()
        {
            string email = subdomain.Value;

            email += "@";
            email += ConfigReader.getString("smtp_domain", "liftprayer.cc");

            return email;
        }


        public void organization_list_helper(DataRow r, Hashtable h)
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            if (appPath.Length > 1) appPath += "/";
            h.Add("app_path", appPath);

            h["edit"] = LiftDomain.Language.Current.SHARED_EDIT.Value.ToLower();

            if (Convert.ToInt32(r["status"]) == 1)
            {
                h["status_description"] = "Approved";
            }
            else //if (Convert.ToInt32(r["status"]) == 0)
            {
                h["status_description"] = "Unapproved";
            }
        }

    }

    public class OrganizationList : List<Organization>
    {
    }
}
