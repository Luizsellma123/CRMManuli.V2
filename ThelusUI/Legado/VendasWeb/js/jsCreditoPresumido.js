jQuery(function ($) {

    //Coloca mascaras nos campos
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataInic").mask("99/99/9999");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataFinal").mask("99/99/9999");

});

function validaRelatorio() {

    var theForm = document.aspnetForm;

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataInic.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataInic.value == null) {
        alert("Favor informar a data inicial.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataInic.focus();
        return false;
    }



    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataFinal.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataFinal.value == null) {
        alert("Favor informar a data final.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataFinal.focus();
        return false;
    }


    return true;
}
