// Place your application-specific JavaScript functions and classes here
// This file is automatically included by javascript_include_tag :defaults


//user agent
var ua = Class.create();

ua.prototype = {
	initialize: function(){
	    var u = navigator.userAgent, d = document;
	    this.ie = typeof d.all != "undefined";
	    this.ns4 = typeof d.layers != "undefined";
	    this.dom = typeof d.getElementById != "undefined";
	    this.safari = /Safari/.test(u);
	    this.moz = /Gecko/.test(u) && !this.safari;
	    this.mie = this.ie && /Mac/.test(u);
	    this.win9x = /Win9/.test(u) || /Windows 9/.test(u);
	    this.o7 = /Opera 7/.test(u);
	    this.opera = /Opera/.test(u);
	    this.supported = (typeof d.write != "undefined") 
	                     && (this.ie || this.ns4 || this.dom);
    }
};

myua = new ua();

//function trace(message){
//	try{
		//console.info(message);
//	}catch(err){
//		alert(message);
//	}
//}



/* -----------------------------------*/
/* -------->>> EMAIL ADMIN <<<--------*/
/* -----------------------------------*/
var EmailAdmin = Class.create();
EmailAdmin.prototype = {
  
  initialize: function() {
    this.emailGroups = $('emailGroups');
    this.emailto = $('emailto');
    this.wallList = $('wallList');
    this.memberSearch = $('memberSearch');
    this.groupSearch = $('groupSearch');
    this.searchInput = $('member_search');
    if($('specificMembers')){
      this.memberList = $('specificMembers').down('ul');
    }
    this.wallSelect = $('wallSelect');
    this.emailForm = $('emailForm');
    this.emailSubmit = $('emailSubmit');
    
    this.addEvents();
  },
  
  addEvents: function(){
    if(this.emailGroups){
      Event.observe(this.emailGroups, 'change', this.checkGroup.bindAsEventListener(this));
    }
    if(this.searchInput){
      Event.observe(this.searchInput, 'keyup', this.findLiveUsers.bindAsEventListener(this));
    }
    if(this.wallSelect){
      Event.observe(this.wallSelect, 'change',this.wallSelected.bindAsEventListener(this));
    }
    if(this.emailForm){
      Event.observe(this.emailForm, 'submit', this.formSubmitted.bindAsEventListener(this));
    }
	if( $('groupSelect') ){
		  Event.observe($('groupSelect'), 'change', this.groupSelected.bindAsEventListener(this));
	}
	
  },
  
  checkGroup: function(){
    this.emailto.value = '';
    this.hideHelpers();
    this.memberList.innerHTML = '';
    var value = this.emailGroups.value;
    if(!value.match(/specific/)){
      this.emailto.value = "group_" + value;
      this.hideHelpers();
      return;
    }
    
  	switch(value.replace('specific','').toLowerCase()){
			case "wall":
				this.showWallList();
				break;
			case "member":
				this.showMemberSearch();
				break;
			case "group":
				this.showGroupSearch();
				break;
			default: return false;
		};
  },
  
  wallSelected: function(){
    this.emailto.value = "wall_" + this.wallSelect.value;
  },
 
  
  
  showWallList: function(){
    this.wallList.show();
    this.highlightLabel(this.wallList);
    this.wallSelected();
  },

 groupSelected: function(){
  this.groupto.value = "group_"+this.groupSelect.value;	

 },
  
  showMemberSearch: function(){
    this.memberSearch.show();
    this.highlightLabel(this.memberSearch);
  },
 
  showGroupSearch: function(){
	this.groupSearch.show();
	this.highlightLabel(this.groupSearch);
  },
  
  resetGroups: function(){
    this.hideHelpers();
    this.emailGroups.options[0].selected = true;
  },
  
  hideHelpers: function(){
    this.wallList.hide();
    this.memberSearch.hide();
	this.groupSearch.hide();
  },
  
  highlightLabel: function(el){
    var label = el.down('label');
    new Effect.Highlight(label, {duration: 1.5});
  },
  
  addMember: function(fname, lname, id, email){
    var value = this.emailto.value;
    regex = new RegExp('user_'+ id);
    if(value.match(regex)){
      //we've already added this member
      return;
    }
    
    if(!this.emailto.value){
      this.emailto.value = "user_" + id;
    }else{
      this.emailto.value += "," + "user_" + id ;
    }
    this.memberList.innerHTML += '<li><strong>'+fname+ ' ' + lname + '</strong> &lt;' + email + '&gt;</li>';
  },
    
	findLiveUsers:function(){
	  var post_value = this.searchInput.value;
		if(post_value){
			new Ajax.Updater('searchResults', '/account/auto_complete', {
				asynchronous:true, 
				evalScripts:true, 
				onComplete:function(){
					Element.hide('staticSpinner')
				}, 
				onLoading:function(){
					Element.show('staticSpinner')
				},
				parameters:"q=" + post_value, 
				onlyLatestOfClass: 'liveUserSearch'
			});
		}	
	},
	
	formSubmitted: function(){
this.emailSubmit.disabled = true;
this.emailSubmit.value = "sending...";
  },

	messageEntered: function(){
this.emailSubmit.disabled = false;
  }
  
  
};

/* -----------------------------------*/
/* -------->>> WALL ADMIN <<<---------*/
/* -----------------------------------*/
var walladmin = Class.create();

