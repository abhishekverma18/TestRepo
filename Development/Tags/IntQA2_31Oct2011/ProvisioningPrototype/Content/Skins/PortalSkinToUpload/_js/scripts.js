$(document).ready(function() {
	buttonDisabled();
	portalTweaks();
});
$(window).load(function () {
//functions that require all assets (most importantly graphics) to be loaded before execution
	setHeights(0);
	if (jQuery.browser.msie && parseInt(jQuery.browser.version, 10) < 7 && parseInt(jQuery.browser.version, 10) > 4) {
	//IE6 PNG Hack
		DD_belatedPNG.fix('div, h1, a');  //option 1, default option;  leave it if it's not breaking things
		//$('body').supersleight();  //option 2... this one tend to break things... but it works better than the other sometimes
	}
});
	//#FUNCTION# disabled buttons
	function buttonDisabled(){
		$('.btn-next .disabled, .btn-prev .disabled').click(function(){
			return false;
		});

		$('#wid-quick-poll a').click(function(){
		if($(this).hasClass('disabled')) {
			return false;
		} else {
			return true;
		}
		});
   }
	//#FUNCTION# tweaks
	function portalTweaks(){
		// Hiding second community only on PortalStaging
		if(location.href.toLowerCase().indexOf("portalstaging") != -1 || location.href.toLowerCase().indexOf("localhost") != -1) {
			if($("#wid-forum-list").size() > 0) {
				$("#wid-forum-list p:not(:first)").hide();
				$("#wid-forum-list h2:not(:first)").hide();
				$("#wid-forum-list ul:not(:first)").hide();
				$("#wid-forum-list span.btn-all:not(:first)").hide();
				
				// HIDING SUB HEADINGS JUST ON PORTALSTAGING
				$("#wid-forum-list p").hide();
				$("#wid-forum-list h2").hide();
			}
			else if($("#wid-forum-full").size() > 0) {
				$("#wid-forum-full p:not(:first)").hide();
				$("#wid-forum-full h2:not(:first)").hide();
				$("#wid-forum-full table:not(:first)").hide();
				$("#wid-forum-full div.pagination:not(:first)").hide();
			}
		}
		//Show "forum is empty" message
		// var forumlist = $("#forum-list");
		// if($(forumlist).children().length == 1 && $(forumlist).children().is("p"))//almost definitely an empty forum list message.
		// {
			// $(forumlist).children("p").css("display", "block");//Display it!
		// }
		
		//Show "forum is empty" message
		var strForumList = $('#forum-list').html();
		if ($.trim(strForumList) == ""){
			$('#forum-list').html('<p>Stay tuned for upcoming discussions!</p>');
			$('#forum-list p').css("display", "block");
		}
		//forum list even classes
		$('#forum-list ul li:even').addClass('even');
		
		// User profile page error/alert messages styling
		if($("#wid-profile").size() > 0) {
			var labelWidth = $("#wid-profile .form-input label").width();
			$("#wid-profile p.msg:not(:first)").css('padding-left', labelWidth + 5 + 'px');
			$("#wid-profile p.msg:not(:first)").css('color','#999999');
		}
		
		//Clear float fixes
		$('.list-item').append('<div class="clear"></div>');
		$('#forum-list li').append('<div class="clear"></div>');
		$('#wid-profile .form-input').append('<div class="clear"></div>');
		
		if($("#wid-quick-poll td").size() > 0) {
			$("#wid-quick-poll td").wrapInner("<div>");
		}
	}
	revertBack = function(that){
	var eventTarget = $(that.currentTarget);
		if($(eventTarget).attr("type") == "password")
		{
			if($(eventTarget).attr("value") == "")
			{
				$(this).hide();
				$("#password-placeholder").show();
			};
		}
		else
		{
			if($(eventTarget).attr("value") == "")
			{
				$(this).hide();
				$("#email-placeholder").show();
			};
			
		}
	}
	function removeSurveyNumbering()
	{
		var el = $(".item-header, #wid-studies-full .link");

		$.each(el, function(index, item)
		{
		var dotindex = $(item).html().toLowerCase().indexOf(".");
		var newhtml = $(item).html().substring(dotindex+1);
		$(item).html(newhtml);
		}); 
	}
	function removeDiscussionNumbering()
	{
		var el = $("#forum-list .link");

		$.each(el, function(index, item)
		{
		var dotindex = $(item).html().toLowerCase().indexOf(".");
		var newhtml = $(item).html().substring(dotindex+1);
		$(item).html(newhtml);
		}); 
	}
	//#FUNCTION# - Make sure all column heights are even
	function setHeights(setInner) {
		var tallestHeight = 0;
		if(location.href.toLowerCase().indexOf("/members/") > 0) {
			tallestHeight = 450;
		}
		var columnDivs = new Array("#column-left", "#column-main", "#column-right");
		$.each(columnDivs, function() {
			if($("" + this).outerHeight(true) > tallestHeight) {
				tallestHeight = $("" + this).outerHeight(true);
			}
		});
		$.each(columnDivs, function() {
			var totalExtra = $("" + this).outerHeight(true) - $("" + this).height();
			var newHeight = tallestHeight - totalExtra;
			$("" + this).height(newHeight + "px");
		});
		//set height for inner container as well
		if(setInner == 1)
		{
			if(location.href.toLowerCase().indexOf("default.aspx") == -1) {
				var totalExtra = $("#column-main > div:first").outerHeight(true) - $("#column-main > div:first").height();
				var totalCMExtra = $("#column-main").outerHeight(true) - $("#column-main").height();
				var newHeight = tallestHeight - totalExtra - totalCMExtra;
				if (jQuery.browser.msie && parseInt(jQuery.browser.version, 10) < 7 && parseInt(jQuery.browser.version, 10) > 4) {
					$("#column-main > div:first").css("height", newHeight + "px");
				} else {
					$("#column-main > div:first").css("min-height", newHeight + "px");
				}
			}
		}
	}