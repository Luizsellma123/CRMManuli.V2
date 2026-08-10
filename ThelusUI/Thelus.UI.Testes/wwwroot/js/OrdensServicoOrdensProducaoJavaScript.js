function ConsultaOrdensServicoOrdensProducao(IDEmpresa, IDOrdemServico, NumeroPedidoSAP, IDITemSAP, DocEntry) {

    if (IDEmpresa != null && IDOrdemServico != null && NumeroPedidoSAP != null && IDITemSAP != null && DocEntry != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RecuperaOrdensServicoOrdensProducao',
            data: "{'IDEmpresa':" + IDEmpresa + ",'IDOrdemServico':" + IDOrdemServico + ",'NumeroPedidoSAP':" + NumeroPedidoSAP
            + ",'IDITemSAP':" + IDITemSAP + ",'DocEntry':" + DocEntry + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSOrdensServicoProdutos = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSOrdensServicoProdutos.Cliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSOrdensServicoProdutos.Empresa);

                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroPedidoSAPModalLabel').text(WSOrdensServicoProdutos.NumeroPedidoSAP);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusPedidoSAPModalLabel').text(WSOrdensServicoProdutos.StatusPedidoSAP);

                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroPedidoCRMModalLabel').text(WSOrdensServicoProdutos.NumeroPedidoCRM);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusPedidoCRMModalLabel').text(WSOrdensServicoProdutos.StatusPedidoCRM);

                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEmissaoModalLabel').text(WSOrdensServicoProdutos.DataEmissao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEntregaModalLabel').text(WSOrdensServicoProdutos.DataEntrega);

                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroOPModalLabel').text(WSOrdensServicoProdutos.NumeroOrdemProducaoSAP);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusOPModalLabel').text(WSOrdensServicoProdutos.StatusOrdemProducao);

                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmbarqueImediatoModalLabel').text(WSOrdensServicoProdutos.EmbarqueImediato);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NomeVendedorModalLabel').text(WSOrdensServicoProdutos.NomeVendedor);

                //LINHA 7
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DepositoModalLabel').text(WSOrdensServicoProdutos.Deposito);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ProdutoModalLabel').text(WSOrdensServicoProdutos.Produto);

                //LINHA 8
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DepositoRelacionalModalLabel').text(WSOrdensServicoProdutos.DepositoRelacional);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ProdutoRelacionalModalLabel').text(WSOrdensServicoProdutos.ProdutoRelacional);

                //LINHA 9
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ItensFormatadoModalLabel').html(WSOrdensServicoProdutos.ItensFormatado);

                //alert("teste 3");

                //alert(dados.d.ItensFormatado);

                $('#modalTitle').html('Ordem Serviço - Principal');
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