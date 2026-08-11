jQuery(function ($) {

    $.mask.definitions['~'] = '[0123456789.]';

    //Coloca mascaras nos campos
    $("#ctl00_ContentPlaceHolder1_txtDataInicial").mask("?99/99/9999");
    $("#ctl00_ContentPlaceHolder1_txtDataFinal").mask("?99/99/9999");
    $("#ctl00_ContentPlaceHolder1_txtVendedor").mask("?9999999");
    $("#ctl00_ContentPlaceHolder1_txtNatureza").mask("?~~~~~~~~~~~~");

    //Caledario nos campos data
    $('#btnCalendar1').click(function () {
        $(this).calendario({
            target: '#ctl00_ContentPlaceHolder1_txtDataInicial'
        });
    });

    $('#btnCalendar2').click(function () {
        $(this).calendario({
            target: '#ctl00_ContentPlaceHolder1_txtDataFinal'
        });
    });
});

function validaDados() {

    var theForm = document.aspnetForm;

    if (theForm.ctl00_ContentPlaceHolder1_txtVendedor.value == null || theForm.ctl00_ContentPlaceHolder1_txtVendedor.value == "") {
        $('#errorVendedor').html(
        '&nbsp<img src="../imagens/atention.png" alt="Alteração" border="0" />&nbsp&nbsp<span>Codigo Vendedor deve ser informado.</span>')
        theForm.ctl00_ContentPlaceHolder1_txtVendedor.focus();
        return false;
    } else {
        $('#errorVendedor').html('');
    }

    if (theForm.ctl00_ContentPlaceHolder1_txtDataInicial.value == null || theForm.ctl00_ContentPlaceHolder1_txtDataInicial.value == "") {
        $('#erroDataInicial').html(
        '&nbsp<img src="../imagens/atention.png" alt="Alteração" border="0" />&nbsp&nbsp<span>Data inicial deve ser informada.</span>')
        theForm.ctl00_ContentPlaceHolder1_txtDataInicial.focus();
        return false;
    } else {
        $('#erroDataInicial').html('');
    }

    if (theForm.ctl00_ContentPlaceHolder1_txtDataFinal.value == null || theForm.ctl00_ContentPlaceHolder1_txtDataFinal.value == "") {
        $('#erroDataFinal').html(
        '&nbsp<img src="../imagens/atention.png" alt="Alteração" border="0" />&nbsp&nbsp<span>Data final deve ser informada.</span>')
        theForm.ctl00_ContentPlaceHolder1_txtDataFinal.focus();
        return false;
    } else {
        $('#erroDataFinal').html('');
    }
    
    return true;
}