using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public class PageAuthorized
    {
        protected static Hashtable authPages = new Hashtable();  // pages that only require authentication
        protected static Hashtable rolePages = new Hashtable(); // pages that require specific roles
        protected static object pageSync = new object();

        public static void check(HttpRequest request, HttpResponse response)
        {
            bool redirect = false;
            int start = request.Path.LastIndexOf('/');
            int end = request.Path.IndexOf("aspx");

            if (start == -1) return;
            if (end == -1) return;

            start += 1;
            end += 4;

            string uri = request.Path.Substring( start, (end - start));

            lock (pageSync)
            {
                int a = -1;
                string authKey = "Auth." + uri;

                if (authPages.ContainsKey(uri))
                {
                    a = Convert.ToInt32(authPages[authKey]);
                }
                else
                {
                    a = ConfigReader.getInt(authKey, 0);
                    authPages[authKey] = a;
                }

                if (a == 1)
                {
                    if (!User.IsLoggedIn)
                    {
                        redirect = true;
                    }
                }

                ArrayList roles = null;
                string roleKey = "Roles." + uri;
            checkAgain:
                if (rolePages.ContainsKey(roleKey))
                {
                    roles = (ArrayList)rolePages[roleKey];

                    if (roles.Count > 0)
                    {
                        bool found = false;

                        for (int i = 0; i < roles.Count & !found; i++)
                        {
                            string role = (string)roles[i];
                            if (User.Current.IsInRole(role)) found = true;
                        }

                        if (!found) redirect = true;
                    }
                }
                else
                {
                    string roleString = ConfigReader.getString(roleKey, "");

                    if (roleString.Length > 0)
                    {
                        string[] roleArray = roleString.Split(new char[] { ';' });
                        ArrayList roleList = new ArrayList();
                        foreach (string s in roleArray) roleList.Add(s);
                        rolePages[roleKey] = roleList;
                        goto checkAgain;
                    }
                }

            }


            if (redirect)
            {
                response.Redirect("Login.aspx");
            }


        }

    }

    
}
