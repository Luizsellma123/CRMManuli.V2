function fdelete(idiTem) {

    //__doPostBack('DELARQ', txtarquivo);
    var theForm = document.forms['aspnetForm'];
    if (!theForm) {
        theForm = document.aspnetForm;
    }
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.idItem.value = idiTem;
        theForm.submit();
    }
    return false;
}

function validaCadRelatorio() {

    var theForm = document.aspnetForm;

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpUsuario[theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpUsuario.selectedIndex].text == "Todos") {
        alert("O valor do campo deve ser diferente de Todos.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpUsuario.focus();
        return false;
    }
    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpCodigo[theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpCodigo.selectedIndex].text == "Todos") {
        alert("O valor do campo deve ser diferente de Todos.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpCodigo.focus();
        return false;
    }
    return true;
}