var currentUpdateEvent;
var globalStartDate;
var globalEndDate;
var globalAllDay;





function AddComEntidade(Entidade) {
    //alert(event.description);

    /*=====================================================
    Funcao chamada ao Clicar na Criacao do JS calendario-amostra
    Ela carrega os dados da Entidade que precisa ser vinculada no Modal
    ======================================================*/
    if ($(this).data("qtip")) $(this).qtip("destroy");

    //PageMethods.addEvent(eventToAdd, addSuccess);
    $('#addDialog').dialog('open');

    $("#addEntNome").text(Entidade.EntNome);
    $("#addEntCodHidden").text(Entidade.EntCod);
    $("#addEntCpfCgc").text(Entidade.EntCpfCgc);
    $("#addEndereco").text(Entidade.Endereco);
    $("#addContatoNome").text(Entidade.ContatoNome);
    $("#addContatoTelefone").text(Entidade.ContatoTelefone);
    $("#addContatoEmail").text(Entidade.ContatoEmail);

    $("#addStatEntComercial").text(Entidade.StatEntComercial.toString());
    $("#addEntStatDescr").text(Entidade.EntStatDescr.toString());
   
    $("#addNFValTotNota").text(Entidade.NFValTotNota.toString());
    
    
    //$('select[name=addClasseCliente]').val(Entidade.ClasseCliente.toString());

    $("#addTotalVendaAnual").text(Entidade.TotalVendaAnual.toString());

    $("#AddIdTipoAgendamentoHidden").text(Entidade.IdTipoAgendamento.toString());
    document.getElementById("ctl00_ContentPlaceHolder1_idTipoAgendamentoVincularEntidadeHiddenField").value = "";


    if (Entidade.DataUltimaVisita.toString() != "01/01/1900") {
        $("#addDataUltimaVisita").text(Entidade.DataUltimaVisita.toString());
    }
    if (Entidade.NFHoraSaida.toString() != "01/01/1900") {
        $("#addNFHoraSaida").text(Entidade.NFHoraSaida.toString());
    }
    
    

    //Cria tabela com Itens da NF
    var container = document.getElementById("addItensNF");
    container.innerHTML = ["<table class='table table-condensed table-responsive'><thead> <tr class='bg-gray-light'><th>Código</th><th>Descrição</th><th>Classe</th><th>Quantidade</th><th>Valor Unitário</th></tr></thead><tbody> " + Entidade.ItensNF.toString() + " </tbody></table>"];

}


