using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiftRoot
{
    public class SubdomainModule : IHttpModule
    {

        public void Init(HttpApplication app)
        {
            app.BeginRequest += new EventHandler(this.OnBeginRequest);
        }

        public void Dispose() { }

        public void OnBeginRequest(object o, EventArgs args)
        {

            HttpApplication app = (HttpApplication)o;
            HttpContext ctx = app.Context;

            if (ctx.Request.Path.ToUpper() == "/DEFAULT.ASPX")
            {
                string[] domainParts = app.Context.Request.Url.Host.Split(".".ToCharArray());

                if (domainParts.Length > 2)
                {
                    string subdomain = domainParts[0];

                    if (subdomain.ToLower() == "www")
                    {
                        ctx.Response.Redirect("/Main/Default.aspx");
                    }
                    else if (subdomain.ToLower() == "support")
                    {
                        ctx.Response.Redirect("/support/Default.aspx");
                    }
                    else if (subdomain.ToLower() == "tracker")
                    {
                        ctx.Response.Redirect("/tracker/Default.aspx");
                    }
                    else
                    {
                        /*
                        HttpCookie subdomainCookie = ctx.Request.Cookies["subdomain"];
                        if (subdomainCookie == null)
                        {
                            subdomainCookie = new HttpCookie("subdomain", subdomain);
                            ctx.Response.Cookies.Add(subdomainCookie);
                        }
                        else
                        {
                            subdomainCookie.Value = subdomain;
                            ctx.Response.Cookies.Set(subdomainCookie);
                        }
                         */
                        //ctx.Server.Transfer("/Lift/Requests.aspx");
                        ctx.Response.Redirect("/Lift/Requests.aspx?org="+subdomain);
                    }
                }
                /*
                else
                {
                    ctx.Response.Redirect("/Main/Default.aspx");
                }
                */
            }
        }
    }
}
