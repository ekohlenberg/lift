<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="SignupUser.aspx.cs"
    Inherits="liftprayer.SignupUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="SignupUser.aspx" method="post" class="large-form" id="SignupUserForm" name="SignupUserForm">
        
        <asp:HiddenField runat="server" ID="id" />
        <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>
        
        <div id="SignupUserPage">
        <br />
            <fieldset class="separator">
                <legend><%=signup_user_fieldset_legend%><em class="hintText"> (<%=signup_user_fieldset_legend2%>) </em></legend>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_EMAIL%> <span>(<%=LiftDomain.Language.Current.SIGNUP_USER_THIS_USED_TO_LOGIN%>)</span>
                </label>
                <asp:TextBox runat="server" ID="user_email" class="inputBox required" size="255" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="user_email"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailValidator"  ControlToValidate="user_email" runat="server"
                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                    
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_PASSWORD%> <span>(<%=LiftDomain.Language.Current.SIGNUP_USER_CHOOSE_NEW_PASSWORD%>)</span>
                </label>
                <asp:TextBox runat="server" ID="password" class="inputBox required" size="30" TextMode="Password" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="password"
                            ErrorMessage="Password is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_PASSWORD_CONFIRMATION%> <span>(<%=LiftDomain.Language.Current.SIGNUP_USER_ENTER_NEW_PASSWORD_AGAIN%>)</span>
                </label>
                <asp:TextBox runat="server" ID="password_confirmation" class="inputBox required" size="30" TextMode="Password" />
                <asp:CompareValidator ID="PasswordValidator" runat="server"  ControlToValidate="password_confirmation"
        ControlToCompare="password" ></asp:CompareValidator>
                </div>
                <!--
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_DESIRED_USER_NAME%><span> (<%=LiftDomain.Language.Current.SIGNUP_USER_THIS_USED_TO_LOGIN%>)</span>
                </label>
                <asp:TextBox runat="server" ID="user_login" class="inputBox required" size="100" type="text" />
                
                </div>
                -->
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_FIRST_NAME%>
                </label>
                <asp:TextBox runat="server" ID="user_first_name" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="user_first_name"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_LAST_NAME%>
                </label>
                <asp:TextBox runat="server" ID="user_last_name" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="user_last_name"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <!--
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_ADDRESS%>
                </label>
                <asp:TextBox runat="server" ID="user_address" class="inputBox required" size="200" type="text" />
               
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_CITY%>
                </label>
                <asp:TextBox runat="server" ID="user_city" class="inputBox required" size="100" type="text" />
               
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_STATE_PROVINCE%>
                </label>
                <asp:TextBox runat="server" ID="user_state" class="inputBox required" size="100" type="text" />
                
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_ZIP_POSTAL_CODE%>
                </label>
                <asp:TextBox runat="server" ID="user_postal_code" class="inputBox required" size="50" type="text" />
                </div>
                -->
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_PHONE%><span>(<%=LiftDomain.Language.Current.SIGNUP_PHONE_REQUIRED%>)</span>
                </label>
                <asp:TextBox runat="server" ID="user_phone" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="user_phone"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                
                <!--
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_TIME_ZONE%>
                </label>
                <asp:DropDownList runat="server" ID="timezone_list" />
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_LANGUAGE%>
                </label>
                <asp:DropDownList runat="server" ID="language_list" />
                </div>
                -->
                
                
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
