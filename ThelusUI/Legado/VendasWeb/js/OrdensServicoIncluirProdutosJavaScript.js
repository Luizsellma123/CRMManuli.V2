function ConsultaOrdensServicoDetalhe(DocEntry, IDITemSAP) {

    if (DocEntry != null && IDITemSAP != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RecuperaOrdensServicoIncluirProdutos',
            data: "{'DocEntry':" + DocEntry + ",'IDITemSAP':" + IDITemSAP + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSOrdensServicoIncluirProdutos = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSOrdensServicoIncluirProdutos.Cliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSOrdensServicoIncluirProdutos.Empresa);

                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroPedidoSAPModalLabel').text(WSOrdensServicoIncluirProdutos.NumeroPedidoSAP);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusPedidoSAPModalLabel').text(WSOrdensServicoIncluirProdutos.StatusPedidoSAP);

                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroPedidoCRMModalLabel').text(WSOrdensServicoIncluirProdutos.NumeroPedidoCRM);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusPedidoCRMModalLabel').text(WSOrdensServicoIncluirProdutos.StatusPedidoCRM);

                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEmissaoModalLabel').text(WSOrdensServicoIncluirProdutos.DataEmissao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEntregaModalLabel').text(WSOrdensServicoIncluirProdutos.DataEntrega);

                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmbarqueImediatoModalLabel').text(WSOrdensServicoIncluirProdutos.EmbarqueImediato);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NomeVendedorModalLabel').text(WSOrdensServicoIncluirProdutos.NomeVendedor);

                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ProdutoModalLabel').text(WSOrdensServicoIncluirProdutos.Produto);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ProdutoRelacionalModalLabel').text(WSOrdensServicoIncluirProdutos.ProdutoRelacional);

                //LINHA 7
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_HistoricoPedidoModalLabel').text(WSOrdensServicoIncluirProdutos.HistoricoPedido);

                //LINHA 8
                if (WSOrdensServicoIncluirProdutos.ImagemCliche != "") {
                    $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ImagemClicheModal').attr('src', WSOrdensServicoIncluirProdutos.ImagemCliche);
                } else {
                    $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ImagemClicheModal').hide();
                }

                $('#modalTitle').html('Ordem Serviço - Incluir Produtos');
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