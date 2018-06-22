<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteRequest.aspx.cs" Inherits="liftprayer.DeleteRequest" %>

try 
{
<%--
element = document.getElementById("request<%=idStr%>");
document.removeChild(element);
--%>
$("request<%=idStr%>").visualEffect("fade");
<%--
$("request<%=idStr%>").parentNode.removeChild($("request<%=idStr%>"));
--%>
} catch (e) 
{
}

