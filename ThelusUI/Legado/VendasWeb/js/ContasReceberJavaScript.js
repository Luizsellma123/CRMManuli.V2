function ConsultaNotaDetalhe(DocEntry, ObjType) {

    if (DocEntry != null && ObjType != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaDadosCRNota',
            data: "{'DocEntry':" + DocEntry + ",'ObjType':" + ObjType + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSContasReceberNotas = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSContasReceberNotas.NomeCliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSContasReceberNotas.NomeEmpresa);
                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NotaFiscalModalLabel').text(WSContasReceberNotas.NotaFiscal);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TipoModalLabel').text("Receber");
                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmissaoModalLabel').text(WSContasReceberNotas.DataEmissao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ParcelaModalLabel').text(WSContasReceberNotas.Parcela);
                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_VencimentoModalLabel').text(WSContasReceberNotas.DataVencimento);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PagamentoModalLabel').text(WSContasReceberNotas.DataPagamento);
                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ValorParcelaModalLabel').text(WSContasReceberNotas.ValorReceber.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TotalNotaModalLabel').text(WSContasReceberNotas.TotalNota.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_BancoModalLabel').text(WSContasReceberNotas.NomeBanco);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AgenciaModalLabel').text(WSContasReceberNotas.Agencia);

                //alert("teste 3");

                //alert(dados.d.NomeCliente);

                $('#modalTitle').html('Financeiro - Conta Corrente Clientes - Receber');
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