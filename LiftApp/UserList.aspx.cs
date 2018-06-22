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
    public partial class UserList : System.Web.UI.Page
    {
        protected DataSet userListSet;
        protected PartialRenderer userListRenderer;

        protected string customPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Organization.setCurrent())
            {
                Response.Redirect(LiftContext.Redirect);
            }

						PageAuthorized.check(Request, Response);

            Organization org = Organization.Current;
            if (org != null)
            {
                customPath = "/custom/";
                customPath += org.subdomain;
            }

            try
            {
                string search = string.Empty;
                UserState state = UserState.unknown;

                searchBtn.Text = LiftDomain.Language.Current.SHARED_SEARCH;

                LiftDomain.User thisUserList = new LiftDomain.User();

                if (IsPostBack)
                {
                    search = liveSearchBox.Text;
                    state = (UserState) Convert.ToInt32(user_status_list.SelectedValue);
                    if (search.Length == 0) search = "%";
                }
                else
                {
                    if (Session["last_user_list_search"] != null)
                    {
                        search = Session["last_user_list_search"].ToString();
                    }
                    else
                    {
                        search = string.Empty;
                    }

                    if (Session["last_user_list_state"] != null)
                    {
                        state = (UserState) Convert.ToInt32(Session["last_user_list_state"]);
                    }
                    else
                    {
                        state = UserState.unknown;
                    }
                }

                initUserStatusList(state);

                //-------------------------------------------------------------------------
                //-- !!!KLUDGE ALERT:  if first time on this page -or- search string is blank, 
                //-- !!!KLUDGE ALERT:  then use a dummy search value which will return no records
                //-------------------------------------------------------------------------
                if (String.IsNullOrEmpty(search))
                {
                    search = "%";

                }

              

                Session["last_user_list_search"] = search;
                Session["last_user_list_state"] = (int) state;

                string searchAction = "SearchUsersByFirstOrLast";

                thisUserList["search"] = search;
                thisUserList["state"] = (int)state;

                if (LiftDomain.User.Current.IsInRole(Role.SYS_ADMIN))
                {
                    thisUserList.OverrideAutoOrgAssignment = true;
                    searchAction = "SearchUsersByFirstOrLastSysAdmin";
                }

                userListSet = thisUserList.doQuery(searchAction);

                if (userListSet.Tables[0].Rows.Count > 0)
                {
                    userListSearchResultsLabel.Visible = false;
                    userListTablePanel.Visible = true;
                    userListRenderer = // new PartialRenderer(HttpContext.Current, userListSet, "_UserList.htm", newPartialRenderer.RenderHelper(thisUserList.user_list_helper));
                    userListRenderer = new UserRenderer(userListSet);
                }
                else
                {
                    if (IsPostBack)
                    {
                        userListSearchResultsLabel.Text = LiftDomain.Language.Current.USER_LIST_NO_MATCHING_RECORDS + ".";
                    }
                    else
                    {
                        userListSearchResultsLabel.Text = LiftDomain.Language.Current.USER_LIST_ENTER_VALUE_TO_MATCH + ".";
                    }
                    userListSearchResultsLabel.Visible = true;

                    userListTablePanel.Visible = false;
                }

            }
            catch (Exception x)
            {
                //TODO: ??? WHAT DO WE DO IF THERE IS AN ERROR ???
                string m = x.Message;
                System.Diagnostics.Debug.Print("[" + DateTime.Now.ToString() + "] *** ERROR IN UserList.aspx.cs::Page_Load(): " + m);
                Logger.log("UserList.aspx.cs", x, "[" + DateTime.Now.ToString() + "] *** ERROR IN UserList.aspx.cs::Page_Load(): " + m);
            }
            finally
            {
            }
        }

        protected void initUserStatusList(UserState defaultState )
        {
            user_status_list.Items.Clear();
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_ANY_STATUS, ((int)UserState.unknown).ToString()));
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_STATUS_CONFIRMED.Value, ((int)UserState.confirmed).ToString()));
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_STATUS_UNCONFIRMED.Value, ((int)UserState.unconfirmed).ToString()));
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_STATUS_DELETED.Value, ((int)UserState.deleted).ToString()));
            user_status_list.Items.Insert(user_status_list.Items.Count, new ListItem(LiftDomain.Language.Current.USER_STATUS_LOCKED.Value, ((int)UserState.locked).ToString()));

            foreach (ListItem thisListItem in user_status_list.Items)
            {
                thisListItem.Selected = (thisListItem.Value == ((int) defaultState).ToString());
            }
        }
    }
}
