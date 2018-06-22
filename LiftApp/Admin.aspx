<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="liftprayer.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        

<div id="container">
        
    <h1><%=LiftDomain.Language.Current.ADMIN_ADMIN_PANEL%></h1>
        
    <div id="adminPanel">
    
        <p class="maxWidth">
        <%=LiftDomain.Language.Current.ADMIN_ADMIN_PANEL_DESCRIPTION1%> <%=LiftDomain.Language.Current.ADMIN_ADMIN_PANEL_DESCRIPTION2%>
        </p>
        
        <div class="rule"><hr /></div>

        <!--
        <a class="adminfloat" href="#">
           <img src="<%=customImagePath%>/layout_administration_icons_help.gif" alt="<%=LiftDomain.Language.Current.ADMIN_HELP_GUIDE%>" border="0" align="left" />	
           <span>
	            <h3><%=LiftDomain.Language.Current.ADMIN_HELP_GUIDE%></h3>
	            <p>
	            <strong><%=LiftDomain.Language.Current.ADMIN_HELP_GUIDE_DESCRIPTION%></strong>
	            </p>
            </span>	   
        </a>
        -->
        <!--
        <a class="adminfloat" href="/configurations">
            <img src="<%=customImagePath%>/layout_administration_icons_config.gif" alt="<%=LiftDomain.Language.Current.ADMIN_CONFIGURATION%>" border="0" align="left" />
            <span>
	            <h3><%=LiftDomain.Language.Current.ADMIN_CONFIGURATION%></h3>
	            <p>
	            <%=LiftDomain.Language.Current.ADMIN_CONFIGURATION_DESCRIPTION%> 
	            </p>
            </span>
        </a>
        -->
        <a class="adminfloat" href="EmailAllUsers.aspx">
            <img src="<%=customImagePath%>/layout_administration_icons_email.gif" alt="<%=LiftDomain.Language.Current.ADMIN_COMMUNITY_EMAILS%>" border="0" align="left" />  
            <span>
                <h3><%=LiftDomain.Language.Current.ADMIN_COMMUNITY_EMAILS%></h3>
                <p>
                <%=LiftDomain.Language.Current.ADMIN_COMMUNITY_EMAILS_DESCRIPTION%>
                </p>
            </span>	     
        </a>
        <!--
        <a class="adminfloat" href="/role/list">
            <img src="<%=customImagePath%>/layout_administration_icons_roles.gif" alt="<%=LiftDomain.Language.Current.ADMIN_MANAGE_ROLES%>" border="0" align="left" /> 
            <span>
	            <h3><%=LiftDomain.Language.Current.ADMIN_MANAGE_ROLES%></h3>
	            <p>
                <%=LiftDomain.Language.Current.ADMIN_MANAGE_ROLES_DESCRIPTION%>
	            </p>
            </span>	   
        </a>
        -->
        <a class="adminfloat" href="<%=manage_organizations_action_page%>">
            <img src="<%=customImagePath%>/layout_administration_icons_groups.gif" alt="<%=LiftDomain.Language.Current.ADMIN_MANAGE_ORGANIZATIONS%>" border="0" align="left" /> 
            <span>
	            <h3><%=LiftDomain.Language.Current.ADMIN_MANAGE_ORGANIZATIONS%></h3>
	            <p>
	            <%=LiftDomain.Language.Current.ADMIN_MANAGE_ORGANIZATIONS_DESCRIPTION%>
	            </p>
            </span>	   
        </a>
        <a class="adminfloat" href="UserList.aspx">
            <img src="<%=customImagePath%>/layout_administration_icons_user.gif" alt="<%=LiftDomain.Language.Current.ADMIN_MANAGE_USERS%>" border="0" align="left" />
            <span>
                <h3><%=LiftDomain.Language.Current.ADMIN_MANAGE_USERS%></h3>
                <p>
                <%=LiftDomain.Language.Current.ADMIN_MANAGE_USERS_DESCRIPTION%>
                </p>
            </span>	       
        </a>
        <a class="adminfloat" href="WallList.aspx">
            <img src="<%=customImagePath%>/layout_administration_icons_wall.gif" alt="<%=LiftDomain.Language.Current.ADMIN_MANAGE_WALLS%>" border="0" align="left" />	
            <span>
                <h3><%=LiftDomain.Language.Current.ADMIN_MANAGE_WALLS%></h3>
                <p>
                <%=LiftDomain.Language.Current.ADMIN_MANAGE_WALLS_DESCRIPTION%>
                </p>
            </span>	   
        </a>
        
    </div>

    <div class="cleaner"></div>

</div>
      
</asp:Content>
