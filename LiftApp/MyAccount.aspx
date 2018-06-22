<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs"
    Inherits="liftprayer.MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
    <form runat="server" action="MyAccount.aspx" method="post" class="large-form" id="myAccountForm" name="myAccountForm">

     <h1><%=LiftDomain.Language.Current.SHARED_MY_ACCOUNT%></h1>

        <asp:HiddenField runat="server" ID="id" />
        
        <script src="/javascripts/validation.js" type="text/javascript"></script>

        <div id="myAccountPage">
            <div id="leftCol">
                <div id="accountPanelUserCard">
                    <fieldset class="separator">
			                
                        <legend><%=LiftDomain.Language.Current.MY_ACCOUNT_CHANGE_MY_ACCOUNT_INFORMATION%></legend>
                        
                        <div>
                        <label>Name</label><br />
                        <asp:Label ID="first_name_label" runat="server" Text="[first_name_label]"></asp:Label> <asp:Label ID="last_name_label" runat="server" Text="[last_name_label]"></asp:Label>
                        </div>

                        <div>
                        <label>Login
                        </label><br />
                        <asp:Label ID="login" runat="server" Text="[login]"></asp:Label>
                        </div>
                        
                        <div>
                        <label><br />
                        <%=LiftDomain.Language.Current.MY_ACCOUNT_LIFT_MEMBER_SINCE%></label>
                        <asp:Label ID="created_at" runat="server" Text="--"></asp:Label>
                        </div>
                        
                                                <hr />
                
                        <div>
                        <label><%=LiftDomain.Language.Current.USER_FIRST_NAME%>
                        </label>
                        <asp:TextBox runat="server" ID="first_name" class="inputBox required" type="text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="first_name"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                        </div>
                        
                        <div>
                        <label><%=LiftDomain.Language.Current.USER_LAST_NAME%>
                        </label>
                        <asp:TextBox runat="server" ID="last_name" class="inputBox required" type="text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="last_name"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                        </div>
                        
                        <div>
                        <label>
                        <%=LiftDomain.Language.Current.USER_EMAIL%>
                        </label>
                        <asp:TextBox runat="server" ID="email" class="inputBox required" type="text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="email"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="EmailValidator"  ControlToValidate="email" runat="server"
     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>
                        </div>
                        
                        <div>
                        <label>
                        <%=LiftDomain.Language.Current.USER_ADDRESS%>
                        </label>
                        <asp:TextBox runat="server" ID="address" class="inputBox required" type="text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="address"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                        </div>
                        
                        <div>
                        <label>
                        <%=LiftDomain.Language.Current.USER_CITY%>
                        </label>
                        <asp:TextBox runat="server" ID="city" class="inputBox required" type="text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="city"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                        </div>
                        
                        <div>
                        <label>
                        </label>
                        <%=LiftDomain.Language.Current.USER_STATE_PROVINCE%>
                        <asp:TextBox runat="server" ID="state_province" class="inputBox required" type="text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="state_province"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                        </div>
                        
                        <div>
                        <label>
                        <%=LiftDomain.Language.Current.USER_ZIP_POSTAL_CODE%>
                        </label>
                        <asp:TextBox runat="server" ID="postal_code" class="inputBox required" type="text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="postal_code"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                        </div>
                        
                        <div>
                        <label>
                        <%=LiftDomain.Language.Current.USER_PHONE%>
                        </label>
                        <asp:TextBox runat="server" ID="phone" class="inputBox required" type="text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="phone"
                            ErrorMessage="Subject is required.">* <%=LiftDomain.Language.Current.SHARED_REQUIRED_FIELD%></asp:RequiredFieldValidator>
                        </div>
                        
                        <div>
                        <label>
                        <%=LiftDomain.Language.Current.USER_LANGUAGE%>
                        </label>
                        <asp:DropDownList runat="server" ID="language_list" />
                        </div>
                        
                        <div>
		                <asp:Button runat="server" id="submitBtn" class="submitBtn" />
                        </div>
                    </fieldset>
                 </div>

                <div class="accountPassword">
                    <fieldset class="separator">
                        <legend><%=LiftDomain.Language.Current.MY_ACCOUNT_CHANGE_MY_PASSWORD%></legend>
                        <img id="midSpinner3" src="/images/spinner.gif" style="display: none;" />
                        <div id="notices1" style="display: none;">
                            <div class="image"></div>
                            <div id="notice_msg" class="msg"></div>
                        </div>
                        <div id="errors" style="display: none;">
                            <div class="image"></div>
                            <div id="error_msg" class="msg"></div>
                        </div>
                        <div>
                        <label>
                        <%=LiftDomain.Language.Current.USER_PASSWORD%>
                        </label>
                        <asp:TextBox runat="server" ID="user_password" class="inputBox required" size="30" TextMode="Password" />
                        </div>
                        
                        <div>
                        <label>
                        <%=LiftDomain.Language.Current.USER_PASSWORD_CONFIRMATION%>
                        </label>
                        <asp:TextBox runat="server" ID="user_password_confirmation" class="inputBox required" size="30" TextMode="Password" />
                        <asp:CompareValidator ID="PasswordValidator" runat="server"  ControlToValidate="user_password_confirmation"
        ControlToCompare="user_password" ></asp:CompareValidator>
                        </div>
                        
                        
                        <div>
			                <asp:Button runat="server" id="submitBtnPassword" class="submitBtn" />
                        </div>
                    </fieldset>

                </div>
                
                <div id="userTimeZone">
                    <fieldset class="separator">
                        <legend><%=LiftDomain.Language.Current.MY_ACCOUNT_CHANGE_MY_TIME_ZONE%></legend>
                        <img id="midSpinner4" src="/images/spinner.gif" style="display: none;" />
                        <div id="notices_time" style="display: none;">
                            <div class="image">
                                </div>
                            <div id="notice_msg_time" class="msg">
                            </div>
                        </div>
                        <div id="errors_time" style="display: none;">
                            <div class="image">
                                </div>
                            <div id="error_msg_time" class="msg">
                            </div>
                        </div>
                        <div>
                        <label>
                            <%=LiftDomain.Language.Current.USER_TIME_ZONE%>
                        </label>
                          <asp:DropDownList runat="server" ID="timezone_list" />
                        </div>
                        
                        <div>
			                <asp:Button runat="server" id="submitBtnTimeZone" class="submitBtn" />
                        </div>
                    </fieldset>
                </div>
                
                <div id="accountRequests">
                    <fieldset class="separator">
                        <legend><%=LiftDomain.Language.Current.MY_ACCOUNT_MY_PRAYER_REQUESTS%> </legend>
                        <img id="midSpinner2" src="/images/spinner.gif" style="display: none;" />
                        <div id="myPrayerRequests">
                            <%=prayerRequestRendererResult%>
                        </div>
                    </fieldset>
                </div>
                
                <div class="accountSubscriptions">
                    <fieldset class="separator">
                        <legend><%=LiftDomain.Language.Current.MY_ACCOUNT_MY_PRAYER_REQUEST_SUBSCRIPTIONS%></legend>
                        <img id="midSpinner1" src="/images/spinner.gif" style="display: none;" />
                        <div id="mySubscriptions">
                            <%=prayerRequestSubscriptionRendererResult%>
                        </div>
                    </fieldset>
                </div>
                
            </div>
            <!-- leftCol -->
            
            <div id="accountPanelBottom" class="clearBoth">
                <fieldset class="separator">
                    <legend><%=LiftDomain.Language.Current.MY_ACCOUNT_MY_PRAYER_SESSIONS%></legend>
                    <p>
                        <%=LiftDomain.Language.Current.MY_ACCOUNT_YOU_HAVE_FULFILLED%> <%=prayer_requests_sum_label%> <%=LiftDomain.Language.Current.MY_ACCOUNT_PRAYER_REQUESTS_DURING%> <%=prayer_sessions_duration_sum_label%> <%=LiftDomain.Language.Current.MY_ACCOUNT_HOURS_OF_PRAYER_SESSIONS%>.
                    </p>
                    <table border="1" cellpadding="1" cellspacing="1" style="font-size:small;">
                        <tr>
                            <td><b><%=LiftDomain.Language.Current.MY_ACCOUNT_PRAYER_SESSION_START_TIME%></b></td>
                            <td><b><%=LiftDomain.Language.Current.MY_ACCOUNT_PRAYER_SESSION_TOTAL_PRAYER_REQUESTS%></b></td>
                            <td><b><%=LiftDomain.Language.Current.MY_ACCOUNT_PRAYER_SESSION_NOTES%></b></td>
                            <td><b><%=LiftDomain.Language.Current.MY_ACCOUNT_PRAYER_SESSION_TOTAL_TIME%></b></td>
                        </tr>
                        <%=prayerSessionRendererResult%>
                    </table>
                </fieldset>
            </div>
            
        </div>
    </form>
    </div>
</asp:Content>