function updateEvent(event, element) {
    //alert(event.description);

    /*=====================================================
    Funcao chamada ao Clicar no evento do calendario
    ======================================================*/


    if ($(this).data("qtip")) $(this).qtip("destroy");

    currentUpdateEvent = event;

    $('#updatedialog').dialog('open');
    
    //Limpa a Msg
    $('#upMensagemLabel').html('');

    $("#upEntNome").text(event.EntNome);
    $("#upEntCod").text(event.EntCod);
    $("#upEntCpfCgc").text(event.EntCpfCgc);
    $("#upEndereco").text(event.Endereco);
    $("#upContatoNome").text(event.ContatoNome);
    $("#upContatoTelefone").text(event.ContatoTelefone);
    $("#upContatoEmail").text(event.ContatoEmail);
    $("#upLinhaProdutoQuantidadeStretch").val(event.LinhaProdutoQuantidadeStretch.toString());
    $("#upLinhaProdutoQuantidadeFitaPP").val(event.LinhaProdutoQuantidadeFitaPP.toString());
    $("#upLinhaProdutoQuantidadeFitaImpressa").val(event.LinhaProdutoQuantidadeFitaImpressa.toString());

    $("INPUT[name=upCondicaoVisita]").val([event.CondicaoVisita]);

    $("#upDataInicio").val(event.DataInicio);
    $("#upHoraInicio").val(event.HoraInicio);
    $("#upDataFim").val(event.DataFinal);
    $("#upHoraFim").val(event.HoraFinal);

    $("#upDescricao").val('');
    $("#upDescricao").val(event.description);
    $("#upDescricao").html(event.description);
    $('select[name=upidLembreteUm]').val([event.idLembreteUm]);

    $('#upVinculaEntidadeHidden').text(event.VinculaEntidade);

    $("INPUT[name=upEstimativaVendaFitaImpressa]").val([event.EstimativaVendaFitaImpressa]);
    $("INPUT[name=upEstimativaVendaFitaPP]").val([event.EstimativaVendaFitaPP]);
    $("INPUT[name=upEstimativaVendaStretch]").val([event.EstimativaVendaStretch]);
    $("INPUT[name=upComRepresentante]").val([event.ComRepresentante]);

    

    $("#upMeioTransporte").val(event.MeioTransporte.toString());
    $("#upKm").val(event.Km.toString());
    $("#upValorEstimadoViagem").val(event.ValorEstimadoViagem.toString());

    $("#upStatEntComercial").text(event.StatEntComercial.toString());
    $("#upEntStatDescr").text(event.EntStatDescr.toString());

    if (event.DataUltimaVisita.toString() != "01/01/1900") {
        $("#upDataUltimaVisita").text(event.DataUltimaVisita.toString());
    }
    if (event.NFHoraSaida.toString() != "01/01/1900") {
        $("#upNFHoraSaida").text(event.NFHoraSaida.toString());
    }


    $("#upNFValTotNota").text(event.NFValTotNota.toString());
    $("#upTotalVendaAnual").text(event.TotalVendaAnual.toString());
    $('select[name=upClasseCliente]').val([event.ClasseCliente.toString()]);


    
    
    //Cria tabela com Itens da NF
    var container = document.getElementById("upItensNF");
    container.innerHTML = ["<table class='table table-condensed table-responsive'><thead> <tr class='bg-gray-light'><th>Código</th><th>Descrição</th><th>Classe</th><th>Quantidade</th><th>Valor Unitário</th></tr></thead><tbody> " + event.ItensNF.toString() + " </tbody></table>"];

    

    //$('.selectpicker').selectpicker('refresh'); //USAR O REFRESH APENAS NO FINAL, SENÃO DA MUITA MERDA!

    
    if(event.VinculaEntidade == "False")
    {
        $("#upComEntidadeDiv").hide();

    }
    


    //Inicia Validacao se a Data Inicial e menor que a data Atual
    var objUpdateDataInicial = new Date();
    objUpdateDataInicial.setYear(event.DataInicio.split("-")[0]);
    objUpdateDataInicial.setMonth(event.DataInicio.split("-")[1] - 1); //- 1 pq em js é de 0 a 11 os meses
    objUpdateDataInicial.setDate(event.DataInicio.split("-")[2]);
    objUpdateDataInicial.setHours(event.HoraInicio.split(":")[0]);
    objUpdateDataInicial.setMinutes(event.HoraInicio.split(":")[1]);

    if (objUpdateDataInicial.getTime() < new Date().getTime()) {
        $("#upDataInicio").prop('disabled', true);
        $("#upHoraInicio").prop('disabled', true);
        $("#upDataFim").prop('disabled', true);
        $("#upHoraFim").prop('disabled', true);
    } else {
        $("#upDataInicio").prop('disabled', false);
        $("#upHoraInicio").prop('disabled', false);
        $("#upDataFim").prop('disabled', false);
        $("#upHoraFim").prop('disabled', false);
    }
    //Fim Validacao se a Data Inicial e menor que a data Atual


    //Inicia Validacao se a Data Final e menor que a data Atual
    var objUpdateDataFinal = new Date();
    objUpdateDataFinal.setYear(event.DataFinal.split("-")[0]);
    objUpdateDataFinal.setMonth(event.DataFinal.split("-")[1] - 1); //- 1 pq em js é de 0 a 11 os meses
    objUpdateDataFinal.setDate(event.DataFinal.split("-")[2]);
    objUpdateDataFinal.setHours(event.HoraFinal.split(":")[0]);
    objUpdateDataFinal.setMinutes(event.HoraFinal.split(":")[1]);

    var df = (new Date().getTime() - objUpdateDataFinal.getTime()) / 1000 / 60 / 60;


    if (df > 48) {
        $("#upDescricao").prop('disabled', true);
    } else {
        $("#upDescricao").prop('disabled', false);
    }
    //Fim Validacao se a Data Final e menor que a data Atual


   
    return false;
}

