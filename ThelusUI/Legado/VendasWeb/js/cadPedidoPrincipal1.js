jQuery(function ($) {

    $.mask.definitions['~'] = '[0123456789,]';

    //Coloca mascaras nos campos
    //ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataProgramada").mask("?99/99/9999");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEmissao").mask("?99/99/9999");
    //$("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega").mask("?99/99/9999");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtQuantidade").mask("?~~~~~~~~~~~~");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtPosicao").mask("?~~~~~");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtValor").mask("?~~~~~~~~~~~~~~");
    $("#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtValorFrete").mask("?~~~~~~~~~~~~");
    //$('#ctl00_ContentPlaceHolder1_txtTransportadora').mask("?9999999");
    //$('#ctl00_ContentPlaceHolder1_txtPedCliente').mask("?9999999999999999999999999999999999999999");

    $('#aspnetForm').keydown(function (event) {
        if (event.keyCode == '13') {
            event.preventDefault();
        }
    });

    //Caledario nos campos data
    $('#btnCalendar1').click(function () {
        $(this).calendario({
            target: '#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega'
        });
    });

    $('#btnCalendar2').click(function () {
        $(this).calendario({
            target: '#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEmissao'
        });
    });

    //Atribui click para abrir prazos
    $('#btnPrazos').click(function () {
        window.open("../cadastros/frmPrazosProducao.aspx", "Pagina", "status=no, width=250, height=180");
    });

    //Atribui click para embarque imediato
    $('#btnEmbarque').click(function () {
        window.open("../cadastros/frmEmbarqueImediato.aspx", "Pagina", "status=no, width=250, height=240");
    });

    //Atribui click para utilizacao
    $('#btnUtilizacao').click(function () {
        window.open("../cadastros/InformativoUtilizacaoWebForm.aspx", "Pagina", "status=no, width=450, height=250");
    });

});


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

function fComposicao(idiTem) {

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

    var theForm = document.aspnetForm;

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_.value == null) {
        alert("Quantidade deve ser preenchida.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_.focus();
        return false;
    }

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtValor.value == null) {
        alert("Valor deve ser preenchido.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtValor.focus();
        return false;
    }

    return true
}

function validaPedido() {

    var theForm = document.aspnetForm;

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega.value == null) {
        alert("Data entrega deve ser preenchida.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega.focus();
        return false;
    }

    if (theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEmissao.value == "" || theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEmissao.value == null) {
        alert("Data emissao deve ser preenchida.");
        theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEmissao.focus();
        return false;
    }

    //Validando se data maior que a atual
    var dia = theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega.value.split("/")[0];
    var mes = theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega.value.split("/")[1];
    var ano = theForm.ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtDataEntrega.value.split("/")[2];

    return true;
}

function dasabilitarenter(e) {

    return false;
    var key;

    if (document.all) {
        key = window.event.keyCode;     //IE
    } else {
        key = e.which;      //firefox
    }

    if (key == 13) {
        return false;
    } else {
    return true;
    }
}

function autoTab(input, e) {
    var ind = 0;
    var isNN = (navigator.appName.indexOf("Netscape") != -1);
    var keyCode = (isNN) ? e.which : e.keyCode;
    var nKeyCode = e.keyCode;
    if (keyCode == 13) {
        if (!isNN) { window.event.keyCode = 0; } // evitar o beep  
        ind = getIndex(input);
        if (input.form[ind].type == 'textarea') {
            return;
        }
        ind++;
        input.form[ind].focus();
        if (input.form[ind].type == 'text') {
            input.form[ind].select();
        }
    }

    function getIndex(input) {
        var index = -1, i = 0, found = false;
        while (i < input.form.length && index == -1)
            if (input.form[i] == input) {
                index = i;
                if (i < (input.form.length - 1)) {
                    if (input.form[i + 1].type == 'hidden') {
                        index++;
                    }
                    if (input.form[i + 1].type == 'button' && input.form[i + 1].id == 'tabstopfalse') {
                        index++;
                    }
                }
            }
            else
                i++;
        return index;
    }
}   
