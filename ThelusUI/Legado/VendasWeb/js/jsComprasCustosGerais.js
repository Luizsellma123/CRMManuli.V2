jQuery(function ($) {

    $.mask.definitions['~'] = '[abcdefghijklmnopqrstuvxzyçw0123456789áàãôéúõ. ,]';

    //Coloca mascaras nos campos
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataInic").mask("99/99/9999");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataFim").mask("99/99/9999");



});



function validaRelatorio() {

    var theForm = document.aspnetForm;

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataInic.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataInic.value == null) {
        alert("Favor informar a data inicial.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataInic.focus();
        return false;
    }



    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataFim.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataFim.value == null) {
        alert("Favor informar a data final.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataFim.focus();
        return false;
    }


    return true;
}
