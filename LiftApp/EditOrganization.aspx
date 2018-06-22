<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="EditOrganization.aspx.cs"
    Inherits="liftprayer.EditOrganization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="EditOrganization.aspx" method="post" class="large-form" id="EditOrganizationForm" name="EditOrganizationForm">
        
        <asp:HiddenField runat="server" ID="id" />

        <script src="/javascripts/validation.js" type="text/javascript"></script>

        <div id="EditOrganizationPage">
        
            <asp:Table runat="server" ID="Table1" BorderStyle="None">
            <asp:TableRow runat="server" ID="TableRow1">
            <asp:TableCell runat="server" ID="TableCell1">
            <a href="EditOrganization.aspx?id=<%=this.id.Value%>"><%=LiftDomain.Language.Current.ORGANIZATION_GENERAL_INFORMATION%></a>&nbsp;|&nbsp;
            </asp:TableCell>
            <asp:TableCell runat="server" ID="TableCell2">
            <a href="EditOrganizationEmails.aspx?o=<%=this.id.Value%>"><%=LiftDomain.Language.Current.ORGANIZATION_EMAILS%></a>&nbsp;|&nbsp;
            </asp:TableCell>
            <asp:TableCell runat="server" ID="TableCell3">
            <a href="EditOrganizationAppearance.aspx?id=<%=this.id.Value%>"><%=LiftDomain.Language.Current.ORGANIZATION_APPEARANCE%></a>&nbsp;|&nbsp;
            </asp:TableCell>
            <asp:TableCell runat="server" ID="TableCell4">
            <a href="EditOrganizationImages.aspx?id=<%=this.id.Value%>"><%=LiftDomain.Language.Current.ORGANIZATION_IMAGES%></a>
            </asp:TableCell>
            </asp:TableRow>
            </asp:Table>
            <br />

            <fieldset class="separator">
                <h2>
                    <asp:Label ID="title_label" runat="server" Text="[title_label]"></asp:Label>
                </h2>
                <legend><%=edit_organization_fieldset_legend%></legend>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_TITLE%>
                </label>
                <asp:TextBox runat="server" ID="organization_title" class="inputBox required" size="100" type="text" tabindex="1" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="organization_title"
                            ErrorMessage="">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_ADDRESS%>
                </label>
                <asp:TextBox runat="server" ID="organization_address" class="inputBox required" size="200" type="text" tabindex="2" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="organization_address"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_CITY%>
                </label>
                <asp:TextBox runat="server" ID="organization_city" class="inputBox required" size="100" type="text" tabindex="3" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="organization_city"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_STATE_PROVINCE%>
                </label>
                <asp:TextBox runat="server" ID="organization_state" class="inputBox required" size="200" type="text" tabindex="4" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="organization_state"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_ZIP_POSTAL_CODE%>
                </label>
                <asp:TextBox runat="server" ID="organization_postal_code" class="inputBox required" size="100" type="text" tabindex="5" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="organization_postal_code"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_PHONE%>
                </label>
                <asp:TextBox runat="server" ID="organization_phone" class="inputBox required" size="50" type="text" tabindex="6" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="organization_phone"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_SUBDOMAIN%>
                </label>
                <asp:TextBox runat="server" ID="organization_subdomain" class="inputBox required" size="20" type="text" tabindex="7" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="organization_subdomain"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                 <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_FOOTER%>
                </label>
                <asp:TextBox runat="server" ID="organization_footer" class="inputBox required" size="20" type="text" tabindex="8" />
                </div>
                <div>
                
                 <div>
                <label>
                <%=LiftDomain.Language.Current.ORG_WHEN_REQUESTS_ENTERED%><br /><br />
                </label>
                	<asp:RadioButton  runat="server" id="default_approved"  Checked="false" GroupName="request_auto_approved" TabIndex="9"/> <label for="request_is_private" accesskey="10" >&nbsp;<%=LiftDomain.Language.Current.ORG_REQUESTS_AUTOMATICALLY_APPROVED%>&nbsp;</label><br /><br />
		            <asp:RadioButton  runat="server" id="default_not_approved"  Checked="false" GroupName="request_auto_approved" TabIndex="10"/> <label for="request_is_public" accesskey="10">&nbsp;<%=LiftDomain.Language.Current.ORG_REQUESTS_MANUALLY_APPROVED%>&nbsp;</label>

                </div>
                <div>
                
                 <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_NEW_USER_SIGNUP%><br /><br />
                </label>
                	<asp:RadioButton  runat="server" id="new_users_create_accounts"  Checked="false" GroupName="new_user_signup" TabIndex="11"/> <label for="request_is_private" accesskey="10" >&nbsp;<%=LiftDomain.Language.Current.ORGANIZATION_NEW_USERS_CREATE_ACCOUNTS%>&nbsp;</label><br /><br />
		            <asp:RadioButton  runat="server" id="new_users_require_approval"  Checked="false" GroupName="new_user_signup" TabIndex="12"/> <label for="request_is_public" accesskey="10">&nbsp;<%=LiftDomain.Language.Current.ORGANIZATION_NEW_USERS_REQUIRE_APPROVAL%>&nbsp;</label>

                </div>
                <div>

                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_TIME_ZONE%>
                </label>
                <asp:DropDownList runat="server" ID="timezone_list" tabindex="13" />
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_LANGUAGE%>
                </label>
                <asp:DropDownList runat="server" ID="language_list" tabindex="14" />
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_STATUS%>
                </label>
                <asp:DropDownList runat="server" ID="organization_status_list" tabindex="15" />
                </div>
                <div>
                    <asp:Button runat="server" ID="submitBtn" class="submitBtn" tabindex="16" />
                </div>
            </fieldset>
            
            
        </div>
        </form>
    </div>
</asp:Content>
