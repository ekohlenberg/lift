<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateTotal.aspx.cs" Inherits="liftprayer.UpdateTotal" %>
try 
{
Element.update("prayerTotal<%=idStr%>", "<%=newTotalStr%>");
$("showPrayerCount<%=idStr%>").visualEffect("fade");
} catch (e) 
{
}

