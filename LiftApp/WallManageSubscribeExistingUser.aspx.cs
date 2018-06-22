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
    public partial class WallManageSubscribeExistingUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = string.Empty;
            string tod = string.Empty;
            string dow = string.Empty;
            string userId = string.Empty;
            string wallId = string.Empty;

            action = Request["action"];
            tod = Request["tod"];
            dow = Request["dow"];
            userId = Request["user_id"];
            wallId = Request["wall_id"];

            Appt a = new Appt();

            if (action == "s") // subscribe
            {
                a["dow"] = dow;
                a["tod"] = tod;
                a["user_id"] = userId;
                a["wall_id"] = wallId;
                a.doCommand("subscribe");
            }
            else if (action == "u") // unsubscribe, then subscribe
            {
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
                
                Appt sub = new Appt();
                sub["dow"] = dow;
                sub["tod"] = tod;
                sub["user_id"] = userId;
                sub["wall_id"] = wallId;
                sub.doCommand("subscribe");
            }
           

            
           
        }

      

    }
}
