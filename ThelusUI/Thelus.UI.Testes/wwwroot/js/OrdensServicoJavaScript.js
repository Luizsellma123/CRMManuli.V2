function ConsultaOrdensServicoDetalhe(IDEmpresa, IDOrdemServico, NumeroPedidoSAP, IDITemSAP) {

    if (IDEmpresa != null && IDOrdemServico != null && NumeroPedidoSAP != null && IDITemSAP != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#OrdensServicoModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RecuperaOrdensServicoProdutos',
            data: "{'IDEmpresa':" + IDEmpresa + ",'IDOrdemServico':" + IDOrdemServico
                + ",'NumeroPedidoSAP':" + NumeroPedidoSAP + ",'IDITemSAP':" + IDITemSAP + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSOrdensServicoProdutosDetalhes = dados.d;

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSOrdensServicoProdutosDetalhes.Cliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSOrdensServicoProdutosDetalhes.Empresa);

                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroPedidoSAPModalLabel').text(WSOrdensServicoProdutosDetalhes.NumeroPedidoSAP);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusPedidoSAPModalLabel').text(WSOrdensServicoProdutosDetalhes.StatusPedidoSAP);

                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroPedidoCRMModalLabel').text(WSOrdensServicoProdutosDetalhes.NumeroPedidoCRM);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusPedidoCRMModalLabel').text(WSOrdensServicoProdutosDetalhes.StatusPedidoCRM);

                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEmissaoModalLabel').text(WSOrdensServicoProdutosDetalhes.DataEmissao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEntregaModalLabel').text(WSOrdensServicoProdutosDetalhes.DataEntrega);

                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmbarqueImediatoModalLabel').text(WSOrdensServicoProdutosDetalhes.EmbarqueImediato);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NomeVendedorModalLabel').text(WSOrdensServicoProdutosDetalhes.NomeVendedor);

                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ProdutoModalLabel').text(WSOrdensServicoProdutosDetalhes.Produto);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ProdutoRelacionalModalLabel').text(WSOrdensServicoProdutosDetalhes.ProdutoRelacional);

                //LINHA 7
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_HistoricoPedidoModalLabel').text(WSOrdensServicoProdutosDetalhes.HistoricoPedido);

                //LINHA 8
                if (WSOrdensServicoProdutosDetalhes.ImagemCliche != "") {
                    $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ImagemClicheModal').attr('src', WSOrdensServicoProdutosDetalhes.ImagemCliche);
                } else {
                    $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ImagemClicheModal').hide();
                }

                $('#modalTitle').html('Ordem Serviço - Principal');
                $('#LoadingDados').hide();
                $('#DadosModal').show();
                $('.modal').data('bs.modal').handleUpdate();

                LocacaoModalClassJS = dados.d;

                //alert("teste 4");

                callback(LocacaoModalClassJS);

                //alert("teste 5");

            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('There was an error' + jqXHR + textStatus + errorThrown);
            },
            beforeSend: function () {
                $('#loading').show();
                //alert("teste 5");
            },
            complete: function () {
                $("#loading").hide();

                alert("teste 6");

            }
        });
    }
}