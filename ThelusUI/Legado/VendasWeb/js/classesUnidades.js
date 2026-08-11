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

function validaCadClasseUnidade() {

    var theForm = document.aspnetForm;
    
    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpGrupo[theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpGrupo.selectedIndex].text == "Todos") {
        alert("Favor informar um valor diferente de Todos");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpGrupo.focus();
        return false;
    }

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpClasse[theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpClasse.selectedIndex].text == "Todos") {
        alert("Favor informar um valor diferente de Todos");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_drpClasse.focus();
        return false;
    }

    return true;
}