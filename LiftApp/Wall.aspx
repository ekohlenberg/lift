<%@ Page Language="C#" MasterPageFile="Layout.Master" AutoEventWireup="true" CodeBehind="Wall.aspx.cs" Inherits="liftprayer.Wall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        
<script language="javascript">

    function updateIncrement(obj, formAction, dow, tod) {
        if (!obj.tagName) {//clicked yes in modal error window
            myModalError.end();
            obj = $(obj).firstChild;
        }
        if (formAction !== 'say_closed') {
            action = true;
            if (formAction === "unsubscribe_from_increment") {
                //TODO: Remove this and add to UNDO que (Gary)
                action = (confirm("<%=LiftDomain.Language.Current.WALL_ARE_YOU_SURE%>"))
            }
            if (action) {
                new Ajax.Request('WallSubscribe.aspx?action=' + formAction + '&dow=' + dow + '&tod=' + tod, { asynchronous: true, evalScripts: true });
                if (formAction === "unsubscribe_from_increment") {
                    obj.parentNode.remove();
                }
            }
        }
    };

</script>
<div id="container">
        

<h1 id="wallLabel" class="highlite"><%=LiftDomain.Language.Current.WALL_PRAYER_TIME_SELECTION%></h1>
<p><%=sentence1%>  <%=LiftDomain.Language.Current.WALL_SENTENCE2%> <%=LiftDomain.Language.Current.WALL_SENTENCE3%></p> 
<div class="wall_legend">
  <div class="legend_box legend_color_open">&nbsp;</div>
  <div class="legend_text"><%=LiftDomain.Language.Current.WALL_OPEN%></div>
  <div class="legend_box legend_color_partial">&nbsp;</div>
  <div class="legend_text"><%=LiftDomain.Language.Current.WALL_SOME_WALLS_OPEN%></div>
  <div class="legend_box legend_color_full">&nbsp;</div>
  <div class="legend_text"><%=LiftDomain.Language.Current.WALL_FULL%></div>
</div>
<div class="cleaner"></div>

<div class="rule"><hr /></div>
<h3><%=LiftDomain.Language.Current.WALL_PRAYER_TIMES%></h3>
<div id="incrementSlots">
  <table border="0" cellpadding="0" cellspacing="0" class="watchmanWall">
  <tr class="daysOfWeek">
    <td class="noBg">&nbsp;</td>
    
      <td><%=LiftDomain.Language.Current.SHARED_SUNDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_MONDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_TUESDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_WEDNESDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_THURSDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_FRIDAY%></td>
    
      <td><%=LiftDomain.Language.Current.SHARED_SATURDAY%></td>
    
  </tr>
  <%=wallRenderer%>

</table>  
</div>  
      </div>
      
</asp:Content>