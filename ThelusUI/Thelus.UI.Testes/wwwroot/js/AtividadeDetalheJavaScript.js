function ConsultaAtividadeDetalhe(IDEmpresa, IDTicket, IDAtividade) {

    if (IDEmpresa != null && IDTicket != null && IDAtividade != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaAtividadeDetalhe',
            data: "{'IDEmpresa':" + IDEmpresa + ",'IDTicket':" + IDTicket + ",'IDAtividade':" + IDAtividade + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSAtividadeDetalhe = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSAtividadeDetalhe.Cliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSAtividadeDetalhe.Empresa);
                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SolicitanteModalLabel').text(WSAtividadeDetalhe.Solicitante);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TicketModalLabel').text(IDTicket);
                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SituacaoModalLabel').text(WSAtividadeDetalhe.Situacao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AssuntoModalLabel').text(WSAtividadeDetalhe.Assunto);
                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DescricaoModalLabel').text(WSAtividadeDetalhe.Descricao);
                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClassificacaoModalLabel').text(WSAtividadeDetalhe.Classificacao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataModalLabel').text(WSAtividadeDetalhe.Data);
                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PrioridadeModalLabel').text(WSAtividadeDetalhe.Prioridade);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AtividadeModalLabel').text(IDAtividade);
                //LINHA 7
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SetorModalLabel').text(WSAtividadeDetalhe.Setor);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ResponsavelModalLabel').text(WSAtividadeDetalhe.Responsavel);
                //LINHA 8
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AssuntoAtividadeModalLabel').text(WSAtividadeDetalhe.AssuntoAtividade);
                //LINHA 9
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DescricaoAtividadeModalLabel').text(WSAtividadeDetalhe.DescricaoAtividade);

                //alert("teste 3");

                //alert(dados.d.NomeCliente);

                $('#modalTitle').html('SAC - Atividade(s)');
                $('#LoadingDados').hide();
                $('#DadosModal').show();
                $('.modal').data('bs.modal').handleUpdate();

                LocacaoModalClassJS = dados.d;

                //alert("teste 4");

                callback(LocacaoModalClassJS);

                alert("teste 5");

            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('There was an error' + jqXHR + textStatus + errorThrown);
            },
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $("#loading").hide();

                alert("teste 6");

            }
        });
    }
}