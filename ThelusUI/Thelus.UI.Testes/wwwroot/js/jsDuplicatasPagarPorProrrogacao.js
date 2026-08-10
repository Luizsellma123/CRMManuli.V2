jQuery(function ($) {

    //Coloca mascaras nos campos
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataVencimentoInicial").mask("99/99/9999");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataVencimentoFinal").mask("99/99/9999");

});

function validaRelatorio() {

    var theForm = document.aspnetForm;

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataVencimentoInicial.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataVencimentoInicial.value == null) {
        alert("Favor informar a data inicial.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataVencimentoInicial.focus();
        return false;
    }

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataVencimentoFinal.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataVencimentoFinal.value == null) {
        alert("Favor informar a data final.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataVencimentoFinal.focus();
        return false;
    }

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TextCodigoEntidadeInicial.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TextCodigoEntidadeInicial.value == null) {
        alert("Favor informar o código inicial da entidade.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TextCodigoEntidadeInicial.focus();
        return false;
    }

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TextCodigoEntidadeFinal.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TextCodigoEntidadeFinal.value == null) {
        alert("Favor informar o código final da entidade.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TextCodigoEntidadeFinal.focus();
        return false;
    }


    return true;
}
