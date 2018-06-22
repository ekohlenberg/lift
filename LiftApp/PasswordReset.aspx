<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs"
    Inherits="liftprayer.PasswordReset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="PasswordReset.aspx" method="post" class="large-form" id="PasswordResetForm" name="PasswordResetForm">
        
        <h3><%=isOK%></h3>
        <p><%=email%></p>

        </form>
    </div>
</asp:Content>
