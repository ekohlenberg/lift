using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using LiftCommon;
using LiftDomain;

namespace liftprayer
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected DataSet prayerRequestSet;
        protected PartialRenderer prayerRequestRenderer;
        protected string prayerRequestRendererResult = string.Empty;

        protected DataSet prayerRequestSubscriptionSet;
        protected PartialRenderer prayerRequestSubscriptionRenderer;
        protected string prayerRequestSubscriptionRendererResult = string.Empty;

        protected DataSet prayerSessionSet;
        protected PartialRenderer prayerSessionRenderer;
        protected string prayerSessionRendererResult = string.Empty;

        protected string prayer_requests_sum_label = "0";
        protected string prayer_sessions_duration_sum_label = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            EmailValidator.ErrorMessage = LiftDomain.Language.Current.SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS;
            PasswordValidator.ErrorMessage = LiftDomain.Language.Current.SHARED_PASSWORDS_DO_NOT_MATCH;

            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

            PageAuthorized.check(Request, Response);

            int initialLanguageId = 1;  //-- 1 = English
            string initialTimeZone = string.Empty;
            string saltValue = string.Empty;
            int sumTotalRequests = 0;
            TimeSpan sumPrayerSessionsDurationTimeSpan = new TimeSpan();

            //-------------------------------------------------------------------------
            //-- do the language setting for the SUBMIT button here
            //-- (unable to place <%=LiftDomain.Language.Current.SHARED_SUBMIT %> in asp:Button Text field)
            //-------------------------------------------------------------------------
            this.submitBtn.Text = LiftDomain.Language.Current.SHARED_SUBMIT;
            this.submitBtnPassword.Text = LiftDomain.Language.Current.SHARED_CHANGE;
            this.submitBtnTimeZone.Text = LiftDomain.Language.Current.SHARED_CHANGE;

            LiftDomain.User thisUser = new LiftDomain.User();

            if (IsPostBack)
            {
                //-------------------------------------------------------------------------
                //-- transfer screen values to the object
                //-------------------------------------------------------------------------
                thisUser.id.Value = int.Parse(id.Value);

                thisUser.login.Value = login.Text;
                thisUser.first_name.Value = first_name.Text;
                thisUser.last_name.Value = last_name.Text;
                thisUser.email.Value = email.Text;
                thisUser.address.Value = address.Text;
                thisUser.city.Value = city.Text;
                thisUser.state_province.Value = state_province.Text;
                thisUser.postal_code.Value = postal_code.Text;
                thisUser.phone.Value = phone.Text;

                thisUser.language_id.Value = Convert.ToInt32(language_list.SelectedItem.Value);

                //TODO: ???what if passwords do not match??? // TO BE DONE IN JAVASCRIPT
                //(user_password.Text != user_password_confirmation.Text)

                if (!String.IsNullOrEmpty(user_password.Text.Trim()))
                {
                    thisUser.password_hash_type.Value = "md5";
                    saltValue = LiftDomain.User.generateRandomSalt();
                    thisUser.password_salt.Value = saltValue;
                    thisUser.crypted_password.Value = LiftDomain.User.hash(user_password.Text, saltValue);
                }

                thisUser.updated_at.Value = LiftTime.CurrentTime;
                thisUser.time_zone.Value = timezone_list.SelectedItem.Value;
                thisUser.previous_increment_id.Value = 0;

                //-------------------------------------------------------------------------
                //-- persist the User object data to the database
                //-------------------------------------------------------------------------
                thisUser.doCommand("save_current");

                Response.Redirect("MyAccount.aspx");
            }
            else
            {
                //-------------------------------------------------------------------------
                //-- query database for data for the current user
                //-------------------------------------------------------------------------
                id.Value = LiftDomain.User.Current.id.Value.ToString();
                thisUser.id.Value = LiftDomain.User.Current.id.Value;
                thisUser = thisUser.doSingleObjectQuery<LiftDomain.User>("select");
            }

            //-------------------------------------------------------------------------
            //-- populate the screen controls
            //-------------------------------------------------------------------------
            first_name_label.Text = thisUser.first_name;
            last_name_label.Text = thisUser.last_name;
            login.Text = thisUser.login;
            created_at.Text = thisUser.created_at.Value.ToString("dddd MMMM dd, yyyy");

            first_name.Text = thisUser.first_name;
            last_name.Text = thisUser.last_name;
            email.Text = thisUser.email;
            address.Text = thisUser.address;
            city.Text = thisUser.city;
            state_province.Text = thisUser.state_province;
            postal_code.Text = thisUser.postal_code;
            phone.Text = thisUser.phone;

            initialLanguageId = thisUser.language_id;
            initLanguageList(initialLanguageId);

            initialTimeZone = thisUser.time_zone;
            initTimeZoneList(initialTimeZone);

            //-------------------------------------------------------------------------
            //-- MY PRAYER REQUESTS
            //-------------------------------------------------------------------------

            LiftDomain.Request prayerRequest = new LiftDomain.Request();
            prayerRequest.user_id.Value = thisUser.id;
            prayerRequestSet = prayerRequest.doQuery("get_my_account_requests");

            if (prayerRequestSet.Tables[0].Rows.Count > 0)
            {
                prayerRequestRenderer = new PartialRenderer(HttpContext.Current, prayerRequestSet, "_MyAccountRequest.htm", new PartialRenderer.RenderHelper(prayerRequest.my_account_request_helper));
                prayerRequestRendererResult = prayerRequestRenderer;
            }
            else
            {
                prayerRequestRendererResult = "<p>" + LiftDomain.Language.Current.MY_ACCOUNT_YOU_HAVE_NO_REQUESTS.Value + ".</p>";
            }

            //-------------------------------------------------------------------------
            //-- MY PRAYER REQUEST SUBSCRIPTIONS
            //-------------------------------------------------------------------------
            LiftDomain.Subscription prayerRequestSubscription = new LiftDomain.Subscription();
            prayerRequestSubscription.user_id.Value = thisUser.id;
            prayerRequestSubscriptionSet = prayerRequestSubscription.doQuery("get_subscription_by_user");

            if (prayerRequestSubscriptionSet.Tables[0].Rows.Count > 0)
            {
                prayerRequestSubscriptionRenderer = new PartialRenderer(HttpContext.Current, prayerRequestSubscriptionSet, "_MyAccountPrayerRequestSubscription.htm", new PartialRenderer.RenderHelper(prayerRequestSubscription.my_account_prayer_request_subscription_helper));
                prayerRequestSubscriptionRendererResult = prayerRequestSubscriptionRenderer;
            }
            else
            {
                prayerRequestSubscriptionRendererResult = "<p>" + LiftDomain.Language.Current.MY_ACCOUNT_YOU_HAVE_NO_SUBSCRIPTIONS.Value + ".</p>";
            }

            //-------------------------------------------------------------------------
            //-- MY PRAYER SESSIONS
            //-------------------------------------------------------------------------
            LiftDomain.Prayersession prayerSessionObject = new LiftDomain.Prayersession();
            prayerSessionObject.user_id.Value = thisUser.id;
            prayerSessionSet = prayerSessionObject.doQuery("get_prayer_sessions_by_user_start_time_desc");

            if (prayerSessionSet.Tables[0].Rows.Count > 0)
            {
                prayerSessionRenderer = new PartialRenderer(HttpContext.Current, prayerSessionSet, "_MyAccountPrayerSession.htm", new PartialRenderer.RenderHelper(prayerSessionObject.my_account_prayer_session_helper));
                prayerSessionRendererResult = prayerSessionRenderer;

                foreach (DataRow thisDataRow in prayerSessionSet.Tables[0].Rows)
                {
                    sumTotalRequests += Convert.ToInt32(thisDataRow["total_requests"]);
                    sumPrayerSessionsDurationTimeSpan = sumPrayerSessionsDurationTimeSpan.Add(Convert.ToDateTime(thisDataRow["end_time"]) - Convert.ToDateTime(thisDataRow["start_time"]));
                }

                prayer_requests_sum_label = Convert.ToString(sumTotalRequests);
                prayer_sessions_duration_sum_label = Convert.ToString(sumPrayerSessionsDurationTimeSpan.Hours) + "." + (((float)((float)sumPrayerSessionsDurationTimeSpan.Minutes / (float)60)) * 10).ToString("0");
            }
            else
            {
                prayerSessionRendererResult = "<tr id='request0'><td valign='top' colspan='4' align='center'>" + LiftDomain.Language.Current.MY_ACCOUNT_YOU_HAVE_NO_SESSIONS.Value + ".</td></tr>";
            }
        }

        protected void initLanguageList(int selectedValue)
        {
            language_list.Items.Clear();
            LiftDomain.Language thisLanguage = new LiftDomain.Language();
            DataSet languageDataSet = thisLanguage.doQuery("select");
            DataTable languageDataTable = languageDataSet.Tables[0];

            language_list.DataValueField = "id";
            language_list.DataTextField = "title";
            language_list.DataSource = languageDataTable;
            language_list.DataBind();

            foreach (ListItem li in language_list.Items)
            {
                if (li.Value == selectedValue.ToString())
                    li.Selected = true;
                else
                    li.Selected = false;
            }
        }

        protected void initTimeZoneList(string selectedValue)
        {
            timezone_list.Items.Clear();
            LiftTime lt = new LiftTime();
            DataSet tzSet = lt.doQuery("get_timezones");
            DataTable tzTable = tzSet.Tables[0];

            timezone_list.DataValueField = "id";
            timezone_list.DataTextField = "name";
            timezone_list.DataSource = tzTable;
            timezone_list.DataBind();

            foreach (ListItem li in timezone_list.Items)
            {
                if (li.Value == selectedValue)
                    li.Selected = true;
                else
                    li.Selected = false;
            }
        }

    }
}
