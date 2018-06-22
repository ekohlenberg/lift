using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;


using LiftCommon;

namespace LiftDomain 
{
    public class LiftContext : BaseLiftDomain
    {
        public static Hashtable contexts = new Hashtable();
        public static object ctxSync = new object();
        public LiftContext()
        {
            this["organization"] = Organization.Current;
            this["user"] = User.Current;

           
        }

        public static LiftContext Current
        {
            get
            {
                LiftContext ctx = null;
                lock( ctxSync )
                {
                    int threadId = Thread.CurrentThread.GetHashCode();
                    if (contexts.ContainsKey(threadId))
                    {
                        ctx = (LiftContext)contexts[threadId];
                    }
                }

                return ctx;
            }
        }

        public void setCtx()
        {
            lock (ctxSync)
            {
                int threadId = Thread.CurrentThread.GetHashCode();
                contexts[threadId] = this;
            }
        }

        public void clearCtx()
        {
            lock (ctxSync)
            {
                lock (ctxSync)
                {
                    int threadId = Thread.CurrentThread.GetHashCode();
                    if (contexts.ContainsKey(threadId))
                    {
                        contexts.Remove(threadId);
                    }
                }

            }
        }


        public static string Redirect
        {
            get
            {
                return ConfigReader.getString("OnErrorRedirect", "/Main/Default.aspx");
            }
        }
        
    }
}
