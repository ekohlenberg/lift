<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="EditOrganizationAppearance.aspx.cs"
    Inherits="liftprayer.EditOrganizationAppearance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="EditOrganizationAppearance.aspx" method="post" class="large-form" id="EditOrganizationAppearanceForm" name="EditOrganizationAppearanceForm">
        
        <asp:HiddenField runat="server" ID="id" />
        <asp:HiddenField runat="server" ID="subdomain" />

        <script src="/javascripts/validation.js" type="text/javascript"></script>

        <div id="EditOrganizationAppearancePage">
        
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
                <legend><%=LiftDomain.Language.Current.ORGANIZATION_APPEARANCE%></legend>
                <div>
                <label>
                <%=LiftDomain.Language.Current.ORGANIZATION_CUSTOM_STYLESHEET%>
                </label>
                <asp:TextBox runat="server" TextMode="MultiLine" Height="400" ID="lift_custom_css" class="inputBox required" type="text" />
                </div>
                <div>
                    <asp:Button runat="server" ID="submitBtn" class="submitBtn" 
                        onclick="submitBtn_Click" />
                </div>
                <div>
                    <asp:Label ID="status_label" runat="server" Text=""></asp:Label>
                </div>
            </fieldset>
            
            

        </div>
        </form>
    </div>
</asp:Content>
