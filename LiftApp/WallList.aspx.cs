using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class WallList : System.Web.UI.Page
    {
        protected PartialRenderer wallListRenderer;

        protected void Page_Load(object sender, EventArgs e)
        {
						PageAuthorized.check(Request, Response);

            addBtn.Text = "Add new Wall";

            if (IsPostBack)
            {
                LiftDomain.Wall newWall = new LiftDomain.Wall();
                newWall.title.Value = wall_title.Text;
                newWall.user_id.Value = LiftDomain.Organization.Current.user_id.Value;
                newWall.doCommand("insert");
            }
            
            wall_title.Text = "";
            LiftDomain.Wall w = new LiftDomain.Wall();
            DataSet wallSet = w.doQuery("get_walls");
            wallListRenderer = new PartialRenderer(HttpContext.Current, wallSet, "_WallList.htm", new PartialRenderer.RenderHelper(render_helper));
        }

        protected void render_helper(DataRow r, Hashtable h)
        {
            lang( h, "wall.wall_leader");
            lang( h, "wall.change");
            lang( h, "wall.manage_this_wall");
            lang( h, "wall.cancel");
            lang( h, "wall.start_typing");
            lang( h, "wall.once_we_find_them");
            lang( h, "wall.are_you_sure_you_want_to_delete");
            lang( h, "wall.all_users_unsubscribed");
            lang( h, "wall.delete");

        }

        void lang(Hashtable h, string p)
        {
            h[p] = LiftDomain.Language.Current.phrase(p);
        }
    }
}