walladmin.prototype = {

    initialize: function() {
        this.wallUndoArray = [];
        //options ------------------------
        this.dragClass = 'userSlot';
        this.dropClass = 'timeSlot';
        this.hoverClass = 'timeSlotHover';

        //configuration-------------------
        this.dragOptions = { revert: true, starteffect: null, endeffect: null, zindex: 15000, scroll: window, scrollSensitivity: 30 };
        this.addButtonClass = "addUser";
        this.newUserModal = {};
        this.newUserModalIsOpen = false;
        this.dragParent = {};
        this.increment_id = '';
        this.dropObjects = [];
        this.liveUserObserver = false;
        this.ctrlDown = false;
        this.reviewList = [];
        //this.checkFormFocus();
        //document.onkeydown = this.keyCheck;
        //document.onkeyup = this.clearKeys;
        this.generateDraggables();
        this.generateDroppables();
    },

    keyCheck: function(evt) {
        var keyCode = myWalladmin.getKeyCode(evt);
        if (keyCode === 17) {//Ctrl key
            myWalladmin.ctrlDown = true;
        } else if (keyCode === 90 && myWalladmin.ctrlDown) {//Check for Ctrl - Z
            myWalladmin.ctrlDown = false; //dont want to auto repeat;
            //make sure an input does not have focus
            myWalladmin.undo();
        }
    },

    clearKeys: function(evt) {
        myWalladmin.ctrlDown = false;
    },

    getKeyCode: function(evt) {
        try {
            return (event.keyCode);
        } catch (err) {
            try {
                return (evt.keyCode);
            } catch (err) { }
        }
        return false;
    },

    /*	checkFormFocus: function(){
    var frmElements1 = Form.getElements('newUserForm');
    var frmElements2 = Form.getElements('findUserForm');
    for(var i= 0; i < frmElements1.length; i++){
    var frmElement = frmElements1[i];
    frmElement.onfocus += " console.info(this)";
    frmElement.onblur += " console.info(this)";
    }
    },
    */
    generateDraggables: function() {
        var cells = document.getElementsByClassName(this.dragClass);
        for (var i = (cells.length - 1); i >= 0; i--) {//counting backwards is 200% faster... really... i promise... i read it on the internets.
            new Draggable(cells[i], this.dragOptions);
        }

        dragListener = {};

        dragListener.onStart = function(evtName, draggable, evt) {
            this.dragParent = myWalladmin.findParentDrop(draggable.element);
            this.dragParent.style.zIndex = 1000; //z-index fix for IE
        }

        dragListener.onEnd = function(evtName, draggable, evt) {
            this.dragParent.style.zIndex = 2; //z-index fix for IE
            var pos = [Event.pointerX(evt), Event.pointerY(evt)];
            var dropObj;
            for (var i = (myWalladmin.dropObjects.length - 1); i >= 0; i -= 1) {//loop through the drop opjects
                dropObj = myWalladmin.dropObjects[i];
                //SAFARI DEBUGGINS (... yeah debuggins. wanna fight about it?)
                /*
                var p = Position.cumulativeOffset(dropObj);
                var d = dropObj.getDimensions();
				
				var x = { min: Position.cumulativeOffset(dropObj)[0], max: p[0] + d.width };
                var y = { min: Position.cumulativeOffset(dropObj)[1], max: p[1] + 55 };//INFO: this is the hardcoded height of the cell, safari2 was getting 0
				
				//console.log(x.min + " " + x.max + " | " + y.min + " " + y.max);
				
				if(pos[0] > x.min && pos[0] > x.max && pos[1] > y.min && pos[1] < y.max){
                console.log("true" + " " + myWalladmin.findDayLabel(dropObj).firstChild.nodeValue);
                }
                */
                //RESULTS, with code above, safari was registering seemingly random hit objects. Should probably have a chat with steve.

                if (Position.within(dropObj, pos[0], pos[1])) {
                    myWalladmin.cellDropped(draggable.element, dropObj, evt);
                    return;
                }
            }
        }
        Draggables.addObserver(dragListener);
    },

    generateDroppables: function() {
        this.dropObjects = document.getElementsByClassName(this.dropClass);
    },

    cellDropped: function(dragObj, dropObj, evt) {
        //make sure its being dropped somewhere new
        if (dropObj !== dragObj.parentNode) {
            //check if there is already a user in this slot.
            if (myWalladmin.slotIsVacant(dropObj)) {
                //this is an empty slot, move the user to this new slot
                myWalladmin.moveToNewSlot(dragObj, dropObj);
            } else {
                //this slot has a user in it, lets swap these two users
                myWalladmin.swapIncrements(dragObj, dropObj);
            }
        }
    },

    moveToNewSlot: function(dragObj, dropObj, noUndo) {
        if (!noUndo) {
            this.addToMoveUndo(dragObj);
        };
        //remove the add button in the droppable and
        //add one to the now vacant space.

        /*var addButton = this.findAddButton(dropObj);
        var addButtonClone = addButton;
        dragObj.parentNode.appendChild(addButtonClone);*/

        //swap the dragobj to the new position
        var newDragObj = this.moveDragObj(dragObj, dropObj);

        //make the new one draggable
        this.addNewDraggable(newDragObj);
        this.updateWall(newDragObj, dropObj);
    },

    swapIncrements: function(dragObj, dropObj, noUndo) {
        var dropObj2 = this.findParentDrop(dragObj);
        if (!noUndo) {
            this.addToSwapUndo(dragObj, dropObj);
        }
        //copy drag objects
        var drag1 = dragObj.remove();
        drag1.style.left = 0;
        drag1.style.top = 0;
        drag1.style.zIndex = 1000;

        var dragObj2 = this.findChildDrag(dropObj);
        var drag2 = dragObj2.remove();
        drag2.style.left = 0;
        drag2.style.top = 0;
        drag2.style.zIndex = 1000;

        //change labels
        this.changeDragLabel(drag2, dropObj2);
        this.changeDragLabel(drag1, dropObj);

        //append new objects
        dropObj2.appendChild(drag2);
        dropObj.appendChild(drag1);

        //make draggable
        this.addNewDraggable(drag1);
        this.addNewDraggable(drag2);

        //update wall
        this.updateWall(drag2, dropObj2);
        this.updateWall(drag1, dropObj);
    },

    addToMoveUndo: function(dragObj) {
        var dropObj = dragObj.parentNode;
        var localUndo = [dragObj, dropObj];
        var userName = this.findUserLabel(dragObj).firstChild.nodeValue;
        var dayLabel = this.findDayLabel(dropObj).firstChild.nodeValue;
        var label = userName + ' has been moved from ' + dayLabel;
        var localUndo = [dragObj, dropObj, label, "move"];
        var user_id = dragObj.id.replace('user_', '');
        this.wallUndoArray.push(localUndo);
        this.showUndoLink(label);
        this.addToReviewList(user_id);
    },

    addToSwapUndo: function(dragObj, dropObj) {
        var revertDropObj = dragObj.parentNode;
        var userName1 = this.findUserLabel(dragObj).firstChild.nodeValue;
        var dragObj2 = this.findChildDrag(dropObj);
        var userName2 = this.findUserLabel(dragObj2).firstChild.nodeValue;
        var label = userName1 + ' and ' + userName2 + ' have been swapped';
        var localUndo = [dragObj, revertDropObj, label, "swap"];
        var user_id = dragObj.id.replace('user_', '');
        this.wallUndoArray.push(localUndo);
        this.showUndoLink(label);
        var user_id = dragObj.id.replace('user_', '');
        this.addToReviewList(user_id);
        var user_id = dragObj2.id.replace('user_', '');
        this.addToReviewList(user_id);

    },

    addToRemoveUndo: function(dragObj, dropObj) {
        var localUndo = [dragObj, dropObj];
        var userName = this.findUserLabel(dragObj).firstChild.nodeValue;
        var dayLabel = this.findDayLabel(dropObj).firstChild.nodeValue;
        var label = userName + ' has been unsubscribed from ' + dayLabel;
        var localUndo = [dragObj, dropObj, label, "remove"];
        var user_id = dragObj.id.replace('user_', '');
        this.wallUndoArray.push(localUndo);
        this.showUndoLink(label);
        this.addToReviewList(user_id);
    },

    undo: function() {
        //make sure we even have undo data.
        if (this.wallUndoArray.length > 0) {
            $('undo').hide();
            var undoArray = this.wallUndoArray.pop();
            var user_id = undoArray[0].id.replace('user_', '');
            this.removeFromReviewList(user_id);
            switch (undoArray[3]) {
                case "swap":
                    this.swapIncrements(undoArray[0], undoArray[1], true);
                    break;
                case "move":
                    this.moveToNewSlot(undoArray[0], undoArray[1], true);
                    break;
                case "remove":
                    this.undoRemoveUser(undoArray[0], undoArray[1]);
                    break;
                default: return false;
            };

            //setup new undo link
            if (this.wallUndoArray.length > 0) {
                var localArray = this.wallUndoArray[this.wallUndoArray.length - 1];
                var label = localArray[2];
                this.showUndoLink(label);
            }
        }
    },

    addToReviewList: function(user_id) {
    /*
        if (!this.inReviewList(user_id)) {
            this.reviewList.push(user_id);
            $('changed_user_array').value = this.reviewList.toString();
            if (!($('changedUsers').visible())) {
                new Effect.Appear('changedUsers');
            }
        }
     */
    },

    review: function() {
        new Ajax.Updater('reviewList', '/wall/get_changed_users', {
            asynchronous: true,
            method: 'post',
            postBody: 'user_array=' + this.reviewList.toString(),
            onComplete: function() {
                Element.hide('staticSpinner');
                new Effect.BlindDown('reviewList', { duration: 0.3 });
            },
            onLoading: function() {
                Element.show('staticSpinner');
            }
        });
    },

    removeFromReviewList: function(user_id) {
        for (var i in this.reviewList) {
            if (this.reviewList[i] === user_id) {
                var removed = this.reviewList.splice(i, 1);
            }
        }
        $('changed_user_array').value = this.reviewList.toString();
        if (this.reviewList.length <= 0) {
            $('changedUsers').hide();
        }
    },

    inReviewList: function(user_id) {
        for (var i in this.reviewList) {
            if (this.reviewList[i] === user_id) {
                return true;
            }
        }
        return false;
    },


    showUndoLink: function(label) {
    /*
        $('undoInfo').innerHTML = label;
        $('undo').hide();
        new Effect.Appear('undo', { duration: 0.5 });
    */    
    },


    findParentDrop: function(dragObj) {
        var pNode = dragObj.parentNode;
        while (!Element.hasClassName(pNode, this.dropClass)) {
            pNode = pNode.parentNode;
        }
        return (pNode);
    },

    findChildDrag: function(dropObj) {
        var children = dropObj.childNodes;
        for (var i = 0; i < children.length; i++) {
            if (children[i].className && Element.hasClassName(children[i], this.dragClass)) {
                return (children[i]);
            }
        }
        return;
    },

    findParentDrag: function(childObj) {
        var pNode = childObj.parentNode;
        while (!Element.hasClassName(pNode, this.dragClass)) {
            pNode = pNode.parentNode;
        }
        return (pNode);
    },

    findAddButton: function(dropObj) {
        var children = dropObj.childNodes;
        for (var i = 0; i < children.length; i++) {
            if (children[i].className === this.addButtonClass) {
                return (children[i]);
            }
        }
        return (false);
    },

    findDayLabel: function(dropObj) {
        //find and copy the label on the dropObj
        //dropObj.cleanWhitespace();
        var children = dropObj.childNodes;
        for (var i = 0; i < children.length; i++) {
            if (children[i].className === 'timeInfo') {
                return (children[i]);
            }
        }
        return false;
    },

    findUserLabel: function(dragObj) {
        var children = dragObj.childNodes;
        for (var i = 0; i < children.length; i++) {
            if (children[i].className === 'userCardName') {
                return (children[i]);
            }
        }
        return false;
    },

    slotIsVacant: function(dropObj) {
        var children = dropObj.childNodes;
        for (var i = 0; i < children.length; i++) {
            if (children[i].className && Element.hasClassName(children[i], this.dragClass)) {
                return (false);
            }
        }
        return (true);
    },

    moveDragObj: function(dragObj, dropObj) {
        //copy the drag object to this cell
        dragClone = dragObj.remove(); //NOTE: had to change prototype.js to make this work
        //reset the clone's position
        dragClone.style.left = 0;
        dragClone.style.top = 0;
        dragClone.style.zIndex = 1000;
        this.changeDragLabel(dragClone, dropObj);
        dropObj.appendChild(dragClone);
        return (dragClone);
    },

    addNewDraggable: function(dragObj) {
        new Draggable(dragObj, this.dragOptions);
    },

    changeDragLabel: function(dragObj, dropObj) {
        var newLabel = this.findDayLabel(dropObj).firstChild.nodeValue;
        //find and replace the label on the dragObj
        var children = dragObj.childNodes;
        for (var i = 0; i < children.length; i++) {
            if (children[i].className === 'userTimeInfo') {
                children[i].innerHTML = newLabel;
            }
        }
    },

    updateWall: function(dragObj, dropObj) {
        var user_id = dragObj.id.sub('user_', '');
        this.increment_id = dropObj.id.sub('cell_', '');
        var uri = window.location.href.split('/');
        var wall_id = uri[uri.length - 1];
        new Ajax.Request('/wall/swap_userslot', {
            asynchronous: true,
            method: 'post',
            postBody: 'user_id=' + user_id + '&increment_id=' + this.increment_id + '&wall_id=' + wall_id,
            onComplete: function() {
                Element.hide('staticSpinner')
            },
            onLoading: function() {
                Element.show('staticSpinner')
            }
        });
    },

    unsubscribe: function(dragObj, dropObj, user_id) {
        //var user_id = dragObj.id.sub('user_', '');
        //this.increment_id = dropObj.id.sub('cell_', '');
        //var uri = window.location.href.split('/');
        //var wall_id = uri[uri.length - 1];
        new Ajax.Request('WallManageUnsubscribe.aspx', {
            asynchronous: true,
            method: 'post',
            postBody: 'user_id=' + user_id,
            onComplete: function() {
                Element.hide('staticSpinner')
            },
            onLoading: function() {
                Element.show('staticSpinner')
            }
        });
    },

    newUserPopup: function(element) {
        //reset the z-index of the current newusermodal parent
        try {
            var pNode = this.findParentDrop(this.newUserModal);
            pNode.style.zIndex = 1; //z-index fix for IE
        } catch (err) { }

        var clone = $('newUserClone');
        this.newUserModal = clone.remove();
        var pNode = this.findParentDrop(element);
        pNode.style.zIndex = 1000; //z-index fix for IE

        this.increment_id = pNode.id.sub('cell_', '');

        //add day label to card
        this.updatenewUserModalLabel(this.newUserModal, pNode);

        //attach card to cell
        pNode.appendChild(this.newUserModal);


        //reset newuserdialog options
        $('dashboardOptions').show();
        $('usersearch').value = 'live user search';
        $('addExistingUser').hide();
        $('addNewUser').hide();
        // Element.update('findUserResults', "<ul class='userlists'><li class='userlist'>Start typing the first name or last name above, and we'll do the rest.</li><li class='userlist'>The results will be listed here, once you find what you're looking for, give it a click. Simple.</li></ul>");
        this.newUserModal.style.position = 'absolute';
        this.newUserModal.style.top = 'auto';
        this.newUserModal.style.left = 'auto';
        this.newUserModal.style.width = '236px';
        this.newUserModal.style.height = '140px';

        $('login').value = "username";
        $('first_name').value = "first name";
        $('last_name').value = "last name";
        $('email').value = "email";
        $('phone').value = "phone"
        $('password').value = "password";
        $('password').type = "text";
        $('current_cell').value = this.increment_id;

        this.createFieldObserver(); //live search observer
        //scale card
        this.scaleCard(this.newUserModal);
        return false;
    },

    createFieldObserver: function() {
        Event.observe('usersearch', 'keyup', this.findLiveUsers);
    },

    findLiveUsers: function() {
        var post_value = $F('usersearch');
        var findUserResults;
        var results;
        var cell = $F('current_cell');

        findUserResults = $('findUserResults');

        if (post_value !== '' && post_value !== 'live user search') {

            new Ajax.Updater('findUserResults', 'WallManageAutoComplete.aspx', {
                asynchronous: true,
                evalScripts: false,
                onComplete: function(response) {
                    Element.hide('staticSpinner');
                },
                onLoading: function() {
                    Element.show('staticSpinner')
                },
                parameters: "q=" + post_value + "&cell=" + cell,
                onlyLatestOfClass: 'liveUserSearch'
            })

        }
    },



    scaleCard: function(newUserModal) {
        if (!myua.opera) {//opera has problems with this animation
            new Effect.Scale(newUserModal, 100, Object.extend({
                beforeStart: function(effect) {
                    $(effect.element).style.display = 'block';
                    Element.setOpacity($(effect.element), 0);
                },
                afterUpdate: function(effect) {
                    Element.setOpacity($(effect.element), effect.position);
                },
                scaleFrom: 0,
                scaleFromCenter: true,
                duration: 0.3,
                afterFinish: function(effect) {
                    //$(effect.element).childNodes.each(function(el){
                    // new Effect.Appear(el,{duration:0.2});
                    //});
                }
            }, arguments[3] || {}));
        } else {
            newUserModal.style.display = "block";
        }
    },

    updatenewUserModalLabel: function(newUserModal, dropObj) {
        //find the legend tag
        var tag_legend = newUserModal.getElementsByTagName('legend')[0];
        tag_legend.innerHTML = 'Add New User';
        var em_tag = document.createElement('em');
        var label = document.createTextNode(' - ' + this.findDayLabel(dropObj).firstChild.nodeValue);
        em_tag.appendChild(label);
        tag_legend.appendChild(em_tag);
        //append day label with this.findDayLabel(dropObj);
    },

    closenewUserModal: function(el) {
        var pNode = this.findParentDrop(el);
        pNode.style.zIndex = 1; //z-index fix for IE
        el.parentNode.style.display = "none";
    },

    addNewUser: function() {
        //clear any undo history, because this action could very well break it badly.
        this.wallUndoArray = [];
        //$('undo').hide();

        Element.show('staticSpinner');
        if (this.formIsValid()) {
            var post_body = Form.serialize($('newUserForm')) + "&cell=" + this.increment_id;
            new Ajax.Request('WallManageAddUser.aspx', {
                asynchronous: true,
                method: 'post',
                postBody: post_body,
                eval: true,
                onComplete: function() {
                    Element.hide('staticSpinner')
                },
                onLoading: function() {
                    Element.show('staticSpinner')
                },
                onFailure: function() {
                    myWalladmin.throwFormError('that user already exists');
                }
            });
        }
    },

    addExistingUser: function(first_name, last_name, user_id, wall_id, dow, tod) {
        //clear any undo history, because this action could very well break it badly.
        this.wallUndoArray = [];


        this.newUserAdded(first_name, last_name, user_id);
        var post_body = "action=s" + "&user_id=" + user_id + "&wall_id=" + wall_id + "&dow=" + dow +"&tod=" + tod;
        new Ajax.Request('WallManageSubscribeExistingUser.aspx', {
            asynchronous: true,
            method: 'post',
            onComplete: function() {
                Element.hide('staticSpinner')
            },
            onLoading: function() {
                Element.show('staticSpinner')
            },
            postBody: post_body
        });

        return false;
    },

    transferUser: function(first_name, last_name, user_id, wall_id, dow, tod, day, wall_name) {
        if (confirm(first_name + " " + last_name + " is already subscribed to " + day + " on the " + wall_name + ", would you like to transfer them to this slot?")) {
            //var post_body = "current_increment=" + old_increment + "&user_id=" + user_id + "&new_increment=" + this.increment_id;
            var post_body = "action=u" + "&user_id=" + user_id + "&wall_id=" + wall_id + "&dow=" + dow + "&tod=" + tod;
            new Ajax.Request('WallManageSubscribeExistingUser.aspx', {
                asynchronous: true,
                method: 'post',
                onComplete: function() {
                    Element.hide('staticSpinner')
                },
                onLoading: function() {
                    Element.show('staticSpinner')
                },
                postBody: post_body
            });

            //remove the usercard for the transfered user if its on this view
            try {
                var userCell = "user_" + user_id;
                $(userCell).remove();
            } catch (err) { }

            //add the new card
            this.newUserAdded(first_name, last_name, user_id);
            return false;
        }
    },

    newUserAdded: function(first_name, last_name, user_id) {
        Element.hide('staticSpinner');
        var parentNode = this.findParentDrop(this.newUserModal);
        parentNode.zIndex = 1;
        this.newUserModal.style.display = "none";
        var newCard = this.createBlankUserCard();
        var dayLabel = this.findDayLabel(parentNode);
        parentNode.appendChild(newCard);

        var userLabels = newCard.getElementsByTagName('span');
        userLabels[0].innerHTML = first_name.substr(0, 1) + ". " + last_name;
        userLabels[1].innerHTML = dayLabel.firstChild.nodeValue;
        newCard.style.display = 'block';
        newCard.style.position = 'absolute';
        newCard.style.position = 'relative';
        newCard.id = 'user_' + user_id;
        this.addNewDraggable(newCard);
        //this.addToReviewList(user_id);
    },

    undoRemoveUser: function(dragObj, dropObj) {
        var post_body = "user_id=" + dragObj.id.replace('user_', '') + "&increment_id=" + dropObj.id.replace('cell_', '');
        new Ajax.Request('/wall/subscribe_existing_user_to_increment', {
            asynchronous: true,
            method: 'post',
            onComplete: function() {
                Element.hide('staticSpinner')
            },
            onLoading: function() {
                Element.show('staticSpinner')
            },
            postBody: post_body
        });
        dropObj.appendChild(dragObj);
        //this.addNewDraggable(dragObj);
    },

    removeUser: function(closeBtn, user_id) {
        //find dragObj
        var dragObj = this.findParentDrag(closeBtn);
        //find dropObj
        var dropObj = this.findParentDrop(dragObj);
        //remove dragObj
        dragObj.remove();
        //update wall
        this.unsubscribe(dragObj, dropObj, user_id);
        this.addToRemoveUndo(dragObj, dropObj);
    },

    createBlankUserCard: function() {
        //creates the following markup
        //      <div class="slot-user userSlot">
        //			<a href="javascript:void(0);" class="wall-remove-btn" title="remove from this slot" onclick="myWalladmin.removeUser(this);"></a>
        //			<span class="userCardName">G. Evans</span>
        //			<br />
        //			<span class="userTimeInfo">Thursday 1:00 AM</span>
        //		</div>

        var blankCard = document.createElement('div');
        blankCard.className = "slot-user userSlot";

        var removeLink = document.createElement('a');
        removeLink.setAttribute('href', 'javascript:void(0);');
        removeLink.className = "wall-remove-btn";
        removeLink.setAttribute('title', 'remove from this slot');
        removeLink.onclick = function() { myWalladmin.removeUser(this); return false; }
        blankCard.appendChild(removeLink);

        var userSpan = document.createElement('span');
        userSpan.className = "userCardName";
        blankCard.appendChild(userSpan);

        var breakTag = document.createElement('br');
        blankCard.appendChild(breakTag);

        var timeSpan = document.createElement('span');
        timeSpan.className = "userTimeInfo";
        blankCard.appendChild(timeSpan);

        return blankCard;
    },

    formIsValid: function() {
        $('userErrorSpan').innerHTML = '';
        var elementList = [$('login'), $('first_name'), $('last_name'), $('email'), $('password')];
        for (var i = (elementList.length - 1); i >= 0; i--) {
            if (elementList[i].value === '' || !elementList[i].value) {
                this.throwFormError(elementList[i].name + " cannot be empty");
                return (false);
            }
        }

        if (($F('email').indexOf('@') < 0) || ($F('email').indexOf('.') < 0)) {
            this.throwFormError("please enter a valid email<br/> address");
            return (false);
        }

        return (true);
    },

    throwFormError: function(errorMsg) {
        //INFO: error class span.userAddError
        Element.hide('staticSpinner');
        $('userErrorSpan').innerHTML = errorMsg;
    }

};



