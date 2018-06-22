<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrayerSessionUtil.aspx.cs" Inherits="liftprayer.PrayerSessionUtil" %>

var currentSessionId = <%=currentSessionId%>
var requestList = new Array(<%=requests.Count%>);
var requestViewed = new Array(<%=requests.Count%>);

<% for (int j = 0; j < requests.Count; j++)
   {%>
requestList[<%=j%>] = <%=requests[j].ToString()%>;
requestViewed[<%=j%>] = 0;
<% }%>
//global vars
var currentWallTab;
var sessionIndex = 0;
var totalPrayersPerSession = <%=requests.Count%>;
var prayerSessionTimer = 60;//60 seconds
var sessionInterval;
var timeStart;
var sessionIsPaused = true;
var sessionIsStarted = false;
var pctToAdd = 0;
var sessionPct = 0;
var expandedNotes = [];

//-------------------------------------

/* -----------------------------------*/
/* --->>> PRAYER SESSION UTILS <<<----*/
/* -----------------------------------*/

function saveSessionNote(){
  $('noteSaving').innerHTML = "saving...";
  Element.show('noteSaving');
  
  new Ajax.Request('PrayerSessionNotes.aspx?s=<%=currentSessionId%>', {asynchronous:true, method:'post', postBody:'notes='+escape($F('notesArea'))});
};

function pauseSession(){
  if(!sessionIsPaused){
     sessionIsPaused = true;
     clearInterval(sessionInterval);
     $('psPlayPause').style.backgroundPosition = "right top";
     $('psPlayPause').attributes.title.value = "resume prayer session";
     pctToAdd = sessionPct;
  }else{
     sessionIsPaused = false;
     $('psPlayPause').style.backgroundPosition = "left top";
     $('psPlayPause').attributes.title.value = "pause prayer session";
     startPrayerRequestTimer()
  }
};


function beginNewSession(){
   $('psPlayPause').onclick = pauseSession;
   $('psPlayPause').style.backgroundPosition = "left top";
   sessionIsPaused = false;
   sessionIsStarted = true;
   pctToAdd = 0;
   clearInterval(sessionInterval);
   Element.show('staticSpinner');
   new Ajax.Request('PrayerSessionGetNextRequest.aspx?s=<%=currentSessionId%>&r='+requestList[sessionIndex]+'&v='+requestViewed[sessionIndex]+'&i='+sessionIndex+'&c='+totalPrayersPerSession, 
   
   {
        asynchronous:true,
       eval:false,
         onSuccess: function( updateText ) {
         var psBody = $('prayerSession');
         psBody.innerHTML = updateText.responseText; 
         Element.update("currentRequestLabel", "<%=currentRequestLabel%> " + (sessionIndex+1) + " " + "<%=currentRequestOf%>" + " " + totalPrayersPerSession);
         Effect.appear('prayerSession', {duration: 1.0});
         
         }
        });
   requestViewed[sessionIndex] = 1;
   startPrayerRequestTimer();
};


function startPrayerRequestTimer(){
  if(!sessionIsPaused && sessionIndex < totalPrayersPerSession){
      var tmpDate = new Date();
      timeStart = tmpDate.getTime();
      sessionInterval = setInterval("updateSessionProgress()",1000);
  }
};



function updateSessionProgress(){
try
{
   var tmpDate = new Date();
   var timeNow = tmpDate.getTime();
   var tDiff = timeNow - timeStart;
   var seconds = tDiff / 1000;
   
  
   sessionPct = Math.floor(seconds / prayerSessionTimer*100);
   sessionPct += pctToAdd;
   if(sessionPct < 100){
      $('psProgress').style.width = sessionPct + "%";
   }else{
      getNextRequest();
      pctToAdd = 0;
     // clearInterval(sessionInterval);
      $('psProgress').style.width = "100%";
   }
 } catch(e)
 {
 }
};

function getNextRequest(){
  if(sessionIndex < totalPrayersPerSession && sessionIsStarted){
      pctToAdd = 0;
      clearInterval(sessionInterval);
      sessionIndex++;
      Element.show('staticSpinner');
      
      if(sessionIndex === totalPrayersPerSession){
         if(Element.hasClassName('psNextLink', 'psLinkActive')){
            Element.removeClassName('psNextLink', 'psLinkActive')
            Element.addClassName('psNextLink', 'psLinkInactive')
         }
      }
      
      
      new Ajax.Request('PrayerSessionGetNextRequest.aspx?s=<%=currentSessionId%>&r='+requestList[sessionIndex]+'&v='+requestViewed[sessionIndex]+'&i='+sessionIndex+'&c='+totalPrayersPerSession, 
      {
        asynchronous:true,
       eval:false,
         onSuccess: function( updateText ) {
         var psBody = $('prayerSession');
         //Effect.fade( 'prayerSession' );
         psBody.innerHTML = updateText.responseText; 
         Element.update("currentRequestLabel", "<%=currentRequestLabel%> " + (sessionIndex+1) + " " + "<%=currentRequestOf%>" + " " + totalPrayersPerSession);
         Effect.appear('prayerSession', {duration: 1.0,from:1.0, to:1.0});
         
        }
      });

       startPrayerRequestTimer();
           
      $(nextUrl).value = 'PrayerSessionGetNextRequest.aspx?s=<%=currentSessionId%>&r='+requestList[sessionIndex]+'&v='+requestViewed[sessionIndex]+'&i='+sessionIndex+'&c='+totalPrayersPerSession;
      requestViewed[sessionIndex] = 1;
  }
};

function getPrevRequest(){
  if(sessionIndex > 0 && sessionIsStarted){
      pctToAdd = 0;
      clearInterval(sessionInterval);
     
      Element.show('staticSpinner');
    
      if(sessionIndex == 0){
         if(Element.hasClassName('psPrevLink', 'psLinkActive')){
             Element.removeClassName('psPrevLink', 'psLinkActive')
             Element.addClassName('psPrevLink', 'psLinkInactive')
         }
      }
    
    
     sessionIndex--;
     new Ajax.Request('PrayerSessionGetPrevRequest.aspx?s=<%=currentSessionId%>&r='+requestList[sessionIndex]+'&v='+requestViewed[sessionIndex]+'&i='+sessionIndex+'&c='+totalPrayersPerSession,
     {
     asynchronous:true,
     eval:false,
     onSuccess: function( updateText ) {
         var psBody = $('prayerSession');
         psBody.innerHTML = updateText.responseText;
         Element.update("currentRequestLabel", "<%=currentRequestLabel%> " + (sessionIndex+1) + " " + "<%=currentRequestOf%>" + " " + totalPrayersPerSession);
          Effect.appear('prayerSession', {duration: 1.0});
         var reqLabel = $('currentRequestLabel');
         reqLabel.value = '<%=currentRequestLabel%> ' + (sessionIndex+1) + ' ' + '<%=currentRequestOf%>' + ' ' + totalPrayersPerSession;
        }
        
      });

     $(prevUrl).value = 'PrayerSessionGetPrevRequest.aspx?s=<%=currentSessionId%>&r='+requestList[sessionIndex]+'&v='+requestViewed[sessionIndex]+'&i='+sessionIndex+'&c='+totalPrayersPerSession;
    
     
     requestViewed[sessionIndex] = 1;
     
     startPrayerRequestTimer();
  }
};

