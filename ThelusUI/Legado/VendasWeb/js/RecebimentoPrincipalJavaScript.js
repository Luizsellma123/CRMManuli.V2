function AnalisePedido(teste) {

    $('#RecebimentoModal').modal();

    $('#ReprovarLinkButton').hide();

    callback();
}

function ConsultaRecebimentoPrincipal(IDEmpresa, IDRecebimento) {

    if (IDRecebimento != null) {

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaRecebimentoPrincipal',
            data: "{'IDEmpresa':" + IDEmpresa + ",'IDRecebimento':" + IDRecebimento + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {

                var WSRecebimentoPrincipal = dados.d;

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroRecebimentoModalTextBox').val(WSRecebimentoPrincipal.IDRecebimento);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EmpresaModalTextBox').val(WSRecebimentoPrincipal.Empresa);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ResponsavelModalTextBox').val(WSRecebimentoPrincipal.Responsavel);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SituacaoModalTextBox').val(WSRecebimentoPrincipal.Situacao);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SetorModalTextBox').val(WSRecebimentoPrincipal.Setor);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_CNPJModalTextBox').val(WSRecebimentoPrincipal.CNPJ);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_FornecedorModalTextBox').val(WSRecebimentoPrincipal.Fornecedor);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NFModalTextBox').val(WSRecebimentoPrincipal.NF);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataRecebimentoModalTextBox').val(WSRecebimentoPrincipal.DataRecebimento);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ObservacaoModalTextBox').val(WSRecebimentoPrincipal.Observacao);

                $('#RecebimentoPrincipalModal').modal();

                callback();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('There was an error' + jqXHR + textStatus + errorThrown);
            }
        });
    }
}