/* -----------------------------------*/
/* ------>>> ACCOUNT UTILS <<<--------*/
/* -----------------------------------*/


function toggleNotes(obj){
  var divToExpand = obj.parentNode.nextSibling;
  //find next div with class of 'psNotesCollapse'
  while (divToExpand.className !== "psNotesCollapse"){
    divToExpand = divToExpand.nextSibling;
  }
  Element.cleanWhitespace(divToExpand);
  if(Element.getHeight(divToExpand) < 1){//expand
      var pNotes = divToExpand.firstChild;
      divToExpand.style.height = "1px";
      new Effect.Scale(divToExpand, ((Element.getHeight(pNotes)+10)*100), {scaleX: false, scaleY: true, scaleContent: false, duration: 0.3} );
  }else{//contract
      new Effect.Scale(divToExpand, 0, {scaleX: false, scaleY: true, scaleContent: false, duration: 0.3} );
  }

};




/* -----------------------------------*/
/* -------->>> WALL UTILS <<<---------*/
/* -----------------------------------*/

//for wall/adminlist
var wallManager = Class.create();
wallManager.prototype = {
    initialize: function() {
        this.pNode = "";
        this.editNode = "";
        this.resultsNode = "";
        this.inputBox = "";
        this.wallUser = "";
        this.wall_id = "";
    },

    findNewWallLeader: function(wall_id, el) {
        this.hideOpenEdits();
        this.pNode = el.parentNode;
        this.formNode = $('edit_item_' + wall_id);
        this.resultsNode = $('searchResults_' + wall_id);
        this.inputBox = $('wall_leader_user_' + wall_id);
        this.wallUser = $('user_name_' + wall_id);
        this.wall_id = wall_id;
        this.displayForm();
    },

    displayForm: function() {
        this.formNode.style.display = "block";
        this.pNode.style.display = "none";
        this.inputBox.value = '';
        this.inputBox.focus();
        this.observeInput();
    },

    observeInput: function() {
        Event.observe(this.inputBox, 'keyup', this.findLiveUsers);
    },

    findLiveUsers: function() {
        var post_value = myWallManager.inputBox.value;
        if (post_value) {
            new Ajax.Updater(myWallManager.resultsNode, 'WallLeaderAutoComplete.aspx', {
                asynchronous: true,
                evalScripts: true,
                onComplete: function() {
                    Element.hide('staticSpinner')
                },
                onLoading: function() {
                    Element.show('staticSpinner')
                },
                parameters: "q=" + post_value,
                onlyLatestOfClass: 'liveUserSearch'
            })
        }
    },

    changeWallLeader: function(first_name, last_name, user_id) {

        var post_body = "user_id=" + user_id + "&wall_id=" + this.wall_id;
        new Ajax.Request('WallLeaderChange.aspx', {
            asynchronous: true,
            method: 'post',
            postBody: post_body
        });

        try {
            this.wallUser.childNodes[0].nodeValue = first_name + " " + last_name;
        } catch (Error) {
            //This can happen the first time a wall is assigned a leader.
            //In this situation, we just reload the page for the user to show the new leader name.
            window.location.reload();
        }
        this.cancel();
    },

    cancel: function() {
        this.formNode.style.display = "none";
        this.pNode.style.display = "block";
        Event.stopObserving(this.inputBox, 'keyup', this.findLiveUsers);
    },

    hideOpenEdits: function() {
        try {
            this.cancel();
        } catch (err) { }
    }

}


