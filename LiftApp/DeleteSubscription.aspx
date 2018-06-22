<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteSubscription.aspx.cs" Inherits="liftprayer.DeleteSubscription" %>
try 
{
<%--
element = document.getElementById("request<%=idStr%>");
document.removeChild(element);
--%>
$("subscription<%=idStr%>").visualEffect("fade");
<%--
$("request<%=idStr%>").parentNode.removeChild($("request<%=idStr%>"));
--%>
} catch (e) 
{
}
