//Added for Offline and Advanced Mode to disable the text boxes
jQuery(document).ready(function () {

    $("#OfflineMode").click(function () {
        $('#PanelAdminEmail').attr("disabled", $(this).is(":checked"));
        $('#PanelAdminEmail').val("");
        $('#PanelPassword').attr("disabled", $(this).is(":checked"));
        $('#PanelPassword').val("");
        $('#PanelAdminUrl').attr("disabled", $(this).is(":checked"));
        $('#PanelAdminUrl').val("");
    });

});