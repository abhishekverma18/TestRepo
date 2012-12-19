//Added for Front end validation of Dynamic GUI
function validate(){
    $('div#ErrorMessages').empty();
    $('.ImageCtrl').each(function (i){
        if($(this).get(0).value==""){
        $('<div class="error">Please select the image(s) to upload</div>').appendTo('div#ErrorMessages');
        }
    });

    $('.TextEntry').each(function (i){
        if($(this).get(0).value==""){
        $('<div class="error">You have not entered anything in the text field</div>').appendTo('div#ErrorMessages');
        }
    });

    if($('#ErrorMessages').children().size()>0){
    return false;
    }
    return true;
}