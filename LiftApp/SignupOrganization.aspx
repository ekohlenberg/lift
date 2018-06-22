<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="SignupOrganization.aspx.cs"
    Inherits="liftprayer.SignupOrganization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="SignupOrganization.aspx" method="post" class="large-form" id="SignupOrganizationForm" name="SignupOrganizationForm">
        
        <asp:HiddenField runat="server" ID="id" />

        <script src="/javascripts/validation.js" type="text/javascript"></script>

        <div id="SignupOrganizationPage">
            <fieldset class="separator">
                <legend><%=LiftDomain.Language.Current.SIGNUP_ORGANIZATION_TITLE%></legend>
                <!--
                <div>
                <label>
                <%=LiftDomain.Language.Current.SIGNUP_ORGANIZATION_TERMS_OF_USE%>
                </label>
                <br />
                <label>
                *** TERMS OF USE GOES HERE ***
                </label>
                </div>
                -->
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_TITLE%>
                </label>
                <asp:TextBox runat="server" ID="organization_title" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="organization_title"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_ADDRESS%>
                </label>
                <asp:TextBox runat="server" ID="organization_address" class="inputBox required" size="200" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="organization_address"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_CITY%>
                </label>
                <asp:TextBox runat="server" ID="organization_city" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="organization_city"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_STATE_PROVINCE%>
                </label>
                <asp:TextBox runat="server" ID="organization_state" class="inputBox required" size="200" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="organization_state"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_ZIP_POSTAL_CODE%>
                </label>
                <asp:TextBox runat="server" ID="organization_postal_code" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="organization_postal_code"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_PHONE%>
                </label>
                <asp:TextBox runat="server" ID="organization_phone" class="inputBox required" size="50" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="organization_phone"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_SUBDOMAIN%>
                </label>
                <asp:TextBox runat="server" ID="organization_subdomain" class="inputBox required" size="20" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="organization_subdomain"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_EMAIL_TO_WEBMASTER%>
                </label>
                <asp:TextBox runat="server" ID="organization_email_to_webmaster" class="inputBox required" size="20" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="organization_email_to_webmaster"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="EmailValidator"  ControlToValidate="organization_email_to_webmaster" runat="server"
     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                </div>
                <div>
                    <asp:Button runat="server" ID="submitBtn" class="submitBtn" />
                </div>
            </fieldset>
            


        </div>
        </form>
    </div>
</asp:Content>