function updateSuccess(updateResult) {

    /*===========================================
    Funcao chamada apos Atualizar um Evento
    ============================================*/
   //$('#demo-calendar').fullCalendar({ events: "../JsonResponse.ashx"});
    //$('#demo-calendar').fullCalendar( 'refetchEvents');

    //As forma acima comentadas de Update nao deram certo, como medida chamamos a funcao de delete e add pois as mesma atualizaram o html do calendario.
    //Dessa forma o refresh ocorre na tela perfeitamente
    $('#demo-calendar').fullCalendar('removeEvents', updateResult.id);
    addSuccess(updateResult);

    



}

function deleteSuccess(deleteResult) {
    /*===========================================
    Funcao chamada apos Deletar um Evento
    ============================================*/
    //alert(currentUpdateEvent.id);
    $('#demo-calendar').fullCalendar('removeEvents', currentUpdateEvent.id);
}

function addSuccess(addResult) {
/*==================================================================================
  Antes os addResult retornava apenas um ID, modificado para retornar um objeto

  Essa funcao é chamada apos incluir um Evento, ela atualiza com os novo dados o Calendario
==================================================================================*/
   // alert(addResult.VinculaEntidade);
   
    if (addResult.IDAgendamento > 0) {
        $('#demo-calendar').fullCalendar('renderEvent',
						{
                        id:addResult.id,
                        title: addResult.title,
                        description: addResult.description,
                        start: addResult.start,
                        end: addResult.end ,
                        allDay: false,
                        className: addResult.className,
                        IdTipoAgendamento: addResult.IdTipoAgendamento,
                        CondicaoVisita : addResult.CondicaoVisita,
                        idLembreteUm: addResult.idLembreteUm,
                        idLembreteDois: addResult.idLembreteDois ,
                        EntCod: addResult.EntCod ,
                        EntNome: addResult.EntNome,
                        EntCpfCgc: addResult.EntCpfCgc ,
                        Endereco: addResult.Endereco,
                        ContatoNome: addResult.ContatoNome,
                        ContatoTelefone: addResult.ContatoTelefone,
                        ContatoEmail: addResult.ContatoEmail,
                        LinhaProdutoQuantidadeStretch: addResult.LinhaProdutoQuantidadeStretch,
                        LinhaProdutoQuantidadeFitaPP: addResult.LinhaProdutoQuantidadeFitaPP,
                        LinhaProdutoQuantidadeFitaImpressa: addResult.LinhaProdutoQuantidadeFitaImpressa,
                        DataInicio: addResult.DataInicio,
                        DataFinal: addResult.DataFinal,
                        HoraInicio: addResult.HoraInicio,
                        HoraFinal: addResult.HoraFinal,
                        VinculaEntidade: addResult.VinculaEntidade,


                        EstimativaVendaFitaImpressa : addResult.EstimativaVendaFitaImpressa,
                        EstimativaVendaFitaPP : addResult.EstimativaVendaFitaPP,
                        EstimativaVendaStretch: addResult.EstimativaVendaStretch,
                        ComRepresentante : addResult.ComRepresentante,
                        MeioTransporte : addResult.MeioTransporte,
                        Km : addResult.Km,
                        ValorEstimadoViagem: addResult.ValorEstimadoViagem,

                        StatEntComercial:addResult.StatEntComercial,
                        EntStatDescr:addResult.EntStatDescr,
                        DataUltimaVisita:addResult.DataUltimaVisita,
                        NFHoraSaida:addResult.NFHoraSaida,
                        NFValTotNota:addResult.NFValTotNota ,
                        ItensNF:addResult.ItensNF,
                        ClasseCliente:addResult.ClasseCliente,
                        TotalVendaAnual:addResult.TotalVendaAnual
    

 
						},
						true // make the event "stick"
					);


		$('#demo-calendar').fullCalendar('unselect');
}

}



