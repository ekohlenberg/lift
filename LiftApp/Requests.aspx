<%@ Page Title="" Language="C#"  MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="liftprayer.Requests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        
<script>
    function updateRequest(url) {
        new Ajax.Request(url, { asynchronous: true, evalScripts: true }); 
        return false;
    }

</script>
<div id="container">
<form id="form1" runat="server">
<script src="javascripts/validation.js" type="text/javascript"></script>
<%if (!HttpContext.Current.User.Identity.IsAuthenticated){%>
<fieldset>
<%=LiftDomain.Language.Current.LOGIN_WELCOME%> 
<a href="SignupUser.aspx"><%= LiftDomain.Language.Current.LOGIN_SIGN_UP%></a>
<%= LiftDomain.Language.Current.LOGIN_COME_ALONGSIDE%>
<a href="Login.aspx"><%= LiftDomain.Language.Current.LAYOUT_LOGIN%></a>  
<%= LiftDomain.Language.Current.LOGIN_IF_YOU_HAVE_AN_ACCOUNT%>
</fieldset>
<%} %>
<table border="0" width="100%">
<% if (!(LiftDomain.Organization.Current.default_signup_mode.Value == (int)LiftDomain.UserSignupMode.user_requires_approval) || (LiftDomain.User.IsLoggedIn))
   {
     %>
<% if ((LiftDomain.User.Current.canApproveRequests) && (active == 1))
   {
     %>
<tr>
<td id="recyclebin" align="right" colspan="4">
<a href="Requests.aspx?active=0"><%=LiftDomain.Language.Current.REQUEST_RECYCLE_BIN%></a>
</td>
</tr>
<tr><td colspan="4">
<h1><%=viewingRequestsFor%></h1>
</td>

</tr>
<%}
   else if (active == 0)
   { %>
   
  
<tr><td colspan="4">
<h1><%=LiftDomain.Language.Current.REQUEST_RECYCLE_BIN%></h1>
</td> 
</tr>
<% }
   else
   {
       %>
       
   <tr><td colspan="4">
<h1><%=viewingRequestsFor%></h1>
</td>

</tr>    
       
    <% }

   %>
<tr>
<td>
 <asp:DropDownList runat="server" ID="requesttype" AutoPostBack="true" class="prayerRequestsSelect" size="1"></asp:DropDownList>&nbsp;
 <span class="bigText"><%=duringTheLast%></span>
<asp:DropDownList runat="server" ID="timeframe"  AutoPostBack="true" class="prayerRequestsSelect" ></asp:DropDownList>
</td>
<td width="30%" align="right">
<asp:TextBox runat="server" class="inputBox" id="liveSearchBox"  Width="150" maxlength="25" name="liveSearchBox" type="text" />
 </td><td align="left" ><asp:Button ID="searchBtn" runat="server"  name="commit" AutoPostBack="true"  />
 </td>
<td id="printButton" align="center" width="10%">
<a href="javascript:" onclick="window.print();return false"><img src="<%=customPath%>/images/printButton.gif" /></a><br />
<a href="javascript:" onclick="window.print();return false"><%=LiftDomain.Language.Current.REQUEST_PRINT_REQUESTS%></a>
</td>
<%
    } %>
</tr></table>
<div id="requestList">
<%=requestRenderer%>
  </div>
<div class="cleaner"></div>
 </form>
 </div>
</asp:Content>