function replaceOldCell(oldCell, repContent){
  if($(oldCell)){
    $(oldCell).innerHTML = repContent;
  }
};



function addCellSpinner(obj){
    if($('cellSpinner')){
      Element.remove('cellSpinner');
    }

    var parentObj = obj.parentNode;
  	var cellSpinner = document.createElement("img");
		cellSpinner.setAttribute('id','cellSpinner');
		cellSpinner.setAttribute('src','/images/spinner-small.gif');
		parentObj.insertBefore(cellSpinner, obj);
};

function updateWallIncrements(wall_id, reqTime, obj){
  if(obj !== currentWallTab){
  
    setActiveWallTab(obj);
    
    Element.show('staticSpinner')
    new Ajax.Updater('incrementSlots', 
    	'/wall/set_wall_increments/?wallId=' + wall_id + '&reqTime=' + reqTime,{
    		asynchronous:true, 
    		evalScripts:true,
    		onLoading: $('staticSpinner').show(),
    		onComplete: function() {$('staticSpinner').hide(); setWallLabel(obj);}
    	}
     );
      currentWallTab = obj;
   }
};

function wallUpdated(){
	//myCellLocator = new celllocator();
	Element.hide("staticSpinner");
};


function setActiveWallTab(obj){
  var nav = document.getElementsByClassName("leftAlignedNav")[0];
  var liNodes = nav.childNodes;
  for(var i in liNodes){
     if(liNodes[i].tagName){
        if(liNodes[i].firstChild.hasClassName('active')){
          liNodes[i].firstChild.removeClassName('active');
        }
     }
   }
   Element.addClassName(obj, 'active');
};


