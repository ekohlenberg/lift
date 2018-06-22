<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="EditOrganizationImages.aspx.cs"
    Inherits="liftprayer.EditOrganizationImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="EditOrganizationImages.aspx" method="post" class="large-form" id="EditOrganizationImagesForm" name="EditOrganizationImagesForm" enctype="multipart/form-data">
        
        <asp:HiddenField runat="server" ID="id" />
        <asp:HiddenField runat="server" ID="subdomain" />

        <script src="/javascripts/validation.js" type="text/javascript"></script>

        <div id="EditOrganizationImagesPage">
        
            <asp:Table runat="server" ID="Table1" BorderStyle="None">
            <asp:TableRow runat="server" ID="TableRow1">
            <asp:TableCell runat="server" ID="TableCell1">
            <a href="EditOrganization.aspx?id=<%=this.id.Value%>"><%=LiftDomain.Language.Current.ORGANIZATION_GENERAL_INFORMATION%></a>&nbsp;|&nbsp;
            </asp:TableCell>
            <asp:TableCell runat="server" ID="TableCell2">
            <a href="EditOrganizationEmails.aspx?id=<%=this.id.Value%>"><%=LiftDomain.Language.Current.ORGANIZATION_EMAILS%></a>&nbsp;|&nbsp;
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
                <legend><%=LiftDomain.Language.Current.ORGANIZATION_IMAGES%></legend>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_UPLOAD_IMAGE_FILES%>
                </label>
                </div>
                <div>
                <table border="0" cellpadding="3">
                <tr>
                <td colspan="3" valign="top">
                    <input type="file" id="File1" name="File1" runat="server" size="69" style="margin-top:0;" />
                </td>
                </tr>
                <tr>
                <td colspan="3" valign="top">
                    <asp:Button runat="server" ID="addBtn" class="submitBtn" onclick="addBtn_Click" Width="160"/>
                </td>
                </tr>
                <tr>
                <td colspan="3">
                &nbsp;
                </td>
                <td colspan="3">
                &nbsp;
                </td>
                </tr>
                <tr>
                <td colspan="3">
                    <asp:ListBox runat="server" ID="fileListBox" Width="530" />
                </td>
                </tr>
                <tr>
                <td style="width:160px">
                &nbsp;
                </td>
                <td valign="top">
                    <asp:Button runat="server" ID="removeBtn" class="submitBtn" onclick="removeBtn_Click" Width="160" />
                </td>
                <td valign="top">
                    <asp:Button runat="server" ID="uploadBtn" class="submitBtn" onclick="uploadBtn_Click" Width="160" />
                </td>
                </tr>
                <tr>
                <td colspan="3">
                &nbsp;
                </td>
                <td colspan="3">
                &nbsp;
                </td>
                </tr>
                <tr>
                <td colspan="3" valign="top">
                    <asp:Label ID="status_label" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                </table>
                <br />
                </div>
                <br />
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_IMAGE_FILE_NAME_LIST%>
                </label>
                </div>
                <div>
                <asp:Panel runat="server" ID="organizationImageListTablePanel">
                <asp:Table runat="server" ID="organizationImageListTable" Border="1" BorderWidth="1" CellPadding="2" CellSpacing="2" Font-Size="X-Small">
                <asp:TableHeaderRow runat="server">
                <asp:TableHeaderCell runat="server">
                &nbsp;
                </asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server">
                <%=LiftDomain.Language.Current.ORGANIZATION_IMAGE_FILE_NAME%>
                </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                </asp:Table>
                </asp:Panel>
                </div>
                
            </fieldset>
            
            <script type="text/javascript">
                var valid = new Validation('EditOrganizationImagesForm', { immediate: true, stopOnFirst: false })
            </script>

        </div>
        </form>
    </div>
</asp:Content>
