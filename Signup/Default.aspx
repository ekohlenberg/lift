<%@ Page Language="C#" MasterPageFile="Signup.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Signup._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
<form id="form1" runat="server" >
    <div id="content" class="default">
		<h3>Create a new LiftPrayer.cc Site</h3>	
		    <table border="0" cellpadding="5" cellspacing="2">
		    <tr>
		        <td align="right">
		            Name of Organization:
		        </td>
		        
		        <td>
		            <asp:TextBox runat="server"  id="orgname" Width="200" />
		        </td>
	        </tr>
	        <tr>
		        <td align="right">
		            Your Name:
		        </td>
		        
		        <td><table>
		            <tr>
		                <td>
		                first
		                </td>
		                <td>
		                last
		                </td>
		            </tr>
		            <tr>
		                <td>
		                <asp:TextBox ID="firstname" runat="server"  Width="95" />
		                </td>
		                <td>
		                <asp:TextBox ID="lastname" runat="server"  Width="95" />
		                </td>
		            </tr>
		            </table>
		        </td>
		     </tr>
		    </table>
		    <br />
		    <br />
		    <p>Please certify that you are an official representative of the organization and that you are permitted to create a LiftPrayer.cc site
for that organization:
		    </p>
		    <p><asp:CheckBox runat="server" ID="auth" />I am authorized to create this LiftPrayer.cc site

		    </p>
		    <p>
		        <asp:Button runat="server" ID="continueBtn" />
		    </p>
    </div>		
  </form>
</asp:Content>