function setWallLabel(obj){
   var amp = /"&", "g"/;
   var newStr = obj.firstChild.nodeValue.replace(amp,'');
   $('wallLabel').innerHTML = newStr.replace(/\(\w*\s*\w*\)/,'');
};

/* -----------------------------------*/
/* -------->>> FORM UTILS <<<---------*/
/* -----------------------------------*/

function charCounter(field,maxLength,countTarget){
  var inputLength=field.value.length;
  if(inputLength>=maxLength){
      field.value=field.value.substring(0,maxLength);
   }
   $(countTarget).innerHTML=maxLength-field.value.length;
};

/* -----------------------------------*/
/* ------>>> REQUEST UTILS <<<--------*/
/* -----------------------------------*/

function deletingCommentPrep(deleteBtn, commentSpinner){
	Element.hide(deleteBtn);
	Element.show(commentSpinner);
}


function setAlternateRow(){
	var commentRows = document.getElementsByClassName('comment');
	var row_class = (commentRows.length - 1)%2 === 0 ? "alternateRow1" : "alternateRow2";
	commentRows[commentRows.length - 1].className += " " + row_class;
	
}

function updateComments(request_id){
  //TODO: add validation
  $('staticSpinner').show();
  new Ajax.Request('/encouragement/create/?comment=' + escape($F('commentArea')) +"&request_id=" + request_id + "&encouragement_type=" + $F('encouragement_type') + "&commentFrom=" + $F('commentFrom') + "&commentFromEmail=" + $F('commentFromEmail') + "&commentListed=" + $F('commentListed'),{
        asynchronous:true, 
        evalScripts:true,
        onLoading: $('commentSubmit').disable(),
        onComplete: function(){$('commentSubmit').enable(); $('staticSpinner').hide();}
        }
   );
};

