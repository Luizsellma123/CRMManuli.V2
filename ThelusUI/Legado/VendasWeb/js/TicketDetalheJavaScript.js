function ConsultaTicketDetalhe(IDEmpresa, IDTicket) {

    if (IDEmpresa != null && IDTicket != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaTicketDetalhe',
            data: "{'IDEmpresa':" + IDEmpresa + ",'IDTicket':" + IDTicket + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSTicketDetalhe = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSTicketDetalhe.Empresa);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSTicketDetalhe.Cliente);                
                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TicketModalLabel').text(IDTicket);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SolicitanteModalLabel').text(WSTicketDetalhe.Solicitante);                
                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ResponsavelModalLabel').text(WSTicketDetalhe.Responsavel);                
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TratativaModalLabel').text(WSTicketDetalhe.Tratativa);
                //LINHA 4              
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SituacaoModalLabel').text(WSTicketDetalhe.Situacao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PrioridadeModalLabel').text(WSTicketDetalhe.Prioridade);
                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AberturaModalLabel').text(WSTicketDetalhe.Abertura);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_FechamentoModalLabel').text(WSTicketDetalhe.Fechamento);
                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SolucaoModalLabel').text(WSTicketDetalhe.Solucao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_OcorrenciaModalLabel').text(WSTicketDetalhe.Ocorrencia);
                //LINHA 7
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_VendedorModalLabel').text(WSTicketDetalhe.Vendedor);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_MotivoModalLabel').text(WSTicketDetalhe.Motivo);
                //LINHA 8
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DescricaoModalLabel').text(WSTicketDetalhe.Descricao);

                //alert("teste 3");

                //alert(dados.d.NomeCliente);

                $('#modalTitle').html('SAC - Ticket(s)');
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