<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="EditOrganizationEmails.aspx.cs"
    Inherits="liftprayer.EditOrganizationEmails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="EditOrganizationEmails.aspx" method="post" class="large-form" id="EditOrganizationEmailsForm" name="EditOrganizationEmailsForm">
        
        <asp:HiddenField runat="server" ID="id" />
        <asp:HiddenField runat="server" ID="orgId" />

        <script src="/javascripts/validation.js" type="text/javascript"></script>

        <div id="EditOrganizationEmailsPage">
        
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
                <legend><%=LiftDomain.Language.Current.ORGANIZATION_EMAILS%></legend>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_FROM_EMAIL_ADDRESS%>
                </label>
                <asp:TextBox runat="server" ID="organization_from_email_address" class="inputBox required" size="20" type="text" />
                <asp:RegularExpressionValidator ID="EmailValidator1"  ControlToValidate="organization_from_email_address" runat="server"
     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_EMAIL_TO_WEBMASTER%>
                </label>
                <asp:TextBox runat="server" ID="organization_email_to_webmaster" class="inputBox required" size="20" type="text" />
                <asp:RegularExpressionValidator ID="EmailValidator2"  ControlToValidate="organization_email_to_webmaster" runat="server"
     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_EMAIL_TO_CONTACT_US%>
                </label>
                <asp:TextBox runat="server" ID="organization_email_to_contact_us" class="inputBox required" size="20" type="text" />
                <asp:RegularExpressionValidator ID="EmailValidator3"  ControlToValidate="organization_email_to_contact_us" runat="server"
     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_EMAIL_TO_ENCOURAGER%>
                </label>
                <asp:TextBox runat="server" ID="organization_email_to_encourager" class="inputBox required" size="20" type="text" />
                <asp:RegularExpressionValidator ID="EmailValidator4"  ControlToValidate="organization_email_to_encourager" runat="server"
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
