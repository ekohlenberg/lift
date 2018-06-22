<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="liftprayer.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        

<div id="container">
 
    <h1><%=LiftDomain.Language.Current.SHARED_CONTACT_US %></h1>
    <h3 style="color:#0a9000;"></h3>
    
    <script src="/javascripts/validation.js" type="text/javascript"></script>

    <!-- <form action="/contact/send_email" method="post" class="large-form" id="newContactForm" name="newContactForm"> -->
    <form runat="server" action="Contact.aspx" method="post" class="large-form" id="newContactForm" name="newContactForm">

        <fieldset>
 
            <legend><%=LiftDomain.Language.Current.SHARED_CONTACT_US %> <em class="hintText"><%=LiftDomain.Language.Current.SHARED_ALL_FIELDS_REQUIRED %></em></legend>

		        <div>
			        <em class="field-count"><span id="fromCounter">100</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
			        <label for="contact_from" accesskey="1"><%=LiftDomain.Language.Current.CONTACTUS_YOUR_NAME %></label>
			        <asp:TextBox runat="server" id="contact_from" name="contact_from" value="" class="inputBox required" onkeyup="charCounter(this,100,'fromCounter')" onpaste="charCounter(this,100,'fromCounter')" tabindex="1"/>
		        </div>
    		
			    <div>
				    <em class="field-count"><span id="fromEmailCounter">255</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
				    <label for="contact_from_email" accesskey="2"><%=LiftDomain.Language.Current.CONTACTUS_YOUR_EMAIL %><span><%=LiftDomain.Language.Current.CONTACTUS_YOUR_EMAIL_REQUIRED%></span></label>
				    <asp:TextBox runat="server" id="contact_from_email" name="contact_from_email" value="" class="inputBox required" onkeyup="charCounter(this,255,'fromEmailCounter')" onpaste="charCounter(this,255,'fromEmailCounter')" tabindex="2"/>
			    </div>
		
			    <div>
				    <em class="field-count"><span id="subjectCounter">200</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
				    <label for="contact_subject" accesskey="3"><%=LiftDomain.Language.Current.CONTACTUS_SUBJECT %><span><%=LiftDomain.Language.Current.CONTACTUS_PLEASE_ENTER_SUBJECT%></span></label>
				    <asp:TextBox runat="server" id="contact_subject" name="contact_subject" value="" class="inputBox required" onkeyup="charCounter(this,200,'subjectCounterCounter')" onpaste="charCounter(this,200,'subjectCounterCounter')" tabindex="3"/>
			    </div>
		
                <div>
	                <label for="contact_message" accesskey="4"><%=LiftDomain.Language.Current.CONTACTUS_MESSAGE %> <span><%=LiftDomain.Language.Current.CONTACTUS_PLEASE_ENTER_MESSAGE%></span></label>
	                <asp:TextBox TextMode="MultiLine" runat="server" id="contact_message" rows="10" cols="60" class="inputBox required" tabindex="4"/>
                </div>
			
		</fieldset>
		
        <input type="hidden" value="1" name="request[organization_id]"/>
        <asp:Button runat="server" id="submitBtn" class="submitBtn" Text="Submit" />
		
    </form> 
 
    <script type="text/javascript">
	    var valid = new Validation('newContactForm', {immediate: true, stopOnFirst: false})
    </script>

</div>
</asp:Content>      