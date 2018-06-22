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
    public partial class WallManageUnsubscribe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = string.Empty;

            userId = Request["user_id"];

            Appt userAppt = new Appt();
            userAppt["user_id"] = userId;
            DataSet userApptSet = userAppt.doQuery("get_user_time");
            if (!DatabaseObject.isNullOrEmpty(userApptSet))
            {
                DataRow apptRow = userApptSet.Tables[0].Rows[0];

                Appt unsub = new Appt();
                unsub["dow"] = apptRow["my_dow"];
                unsub["tod"] = apptRow["my_tod"];
                unsub["user_id"] = userId;
                unsub.doCommand("unsubscribe");
            }
        }
    }
}