function filter_request_types(typeObj, timeObj){
  Element.show('staticSpinner')
  new Ajax.Updater('requestList', '/request/requesttype_changed/?reqtype=' + typeObj[typeObj.selectedIndex].value + '&timeMod=' + timeObj[timeObj.selectedIndex].value,
        {asynchronous:true, evalScripts:true, onComplete: clearSpinner}
   );
};

/* -----------------------------------*/
/* ------>>> GLOBAL UTILS <<<---------*/
/* -----------------------------------*/

function clearSpinner(){
  if($('staticSpinner')) Element.hide("staticSpinner");
  if($('commentSpinner')) Element.hide("commentSpinner");
}

//----------------------------------------------------------------
//                       PROTOTYPE EXTENSION
//----------------------------------------------------------------
// Extension to Ajax allowing for classes of requests of which only one (the latest) is ever active at a time
// - stops queues of now-redundant requests building up / allows you to supercede one request with another easily.

// just pass in onlyLatestOfClass: 'classname' in the options of the request

Ajax.currentRequests = {};

Ajax.Responders.register({
  onCreate: function(request) {
    if (request.options.onlyLatestOfClass && Ajax.currentRequests[request.options.onlyLatestOfClass]) {
            // if a request of this class is already in progress, attempt to abort it before launching this new request
           try { Ajax.currentRequests[request.options.onlyLatestOfClass].transport.abort(); } catch(e) {}
        }
        // keep note of this request object so we can cancel it if superceded
        Ajax.currentRequests[request.options.onlyLatestOfClass] = request;
  },
    onComplete: function(request) {
    if (request.options.onlyLatestOfClass) {
            // remove the request from our cache once completed so it can be garbage collected
             Ajax.currentRequests[request.options.onlyLatestOfClass] = null;
        }
    }
});

