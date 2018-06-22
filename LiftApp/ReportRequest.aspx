<%@ Page Language="C#"  MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="ReportRequest.aspx.cs" Inherits="liftprayer.ReportRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
<div id="container">

<asp:Label ForeColor="Red" ID="errMsg" runat="server"></asp:Label>
<br />

<%=requestRenderer%>

<div id="commentsSection">
	<%=encRenderer %>
<div>
<script type="text/javascript" language="javascript">



</script>

<form runat="server" action="ReportRequest.aspx" class="large-form left-margin">
   
  <fieldset>
   <asp:HiddenField runat="server" ID="request_id"  />
   
    <legend><%=LiftDomain.Language.Current.REQUEST_HOW_INAPPROPRIATE%></legend>
	  	<span id="validationMessage"></span>
    <img src="/images/spinner-small.gif" id="commentSpinner" style="display: none;"  />

    <div>
        <em class="field-count"><span id="commentAreaCounter">350</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT%></em>
        <label for="note" accesskey="2"><%=LiftDomain.Language.Current.REQUEST_DESCRIPTION_LABEL%><span><%=LiftDomain.Language.Current.REQUEST_TYPE_YOUR_DESCRIPTION_HERE%></span></label>
        <asp:Textbox runat="server" id="note" name="note"   TextMode="MultiLine" rows="10" cols="60" class="inputBox" onkeyup="charCounter(this,350,'commentAreaCounter')" onpaste="charCounter(this,350,'commentAreaCounter')" tabindex="1"></asp:Textbox>
       
    </div>

	<fieldset>
		<legend><%=LiftDomain.Language.Current.REQUEST_REQUESTER%> <em class="hintText">(<%=LiftDomain.Language.Current.REQUEST_FIELDS_OPTIONAL%>)</em></legend>
		<div><%=LiftDomain.Language.Current.REQUEST_PLEASE_ENTER_INFO%></div>
		<div>
			<em class="field-count"><span id="fromCounter">100</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT%></em>
			<label for="from" accesskey="3"><%=LiftDomain.Language.Current.REQUEST_YOUR_NAME%></label>
			<asp:Textbox runat="server" id="from" name="from"  class="inputBox required" type="text" onkeyup="charCounter(this,100,'fromCounter')" onpaste="charCounter(this,100,'fromCounter')" tabindex="2" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="from"
                            ErrorMessage="Field is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
		</div>
	
		<div>
			<em class="field-count"><span id="fromEmailCounter">255</span> <%=LiftDomain.Language.Current.SHARED_CHARACTERS_LEFT%></em>
			<label for="from_email" accesskey="4"><%=LiftDomain.Language.Current.REQUEST_YOUR_EMAIL%><span><%=LiftDomain.Language.Current.CONTACTUS_YOUR_EMAIL_REQUIRED%></span></label>
			<asp:Textbox runat="server" id="from_email" name="from_email"   class="inputBox required" type="text" onkeyup="charCounter(this,255,'fromEmailCounter')" onpaste="charCounter(this,255,'fromEmailCounter')" tabindex="3" />
			
		</div>
		
       

	</fieldset>
	
	 <div>
            <img height="30" alt="" src=BuildCaptcha.aspx width="80">
            <label>
            (<%=LiftDomain.Language.Current.SIGNUP_USER_TYPE_CODE_FROM_IMAGE%>)
            </label>
            <asp:TextBox runat="server" ID="txtCaptcha" class="inputBox required" size="30" tabindex="4" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCaptcha"
                        ErrorMessage="Field is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
        </div>
	
    <asp:Button runat="server"  class="submitBtn" name="submitBtn" type="submit" value="submit" id="submitBtn" tabindex="5" />
    
  </fieldset>
</form>


</div>

 </div>

</div>
</asp:Content>
