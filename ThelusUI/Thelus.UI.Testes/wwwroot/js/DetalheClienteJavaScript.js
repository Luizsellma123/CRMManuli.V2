function ConsultaClienteDetalhe(IDCliente) {

    if (IDCliente != null) {
        $('#modalTitle').html('Carregando...');
        $('#DadosModal').hide();
        $('#LoadingDados').show();
        $('#fullReservaModal').modal();

        //alert("teste 1");

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RecuperaDetalheCliente',
            data: "{'IDCliente':" + IDCliente + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {
                var WSClienteDetalhes = dados.d;

                //alert("teste 2");

                //LINHA 1
                $('#ctl00_ContentPlaceHolder1_ClienteModalLabel').text(WSClienteDetalhes.Cliente);
                //alert(dados.d.Cliente);
                $('#ctl00_ContentPlaceHolder1_CNPJModalLabel').text(WSClienteDetalhes.CNPJ);
                //alert(dados.d.Cidade);

                //LINHA 2
                $('#ctl00_ContentPlaceHolder1_TelefoneModalLabel').text(WSClienteDetalhes.Telefone);
                //alert(dados.d.Cliente);
                $('#ctl00_ContentPlaceHolder1_CidadeModalLabel').text(WSClienteDetalhes.Cidade);
                //alert(dados.d.Cidade);

                //LINHA 3
                $('#ctl00_ContentPlaceHolder1_VendedorModalLabel').text(WSClienteDetalhes.Vendedor);
                //alert(dados.d.Vendedor);
                $('#ctl00_ContentPlaceHolder1_ClasseModalLabel').text(WSClienteDetalhes.Classe);
                //alert(dados.d.Classe);

                //LINHA 4
                $('#ctl00_ContentPlaceHolder1_UltimoHistoricoModalLabel').text(WSClienteDetalhes.UltimoHistorico);
                //alert(dados.d.UltimoHistorico);

                //alert("teste 3");                              

                $('#modalTitle').html('Detalhes do Cliente');
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