//----------------------------------------------------------------
//                       REQUEST REMOVAL
//----------------------------------------------------------------
var removalForm = Class.create();

removalForm.prototype = {
	initialize: function(user_name, user_email, request, el){
		this.removeLink = null;
		this.validateForm = null;
		
	},
	
	requestRemoval: function(user_name, user_email, request, el){
		var formContainer = $("requestRemoveForm_" + request);
		if(el === this.removeLink){//if this is the same remove link, then toggle the form.
			myRemovalform.hideForm(formContainer);
			return false;
		};
		this.removeLink = el;
		try{$('requestRemovalForm').remove();}catch(err){}//close any other open removal forms
		
		
		/*create the following markup and append to formContainer
		
			<form action="#" method="post" class="large-form" id="requestRemovalForm">
				<fieldset>
					<legend>Request Removal<em class="hintText"> (All fields are required.) </em></legend>
					<div>
						<label for="user_name" accesskey="1">Your Name</label>
						<input id="user_name" name="user[name]" size="30" type="text" class="inputBox required" />
					</div>
					<div>
						<label for="user_email" accesskey="2">Your email address</label>
						<input id="user_email" name="user[email]" size="30" type="text" class="inputBox required" />
					</div>
					<div>
						<label for="user_reason" accesskey="3">Reason for removal?</label>
						<textarea id="user_reason" name="user[reason]" rows="10" cols="60" class="inputBox required" ></textarea>
					</div>
					<input name="sendRequest" type="submit" value="Send Request" id="submitButton" class="submitBtn"/>
					<a href="#" onclick="new Effect.Fade('<%= 'requestRemoveForm_' + request.id.to_s %>', {duration: 0.5}); return false;">cancel</a>
				</fieldset>
			</form>
		*/
	
		var theForm = document.createElement('form');
			theForm.setAttribute('action','#');
			theForm.setAttribute('method','post');
			theForm.id = 'requestRemovalForm';
			theForm.className = 'large-form';
			theForm.onsubmit = function(){
				//TODO: send info to database
				if(myRemovalform.validateForm.validate()){
					el.innerHTML = 'removal request sent';
					el.onclick=function(){javascript:void(0); return false};
					el.setAttribute('href','javascript:void(0);');
					new Effect.Highlight(el, {duration: 5, afterFinish: function(){el.removeAttribute('style');}});
					myRemovalform.hideForm(formContainer);
					return false;
				}else{
					return false;
				}
			};
			
		var fieldset = document.createElement('fieldset');
		var legend = document.createElement('legend');
		var text = document.createTextNode('Request removal');
		var em = document.createElement('em');
			em.className = 'hintText';
		var text2 = document.createTextNode('(All fields are required.)');
		
		em.appendChild(text2);
		legend.appendChild(text);
		legend.appendChild(em);
		fieldset.appendChild(legend);
		
		//your name field
		var div = document.createElement('div');
		var label = document.createElement('label');
			label.setAttribute('for','user_name');
			label.setAttribute('accesskey','1');
		var text = document.createTextNode('Your Name');
		var input = document.createElement('input');
			input.id='user_name';
			input.className = 'inputBox required';
			input.setAttribute('name','user[name]');
			input.setAttribute('size','30');
			input.setAttribute('type','text');
			input.value=user_name;
			
		label.appendChild(text);
		div.appendChild(label);
		div.appendChild(input);
		fieldset.appendChild(div);
		
		//your email field
		var div = document.createElement('div');
		var label = document.createElement('label');
			label.setAttribute('for','user_email');
			label.setAttribute('accesskey','2');
		var text = document.createTextNode('Your Email Address');
		var input = document.createElement('input');
			input.id='user_email';
			input.className = 'inputBox required';
			input.setAttribute('name','user[email]');
			input.setAttribute('size','30');
			input.setAttribute('type','text');
			input.value = user_email
			
		label.appendChild(text);
		div.appendChild(label);
		div.appendChild(input);
		fieldset.appendChild(div);
		
		//reason for removal field
		var div = document.createElement('div');
		var label = document.createElement('label');
			label.setAttribute('for','user_reason');
			label.setAttribute('accesskey','3');
		var text = document.createTextNode('Reason for removal?');
		var input = document.createElement('textarea');
			input.id='user_reason';
			input.className = 'inputBox required';
			input.setAttribute('name','user[reason]');
			input.setAttribute('rows','10');
			input.setAttribute('cols','60');
			
		label.appendChild(text);
		div.appendChild(label);
		div.appendChild(input);
		fieldset.appendChild(div);
		
		//submit button
		var input = document.createElement('input');
			input.id='submitButton';
			input.className = 'submitBtn';
			input.setAttribute('name','sendRequest');
			input.setAttribute('type','submit');
			input.value='Send Request';
			
		fieldset.appendChild(input);
		
		//cancel button
		var cancelBtn = document.createElement('a');
			cancelBtn.setAttribute('href','#');
			cancelBtn.onclick = function(){
				myRemovalform.hideForm(formContainer);
				return false;
			};
		var text = document.createTextNode('cancel');
		
		cancelBtn.appendChild(text);
		fieldset.appendChild(cancelBtn);
		theForm.appendChild(fieldset);
		formContainer.appendChild(theForm);
		this.validateForm = new Validation('requestRemovalForm', {immediate: false, stopOnFirst: false});
		this.showForm(formContainer);
		return false;		
	}, 
	
	showForm: function(formContainer){
		//IE6: problem is position: relative for .prayerRequest
		formContainer.hide();
		new Effect.BlindDown(formContainer,{
			duration: 0.5, 
			afterFinish:function(){
				if($F('user_name')){
					$('user_reason').focus()
				}else{
					$('user_name').focus()
				}
			}
		});
	},
	
	hideForm: function(formContainer){
		new Effect.BlindUp(formContainer, {
			duration: 0.5, 
			afterFinish: function(){
				$('requestRemovalForm').remove();
			}
		});
		this.removeLink = null;
	}
}

