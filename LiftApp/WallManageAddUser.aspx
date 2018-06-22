<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WallManageAddUser.aspx.cs" Inherits="liftprayer.WallManageAddUser" %>
var divElt = document.createElement('div');
divElt.id='user_<%=userId%>';
divElt.class='slot-user userSlot';
divElt.innerHtml = '<a href="javascript:void(0);" class="wall-remove-btn" title="remove from this slot" onclick="myWalladmin.removeUser(this);"></a><span class=""userCardName""><%=appt%></span><br /><span class=""userTimeInfo""><%=dayName%> <%=title%></span>';
$('cell_<%=wallId%>_<%=dow%>_<%=tod%>).appendChild(divElt);
