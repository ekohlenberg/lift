<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="EmailAllUsers.aspx.cs" Inherits="liftprayer.EmailAllUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        

<div id="container">
 
    <h1><%=LiftDomain.Language.Current.EMAIL_ALL_USERS_CAPTION%></h1>
    <h3 style="color:#0a9000;"></h3>
    
    <script src="/javascripts/validation.js" type="text/javascript"></script>

    <!-- <form action="/contact/send_email" method="post" class="large-form" id="newEmailAllUsersForm" name="newEmailAllUsersForm"> -->
    <form runat="server" action="EmailAllUsers.aspx" method="post" class="large-form" id="newEmailAllUsersForm" name="newEmailAllUsersForm">

        <fieldset>
 
            <legend><%=LiftDomain.Language.Current.EMAIL_ALL_USERS_CAPTION%> <em class="hintText"><%=LiftDomain.Language.Current.SHARED_ALL_FIELDS_REQUIRED %></em></legend>

			    <div>
				    <em class="field-count"><span id="subjectCounter">200</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
				    <label for="email_subject" accesskey="3"><%=LiftDomain.Language.Current.CONTACTUS_SUBJECT %><span><%=LiftDomain.Language.Current.CONTACTUS_PLEASE_ENTER_SUBJECT%></span></label>
				    <asp:TextBox runat="server" id="email_subject" name="email_subject" value="" class="inputBox required" onkeyup="charCounter(this,200,'subjectCounterCounter')" onpaste="charCounter(this,200,'subjectCounterCounter')" tabindex="3"/>
			    </div>
		
                <div>
	                <label for="email_message" accesskey="4"><%=LiftDomain.Language.Current.CONTACTUS_MESSAGE %> <span><%=LiftDomain.Language.Current.CONTACTUS_PLEASE_ENTER_MESSAGE%></span></label>
	                <asp:TextBox TextMode="MultiLine" runat="server" id="email_message" rows="10" cols="60" class="inputBox required" tabindex="4"/>
                </div>
			
		</fieldset>
		
        <input type="hidden" value="1" name="request[organization_id]"/>
        <asp:Button runat="server" id="submitBtn" class="submitBtn" Text="Submit" />
		
    </form> 
 
    <script type="text/javascript">
	    var valid = new Validation('newEmailAllUsersForm', {immediate: true, stopOnFirst: false})
    </script>

</div>
</asp:Content>      