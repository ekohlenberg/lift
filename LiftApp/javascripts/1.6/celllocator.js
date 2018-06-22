// -----------------------------------------------------------------------------------
//
//	Celllocator v1.0  
//	by Travis Beck - smoothoperatah@gmail.com
//	11/17/06
//
//	For more information on this script, visit:
//	...
//
//	Licensed under the Creative Commons Attribution 2.5 License - http://creativecommons.org/licenses/by/2.5/
//	
//	Credit also due to those who have helped, inspired, and made their code available to the public.
//	Including: Lokesh Dhakar(huddletogether.com), Peter-Paul Koch(quirksmode.org), Thomas Fuchs(script.aculo.us).
//
//  This class is dependent upon prototype.
//													
// -----------------------------------------------------------------------------------


// -----------------------------------------------------------------------------------

//
//	Configuration
//
var activeColumnClass = "highliteColumn";		
var activeRowClass = "highliteRow";
var activeSlotClass = "activeSlot";

//if you are using another tag to wrap your text inside your row and column <tr> tags, 
//then put the tag name here ('span', 'div', 'p', etc..).
//otherwise leave as false or null

var rowChildTag = 'span'
var columnChildTag = false;

// -----------------------------------------------------------------------------------

//
//	Global Variables
//

var activeColumn = null;
var activeRow = null;
var activeTable = null;
var rowLabel;
var columnLabel;
var saveContent;

// -----------------------------------------------------------------------------------

var celllocator = Class.create();

celllocator.prototype = {
	
	// initialize()
	// Constructor runs on completion of the DOM loading. Loops through TD tags looking for 
	// 'celllocator' references and applies mouseover events to appropriate links. 

	initialize: function() {	
		activeColumn = null;
		activeRow = null;
		activeTable = null;
			
		if (!document.getElementsByTagName){ return; }
		var cells = document.getElementsByTagName('td');

		// loop through all TD tags
		for (var i=0; i<cells.length; i++){
			var cell = cells[i];
			
			var relAttribute = String(cell.getAttribute('rel'));
			
			// use the string.match() method to catch 'celllocator' references in the rel attribute
			if (relAttribute.toLowerCase().match('celllocator')){
				cell.onmouseover = function () {myCellLocator.locate(this); return false;}
				cell.onmouseout = function () {myCellLocator.clear(this); return false;}
			}
		}
	},
	
	locate: function(el){
		//find out where in the node list this element is
		
		var columnIndex = myCellLocator.findColumnIndex(el);
		if(columnIndex){
			myCellLocator.highLiteColumnHeading(columnIndex, el);
			myCellLocator.highLiteRowHeading(el);
			myCellLocator.activate(el);
		}else{
			//failure could not find index of node
			console.warning("could not find node index");
		}
	},
	
	findColumnIndex: function(el){
		//loop through all siblings till we find me and return the column index.
		var parentTD = el;
		var parentTR = parentTD.parentNode;
		var tdSibling = parentTR.firstChild;
		var index = 0;
		while (tdSibling){
			if(tdSibling.tagName){//make sure its nots a raw text node.
				if(tdSibling == parentTD) return(index);
				index ++;
			}
			tdSibling = tdSibling.nextSibling;
		}
		return null;
	},
	
	highLiteColumnHeading: function(index, el){
		if(!activeTable){
			activeTable = myCellLocator.findTableElement(el);
		}
		//highlight the column indicator
		var rows = activeTable.getElementsByTagName('tr');
		var firstRow = rows[0];
		var cells = firstRow.getElementsByTagName('td');
		var columnToHilite = cells[index];
		
		if(columnChildTag){
			var childTag = columnToHilite.getElementsByTagName(rowChildTag)[0];
			columnToHilite = childTag;
		}

		columnLabel = columnToHilite.firstChild.nodeValue;
	
		if((activeColumn) && (activeColumn != columnToHilite)){
			if(Element.hasClassName(activeColumn,activeColumnClass)){
				Element.removeClassName(activeColumn,activeColumnClass);
			}
		}
		
		activeColumn = columnToHilite;
		Element.addClassName(columnToHilite,activeColumnClass);
	},
	
	highLiteRowHeading: function(el){
		var parentTD = el;
		var parentTR = parentTD.parentNode;
		var columns = parentTR.getElementsByTagName('td');
		var rowToHilite = columns[0];
		
		if(rowChildTag){
			var childTag = rowToHilite.getElementsByTagName(rowChildTag)[0];
			rowToHilite = childTag;
		}
		
		rowLabel = rowToHilite.firstChild.nodeValue;
		
		if((activeRow) && (activeRow != rowToHilite)){
			if(Element.hasClassName(activeRow,activeRowClass)){
				Element.removeClassName(activeRow,activeRowClass);
			}
		}
		activeRow = rowToHilite;
		Element.addClassName(rowToHilite,activeRowClass);
	},
	
	findTableElement: function(el){
		pNode = el.parentNode;
		while(!pNode.tagName.toLowerCase().match("table")){
			pNode = pNode.parentNode;
		}
		return(pNode);
	},
	
	clear: function(el){
		if(Element.hasClassName(activeColumn,activeColumnClass)){
			Element.removeClassName(activeColumn,activeColumnClass);
			activeColumn = null
		}
		
		if(Element.hasClassName(activeRow,activeRowClass)){
			Element.removeClassName(activeRow,activeRowClass);
			activeRow = null;
		}
		
		myCellLocator.deactivate(el);
		
	},
	
	activate: function(el){
		if(activeSlotClass){
			Element.addClassName(el,activeSlotClass);
		}
		saveContent = el.innerHTML;
		//el.innerHTML = rowLabel + " - " + columnLabel + "<br/><br/>" + saveContent.toUpperCase();
	},
	
	deactivate: function(el){
		Element.removeClassName(el,activeSlotClass);
		//el.innerHTML = saveContent;
	}
	
}



// addLoadEvent()
// Adds event to window.onload without overwriting currently assigned onload functions.
// Function found at Simon Willison's weblog - http://simon.incutio.com/
//
function addLoadEvent(func)
{	
	var oldonload = window.onload;
	if (typeof window.onload != 'function'){
    	window.onload = func;
	} else {
		window.onload = function(){
		oldonload();
		func();
		}
	}
}

// ---------------------------------------------------

function initCelllocator() {

		
		myCellLocator = new celllocator(); 
}

addLoadEvent(initCelllocator);	// run initLightbox onLoad