//----------------------------------------------------------------
//                       MODAL ERROR WINDOW
//----------------------------------------------------------------

var modalActive = false;
var modalError = Class.create();

modalError.prototype = {
	
	initialize: function() {	
		var objBody = document.getElementsByTagName("body").item(0);
		var objOverlay = document.createElement("div");
		objOverlay.setAttribute('id','screenDim');
		objOverlay.style.display = 'none';
		objBody.appendChild(objOverlay);
		
		var objErrorMessage = document.createElement("div");
		objErrorMessage.setAttribute('id','errorMessage');
		objErrorMessage.style.display = 'none';
		objBody.appendChild(objErrorMessage);
	
		var objErrorMessageBody = document.createElement("div");
		objErrorMessageBody.setAttribute('id','errorMessageBody');
		objErrorMessage.appendChild(objErrorMessageBody);
	},
	
	start: function(messageBody) {
  	if(!modalActive){
  	  modalActive = true;
  		var arrayPageSize = getPageSize();
  		Element.setHeight('screenDim', arrayPageSize[1]);
  		new Effect.Appear('screenDim', { duration: 0.2, from: 0.0, to: 0.8 });
  		
  		// calculate top offset for the modal and display 
  		var arrayPageSize = getPageSize();
  		var arrayPageScroll = getPageScroll();
  		var modalTop = arrayPageScroll[1] + (arrayPageSize[3] / 15);
  
  		Element.setTop('errorMessage', modalTop);
  		$('errorMessageBody').innerHTML = messageBody;
  		Element.show('errorMessage');
		}
		
	},
	
	end: function() {
	  modalActive = false;
		Element.hide('errorMessage');
		new Effect.Fade('screenDim', { duration: 0.2});
	}
};




// MODAL ERROR WINDOW HELPER UTILS
// -----------------------------------------------------------------------------------
//
//	Additional methods for Element added by SU, Couloir
//	- further additions by Lokesh Dhakar (huddletogether.com)
//
Object.extend(Element, {
	getWidth: function(element) {
	   	element = $(element);
	   	return element.offsetWidth; 
	},
	setWidth: function(element,w) {
	   	element = $(element);
    	element.style.width = w +"px";
	},
	setHeight: function(element,h) {
   		element = $(element);
    	element.style.height = h +"px";
	},
	setTop: function(element,t) {
	   	element = $(element);
    	element.style.top = t +"px";
	},
	setSrc: function(element,src) {
    	element = $(element);
    	element.src = src; 
	},
	setHref: function(element,href) {
    	element = $(element);
    	element.href = href; 
	},
	setInnerHTML: function(element,content) {
		element = $(element);
		element.innerHTML = content;
	}
});

// -----------------------------------------------------------------------------------


// -----------------------------------------------------------------------------------

//
// getPageScroll()
// Returns array with x,y page scroll values.
// Core code from - quirksmode.org
//
function getPageScroll(){
	var yScroll;
	if (self.pageYOffset) {
		yScroll = self.pageYOffset;
	} else if (document.documentElement && document.documentElement.scrollTop){	 // Explorer 6 Strict
		yScroll = document.documentElement.scrollTop;
	} else if (document.body) {// all other Explorers
		yScroll = document.body.scrollTop;
	}
	arrayPageScroll = ['',yScroll]; 
	return arrayPageScroll;
}

// -----------------------------------------------------------------------------------


// -----------------------------------------------------------------------------------

//
// getPageSize()
// Returns array with page width, height and window width, height
// Core code from - quirksmode.org
// Edit for Firefox by pHaez
//
function getPageSize(){
	
	var xScroll, yScroll;
	
	if (window.innerHeight && window.scrollMaxY) {	
		xScroll = document.body.scrollWidth;
		yScroll = window.innerHeight + window.scrollMaxY;
	} else if (document.body.scrollHeight > document.body.offsetHeight){ // all but Explorer Mac
		xScroll = document.body.scrollWidth;
		yScroll = document.body.scrollHeight;
	} else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
		xScroll = document.body.offsetWidth;
		yScroll = document.body.offsetHeight;
	}
	
	var windowWidth, windowHeight;
	if (self.innerHeight) {	// all except Explorer
		windowWidth = self.innerWidth;
		windowHeight = self.innerHeight;
	} else if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode
		windowWidth = document.documentElement.clientWidth;
		windowHeight = document.documentElement.clientHeight;
	} else if (document.body) { // other Explorers
		windowWidth = document.body.clientWidth;
		windowHeight = document.body.clientHeight;
	}	
	
	// for small pages with total height less then height of the viewport
	if(yScroll < windowHeight){
		pageHeight = windowHeight;
	} else { 
		pageHeight = yScroll;
	}

	// for small pages with total width less then width of the viewport
	if(xScroll < windowWidth){	
		pageWidth = windowWidth;
	} else {
		pageWidth = xScroll;
	}


	arrayPageSize = [pageWidth,pageHeight,windowWidth,windowHeight]; 
	return arrayPageSize;
}

// -----------------------------------------------------------------------------------


// ---------------------------------------------------

//
// addLoadEvent()
// Adds event to window.onload without overwriting currently assigned onload functions.
// Function found at Simon Willison's weblog - http://simon.incutio.com/
//
function addLoadEvent(func)
{	
	var oldonload = window.onload;
	if (typeof window.onload !== 'function'){
    	window.onload = func;
	} else {
		window.onload = function(){
		oldonload();
		func();
		}
	}

}


// ---------------------------------------------------

function initModalError() { myModalError = new modalError(); };

function cancelRequestEdit(request_id)
{
	Effect.BlindUp('editRequest_' + request_id); 
	new Effect.ScrollTo('request' + request_id); 
	Effect.toggle('editLink_' + request_id); 
	return false;
}

addLoadEvent(initModalError);	// run initLightbox onLoad
