<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs"
    Inherits="liftprayer.EditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <form runat="server" action="EditUser.aspx" method="post" class="large-form" id="EditUserForm" name="EditUserForm">
        
        <asp:HiddenField runat="server" ID="id" />

        <script src="/javascripts/validation.js" type="text/javascript"></script>

        <div id="EditUserPage">
            <fieldset class="separator">
                <h2>
                    <asp:Label ID="login_label" runat="server" Text="[login_label]"></asp:Label>
                </h2>
                <legend><%=edit_user_fieldset_legend%></legend>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_LOGIN%>
                </label>
                <asp:TextBox runat="server" ID="user_login" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="user_login"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_EMAIL%>
                </label>
                <asp:TextBox runat="server" ID="user_email" class="inputBox required" size="200" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="user_email">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailValidator"  ControlToValidate="user_email" runat="server"
     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_PASSWORD%> <span>(<%=LiftDomain.Language.Current.USER_LEAVE_PASSWORD_EMPTY%>)</span>
                </label>
                <asp:TextBox runat="server" ID="password" class="inputBox required" size="30" TextMode="Password" />
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="password"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                            
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_PASSWORD_CONFIRMATION%>
                </label>
                <asp:TextBox runat="server" ID="password_confirmation" class="inputBox required" size="30" TextMode="Password" />
                <asp:CompareValidator ID="PasswordValidator" runat="server"  ControlToValidate="password_confirmation"
        ControlToCompare="password" ></asp:CompareValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_FIRST_NAME%>
                </label>
                <asp:TextBox runat="server" ID="user_first_name" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="user_first_name"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_LAST_NAME%>
                </label>
                <asp:TextBox runat="server" ID="user_last_name" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="user_last_name"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_ADDRESS%>
                </label>
                <asp:TextBox runat="server" ID="user_address" class="inputBox required" size="200" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="user_address"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_CITY%>
                </label>
                <asp:TextBox runat="server" ID="user_city" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="user_city"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_STATE_PROVINCE%>
                </label>
                <asp:TextBox runat="server" ID="user_state" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="user_state"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_ZIP_POSTAL_CODE%>
                </label>
                <asp:TextBox runat="server" ID="user_postal_code" class="inputBox required" size="50" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="user_postal_code"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_PHONE%>
                </label>
                <asp:TextBox runat="server" ID="user_phone" class="inputBox required" size="100" type="text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="user_phone"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_STATUS%>
                </label>
                <asp:DropDownList runat="server" ID="user_status_list" />
                </div>
                <div class="nestedFormList">
                    <h2><%=LiftDomain.Language.Current.SHARED_ROLES%></h2>
                    
                    <asp:CheckBox runat="server" ID="user_roles_10" Text="Watchman" TextAlign="Right" />  <em class="hintText" >(Access to Prayer Session and Watchman Wall)</em><br />
                    <asp:CheckBox runat="server" ID="user_roles_8" Text="Wall Leader" TextAlign="Right" />  <em class="hintText" >(Can send email to Wall subscribers)</em><br />
                    <asp:CheckBox runat="server" ID="user_roles_7" Text="Moderator" TextAlign="Right" />  <em class="hintText" >(Can edit and approve prayer requests)</em><br />
                    <asp:CheckBox runat="server" ID="user_roles_14" Text="Organization Admin" TextAlign="Right" />  <em class="hintText" >(Can edit Organization Settings)</em><br />
                    <asp:CheckBox runat="server" ID="user_roles_13" Text="System Admin" TextAlign="Right" />  <em class="hintText" >(Site Administrator)</span><br />
<%--                    
                    <asp:CheckBox runat="server" ID="user_roles_11" Text="TestAdmin" TextAlign="Right" /><br />
                    <asp:CheckBox runat="server" ID="user_roles_12" Text="admintest" TextAlign="Right" /><br />
--%>                
                </div>
                <div>
                <label>
                Organization
                </label>
                <asp:DropDownList runat="server" ID="org_list" />
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_TIME_ZONE%>
                </label>
                <asp:DropDownList runat="server" ID="timezone_list" />
                </div>
                <div>
                <label>
                <%=LiftDomain.Language.Current.USER_LANGUAGE%>
                </label>
                <asp:DropDownList runat="server" ID="language_list" />
                </div>
                <div>
                    <asp:Button runat="server" ID="submitBtn" class="submitBtn" />
                </div>
            </fieldset>
            
            <asp:Table runat="server" ID="bottomNavTable" BorderStyle="None">
            <asp:TableRow runat="server" ID="bottomNavTableRow">
            <asp:TableCell runat="server" ID="bottomNavTableCellDelete">
            <a href="DeleteUser.aspx?id=<%=delete_user_id%>&redirect_to_page=<%=redirect_after_delete_to_page%>" onclick="javascript:return confirm('<%=LiftDomain.Language.Current.USER_DELETE_USER_CONFIRMATION%>')" title="<%=LiftDomain.Language.Current.USER_DELETE_USER_CAPTION%>"><%=LiftDomain.Language.Current.SHARED_DELETE%></a>&nbsp;|&nbsp;
            </asp:TableCell>
            <asp:TableCell runat="server" ID="bottomNavTableCellBack">
            <a href="javascript: history.go(-1)"><%=LiftDomain.Language.Current.SHARED_BACK%></a>
            </asp:TableCell>
            </asp:TableRow>
            </asp:Table>

            

        </div>
        </form>
    </div>
</asp:Content>
