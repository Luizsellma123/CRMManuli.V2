function AnalisePedido(numero) {

    $('#CertificadoModal').modal();

    callback();
}

function ConsultaCENPROTProtestos(IDCliente, IDAnalise, IDCartorio) {

    if (IDCliente != null && IDAnalise != null && IDCartorio != null) {

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/ConsultaCENPROTProtestos',
            data: "{'IDCliente':" + IDCliente + ", 'IDAnalise':" + IDAnalise + ", 'IDCartorio':" + IDCartorio + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {

                var WSConsultaCENPROTProtestos = dados.d;

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_CartorioModalTextBox').val(WSConsultaCENPROTProtestos.Cartorio);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_CodigoModalTextBox').val(WSConsultaCENPROTProtestos.Codigo);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_EnderecoModalTextBox').val(WSConsultaCENPROTProtestos.Endereco);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TelefoneModalTextBox').val(WSConsultaCENPROTProtestos.Telefone);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_CidadeModalTextBox').val(WSConsultaCENPROTProtestos.Cidade);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_BairroModalTextBox').val(WSConsultaCENPROTProtestos.Bairro);

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_QuantidadeModalTextBox').val(WSConsultaCENPROTProtestos.Quantidade);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_TotalModalTextBox').val(WSConsultaCENPROTProtestos.Total);

                var htmlProtestos = WSConsultaCENPROTProtestos.HTMLProtestos;

                var divProtestos = document.getElementById('DivProtestosModal');

                divProtestos.innerHTML = htmlProtestos;              

                $('#CENPROTProtestosModal').modal();

                callback();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('There was an error' + jqXHR + textStatus + errorThrown);
            }
        });
    }
}