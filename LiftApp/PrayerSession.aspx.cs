using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftDomain;

namespace liftprayer
{
    public partial class PrayerSession : System.Web.UI.Page
    {
        protected int currentSessionId = 0;
        protected string currentRequestLabel = "&nbsp;";
        protected void Page_Load(object sender, EventArgs e)
        {
            commentSubmit.Text = Language.Current.PS_END_SESSION;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            Prayersession ps = new Prayersession();
            if (IsPostBack)
            {
                currentSessionId = Convert.ToInt32(sessionIdStr.Value);
                ps.id.Value = currentSessionId;
                ps.note.Value = notesArea.Text;
                ps.doCommand("end_session");
                Response.Redirect("Requests.aspx");
            }
            else
            {
                if (LiftDomain.User.IsLoggedIn)
                {
                    currentSessionId = (int)ps.doCommand("create_session");
                    sessionIdStr.Value = currentSessionId.ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx?target=PrayerSession.aspx");
                }
            }
        }
    }
}
