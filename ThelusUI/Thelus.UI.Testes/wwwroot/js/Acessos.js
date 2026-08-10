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
