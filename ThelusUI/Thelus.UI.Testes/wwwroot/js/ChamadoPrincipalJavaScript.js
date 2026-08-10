function AnalisePedido(teste) {

    $('#ChamadoModal').modal();

    $('#ReprovarLinkButton').hide();

    callback();
}

function ConsultaChamadoPrincipal(IDChamado) {

    if (IDChamado != null) {

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaChamadoPrincipal',
            data: "{'IDChamado':" + IDChamado + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {

                var WSChamadoPrincipal = dados.d;

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroChamadoModalTextBox').val(WSChamadoPrincipal.IDChamado);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusChamadoModalTextBox').val(WSChamadoPrincipal.Status);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SolicitanteModalTextBox').val(WSChamadoPrincipal.Solicitante);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AberturaModalTextBox').val(WSChamadoPrincipal.Abertura);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClassificacaoModalTextBox').val(WSChamadoPrincipal.Classificacao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SetorModalTextBox').val(WSChamadoPrincipal.Setor);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SistemaModalTextBox').val(WSChamadoPrincipal.Sistema);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PrioridadeModalTextBox').val(WSChamadoPrincipal.Prioridade);

                var htmlResponsaveis = WSChamadoPrincipal.HTMLResponsaveis;

                var divResponsaveis = document.getElementById('DivResponsaveisModal');

                divResponsaveis.innerHTML = htmlResponsaveis;

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AssuntoModalTextBox').val(WSChamadoPrincipal.Assunto);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DescricaoModalTextBox').val(WSChamadoPrincipal.Descricao);

                $('#ChamadoPrincipalModal').modal();

                callback();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('There was an error' + jqXHR + textStatus + errorThrown);
            }
        });
    }
}