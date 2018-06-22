<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Layout.Master" CodeBehind="PrayerSession.aspx.cs"
    Inherits="liftprayer.PrayerSession" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="PrayerSessionUtil.aspx?sessionId=<%=currentSessionId%>" type="text/javascript"></script>

    <div id="container">
         <input type=hidden id="nextUrl" size=100 />
         <input type=hidden id="prevUrl" size=100 />
        <h1>
            <%=LiftDomain.Language.Current.PS_NEW_PRAYER_SESSION%></h1>
        <img id="spinner" src="/images/spinner.gif" style="display: none;" />
        <div>
            <h3><span id="currentRequestLabel"><%=currentRequestLabel%></span></h3>
        </div>
        <div id="psHolder">
            <div id="prayerSession">
                <div id="psBody0" class="psSlider">
                    <h3>
                       <%=LiftDomain.Language.Current.PS_WELCOME%></h3>
                    <p>
                        <%=LiftDomain.Language.Current.PS_SENTENCE1%></br> 
                        <%=LiftDomain.Language.Current.PS_SENTENCE2%>
                        <%=LiftDomain.Language.Current.PS_SENTENCE3%>
                        <%=LiftDomain.Language.Current.PS_SENTENCE4%>
                        <%=LiftDomain.Language.Current.PS_SENTENCE5%>
                        <%=LiftDomain.Language.Current.PS_SENTENCE6%>
                        <%=LiftDomain.Language.Current.PS_SENTENCE7%>

                    </p>
                </div>
            </div>
            <div id="psControls">
                <a id="psPrevLink" title="view previous prayer request" class="psLinkInactive" href="javascript:void(0);"
                    onclick="getPrevRequest()"><span><%=LiftDomain.Language.Current.PS_PREVIOUS%></span></a> <a id="psPlayPause" href="javascript:void(0);"
                        title="begin session" onclick="beginNewSession()" style="background-position: right top;">
                        <span><%=LiftDomain.Language.Current.PS_PLAY_PAUSE%>
                        </span></a> <a class="psLinkActive" id="psNextLink" href="javascript:void(0);"
                            title="view next prayer request" onclick="getNextRequest()"><span><%=LiftDomain.Language.Current.PS_NEXT%>
</span></a>
            </div>
            <div id="psProgressHolder">
                <div id="psProgress">
                </div>
            </div>
            <form runat="server" action="PrayerSession.aspx" method="post" name="psNotesForm" class="large-form"
            id="psNotesForm" style="width: 670px;">
            <asp:HiddenField runat="server" ID="sessionIdStr" />
            <fieldset>
                <span id="noteSaving" style="display: none;">saving...</span> <legend><%=LiftDomain.Language.Current.PS_NOTES%>
</legend>
                <div>
                    <label for="notesArea" accesskey="1">
                        <%=LiftDomain.Language.Current.PS_YOUR_NOTES%>
</label>
                    <asp:TextBox runat="server" TextMode="MultiLine" id="notesArea" name="notesArea"  rows="15" cols="60" class="inputBox tall"/>
                </div>
                <input name="endSession" type="hidden" value="true" />
                <asp:button runat="server" class="submitBtn" name="commentSubmit" type="submit" id="commentSubmit" />
            </fieldset>
            </form>
        </div>

        <script type="text/javascript">
            startPrayerRequestTimer();
        </script>

    </div>
</asp:Content>
