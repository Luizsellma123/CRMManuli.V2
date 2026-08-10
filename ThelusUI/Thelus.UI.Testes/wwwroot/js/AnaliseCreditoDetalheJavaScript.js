function MostraModalGrafiasSemelhantes(linkButtonId, linkButtonText) {

    if (linkButtonId != null) {

        if (linkButtonId == 'GrafiasSemelhantesLinkButton' && linkButtonText != '0 Variações Encontradas') {

            $('#GrafiasSemelhantesModalTitle').html('Grafias Semelhantes - Empresa');

            $('#GrafiasSemelhantesDiv').show();

            $('#GrafiasSemelhantesModal').modal();

            callback();
        }
    }
}

function MostraModalAnotacoesNegativasSociosAdm(linkButtonId, linkButtonText) {

    if (linkButtonId != null) {

        $('#AnotacoesNegativasSociosAdmModalTitle').html('Resumo - Anotações dos sócios e administradores');

        $('#AnotacoesNegativasSociosAdmDiv').show();

        $('#AnotacoesNegativasSociosAdmModal').modal();

        callback();

    }
}

function MostraModalAnotacoesNegativasEmpresa(linkButtonId, linkButtonText) {

    var ID = linkButtonId.substring(25, linkButtonId.length);

    var IDNome = ID.replace('LinkButton', '');

    var GridViewRowsID = '#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_' + IDNome + 'EmpresaGridViewRows';

    var elemento = document.querySelector(GridViewRowsID);

    var GridViewRowsCount = elemento.textContent;

    EscondeDivsAnotacoesNegativasEmpresa();

    if (linkButtonId != null) {

        var elementoConcetreResumoEmpresa = document.querySelector('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ConcetreResumoEmpresaGridViewRows');

        var ConcetreResumoEmpresaGridViewRowsCount = elementoConcetreResumoEmpresa.textContent;

        if (ConcetreResumoEmpresaGridViewRowsCount > 0) {
            $('#ConcetreResumoEmpresaDiv').show();
        }

        if (ID == 'PefinLinkButton') {
            $('#AnotacoesNegativasEmpresaModalTitle').html('Pefin - Empresa');

            if (linkButtonText != ' Nada Consta  ' && GridViewRowsCount > 0) {
                $('#PefinEmpresaDiv').show();
            }
        }
        else if (ID == 'ProtestoLinkButton') {
            $('#AnotacoesNegativasEmpresaModalTitle').html('Protesto - Empresa');

            if (linkButtonText != ' Nada Consta  ' && GridViewRowsCount > 0) {
                $('#ProtestoEmpresaDiv').show();
            }
        }
        else if (ID == 'ChequesLinkButton') {
            $('#AnotacoesNegativasEmpresaModalTitle').html('Cheques - Empresa');

            if (linkButtonText != ' Nada Consta  ' && GridViewRowsCount > 0) {
                $('#ChequesEmpresaDiv').show();
            }
        }
        else if (ID == 'ParticipacaoFalenciaLinkButton') {
            $('#AnotacoesNegativasEmpresaModalTitle').html('ParticipacaoFalencia - Empresa');

            if (linkButtonText != ' Nada Consta  ' && GridViewRowsCount > 0) {
                $('#ParticipacaoFalenciaEmpresaDiv').show();
            }
        }
        else if (ID == 'RefinLinkButton') {
            $('#AnotacoesNegativasEmpresaModalTitle').html('Refin - Empresa');

            if (linkButtonText != ' Nada Consta  ' && GridViewRowsCount > 0) {
                $('#RefinEmpresaDiv').show();
            }
        }
        else if (ID == 'AcaoJudicialLinkButton') {
            $('#AnotacoesNegativasEmpresaModalTitle').html('Ação Judicial - Empresa');

            if (linkButtonText != ' Nada Consta  ' && GridViewRowsCount > 0) {
                $('#AcaoJudicialEmpresaDiv').show();
            }
        }
        else if (ID == 'RechequeLinkButton') {
            $('#AnotacoesNegativasEmpresaModalTitle').html('Recheques - Empresa');

            if (linkButtonText != ' Nada Consta  ' && GridViewRowsCount > 0) {
                $('#RechequeEmpresaDiv').show();
            }
        }
        else if (ID == 'DividaVencidaLinkButton') {
            $('#AnotacoesNegativasEmpresaModalTitle').html('Dívida Vencida - Empresa');

            if (linkButtonText != ' Nada Consta  ' && GridViewRowsCount > 0) {
                $('#DividaVencidaEmpresaDiv').show();
            }
        }
                
        if (ConcetreResumoEmpresaGridViewRowsCount > 0 || GridViewRowsCount > 0) {
            $('#AnotacoesNegativasEmpresaModal').modal();

            callback();
        }
    }
}

function EscondeDivsAnotacoesNegativasEmpresa() {

    $('#ConcetreResumoEmpresaDiv').hide();

    $('#PefinEmpresaDiv').hide();
    $('#ProtestoEmpresaDiv').hide();
    $('#ChequesEmpresaDiv').hide();
    $('#ParticipacaoFalenciaEmpresaDiv').hide();

    $('#RefinEmpresaDiv').hide();
    $('#AcaoJudicialEmpresaDiv').hide();
    $('#RechequeEmpresaDiv').hide();
    $('#DividaVencidaEmpresaDiv').hide();

}