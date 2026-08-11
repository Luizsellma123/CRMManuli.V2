jQuery(function ($) {

    $.mask.definitions['~'] = '[0123456789,]';

    //Coloca mascaras nos campos
    $("#ctl00_ContentPlaceHolder1_txtDataCancelamento").mask("?99/99/9999");

    //Caledario nos campos data
    $('#btnCalendar1').click(function () {
        $(this).calendario({
            target: '#ctl00_ContentPlaceHolder1_txtDataCancelamento'
        });
    });

});

function confirmar() {
    if (confirm("Confirma cancelamento do pedido?"))
        return true;
    else
        return false;
}