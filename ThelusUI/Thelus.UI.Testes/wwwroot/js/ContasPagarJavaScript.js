function ConsultaNotaDetalhe(DocEntry, ObjType) {

    if (DocEntry != null && ObjType != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaDadosCPNota',
            data: "{'DocEntry':" + DocEntry + ",'ObjType':" + ObjType + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSContasPagarNotas = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSContasPagarNotas.NomeCliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSContasPagarNotas.NomeEmpresa);
                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NotaFiscalModalLabel').text(WSContasPagarNotas.NotaFiscal);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TipoModalLabel').text("Pagar");
                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmissaoModalLabel').text(WSContasPagarNotas.DataEmissao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ParcelaModalLabel').text(WSContasPagarNotas.Parcela);
                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_VencimentoModalLabel').text(WSContasPagarNotas.DataVencimento);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PagamentoModalLabel').text(WSContasPagarNotas.DataPagamento);
                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ValorParcelaModalLabel').text(WSContasPagarNotas.ValorPagar.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TotalNotaModalLabel').text(WSContasPagarNotas.TotalNota.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_BancoModalLabel').text(WSContasPagarNotas.NomeBanco);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AgenciaModalLabel').text(WSContasPagarNotas.Agencia);

                // alert("teste 3");

                //alert(dados.d.NomeCliente);

                $('#modalTitle').html('Financeiro - Conta Corrente Clientes - Pagar');
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