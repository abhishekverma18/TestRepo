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
    }
    $("#HdnSelectedLayout").val("1");
});

function BuildLayout(loginImagePath, portalImagePath, surveyImagePath) {
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
    $("#HdnSelectedLayout").val("1");
}