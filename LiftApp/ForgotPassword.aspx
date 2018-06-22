<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs"
    Inherits="liftprayer.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="ForgotPassword.aspx" method="post" class="large-form" id="ForgotPasswordForm" name="ForgotPasswordForm">
        
        <script src="/javascripts/validation.js" type="text/javascript"></script>

        <div id="ForgotPasswordPage">
            <fieldset class="separator">
                <legend><%=forgot_password_fieldset_legend%></legend>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_EMAIL%>
                </label>
                <asp:TextBox runat="server" ID="user_email" class="inputBox required" size="255" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="user_email"
                            ErrorMessage="Field is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailValidator"  ControlToValidate="user_email" runat="server"
                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                    
                </div>
                
                <div>
                <img height="30" alt="" src=BuildCaptcha.aspx width="80">
                <label>
                (<%=LiftDomain.Language.Current.SIGNUP_USER_TYPE_CODE_FROM_IMAGE%>)
                </label>
                <asp:TextBox runat="server" ID="txtCaptcha" class="inputBox required" size="30" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCaptcha"
                            ErrorMessage="Field is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>

                <div>
                    <asp:Button runat="server" ID="submitBtn" class="submitBtn" 
                        onclick="submitBtn_Click" CausesValidation="true"/>
                </div>
            </fieldset>

        </div>
        </form>
    </div>
</asp:Content>
