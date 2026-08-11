jQuery(function ($) {

    $.mask.definitions['~'] = '[0123456789,]';

    $("#ctl00_ContentPlaceHolder1_txtValorUnitario").mask("?~~~~~~~~~~~~~~~~~~~~");
});


function fcarrega(valor){

    alert("teste"+valor);
    
    $("#ctl00_ContentPlaceHolder1_txtValorUnitario").value = valor;

    return false;
}