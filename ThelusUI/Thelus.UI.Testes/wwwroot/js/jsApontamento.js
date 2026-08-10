jQuery(function ($) {

  
    $.mask.definitions['~'] = '[0123456789,]';

    $("#ctl00_ContentPlaceHolder1_txtDataInicial").mask("99/99/9999");
    $("#ctl00_ContentPlaceHolder1_txtHoraInicial").mask("99:99");


    $("#ctl00_ContentPlaceHolder1_txtDataFinal").mask("99/99/9999");
    $("#ctl00_ContentPlaceHolder1_txtHoraFinal").mask("99:99");


    $("#ctl00_ContentPlaceHolder1_txtQtdBoa").mask("?~~~~~~~~~~~~~~", { placeholder: " " });
    $("#ctl00_ContentPlaceHolder1_txtQtdRefugada").mask("?~~~~~~~~~~~~~~", { placeholder: " " });
    $("#ctl00_ContentPlaceHolder1_txtQtdReprocesso").mask("?~~~~~~~~~~~~~~", { placeholder: " " });
    $("#ctl00_ContentPlaceHolder1_txtQtdRetalho").mask("?~~~~~~~~~~~~~~", { placeholder: " " });
});




function excluir(FuncCod) {

    decisao = confirm("Deseja Realmente Remover essa Funcionario?");

    if (decisao) {
        var theForm = document.forms['aspnetForm'];
        if (!theForm) {
            theForm = document.aspnetForm;
        }


        if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
            theForm.ctl00_ContentPlaceHolder1_FuncCodDelet.value = FuncCod;
            theForm.submit();
        }
    }

    return false;
}




function validaCampos() {
    var theForm = document.aspnetForm;

    if (theForm.ctl00_ContentPlaceHolder1_txtDataInicial.value == "" || theForm.ctl00_ContentPlaceHolder1_txtDataInicial.value == null) {
        alert("Verifique a Data Inicial.");
        theForm.ctl00_ContentPlaceHolder1_txtDataInicial.focus();
        return false;
    }

    if (theForm.ctl00_ContentPlaceHolder1_txtHoraInicial.value == "" || theForm.ctl00_ContentPlaceHolder1_txtHoraInicial.value == null) {
        alert("Verifique a Hora Inicial.");
        theForm.ctl00_ContentPlaceHolder1_txtHoraInicial.focus();
        return false;
    }

    if (theForm.ctl00_ContentPlaceHolder1_txtDataFinal.value == "" || theForm.ctl00_ContentPlaceHolder1_txtDataFinal.value == null) {
        alert("Verifique a Data Final.");
        theForm.ctl00_ContentPlaceHolder1_txtDataFinal.focus();
        return false;
    }

    if (theForm.ctl00_ContentPlaceHolder1_txtHoraFinal.value == "" || theForm.ctl00_ContentPlaceHolder1_txtHoraFinal.value == null) {
        alert("Verifique a Hora Final.");
        theForm.ctl00_ContentPlaceHolder1_txtHoraFinal.focus();
        return false;
    }

    if (theForm.ctl00_ContentPlaceHolder1_txtQtdBoa.value == "" || theForm.ctl00_ContentPlaceHolder1_txtQtdBoa.value == null) {
        alert("Informe a quantidade Boa.");
        theForm.ctl00_ContentPlaceHolder1_txtQtdBoa.focus();
        return false;
    }

    return true;
}