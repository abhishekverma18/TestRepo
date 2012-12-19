//Added for Dynamic template list module
$(document).ready(function () {
    // Get total count of Layouts 
    var layoutCount = $("#HdnLayoutCount").val();

    // Loop to create dynamic divs according to number of layouts
    for (var layout = 1; layout <= layoutCount; layout++) {

        var layoutId = "layout" + layout;

        var loginDivId = "loginDiv" + layout;
        var portalDivId = "portalDiv" + layout;
        var surveyDivId = "surveyDiv" + layout;

        $('<div  id=' + layoutId + '></div>').appendTo('div#LayoutSectionContainer');
        $('<div class="setLayout" id=' + loginDivId + '></div>').appendTo('div#' + layoutId);
        $('<div class="setLayout" id=' + portalDivId + '></div>').appendTo('div#' + layoutId);
        $('<div class="setLayout" id=' + surveyDivId + '></div>').appendTo('div#' + layoutId);
        $('<div id="loginImageError"></div>').appendTo('div#' + loginDivId);
        $('<div id="portalImageError"></div>').appendTo('div#' + portalDivId);
        $('<div id="surveyImageError"></div>').appendTo('div#' + surveyDivId);
    }

    //Setting the initial value on DOM ready
    $("#HdnSelectedLayout").val("1");

});


function BuildLayout(loginImagePath, portalImagePath, surveyImagePath) {

    /*Start---Code block for  handling missing images----*/
    $('div#loginImageError').empty();
    $('div#portalImageError').empty();
    $('div#surveyImageError').empty();
    $.ajax({
        url: loginImagePath,
        type: 'HEAD',
        error: function () {
            $('<div class="errorMissingImage">There is no login image sample provided for this template</div>').appendTo('div#loginImageError');
        }

    });
    $.ajax({
        url: portalImagePath,
        type: 'HEAD',
        error: function () {
            $('<div class="errorMissingImage">There is no portal image sample provided for this template</div>').appendTo('div#portalImageError');
        }

    });
    $.ajax({
        url: surveyImagePath,
        type: 'HEAD',
        error: function () {
            $('<div class="errorMissingImage">There is no survey image sample provided for this template</div>').appendTo('div#surveyImageError');
        }

    });
    /*End---Code block for handling missing images----*/

    /*Start----Code block for diaplaying Layout images to dynamic Divs-------*/
    var layoutCount = $("#HdnLayoutCount").val();

    for (var layout = 1; layout <= layoutCount; layout++) {
        var layoutId = "layout" + layout;
        var loginDivId = "loginDiv" + layout;
        var portalDivId = "portalDiv" + layout;
        var surveyDivId = "surveyDiv" + layout;
        $("#" + loginDivId).attr("style", "background-image:url(" + loginImagePath + ")");
        $("#" + portalDivId).attr("style", "background-image:url(" + portalImagePath + ")");
        $("#" + surveyDivId).attr("style", "background-image:url('" + surveyImagePath + "')");
    }
    /*End----Code block for diaplaying Layout images to dynamic Divs-------*/
}