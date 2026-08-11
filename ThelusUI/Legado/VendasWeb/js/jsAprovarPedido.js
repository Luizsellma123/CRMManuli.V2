jQuery(function ($) {

    //alert("Teste");
    $.mask.definitions['~'] = '[0123456789,]';

    $("#ctl00_ContentPlaceHolder1_txtQuantidade").mask("?~~~~~~~~~~~~~~", { placeholder: " " });
    $("#ctl00_ContentPlaceHolder1_txtPesoLiquido").mask("?~~~~~~~~~~~~~~", { placeholder: " " });
    $("#ctl00_ContentPlaceHolder1_txtPesoBruto").mask("?~~~~~~~~~~~~~~", { placeholder: " " });

});

function consultaSaldo(idiTem) {

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

function validaItem() {
    //alert("teste");
}


