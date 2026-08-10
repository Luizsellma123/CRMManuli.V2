jQuery(function ($) {

    $.mask.definitions['~'] = '[0123456789,.]';

    //Coloca mascaras nos campos
    $("#ctl00_ContentPlaceHolder1_ProdutosTextBox").mask("?~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

});

function ConsultaNota(CodigoEmpresa, NumeroNotaFiscal, NumeroSerialNota, NumeroPedidoSAP) {
   
    if (NumeroNotaFiscal != 0 && NumeroNotaFiscal != null && NumeroPedidoSAP != 0 && NumeroPedidoSAP != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaDadosNota',
            data: "{'CodigoEmpresa':" + CodigoEmpresa + ",'NumeroNotaFiscal':" + NumeroNotaFiscal + ",'NumeroPedidoSAP':" + NumeroPedidoSAP + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSClasseNotaFiscal = dados.d;
                //alert("teste" + WSClasseNotaFiscal.NomeEmpresa);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpCod').text(WSClasseNotaFiscal.CodigoEmpresa);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpNome').text(WSClasseNotaFiscal.NomeEmpresa);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EntCod').text(WSClasseNotaFiscal.CodigoCliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EntCpfCgc').text(WSClasseNotaFiscal.NumeroCNPJ);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EntNome').text(WSClasseNotaFiscal.NomeCliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedVendaData').text(WSClasseNotaFiscal.DataDigitacao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NFHoraSaida').text(WSClasseNotaFiscal.DataSaida);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PrevisaoEntrega').text(WSClasseNotaFiscal.PrevisaoEntrega);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EntEnderCompleto').text(WSClasseNotaFiscal.Endereco);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EntBair').text(WSClasseNotaFiscal.Bairro);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_CidNome').text(WSClasseNotaFiscal.Cidade);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_UfSigla').text(WSClasseNotaFiscal.UnidadeFederativa);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EntCep').text(WSClasseNotaFiscal.CEPCliente);
                //$('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_CondPagCod').text(WSClasseNotaFiscal.CondicaoPagamento);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_CondPagPedVendaNome').text(WSClasseNotaFiscal.CondicaoPagamento);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_VendCod').text(WSClasseNotaFiscal.CodigoVendedor);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_VendNome').text(WSClasseNotaFiscal.NomeVendedor);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedVendaValMerc').text(WSClasseNotaFiscal.TotalMercadorias.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedVendaValIpiCalc').text(WSClasseNotaFiscal.TotalIPI.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedVendaValIcms').text(WSClasseNotaFiscal.TotalICMS.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_IcmsDiferido').text(WSClasseNotaFiscal.TotalDiferimentoICMS.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedVendaValTotal').text(WSClasseNotaFiscal.TotalComIPI.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedVendaStatFrete').text(WSClasseNotaFiscal.Frete);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EntCodTransp').text(WSClasseNotaFiscal.CodigoTransportadora);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EntNomeTransp').text(WSClasseNotaFiscal.NomeTransportadora);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedVendaTexto').text(WSClasseNotaFiscal.ObservacaoNota);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PedVendaTextoHist').text(WSClasseNotaFiscal.HistoricoPedido);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ItensFormatados').html(WSClasseNotaFiscal.ItensFormatado);

                $('#modalTitle').html('Nota Fiscal: ' + NumeroSerialNota);
                $('#LoadingDados').hide();
                $('#DadosModal').show();
                $('.modal').data('bs.modal').handleUpdate();

                //$('#fullReservaModal').modal();

                LocacaoModalClassJS = dados.d;
                /*$(dados.d).each(function () {
                    alert(dados.d.title);
                    
                });*/
                callback(LocacaoModalClassJS);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('There was an error' + jqXHR + textStatus + errorThrown);
            },
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $("#loading").hide();
            }
        });

        /*
        $('#modalTitle').html('Teste');
        //$('#modalBody').html('');
        $('#eventUrl').attr('href', '');
        //$('#eventUrl').attr('href', event.url);
        $('#fullReservaModal').modal();
        */
    }
}