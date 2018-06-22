<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveRequest.aspx.cs" Inherits="liftprayer.ApproveRequest" %>
try 
{
<% if (isApprovedStr == "1")
   { %>
    $("awaitingApprovalMessage<%=idStr%>").visualEffect("fade");
    $("approveRequest<%=idStr%>").visualEffect("fade");
    $("updateAwaitingApprovalMessage<%=idStr%>").visualEffect("fade");
<% }
   else
   { %>
    $("request<%=idStr%>").visualEffect("fade");
<% } %>
} catch (e) 
{
}