function updateEventOnDropResize(event, allDay) {

    //alert("allday: " + allDay);
    var eventToUpdate = {
        id: event.id,
        start: event.start
    };

    // FullCalendar 1.x
    //if (allDay) {
    //    eventToUpdate.start.setHours(0, 0, 0);
    //}

    if (event.end === null) {
        eventToUpdate.end = eventToUpdate.start;
    }
    else {
        eventToUpdate.end = event.end;

        // FullCalendar 1.x
        //if (allDay) {
        //    eventToUpdate.end.setHours(0, 0, 0);
        //}
    }

    // FullCalendar 1.x
    //eventToUpdate.start = eventToUpdate.start.format("dd-MM-yyyy hh:mm:ss tt");
    //eventToUpdate.end = eventToUpdate.end.format("dd-MM-yyyy hh:mm:ss tt");

    // FullCalendar 2.x
    var endDate;
    if (!event.allDay) {
        endDate = new Date(eventToUpdate.end + 60 * 60000);
        endDate = endDate.toJSON();
    }
    else {
        endDate = eventToUpdate.end.toJSON();
    }

    eventToUpdate.start = eventToUpdate.start.toJSON();
    eventToUpdate.end = eventToUpdate.end.toJSON(); //endDate;
    eventToUpdate.allDay = event.allDay;

    PageMethods.UpdateEventTime(eventToUpdate, UpdateTimeSuccess);
}

function eventDropped(event, dayDelta, minuteDelta, allDay, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    // FullCalendar 1.x
    //updateEventOnDropResize(event, allDay);

    // FullCalendar 2.x
    updateEventOnDropResize(event);
}

function eventResized(event, dayDelta, minuteDelta, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}

function checkForSpecialChars(stringToCheck) {
    var pattern = /[^A-Za-z0-9 ]/;
    return pattern.test(stringToCheck); 
}



