function ConsultaNotaDetalhe(DocEntry, ObjType) {

    if (DocEntry != null && ObjType != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RecuperaNotasDevolucoes',
            data: "{'DocEntry':" + DocEntry + ",'ObjType':" + ObjType + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSDevolucoes = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ClienteModalLabel').text(WSDevolucoes.NomeCliente);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalLabel').text(WSDevolucoes.NomeEmpresa);
                //LINHA 2
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NotaFiscalModalLabel').text(WSDevolucoes.NotaFiscal);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TipoModalLabel').text("Receber");
                //LINHA 3
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmissaoModalLabel').text(WSDevolucoes.DataEmissao);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ParcelaModalLabel').text(WSDevolucoes.Parcela);
                //LINHA 4
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_VencimentoModalLabel').text(WSDevolucoes.DataVencimento);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_PagamentoModalLabel').text(WSDevolucoes.DataPagamento);
                //LINHA 5
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ValorParcelaModalLabel').text(WSDevolucoes.ValorPagar.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TotalNotaModalLabel').text(WSDevolucoes.TotalNota.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' }));
                //LINHA 6
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_BancoModalLabel').text(WSDevolucoes.NomeBanco);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_AgenciaModalLabel').text(WSDevolucoes.Agencia);

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