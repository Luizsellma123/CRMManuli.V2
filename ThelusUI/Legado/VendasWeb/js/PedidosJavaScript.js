function ConsultaNotaDetalhe(DocEntry, ObjType) {

    if (DocEntry != null ) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RecuperaPedidos',
            data: "{'DocEntry':" + DocEntry + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSPedidos = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSPedidos.NomeCliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSPedidos.NomeEmpresa);
                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedidoSAPModalLabel').text(WSPedidos.PedidoSAP);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedidoCRMModalLabel').text(WSPedidos.PedidoCRM);
                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataEmissaoModalLabel').text(WSPedidos.DataEmissao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TotalPedidoModalLabel').text(WSPedidos.TotalPedido.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_HistoricoModalLabel').text(WSPedidos.HistoricoPedido);
               
                //alert("teste 3");

                //alert(dados.d.NomeCliente);

                $('#modalTitle').html('Financeiro - Conta Corrente Clientes - Pedidos');
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
            },
            complete: function () {
                $("#loading").hide();

                alert("teste 6");

            }
        });
    }
}