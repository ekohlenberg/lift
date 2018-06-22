<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="liftprayer.Login"
    MasterPageFile="Layout.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <h1>
            <%= LiftDomain.Language.Current.LOGIN_HEADING %>
        </h1>
        <p>
            <%= LiftDomain.Language.Current.LOGIN_REGISTER %>
            <strong><a href="SignupUser.aspx">
                <%= LiftDomain.Language.Current.LOGIN_REGISTER_LINK %></a></strong>.
        </p>
        <table cellspacing=20 ><tr><td>
        <form runat="server" action="Login.aspx" method="post" class="large-form" id="loginForm"    name="loginForm">
        <asp:HiddenField ID="dest" runat=server />
        
                    <fieldset class="separator">
                    <legend>
                        <%= LiftDomain.Language.Current.LOGIN_LEGEND%></legend>
                    <asp:Login ID="Login1" runat="server" >   
                    <LayoutTemplate>
                    <div>
                        <asp:Label runat="server" AssociatedControlID="UserName" AccessKey="1">
                    <%=LiftDomain.Language.Current.SIGNUP_USER_YOUR_EMAIL %><span>(<%= LiftDomain.Language.Current.LOGIN_PLEASE_ENTER_EMAIL %>)</span></asp:Label>
                        <asp:TextBox runat="server"  ID="UserName" class="inputBox required" TabIndex="1" width=400 />
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            ErrorMessage="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:Label runat="server" AssociatedControlID="Password" AccessKey="2"><%= LiftDomain.Language.Current.LOGIN_PASSWORD %><span>(<%= LiftDomain.Language.Current.LOGIN_PASSWORD_HELP %>)</span></asp:Label>
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" class="inputBox required"
                            TabIndex="2" width=400 />
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                            ErrorMessage="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" />
                    </div>
                    <div>   
                    <asp:Button ID="LoginButton" runat="server" CssClass="submitBtn" CommandName="Login"
                            Text="Log In" ValidationGroup="Login1" />
                    </div>
                    </LayoutTemplate> 
                    </asp:Login>
                            

                </fieldset>
                
                
        
        </form>
        </td>
        <td valign="top">
        
                <p><%= LiftDomain.Language.Current.LOGIN_FORGOT_YOUR_PASSWORD %></p>
                <p><strong><a href="ForgotPassword.aspx"><%= LiftDomain.Language.Current.LOGIN_REQUEST_A_NEW_ONE%></a></strong></p>
                
        </td>
        </tr></table>
    </div>
</asp:Content>
