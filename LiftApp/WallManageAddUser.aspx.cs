using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class WallManageAddUser : System.Web.UI.Page
    {
        string userId = string.Empty;
        string appt = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string cell = string.Empty;
            string wallId = string.Empty;
            string dow = string.Empty;
            string tod = string.Empty;
            string login = string.Empty;
            string first_name = string.Empty;
            string last_name = string.Empty;
            string phone = string.Empty;
            string email = string.Empty;
            string password = string.Empty;

            LiftDomain.Organization.setCurrent();

            cell = Request["cell"];
            string[] parts = cell.Split(new char[] { '_' });
            wallId = parts[0];
            dow = parts[1];
            tod = parts[2];

            login = Request["login"];
            first_name = Request["first_name"];
            last_name = Request["last_name"];
            phone = Request["phone"];
            email = Request["email"];
            password = Request["password"];

            LiftDomain.User thisUser = new LiftDomain.User();

            thisUser.password_hash_type.Value = "md5";
            string saltValue = LiftDomain.User.generateRandomSalt();
            thisUser.password_salt.Value = saltValue;

            thisUser.crypted_password.Value = LiftDomain.User.hash(password, saltValue);
            thisUser.last_password_changed_date.Value = LiftTime.CurrentTime;

            thisUser.state.Value = 1;
            thisUser.created_at.Value = LiftTime.CurrentTime;
            thisUser.last_logged_in_at.Value = new DateTime(2000, 1, 1, 0, 0, 0); //-- DateTime.MinValue;
            thisUser.login_failure_count.Value = 0;

            thisUser.login.Value = login;
            thisUser.email.Value = email;

            thisUser.first_name.Value = first_name;
            thisUser.last_name.Value = last_name;
            thisUser.address.Value = string.Empty;
            thisUser.city.Value = string.Empty;
            thisUser.state_province.Value = string.Empty;
            thisUser.postal_code.Value = string.Empty;
            thisUser.phone.Value = phone;

            thisUser.time_zone.Value = Organization.Current.time_zone.Value;
            thisUser.language_id.Value = Organization.Current.language_id.Value;

            thisUser.previous_increment_id.Value = 0;
            thisUser.updated_at.Value = LiftTime.CurrentTime;

            bool ok = true;
            if (LiftDomain.User.checkEmailExists(email))
            {
                ok = false;
            }

            if (LiftDomain.User.checkUsernameExists(login))
            {
                ok = false;
            }


            if (ok)
            {
                thisUser.id.Value = Convert.ToInt32(thisUser.doCommand("save"));

                Appt a = new Appt();

                a["dow"] = dow;
                a["tod"] = tod;
                a["user_id"] = thisUser.id.Value;
                a["wall_id"] = wallId;
                a.doCommand("subscribe");

                userId = thisUser.id.Value.ToString();
                appt = first_name.Substring(0, 1);
                appt += ". ";
                appt += last_name;
            }
        }
    }
}
