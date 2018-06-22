<%@ Page Language="C#"  MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="WallList.aspx.cs" Inherits="liftprayer.WallList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">      

<script src="/javascripts/validation.js" type="text/javascript"></script>
<script type="text/javascript">
    //var valid = new Validation('newWallForm', { immediate: true, stopOnFirst: false });
    var myWallManager = new wallManager();
</script>

<div id="container">

<div>
    <h1><%=LiftDomain.Language.Current.WALL_MANAGE_WALLS%></h1>
  
</div>

<%=wallListRenderer%>	

<div>
    <form  runat="server" action="WallList.aspx" method="post" class="large-form" id="newWallForm">
	<fieldset>
		<legend><%=LiftDomain.Language.Current.WALL_ADD_A_NEW_WALL%></legend>
        <div>
            <label for="wall_title"><%=LiftDomain.Language.Current.WALL_TITLE%> <span> (<%=LiftDomain.Language.Current.WALL_YOU_CAN_CHANGE_THIS_LATER%>)</span></label>
            <asp:TextBox runat="server" id="wall_title" name="wall_title" size="30" type="text" value="" class="inputBox required"/>
		</div>
		
		<!--
		<fieldset>
		<legend>Wall increments:<em class="hintText">(you <strong>cannot</strong> change this later.)</em></legend>
		<div>
			<label>
				<input type="radio" id="wall_increment_option_hour" name="wall[increment_option]" value="hour" checked="checked" />
				1 hour <em class="hintText">(12:00 1:00 2:00 3:00...)</em></label>
			<label>
				<input type="radio" id="wall_increment_option_half_hour" name="wall[increment_option]" value="half_hour" />
				30 minute <em class="hintText">(12:00 12:30 1:00 1:30...)</em></label>
		</div>
		</fieldset>
		-->
        <!--<input type="submit" value="Add new wall" class="submitBtn" />-->
        <asp:Button runat="server" class="submitBtn" ID="addBtn" />
	</fieldset>
    </form>
</div>

</div>

</asp:Content>