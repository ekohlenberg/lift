<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="OrganizationList.aspx.cs" Inherits="liftprayer.OrganizationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="container">

    <div id="Div1">
    
        <div id="commands">
            <h2><%=LiftDomain.Language.Current.ORGANIZATION_LIST_CURRENT_ORGANIZATIONS%></h2>
            <div id="liveSearch" class="userform">
<%--
                <form action="/organization/live_search" method="post" onsubmit="Element.show('staticSpinner'); new Ajax.Request('/organization/live_search', {asynchronous:true, evalScripts:true, method:'get', onComplete:function(request){Element.hide('staticSpinner')}, parameters:Form.serialize(this)}); return false;">
                <input class="inputBox" id="liveSearchBox" maxlength="25" name="liveSearchBox" type="text" />
                <input name="commit" type="submit" value="Search" />
--%>                
                <form runat="server" action="OrganizationList.aspx" method="post" class="large-form" id="organizationListForm" name="organizationListForm">
                <asp:TextBox runat="server" class="inputBox" id="liveSearchBox"  Width="150" maxlength="25" name="liveSearchBox" type="text" />
                <asp:Button ID="searchBtn" runat="server" name="commit" AutoPostBack="true" />

                </form>
            </div>
            <a href="SignupOrganization.aspx">
                <img alt="organization_add" src="<%=customImagePath%>/layout_organization_add.gif" /><br />
                <%=LiftDomain.Language.Current.ORGANIZATION_LIST_ADD_ORGANIZATION%></a>
        </div>
        
        <div class="cleaner">
            &nbsp;
        </div>
        
        <div id="listmain">
            <br />
            <asp:Label runat="server" ID="organizationListSearchResultsLabel"></asp:Label>
            <asp:Panel runat="server" ID="organizationListTablePanel">
                <table id="organizationListTable" border="1" cellpadding="1" cellspacing="1" style="font-size:small;">
                    <tr>
                        <td>&nbsp;</td>
                        <td><b><%=LiftDomain.Language.Current.ORGANIZATION_TITLE%></b></td>
                        <td><b><%=LiftDomain.Language.Current.ORGANIZATION_SUBDOMAIN%></b></td>
                        <td><b><%=LiftDomain.Language.Current.ORGANIZATION_STATUS%></b></td>
                    </tr>
                    <%=organizationListRenderer%>
                </table>
            </asp:Panel>
        </div>
    </div>
    
</div>
    
</asp:Content>
