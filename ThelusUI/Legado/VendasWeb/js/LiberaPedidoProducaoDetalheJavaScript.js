function RecuperaPedidoProdutoDetalhe(IDEmpresa, NumeroPedidoSAP, NumeroPedidoCRM, CodigoItemSAP, Cliche) {

    if (IDEmpresa != null && NumeroPedidoSAP != null && NumeroPedidoCRM != null
        && CodigoItemSAP != null && Cliche != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#PedidoProdutoDetalheModal').modal();

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaPedidoProdutoDetalhe',
            data: "{'IDEmpresa':" + IDEmpresa
                + ",'NumeroPedidoSAP':" + NumeroPedidoSAP
                + ",'NumeroPedidoCRM':" + NumeroPedidoCRM
                + ",'CodigoItemSAP': \"" + CodigoItemSAP + "\""
                + ",'Cliche': \"" + Cliche + "\"}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {

                var WSPedidoProdutoDetalhe = dados.d;

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSPedidoProdutoDetalhe.Cliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSPedidoProdutoDetalhe.Empresa);

                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroPedidoCRMModalLabel').text(WSPedidoProdutoDetalhe.NumeroPedidoCRM);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_StatusPedidoCRMModalLabel').text(WSPedidoProdutoDetalhe.StatusPedidoCRM);

                //LINHA 3                
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NomeVendedorModalLabel').text(WSPedidoProdutoDetalhe.Vendedor);

                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEmissaoModalLabel').text(WSPedidoProdutoDetalhe.DataEmissao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEntregaModalLabel').text(WSPedidoProdutoDetalhe.DataEntrega);

                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmbarqueImediatoModalLabel').text(WSPedidoProdutoDetalhe.EmbarqueImediato);


                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ProdutoModalLabel').text(WSPedidoProdutoDetalhe.Produto);

                //LINHA 7
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClicheModalLabel').text(WSPedidoProdutoDetalhe.Cliche);

                if (WSPedidoProdutoDetalhe.ImagemCliche != "") {
                    $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ImagemClicheModal').attr('src', WSPedidoProdutoDetalhe.ImagemCliche);
                } else {
                    $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ImagemClicheModal').hide();
                }

                $('#modalTitle').html('Pedido Produtos - Detalhe');
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