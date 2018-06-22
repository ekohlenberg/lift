<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrayerSessionGetPrevRequest.aspx.cs" Inherits="liftprayer.PrayerSessionGetPrevRequest" %>
<div id="psBody<%=currentReqOrdinal%>" class="psSlider">
<h3><%=title%> - <%=requestType%></h3>
<p class="psSubmittedBy">
<%=LiftDomain.Language.Current.REQUEST_SUBMITTED_BY%> <%=from%> (<%=aboutTime%>)</p>
<p class="psDescription"><%=description%></p>  
</div>

