<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="EditRequest.aspx.cs" Inherits="liftprayer.EditRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        

<div id="container">
 
<h1><%=LiftDomain.Language.Current.REQUEST_REQUEST_PRAYER%></h1>
<h3 style="color:#0a9000;"></h3>
<script src="/javascripts/validation.js" type="text/javascript"></script>
<%-- Client Side Validation --%>
<script type="text/javascript" language="javascript">

    function public_private_selected_client_validate(source, args) {
        //Requires that the user choose public or private
        if (document.getElementById("<%=request_is_private.ClientID%>").checked ||
           document.getElementById("<%=request_is_public.ClientID %>").checked) {
            args.IsValid = true
        }
        else {
            args.IsValid = false
        }
    }

</script>
<form runat="server" action="EditRequest.aspx" method="post" class="large-form" id="newRequestForm" name="newReqeustForm">
			<fieldset>
			<asp:HiddenField runat="server" ID="id" />
			<legend><%=LiftDomain.Language.Current.REQUEST_DETAILS%> <em class="hintText">(<%=LiftDomain.Language.Current.REQUEST_ALL_FIELDS_REQUIRED%>)</em></legend>
			<div>
				<em class="field-count"><span id="titleCounter">75</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
				<label for="request_title" accesskey="1"><%=LiftDomain.Language.Current.REQUEST_SUBJECT%><span>(<%=LiftDomain.Language.Current.REQUEST_TO_BE_PRAYED_FOR%>)</span></label>
				<asp:TextBox runat="server" id="request_title" name="request_title" value=""  class="inputBox required" onkeyup="charCounter(this,75,'titleCounter')" onpaste="charCounter(this,75,'titleCounter')" tabindex="1"/>
				<asp:RequiredFieldValidator ID="SubjectRequired" runat="server" ControlToValidate="request_title"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
			</div>
		

			
				<div>
					<label for="request_group_relationship_type_id" accesskey="3"><%=LiftDomain.Language.Current.REQUEST_PERSONS_RELATIONSHIP%> <%=LiftDomain.Organization.Current.title %><span>(<%=LiftDomain.Language.Current.REQUEST_PERSON_BEING_PRAYED_FOR%>)</span></label>
					 <asp:DropDownList runat="server" ID="request_group_relationship"  class="prayerRequestsSelect" size="1" TabIndex="2"></asp:DropDownList>
				</div>
			
			
			<div>
				<label for="request_requesttype_id" accesskey="3"><%=LiftDomain.Language.Current.REQUEST_REQUEST_CATEGORY%><span>(<%=LiftDomain.Language.Current.REQUEST_SELECT_CATEGORY%>)</span></label>
				  <asp:DropDownList runat="server" ID="request_type"  class="prayerRequestsSelect" size="1" TabIndex="3"></asp:DropDownList>
			</div>
			
			<div>
				<em class="field-count"><span id="bodytextCounter">350</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
				<label for="request_description" accesskey="4"><%=LiftDomain.Language.Current.REQUEST_DETAIL%><span>(<%=LiftDomain.Language.Current.REQUEST_DESCRIPTION%>)</span></label>
				
				<asp:TextBox TextMode="MultiLine" runat="server" id="request_description"  rows="10" cols="60" class="inputBox required" onkeyup="charCounter(this,350,'bodytextCounter')" onpaste="charCounter(this,350,'bodytextCounter')"  tabindex="4"/>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="request_description"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                            
