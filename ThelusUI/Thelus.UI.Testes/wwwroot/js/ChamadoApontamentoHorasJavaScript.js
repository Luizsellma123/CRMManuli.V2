function ConsultaChamadoApontamentoHoras(IDChamado, IDUsuarioResponsavel, IDApontamento) {

    if (IDChamado != null || IDUsuarioResponsavel != null || IDApontamento != null) {

        $.ajax({
            type: "POST",
            url: '../WebServiceCRM/ComunicacaoCRM.asmx/RetornaChamadoApontamentoHoras',
            data: "{'IDChamado':" + IDChamado + ",'IDUsuarioResponsavel':" + IDUsuarioResponsavel + ",'IDApontamento':" + IDApontamento + "}",
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (dados) {

                var WSChamadoApontamentoHoras = dados.d;

                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_SolicitanteModalTextBox').val(WSChamadoApontamentoHoras.Solicitante);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroChamadoModalTextBox').val(WSChamadoApontamentoHoras.IDChamado);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_ResponsavelModalTextBox').val(WSChamadoApontamentoHoras.Responsavel);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DataModalTextBox').val(WSChamadoApontamentoHoras.DataApontamento);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_NumeroHorasModalTextBox').val(WSChamadoApontamentoHoras.NumeroHoras);
                $('#ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_DescricaoModalTextBox').val(WSChamadoApontamentoHoras.Descricao);

                $('#ChamadoApontamentoHorasModal').modal();

                callback();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('There was an error' + jqXHR + textStatus + errorThrown);
            }
        });
    }
}