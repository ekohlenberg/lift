<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="WallManage.aspx.cs" Inherits="liftprayer.WallManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        


<div id="container">
        

<h1 id="wallLabel" class="highlite">Manage Wall</h1>

<div class="cleaner"></div>

<div class="rule"><hr />
</div>
<form runat="server" id="form1">
<asp:DropDownList AutoPostBack="true" runat="server" id="wallList" />

<div id="incrementSlots">
  <table border="0" cellpadding="0" cellspacing="0" class="watchmanWall">
  <tr class="daysOfWeek">
    <td class="noBg">&nbsp;</td>
    
      <td><%=LiftDomain.Language.Current.SHARED_SUNDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_MONDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_TUESDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_WEDNESDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_THURSDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_FRIDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_SATURDAY%></td>
    
  </tr>
  <%=wallManageRenderer%>

</table>  
</div>  
</form>
<div id="newUserClone" class="new-wall-user" style="display: none;">
    <a href="javascript:void(0);" class="close-btn-new-user" onclick="myWalladmin.closenewUserModal(this); return false;" title="cancel"></a>
    <!--Create new or Find existing?-->
    <div id="dashboardOptions">
        <a href="#" class="dashboardOption dashBoardaddAnExistingUser" onclick="$('dashboardOptions').hide(); $('addExistingUser').show(); return false;">Add an Existing User</a> 
		<a href="#" class="dashboardOption dashBoardcreateNewUser" onclick="$('dashboardOptions').hide(); $('addNewUser').show(); return false;">Create a New User</a>
    </div>
    <!--Add new user section-->
    <div id="addNewUser">
        <form action="javascript:myWalladmin.addNewUser(this);" id="newUserForm" class="form-small">
            <fieldset>
				<legend>Add New User<em id="newUserDateInfo"></em></legend>
				<div>
				    <input name="login" type="text" size="30" class="inputBox labelInside" id="login" value="username" onfocus="if(this.value=='username'){this.value=''; this.removeClassName('labelInside');}" onblur="if(this.value==''){this.value='username'; this.addClassName('labelInside');}"/>
				</div>
				<div class="twoColumnLeft">
				    <input id="first_name" name="first_name" size="30" type="text" class="inputBox labelInside"  value="first name" onfocus="if(this.value=='first name'){this.value=''; this.removeClassName('labelInside');}" onblur="if(this.value==''){this.value='first name'; this.addClassName('labelInside');}" />
				</div>
				<div class="twoColumnRight">
				    <input id="last_name" name="last_name" size="30" type="text" class="inputBox labelInside" value="last name" onfocus="if(this.value=='last name'){this.value=''; this.removeClassName('labelInside');}" onblur="if(this.value==''){this.value='last name'; this.addClassName('labelInside');}"/>
				</div>
				
				<div class="cleaner"></div>
				<div>
				    <input id="phone" name="phone" size="30" type="text" class="inputBox labelInside" value="phone" onfocus="if(this.value=='phone'){this.value=''; this.removeClassName('labelInside');}" onblur="if(this.value==''){this.value='phone'; this.addClassName('labelInside');}"/>
				</div>
				<div>
				    <input id="email" name="email" size="30" type="text" class="inputBox labelInside" value="email" onfocus="if(this.value=='email'){this.value=''; this.removeClassName('labelInside');}" onblur="if(this.value==''){this.value='email'; this.addClassName('labelInside');}"/>
				</div>
				<div>
				    <input id="password" name="password" size="30" type="text" class="inputBox labelInside" value="password" onfocus="if(this.value=='password'){this.value=''; this.removeClassName('labelInside');}" onblur="if(this.value==''){this.value='password'; this.addClassName('labelInside');}"/>
				</div>
				<span id="userErrorSpan" class="userAddError"></span>
            <input name="commit" type="submit" value="Im done!" class="submitBtn"/>
            </fieldset>
        </form>
    </div>

    <!--Add current user section-->
    <div id="addExistingUser">
        <input type="hidden" id="current_cell" />
        <div  id="findUserForm" class="form-small">
            <fieldset>
				<legend>Find an Existing User</legend>
				<div>
				    <input name="usersearch" type="text" size="30" class="inputBox" id="usersearch" value="live user search" onfocus="if(this.value=='live user search'){this.value='';}" onblur="if(this.value==''){this.value='live user search';}"/>
				</div>
				<div id="findUserResults">
				<ul class="userlists">
					<li class="userlist">Start typing the first name or last name above, and we'll do the rest.</li>
					<li class="userlist">The results will be listed here, once you find what you're looking for, give it a click. Simple.</li>
				</ul>
				</div>
            </fieldset>
        </div>
    </div>
</div>
    <div class="slot-user userSlot" style="display: none;">
    <a href="javascript:void(0);" class="wall-remove-btn" title="remove from this slot" onclick="myWalladmin.remove(this);"></a> <span class="userCardName">Username</span> <br />
    <span class="userTimeInfo">Time info</span>
</div>
</div>
      
 <script type="text/javascript">
        //<![CDATA[
          var myWalladmin = new walladmin();
          //]]>
</script>
</asp:Content>