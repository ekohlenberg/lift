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
    public partial class OrganizationList : System.Web.UI.Page
    {
        protected DataSet organizationListSet;
        protected PartialRenderer organizationListRenderer;
        protected string customImagePath = string.Empty;

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
                customImagePath = "/custom/";
                customImagePath += org.subdomain;
                customImagePath += "/images";
            }

            string search = string.Empty;

            searchBtn.Text = LiftDomain.Language.Current.SHARED_SEARCH;

            LiftDomain.Organization thisOrganizationList = new LiftDomain.Organization();

            if (IsPostBack)
            {
                search = liveSearchBox.Text;
            }
            else
            {
                if (Session["last_organization_list_search"] != null)
                {
                    search = Session["last_organization_list_search"].ToString();
                }
                else
                {
                    search = string.Empty;
                }
            }

            //-------------------------------------------------------------------------
            //-- !!!KLUDGE ALERT:  if first time on this page -or- search string is blank, 
            //-- !!!KLUDGE ALERT:  then use a dummy search value which will return no records
            //-------------------------------------------------------------------------
            if (String.IsNullOrEmpty(search))
            {
                search = "!l0v3TURTL3S";
            }

            Session["last_organization_list_search"] = search;

            thisOrganizationList["search"] = search;
            organizationListSet = thisOrganizationList.doQuery("SearchOrganizationsByTitleOrSubdomain");

            if (organizationListSet.Tables[0].Rows.Count > 0)
            {
                organizationListSearchResultsLabel.Visible = false;
                organizationListTablePanel.Visible = true;
                organizationListRenderer = new PartialRenderer(HttpContext.Current, organizationListSet, "_OrganizationList.htm", new PartialRenderer.RenderHelper(thisOrganizationList.organization_list_helper));
            }
            else
            {
                if (IsPostBack)
                {
                    organizationListSearchResultsLabel.Text = LiftDomain.Language.Current.ORGANIZATION_LIST_NO_MATCHING_RECORDS + ".";
                }
                else
                {
                    organizationListSearchResultsLabel.Text = LiftDomain.Language.Current.ORGANIZATION_LIST_ENTER_VALUE_TO_MATCH + ".";
                }
                organizationListSearchResultsLabel.Visible = true;

                organizationListTablePanel.Visible = false;
            }

        }
    }
}