<br />
			<asp:RadioButton  runat="server" id="request_is_private"  Checked="false" GroupName="request_public_private" TabIndex="5"/> <label for="request_is_private" accesskey="10" >&nbsp;<%=LiftDomain.Language.Current.REQUEST_IS_PRIVATE%>&nbsp;</label><br /><br />

		    <asp:RadioButton  runat="server" id="request_is_public"  Checked="false" GroupName="request_public_private" TabIndex="6"/> <label for="request_is_public" accesskey="10">&nbsp;<%=LiftDomain.Language.Current.REQUEST_IS_PUBLIC%>&nbsp;</label>

			</div>
		
			
		</fieldset>
		
		<fieldset>
			<legend><%=LiftDomain.Language.Current.REQUEST_SUBMITTED_BY%><em class="hintText">(<%=LiftDomain.Language.Current.REQUEST_ALL_FIELDS_REQUIRED%>)</em></legend>
			<!-- <div>Please enter the following information about yourself.</div> -->
			<div>
				<em class="field-count"><span id="fromCounter">100</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
				<label for="request_from" accesskey="6"><%=LiftDomain.Language.Current.REQUEST_YOUR_NAME%></label>
				<asp:TextBox runat="server"  id="request_from"  class="inputBox required" onkeyup="charCounter(this,100,'fromCounter')" onpaste="charCounter(this,100,'fromCounter')"  tabindex="7"/>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="request_from"
                            ErrorMessage="Field is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
			</div>
		
			<div>
				<em class="field-count"><span id="fromEmailCounter">255</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
				<label for="request_from_email" accesskey="7"><%=LiftDomain.Language.Current.REQUEST_YOUR_EMAIL%><span>(<%=LiftDomain.Language.Current.REQUEST_EMAIL_IS_REQUIRED%>)</span></label>
				<asp:TextBox runat="server" id="request_from_email"  class="inputBox required"  onkeyup="charCounter(this,255,'fromEmailCounter')" onpaste="charCounter(this,255,'fromEmailCounter')"  tabindex="8"/>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="request_from_email"
                            ErrorMessage="Field is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
			</div>

            <div>
            <img height="30" alt="" src=BuildCaptcha.aspx width="80">
            <label>
            (<%=LiftDomain.Language.Current.SIGNUP_USER_TYPE_CODE_FROM_IMAGE%>)
            </label>
            <asp:TextBox runat="server" ID="txtCaptcha" class="inputBox required" size="30" TabIndex="9" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCaptcha"
                        ErrorMessage="Field is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
            </div>

		</fieldset>
		
		<fieldset>
			<legend><%=LiftDomain.Language.Current.REQUEST_ENCOURAGEMENT%></legend>
			<div><%=LiftDomain.Language.Current.REQUEST_SEND_ENCOURAGEMENT%></div>
							<div>
				<label for="request_encouragement_address" accesskey="8"><%=LiftDomain.Language.Current.REQUEST_ADDRESS%> <span>(<%=LiftDomain.Language.Current.REQUEST_RECIPIENT_ADDRESS%>)</span></label>
				<asp:TextBox runat="server" TextMode="MultiLine"  id="request_encouragement_address"  rows="3" cols="60" class="inputBox"  tabindex="10"/>
			</div>
							
			
			<div>
				<em class="field-count"><span id="EncouragementPhoneCounter">20</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT %></em>
				<label for="request_encouragement_phone" accesskey="9"><%=LiftDomain.Language.Current.REQUEST_PHONE%><span>(<%=LiftDomain.Language.Current.REQUEST_YOUR_PHONE_NUMBER%>)</span></label>
				<asp:TextBox runat="server" id="request_encouragement_phone"  class="inputBox"  onkeyup="charCounter(this,20,'EncouragementPhoneCounter')" onpaste="charCounter(this,255,'EncouragementPhoneCounter')"  tabindex="11"/>
			</div>
			
		</fieldset>
		
		<%if (encouragementRenderer.HasData)
        {
        %>
            
		    <fieldset>
		    <legend>Updates</legend>
		    <%=encouragementRenderer%>
		    </fieldset>
		<%
        }
	    %>
		
	   <input type="hidden" value="1" name="request[organization_id]"/>
			<asp:Button runat="server" id="submitBtn" class="submitBtn" tabIndex="12" />
		<br />            
<asp:CustomValidator id="public_private_selected" runat="server"  
  
  Display="Dynamic"
  OnServerValidate="public_private_selected_server_validate" 
  ClientValidationFunction="public_private_selected_client_validate" />
		
		
</form> 

<!-- 
<script type="text/javascript">
	var valid = new Validation('newRequestForm', {immediate: true, stopOnFirst: false})
</script>-->

 

      </div>
</asp:Content>      