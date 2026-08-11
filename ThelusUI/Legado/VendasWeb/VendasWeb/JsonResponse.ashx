<%@ WebHandler Language="C#" Class="JsonResponse" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;

public class JsonResponse : IHttpHandler, IRequiresSessionState
{
    /*****************************************************************************************************************************
        Json Utilizado no Calendario,
        ele faz a carga de todas os Agendamentos feitos e retorna no padrao para abrir no calendario
     
     ******************************************************************************************************************************/

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        DateTime start = Convert.ToDateTime(context.Request.QueryString["start"]); //Data Inicial do Evento
        DateTime end = Convert.ToDateTime(context.Request.QueryString["end"]); //Data Final do Evento


        List<int> idList = new List<int>(); //Lista de IDs dos Eventos
        List<ImproperCalendarEvent> tasksList = new List<ImproperCalendarEvent>(); //Detalhamento do Evento

        //Generate JSON serializable events

        foreach (CalendarEvent cevent in EventDAO.getEvents(start, end))
        {

            /**
             * Chama o Metodo getEvents da EventDAO que retorna um DataTable de Eventos
             */

            //Adiciona os Eventos na Lista Criada Anteriormente
            tasksList.Add(new ImproperCalendarEvent
            {
                id = cevent.id,
                title = cevent.title,

                start = String.Format("{0:s}", cevent.start),
                end = String.Format("{0:s}", cevent.end),

                description = cevent.description,
                allDay = cevent.allDay,
                className = cevent.className,


                IdTipoAgendamento = cevent.IdTipoAgendamento,
                CondicaoVisita = cevent.CondicaoVisita,
                idLembreteUm = cevent.idLembreteUm,
                idLembreteDois = cevent.idLembreteDois,
                EntCod = cevent.EntCod,
                EntNome = cevent.EntNome,
                EntCpfCgc = cevent.EntCpfCgc,
                Endereco = cevent.Endereco,
                ContatoNome = cevent.ContatoNome,
                ContatoTelefone = cevent.ContatoTelefone,
                ContatoEmail = cevent.ContatoEmail,
                LinhaProdutoQuantidadeStretch = cevent.LinhaProdutoQuantidadeStretch.ToString("n2"),
                LinhaProdutoQuantidadeFitaPP = cevent.LinhaProdutoQuantidadeFitaPP.ToString("n2"),
                LinhaProdutoQuantidadeFitaImpressa = cevent.LinhaProdutoQuantidadeFitaImpressa.ToString("n2"),

                DataInicio = cevent.DataInicio.ToString("yyyy-MM-dd"),
                HoraInicio = cevent.DataInicio.ToString("HH:mm"),
                DataFinal = cevent.DataFinal.ToString("yyyy-MM-dd"),
                HoraFinal = cevent.DataFinal.ToString("HH:mm"),
                VinculaEntidade = Convert.ToString(cevent.VinculaEntidade),

                ComRepresentante = cevent.ComRepresentante,
                MeioTransporte = cevent.MeioTransporte,
                Km = cevent.Km,
                ValorEstimadoViagem = cevent.ValorEstimadoViagem,
                StatEntComercial = cevent.StatEntComercial,
                EntStatDescr = cevent.EntStatDescr,
                DataUltimaVisita = cevent.DataUltimaVisita.ToString("dd/MM/yyyy"),
                EstimativaVendaStretch= cevent.EstimativaVendaStretch,
                EstimativaVendaFitaPP = cevent.EstimativaVendaFitaPP,
                EstimativaVendaFitaImpressa = cevent.EstimativaVendaFitaImpressa,
                NFHoraSaida = cevent.NFHoraSaida.ToString("dd/MM/yyyy"),
                NFValTotNota = cevent.NFValTotNota,
                
                ClasseCliente = cevent.ClasseCliente,
                TotalVendaAnual = cevent.TotalVendaAnual,
                
                ItensNF = cevent.ItensNF



            }
                );


            idList.Add(cevent.id);
        }

        context.Session["idList"] = idList;

        //Serialize events to string
        System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string sJSON = oSerializer.Serialize(tasksList);

        //Write JSON to response object
        context.Response.Write(sJSON);
    }

    public bool IsReusable
    {
        get { return false; }
    }

    // FullCalendar 1.x Methods *******

    /// <summary>
    /// Converts a UTC transformed timestamp into a local datetime
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    //private DateTime ConvertFromTimeStamp(long timestamp) {
    //    long ticks = (timestamp * 10000000) + 621355968000000000;
    //    return new DateTime(ticks, DateTimeKind.Utc).ToLocalTime();
    //}

    //private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    //private static long ConvertToTimestamp(DateTime value) {
    //    TimeSpan elapsedTime = value - Epoch;
    //    return (long)elapsedTime.TotalSeconds;
    //}

    //private String convertCalendarEventIntoString(CalendarEvent cevent) {
    //    String allDay = "true";

    //    if (ConvertToTimestamp(cevent.start).ToString().Equals(ConvertToTimestamp(cevent.end).ToString())) {
    //        if (cevent.start.Hour == 0 && cevent.start.Minute == 0 && cevent.start.Second == 0) {
    //            allDay = "true";
    //        }
    //        else {
    //            allDay = "false";
    //        }
    //    }
    //    else {
    //        if (cevent.start.Hour == 0 && cevent.start.Minute == 0 && cevent.start.Second == 0
    //            && cevent.end.Hour == 0 && cevent.end.Minute == 0 && cevent.end.Second == 0) {
    //            allDay = "true";
    //        }
    //        else {
    //            allDay = "false";
    //        }
    //    }
    //    return "{" +
    //              "id: '" + cevent.id + "'," +
    //              "title: '" + HttpContext.Current.Server.HtmlEncode(cevent.title) + "'," +
    //              "start:  " + ConvertToTimestamp(cevent.start).ToString() + "," +
    //              "end: " + ConvertToTimestamp(cevent.end).ToString() + "," +
    //              "allDay:" + allDay + "," +
    //              "description: '" + HttpContext.Current.Server.HtmlEncode(cevent.description) + "'" +
    //              "},";
    //}
}