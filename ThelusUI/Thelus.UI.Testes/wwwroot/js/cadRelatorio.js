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

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtNome.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtNome.value == null) {
        alert("Favor informar o nome do Relatorio");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtNome.focus();
        return false;
    }

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtPagina.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtPagina.value == null) {
        alert("Favor informar a Pagina");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtPagina.focus();
        return false;
    }

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpSetor[theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpSetor.selectedIndex].text == "Todos") {
        alert("Favor informar setor diferente de Todos");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpSetor.focus();
        return false;
    }

    return true;
}