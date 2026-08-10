
jQuery(function ($) {


});

function aprovar(idPedido,IdEmpresa) {

    //__doPostBack('DELARQ', txtarquivo);
    var theForm = document.forms['aspnetForm'];
    if (!theForm) {
        theForm = document.aspnetForm;
    }
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {

        theForm.ctl00_ContentPlaceHolder1_ltlIdPedido.value = idPedido;
        theForm.ctl00_ContentPlaceHolder1_ltlIdEmpresa.value = IdEmpresa;
        theForm.ctl00_ContentPlaceHolder1_tipoAprovacao.value = "APROVAR";
        theForm.submit();
    }
    return false;
}



function faturar(idPedido, IdEmpresa) {

    //__doPostBack('DELARQ', txtarquivo);
    var theForm = document.forms['aspnetForm'];
    if (!theForm) {
        theForm = document.aspnetForm;
    }
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {

        theForm.ctl00_ContentPlaceHolder1_ltlIdPedido.value = idPedido;
        theForm.ctl00_ContentPlaceHolder1_ltlIdEmpresa.value = IdEmpresa;
        theForm.ctl00_ContentPlaceHolder1_tipoAprovacao.value = "FATURAR";
        theForm.submit();
    }
    return false;
}


