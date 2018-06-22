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
    public partial class WallManage : System.Web.UI.Page
    {
        protected WallManageRenderer wallManageRenderer;
        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);

            DataSet apptSet = new DataSet();
            Appt a = new Appt();

            int initialWall = 1;
            if (IsPostBack)
            {
                initialWall = Convert.ToInt32(wallList.SelectedValue);
            }
            else
            {
                if (!string.IsNullOrEmpty(Request["initial"]))
                {
                    initialWall = Convert.ToInt32(Request["initial"]);
                }
            }

            initWalls(initialWall);
            a["wall_id"] = initialWall;
            a["organization_id"] = Organization.Current.id.Value;
            a["tzoffset"] = LiftTime.UserTzOffset;

            apptSet = a.doQuery("get_wall_subscribers");

            wallManageRenderer = new WallManageRenderer(apptSet);
            
        }

        protected void initWalls(int initialValue)
        {
            wallList.Items.Clear();
            LiftDomain.Wall wall = new LiftDomain.Wall();
            List<LiftDomain.Wall> wList = wall.doQuery<LiftDomain.Wall>("select");

            
            
            foreach (LiftDomain.Wall w in wList)
            {
                wallList.Items.Add(new ListItem(w.title, w.id.Value.ToString()));
            }

            
            foreach (ListItem li in wallList.Items)
            {
                object val = li.Value;
                try
                {
                    if (Convert.ToInt32(val) == initialValue)
                        li.Selected = true;
                    else
                        li.Selected = false;
                }
                catch (Exception x)
                {
                    string m = x.Message;
                }
            }
        }
    }
}
