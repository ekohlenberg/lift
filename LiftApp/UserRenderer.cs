using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

using LiftCommon;
using LiftDomain;


namespace liftprayer
{
    public class UserRenderer : PartialRenderer
    {
        protected string inPlaceSelectHtml = string.Empty;
        protected string normalHtml = string.Empty;

        public UserRenderer(DataSet ds)
        {

            mCtx = HttpContext.Current;
            mDs = ds;
            mFilename = "_UserList.htm";
            mRh = new RenderHelper(render_helper);
            
            setHtml();
        }

        protected void setHtml()
        {
            string userStatus = string.Empty;
/*
            new Ajax.InPlaceCollectionEditor( element_2, '/url_to_validate_and_save_selection/', { 
  collection: [['key_1', 'value one'], ['key_2', 'value two'], ['key_3', 'value three']] 
});
 */

            /*
             * <p id="tobeedited3">three</p> (should manually select "one")
<script>
new Ajax.InPlaceCollectionEditor(
  'tobeedited3', '_ajax_inplaceeditor_result.html', {
  collection: [[0,'one'],[1,'two'],[2,'three']],
  value: 0,
  ajaxOptions: {method: 'get'} //override so we can use a static for the result
});
</script>


             * */

            inPlaceSelectHtml = @"<span class=""in_place_editor_field"" id=""user_state_${id}_in_place_select"">${user_status_description}</span><script type=""text/javascript"">
                                //<![CDATA[
                                new Ajax.InPlaceCollectionEditor('user_state_${id}_in_place_select', 
                                    '${app_path}UpdateUserStatus.aspx?id=${id}', 
                                    { ${status_list}, 
                                    value: ${current_state},
                                    ajaxOptions: {method: 'post'} //override so we can use a static for the result
                                    })
                                //]]>
                                </script>";

            string statusList = " collection: [[${confirmed_key}, '${confirmed_value}'], [${unconfirmed_key}, '${unconfirmed_value}'], [${locked_key}, '${locked_value}'], [${deleted_key}, '${deleted_value}']] ";
            userStatus = User.getUserStatusDescription((int)UserState.confirmed);
            statusList = statusList.Replace("${confirmed_key}", ((int)UserState.confirmed).ToString());
            statusList = statusList.Replace("${confirmed_value}", userStatus);

            userStatus = User.getUserStatusDescription((int)UserState.unconfirmed);
            statusList = statusList.Replace("${unconfirmed_key}", ((int)UserState.unconfirmed).ToString());
            statusList = statusList.Replace("${unconfirmed_value}", userStatus);

            userStatus = User.getUserStatusDescription((int)UserState.locked);
            statusList = statusList.Replace("${locked_key}", ((int)UserState.locked).ToString());
            statusList = statusList.Replace("${locked_value}", userStatus);

            userStatus = User.getUserStatusDescription((int)UserState.deleted);
            statusList = statusList.Replace("${deleted_key}", ((int)UserState.deleted).ToString());
            statusList = statusList.Replace("${deleted_value}", userStatus);

                   

            inPlaceSelectHtml = inPlaceSelectHtml.Replace("${status_list}", statusList);


            normalHtml = "${user_status_description}";
        }
        public void render_helper(DataRow r, Hashtable h)
        {
            string userStatusHtml = normalHtml;

            bool sysAdmin = LiftDomain.User.Current.IsInRole(Role.SYS_ADMIN);

            if (sysAdmin)
            {
                h["str_org_title"] = "<td align=\"top\">" + r["org_title"] + "</td>";
            }
            else
            {
                h["str_org_title"] = "";
            }

            h.Add("app_path", AppPath);



            h["edit"] = LiftDomain.Language.Current.SHARED_EDIT.Value.ToLower();

            h["full_name"] = r["first_name"].ToString() + " " + r["last_name"].ToString();



            string userStatus = string.Empty;
            int currentState = Convert.ToInt32(r["state"]);

            userStatus = User.getUserStatusDescription(currentState);

            if (User.Current.isAdmin)
            {
                userStatusHtml = inPlaceSelectHtml;
            }

            userStatusHtml = userStatusHtml.Replace("${id}", r["id"].ToString());
            userStatusHtml = userStatusHtml.Replace("${app_path}", AppPath);
            userStatusHtml = userStatusHtml.Replace("${user_status_description}", userStatus);
            userStatusHtml = userStatusHtml.Replace("${current_state}", currentState.ToString());


            h["user_status_html"] = userStatusHtml;
        }

        
    }
}