/*===================================================
Botoes de Acao dos Modais no Calendario

Sendo Eles: #updatedialog
            #addDialog

===================================================*/
$(document).ready(function () {
    // update Dialog
    $('#updatedialog').dialog({
        autoOpen: false,
        width: 900,
        heigth: 3000,
        buttons: {
            "Atualizar": function () {
                //Cria um Objetito para enviar para o WebServic
                var eventToUpdate = {
                    id: currentUpdateEvent.id,
                    start: $("#upDataInicio").val() + ' ' + $("#upHoraInicio").val(),
                    end: $("#upDataFim").val() + ' ' + $("#upHoraFim").val(),
                    LinhaProdutoQuantidadeStretch: $("#upLinhaProdutoQuantidadeStretch").val(),
                    LinhaProdutoQuantidadeFitaPP: $("#upLinhaProdutoQuantidadeFitaPP").val(),
                    LinhaProdutoQuantidadeFitaImpressa: $("#upLinhaProdutoQuantidadeFitaImpressa").val(),
                    CondicaoVisita: $("input[name='upCondicaoVisita']:checked").val(),
                    DataInicio: $("#upDataInicio").val(),
                    HoraInicio: $("#upHoraInicio").val(),
                    DataFinal: $("#upDataFim").val(),
                    HoraFinal: $("#upHoraFim").val(),
                    DescricaoCompromisso: $("#upDescricao").val(),
                    idLembreteUm: $('#upidLembreteUm').val(),

                    EstimativaVendaFitaImpressa: $("input[name='upEstimativaVendaFitaImpressa']:checked").val(),
                    EstimativaVendaFitaPP: $("input[name='upEstimativaVendaFitaPP']:checked").val(),
                    EstimativaVendaStretch: $("input[name='upEstimativaVendaStretch']:checked").val(),
                    ComRepresentante: $("input[name='upComRepresentante']:checked").val(),
                    MeioTransporte: $("#upMeioTransporte").val(),
                    Km: $("#upKm").val(),
                    ValorEstimadoViagem: $("#upValorEstimadoViagem").val(),
                    ClasseCliente: $('#upClasseCliente').val()


                };





                var salvar = 'sim';
                var LinhaProdutoQuantidade = 0;
                var Menssagem = '<strong>Você possui o(s) seguinte(s) erro(s): </strong><br/><br/><ul>';
                if ($('#upVinculaEntidadeHidden').text() == 1) {

                }

                if (eventToUpdate.DataInicio == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Data Inicio é obrigatório! </li>';
                }

                if (eventToUpdate.HoraInicio == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Hora Inicio é obrigatório! </li>';
                }

                if (eventToUpdate.DataFinal == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Data Final é obrigatório! </li>';
                }

                if (eventToUpdate.HoraFinal == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Hora Final é obrigatório! </li>';
                }

                if (eventToUpdate.DescricaoCompromisso == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Descrição é obrigatório! </li>';
                }

                if (eventToUpdate.DataInicio + ' ' + eventToUpdate.HoraInicio > eventToUpdate.DataFinal + ' ' + eventToUpdate.HoraFinal) {
                    salvar = 'nao';
                    Menssagem += '<li> Os Campos Data e Hora Inicio devem ser menores que Data e Hora Final! </li>';
                }


                //Inicia Validacao se a Data Inicial e menor que a data Atual
                var objUpdateDataInicial = new Date();
                objUpdateDataInicial.setYear(eventToUpdate.DataInicio.split("-")[0]);
                objUpdateDataInicial.setMonth(eventToUpdate.DataInicio.split("-")[1] - 1); //- 1 pq em js é de 0 a 11 os meses
                objUpdateDataInicial.setDate(eventToUpdate.DataInicio.split("-")[2]);
                objUpdateDataInicial.setHours(eventToUpdate.HoraInicio.split(":")[0]);
                objUpdateDataInicial.setMinutes(eventToUpdate.HoraInicio.split(":")[1]);

                var objUpdateDataFinal = new Date();
                objUpdateDataFinal.setYear(eventToUpdate.DataFinal.split("-")[0]);
                objUpdateDataFinal.setMonth(eventToUpdate.DataFinal.split("-")[1] - 1); //- 1 pq em js é de 0 a 11 os meses
                objUpdateDataFinal.setDate(eventToUpdate.DataFinal.split("-")[2]);
                objUpdateDataFinal.setHours(eventToUpdate.HoraFinal.split(":")[0]);
                objUpdateDataFinal.setMinutes(eventToUpdate.HoraFinal.split(":")[1]);

                var df = (new Date().getTime() - objUpdateDataFinal.getTime()) / 1000 / 60 / 60;

                if ((objUpdateDataInicial.getTime() < new Date().getTime()) && (df > 48)) {
                    salvar = 'nao';
                    Menssagem += '<li> Os Campos Data e Hora Inicio nao devem ser menores que Data e Hora Atual e os Campos Data e Hora Final não podem ser menor que 48 horas da Data e Hora Atual! </li>';
                }
                //Fim Validacao se a Data Inicial e menor que a data Atual


                //Inicia Valida se alguma quantidade de Linha de Produto foi informada
                if (eventToUpdate.LinhaProdutoQuantidadeStretch == '') {
                    eventToUpdate.LinhaProdutoQuantidadeStretch = 0;
                }
                else {
                    LinhaProdutoQuantidade = 1;
                }

                if (eventToUpdate.LinhaProdutoQuantidadeFitaPP == '') {
                    eventToUpdate.LinhaProdutoQuantidadeFitaPP = 0;
                } else {
                    LinhaProdutoQuantidade = 1;
                }

                if (eventToUpdate.LinhaProdutoQuantidadeFitaImpressa == '') {
                    eventToUpdate.LinhaProdutoQuantidadeFitaImpressa = 0;
                } else {
                    LinhaProdutoQuantidade = 1;
                }

                if (LinhaProdutoQuantidade == 0) {
                    salvar = 'nao';
                    Menssagem += '<li>Informar valor ao menos para uma Linha de Produto! </li>';
                }

                //Fim Valida se alguma quantidade de Linha de Produto foi informada

                if (eventToUpdate.Km == '') {
                    eventToUpdate.Km = 0;
                }

                if (eventToUpdate.ValorEstimadoViagem == '') {
                    eventToUpdate.ValorEstimadoViagem = 0;
                }

                if (salvar == 'sim') {
                    //Chama o WEB Servic que esta na .cs da tela FrmCalendario e retorna para a funcao de Sucesso
                    PageMethods.UpdateEvent(eventToUpdate, updateSuccess);
                    $(this).dialog("close");
                } else {

                    var MensagemCustom = "";

                    MensagemCustom = " <div style='width:799px; padding:15px;margin-bottom:20px;border:1px solid transparent;border-radius:4px color:#a94442; background-color:#f2dede; border-color:#ebccd1; '>";
                    MensagemCustom += Menssagem;
                    MensagemCustom += "</ul></div>";


                    $('#upMensagemLabel').html(MensagemCustom);


                }


            },
            "Excluir": function () {
                //Cria um Objetito para enviar para o WebServic
                var eventToDelete = {
                    DataInicio: $("#upDataInicio").val(),
                    HoraInicio: $("#upHoraInicio").val(),
                    DataFinal: $("#upDataFim").val(),
                    HoraFinal: $("#upHoraFim").val(),
                    id: currentUpdateEvent.id
                };

                var MensagemCustom = "";

                //Inicia Validacao se a Data Inicial e menor que a data Atual
                var objDeleteDataInicial = new Date();
                objDeleteDataInicial.setYear(eventToDelete.DataInicio.split("-")[0]);
                objDeleteDataInicial.setMonth(eventToDelete.DataInicio.split("-")[1] - 1); //- 1 pq em js é de 0 a 11 os meses
                objDeleteDataInicial.setDate(eventToDelete.DataInicio.split("-")[2]);
                objDeleteDataInicial.setHours(eventToDelete.HoraInicio.split(":")[0]);
                objDeleteDataInicial.setMinutes(eventToDelete.HoraInicio.split(":")[1]);

                if (objDeleteDataInicial.getTime() >= new Date().getTime()) {
                    //Chama o WEB Servic que esta na .cs da tela FrmCalendario e retorna para a funcao de Sucessso
                    PageMethods.deleteEvent(eventToDelete, deleteSuccess);
                    $(this).dialog("close");
                } else {
                    var Menssagem = '<strong>Atenção!</strong><br/><br/><ul>';
                    Menssagem += 'Não é possivel excluir um agendamento do passado! </li>';
                    MensagemCustom = " <div style='width:799px; padding:15px;margin-bottom:20px;border:1px solid transparent;border-radius:4px color:#a94442; background-color:#f2dede; border-color:#ebccd1; '>";
                    MensagemCustom += Menssagem;
                    MensagemCustom += "</ul></div>";

                    $('#upMensagemLabel').html(MensagemCustom);
                }


            }
        }
    });

    //add dialog
    $('#addDialog').dialog({
        autoOpen: false,
        width: 900,
        heigth: 3000,
        buttons: {
            "Incluir": function () {
                //Cria um Objetito para enviar para o WebServic
                var eventToAdd = {
                    start: $("#addDataInicio").val() + ' ' + $("#addHoraInicio").val(),
                    end: $("#addDataFim").val() + ' ' + $("#addHoraFim").val(),
                    EntCod: $("#addEntCodHidden").text(),
                    LinhaProdutoQuantidadeStretch: $("#addLinhaProdutoQuantidadeStretch").val(),
                    LinhaProdutoQuantidadeFitaPP: $("#addLinhaProdutoQuantidadeFitaPP").val(),
                    LinhaProdutoQuantidadeFitaImpressa: $("#addLinhaProdutoQuantidadeFitaImpressa").val(),

                    CondicaoVisita: $("input[name='addCondicaoVisita']:checked").val(),

                    DataInicio: $("#addDataInicio").val(),
                    HoraInicio: $("#addHoraInicio").val(),
                    DataFinal: $("#addDataFim").val(),
                    HoraFinal: $("#addHoraFim").val(),

                    DescricaoCompromisso: $("#addDescricao").val(),

                    idLembreteUm: $('#addidLembreteUm').val(),
                    IdTipoAgendamento: $('#AddIdTipoAgendamentoHidden').text(),


                    EstimativaVendaFitaImpressa: $("input[name='addEstimativaVendaFitaImpressa']:checked").val(),

                    EstimativaVendaFitaPP: $("input[name='addEstimativaVendaFitaPP']:checked").val(),
                    EstimativaVendaStretch: $("input[name='addEstimativaVendaStretch']:checked").val(),
                    ComRepresentante: $("input[name='addComRepresentante']:checked").val(),
                    MeioTransporte: $("#addMeioTransporte").val(),
                    Km: $("#addKm").val(),
                    ValorEstimadoViagem: $("#addValorEstimadoViagem").val(),
                    ClasseCliente: $('#addClasseCliente').val()

                };



                var salvar = 'sim';
                var LinhaProdutoQuantidade = 0;
                var Menssagem = '<strong>Você possui o(s) seguinte(s) erro(s): </strong><br/><br/><ul>';

                if ($('#addVinculaEntidadeHidden').text() == 1) {

                }

                if (eventToAdd.DataInicio == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Data Inicio é obrigatório! </li>'
                }

                if (eventToAdd.HoraInicio == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Hora Inicio é obrigatório! </li>'
                }

                if (eventToAdd.DataFinal == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Data Final é obrigatório! </li>'
                }

                if (eventToAdd.HoraFinal == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Hora Final é obrigatório! </li>'
                }

                if (eventToAdd.DescricaoCompromisso == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O Campo Descrição é obrigatório! </li>'
                }

                if (eventToAdd.DataInicio + ' ' + eventToAdd.HoraInicio > eventToAdd.DataFinal + ' ' + eventToAdd.HoraFinal) {
                    salvar = 'nao';
                    Menssagem += '<li> Os Campos Data e Hora Inicio devem ser menores que Data e Hora Final! </li>'
                }




                //Inicia Validacao se a Data Inicial e menor que a data Atual
                var objAddDataInicial = new Date();
                objAddDataInicial.setYear(eventToAdd.DataInicio.split("-")[0]);
                objAddDataInicial.setMonth(eventToAdd.DataInicio.split("-")[1] - 1); //- 1 pq em js é de 0 a 11 os meses
                objAddDataInicial.setDate(eventToAdd.DataInicio.split("-")[2]);
                objAddDataInicial.setHours(eventToAdd.HoraInicio.split(":")[0]);
                objAddDataInicial.setMinutes(eventToAdd.HoraInicio.split(":")[1]);


                if (objAddDataInicial.getTime() < new Date().getTime()) {
                    salvar = 'nao';
                    Menssagem += '<li> Os Campos Data e Hora Inicio nao deve ser menores que Data e Hora Atual! </li>';
                }
                //Fim Validacao se a Data Inicial e menor que a data Atual

                //Inicia Valida se alguma quantidade de Linha de Produto foi informada
                if (eventToAdd.LinhaProdutoQuantidadeStretch == '') {
                    eventToAdd.LinhaProdutoQuantidadeStretch = 0;
                }
                else {
                    LinhaProdutoQuantidade = 1;
                }

                if (eventToAdd.LinhaProdutoQuantidadeFitaPP == '') {
                    eventToAdd.LinhaProdutoQuantidadeFitaPP = 0;
                } else {
                    LinhaProdutoQuantidade = 1;
                }

                if (eventToAdd.LinhaProdutoQuantidadeFitaImpressa == '') {
                    eventToAdd.LinhaProdutoQuantidadeFitaImpressa = 0;
                } else {
                    LinhaProdutoQuantidade = 1;
                }

                if (LinhaProdutoQuantidade == 0) {
                    salvar = 'nao';
                    Menssagem += '<li>Informar valor ao menos para uma Linha de Produto! </li>';
                }

                //Fim Valida se alguma quantidade de Linha de Produto foi informada

                if (eventToAdd.ComRepresentante == '') {
                    salvar = 'nao';
                    Menssagem += '<li> O campo Estimativa de Venda Fita Impressa é obrigatório!</li>';
                }

                if (eventToAdd.Km == '') {
                    eventToAdd.Km = 0;
                }

                if (eventToAdd.ValorEstimadoViagem == '') {
                    eventToAdd.ValorEstimadoViagem = 0;
                }



                if (salvar == 'sim') {
                    //Chama o WEB Servic que esta na .cs da tela FrmCalendario e retorna para a funcao de Sucessso
                    PageMethods.addEvent(eventToAdd, addSuccess);
                    $(this).dialog("close");


                } else {

                    var MensagemCustom = "";

                    MensagemCustom = " <div style='width:799px; padding:15px;margin-bottom:20px;border:1px solid transparent;border-radius:4px color:#a94442; background-color:#f2dede; border-color:#ebccd1; '>";
                    MensagemCustom += Menssagem;
                    MensagemCustom += "</ul></div>";
                    $('#addMensagemLabel').html(MensagemCustom);
                }
            }
        }
    });


    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var options = {
        weekday: "long", year: "numeric", month: "short",
        day: "numeric", hour: "2-digit", minute: "2-digit"
    };
});


function ValidaDecimal(stringToCheck) {
    //var pattern = /[^\d*[,]\d{2}$]/;
    var pattern = /[^A-Za-z0-9 ]/;
    return pattern.test(stringToCheck);
}


