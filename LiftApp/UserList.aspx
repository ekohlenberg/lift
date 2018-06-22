<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="liftprayer.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="container">

    <div id="Div1">
    
        <div id="commands">
            <h2><%=LiftDomain.Language.Current.USER_LIST_CURRENT_USERS%></h2>
            <div id="liveSearch" class="userform">
<%--
                <form action="/user/live_search" method="post" onsubmit="Element.show('staticSpinner'); new Ajax.Request('/user/live_search', {asynchronous:true, evalScripts:true, method:'get', onComplete:function(request){Element.hide('staticSpinner')}, parameters:Form.serialize(this)}); return false;">
                <input class="inputBox" id="liveSearchBox" maxlength="25" name="liveSearchBox" type="text" />
                <input name="commit" type="submit" value="Search" />
--%>                
                <form runat="server" action="UserList.aspx" method="post" class="large-form" id="userListForm" name="userListForm">
                <asp:TextBox runat="server" class="inputBox" id="liveSearchBox"  Width="150" maxlength="25" name="liveSearchBox" type="text" />
                  &nbsp;
                <%=LiftDomain.Language.Current.USER_STATUS%>
                <asp:DropDownList runat="server" ID="user_status_list" Width="150" />
                <asp:Button ID="searchBtn" runat="server" name="commit" AutoPostBack="true" />

                </form>
            </div>
            <a href="EditUser.aspx?id=0">
                <img alt="User_add" src="<%=customPath%>/images/user_add.gif" /><br />
                <%=LiftDomain.Language.Current.USER_LIST_ADD_USER%></a>
        </div>
        
        <div class="cleaner">
            &nbsp;
        </div>
        
        <div id="listmain">
            <br />
            <asp:Label runat="server" ID="userListSearchResultsLabel"></asp:Label>
            <asp:Panel runat="server" ID="userListTablePanel">
                <table id="userListTable" border="1" cellpadding="1" cellspacing="1" style="font-size:small;">
                    <tr>
                        <td>&nbsp;</td>
                        <%if (LiftDomain.User.Current.IsInRole(LiftDomain.Role.SYS_ADMIN)) { %>
                            <td><b>Organization</b></td>
                        <%}%>
                        <td><b><%=LiftDomain.Language.Current.USER_USER_NAME%></b></td>
                        <td><b><%=LiftDomain.Language.Current.USER_LOGIN%></b></td>
                        <td><b><%=LiftDomain.Language.Current.USER_EMAIL%></b></td>
                        <td><b><%=LiftDomain.Language.Current.USER_STATUS%></b></td>
                    </tr>
                    <%=userListRenderer%>
                </table>
            </asp:Panel>
        </div>
    </div>
    
</div>
    
</asp:Content>
