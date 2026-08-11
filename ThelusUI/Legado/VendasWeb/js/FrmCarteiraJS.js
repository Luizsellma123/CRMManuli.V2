jQuery(function ($) {

    document.getElementById("EntCodDetalhar").style.display = 'none'; //Deixa campo auxiliar invisivel

    $('.btn.btn-primary.fa.fa-plus-square').click(function () {
        $('#Detalhe1').remove();
        var IDCliente = $(this).attr('data-id');
        var Aux = document.getElementById('EntCodDetalhar').value;

        if (Aux != IDCliente) {
            $(this).closest('tr').after('<tr id="Detalhe1"><td colspan="9" style="padding:10px;border: 1px solid #eee;"></td></tr>');
            $('#Detalhe1 td').load('CarteiraDetalheWebForm.aspx?IDCliente=' + IDCliente + ' #contentDetalhe');

            document.getElementById('EntCodDetalhar').value = IDCliente;

        }
        else {
            document.getElementById('EntCodDetalhar').value = "";
        }

        return false;
    });
});