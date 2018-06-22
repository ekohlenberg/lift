using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class Wall : System.Web.UI.Page
    {
        protected WallRenderer wallRenderer;

        protected string sentence1 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LiftDomain.User.IsLoggedIn)
            {
                Appt a = new Appt();
                DataSet apptSet = a.doQuery("get_stats");

                wallRenderer = new WallRenderer(apptSet);

                sentence1 = Organization.Current.title.ToString();
                sentence1 += " has ";

                LiftDomain.Wall w = new LiftDomain.Wall();
                long walls = w.doCommand("get_wall_count");

                sentence1 += walls.ToString();

                sentence1 += " prayer walls.";

                sentence1 = Language.Current.translate(sentence1);
            }
            else
            {
                Response.Redirect("Login.aspx?target=Wall.aspx");
            }

        }
    }
}
