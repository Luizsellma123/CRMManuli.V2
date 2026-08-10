
// Misc-FullCalendar.js
// ====================================================================
// This file should not be included in your project.
// This is just a sample how to initialize plugins or components.
//
// - ThemeOn.net -




$(document).ready(function () {


    // Calendar
    // =================================================================
    // Require Full Calendar
    // -----------------------------------------------------------------
    // http://fullcalendar.io/
    // =================================================================

    // initialize the external events
    // -----------------------------------------------------------------
    $('#demo-external-events .fc-event').each(function () {
        // store data so the calendar knows to render an event upon drop
        $(this).data('event', {
            title: $.trim($(this).text()), // use the element's text as the event title
            stick: true, // maintain when user navigates (see docs on the renderEvent method)
            className: $(this).data('class')
        });


        // make the event draggable using jQuery UI
        $(this).draggable({
            zIndex: 99999,
            revert: true,      // will cause the event to go back to its
            revertDuration: 0  //  original position after the drag
        });
    });


    // Initialize the calendar
    // -----------------------------------------------------------------
    $('#demo-calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
       eventClick: updateEvent,
        selectable: true,
        selectHelper: true,
        //select: selectDate,
        editable: true,
        events: "../JsonResponse.ashx",
        eventDrop: eventDropped,
        eventResize: eventResized,
        eventRender: function(event, element) {
            //alert(event.teste);
            element.qtip({
                content: {
                    //text: qTipText(event.start, event.end, event.description,event.EntNome),
                    title: '<strong>' + event.title + '</strong>'
                },
                position: {
                    my: 'bottom left',
                    at: 'top right'
                },
                style: { classes: 'qtip-shadow qtip-rounded' }
            });
        },
        drop: function () {
            // is the "remove after drop" checkbox checked?
            if ($('#drop-remove').is(':checked')) {
                // if so, remove the element from the "Draggable Events" list
                $(this).remove();
            }
        },
        eventLimit: true, // allow "more" link when too many events        
    });




    
    //Customizacao Manuli, verifica se a Hidden com o codigo da Entidade foi Alimentada
    //Se estiver com valor indica que uma entidade precisa ser vinculada aos dados que aparecem no modal
    if( document.getElementById("ctl00_ContentPlaceHolder1_EntCodHiddenField").value != '')
    {

        //Cria Objeto para enviar no WebService
        var EntidadeConsultar = {
                    
                    EntCod: document.getElementById("ctl00_ContentPlaceHolder1_EntCodHiddenField").value,
                    IdTipoAgendamento: document.getElementById("ctl00_ContentPlaceHolder1_idTipoAgendamentoVincularEntidadeHiddenField").value

                };

       //Limpa Hiddens
       //document.getElementById("ctl00_ContentPlaceHolder1_EntCodHiddenField").value = '';
       //document.getElementById("ctl00_ContentPlaceHolder1_idTipoAgendamentoVincularEntidadeHiddenField").value = '';


      //Chama Servidor e retornar com Dados para Abrir Modal
      PageMethods.ConsultaEntidadeAdd(EntidadeConsultar, AddComEntidade);
    }

});

