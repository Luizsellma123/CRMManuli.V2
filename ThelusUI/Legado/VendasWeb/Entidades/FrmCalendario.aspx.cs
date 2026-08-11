using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data;
using VendasWeb.GerencialVendas;


namespace VendasWeb.Entidades
{
    public partial class FrmCalendario : System.Web.UI.Page
    {

        CalendarEvent ObjCalendarEvent = new CalendarEvent();
        List<string> ListidTipoAgendamento = new List<string>();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                #region Tipos Agendamentos
                DataTable outputTable = new DataTable();
                outputTable = ObjCalendarEvent.Consulta_Tipos_Agendamentos();
                if (outputTable.Rows.Count > 0)
                {
                    TiposAgendamentoLiteral.Text = "";
                    ListidTipoAgendamento = new List<string>();

                    foreach (DataRow row in outputTable.Rows)
                    {
                        TiposAgendamentoLiteral.Text += " <div class=\"fc-event\" data-class=\"" + row["Cor"].ToString() + "\" "
                                                     + " onClick=\"return TipoAgendamentos('" + row["VinculaEntidade"].ToString() + "','" + row["idTipoAgendamento"].ToString() + "')\"> "
                                                     + row["DescricaoTipoAgendamento"].ToString()
                                                     + " </div>";



                       
                    }

                    
                }



                TipoAgendamentoCheckBoxList.DataSource = outputTable;
                TipoAgendamentoCheckBoxList.DataTextField = "TipoAgendamentoFormatado";
                TipoAgendamentoCheckBoxList.DataValueField = "idTipoAgendamento";
                
                TipoAgendamentoCheckBoxList.DataBind();
                
                for (int i = 0; i < TipoAgendamentoCheckBoxList.Items.Count; i++)
                {

                    TipoAgendamentoCheckBoxList.Items[i].Selected = true;
                   
                    
                    //Cria Uma Lista Com os Tipos Agendamento para Filtro
                    ListidTipoAgendamento.Add(TipoAgendamentoCheckBoxList.Items[i].Value);
                    Session["ListidTipoAgendamento"] = ListidTipoAgendamento;
                }


                #endregion


                #region Usuarios
                ObjCalendarEvent.UsuCod = Session["usuario"].ToString();
                UsuarioDropDownList.DataSource = ObjCalendarEvent.Consulta_agenda_usuario_UsuCod();
                UsuarioDropDownList.DataTextField = "UsuCod";
                UsuarioDropDownList.DataValueField = "UsuCod";
                UsuarioDropDownList.DataBind();

                Session["usuarioAgendamento"] = UsuarioDropDownList.Value;

                #endregion

                
                if(Session["ObjCalendarEvent"] != null)
                {

                    ObjCalendarEvent = (CalendarEvent)Session["ObjCalendarEvent"];
                    idTipoAgendamentoVincularEntidadeHiddenField.Value = ObjCalendarEvent.IdTipoAgendamento.ToString();
                    EntCodHiddenField.Value = ObjCalendarEvent.EntCod;
                    Session["ObjCalendarEvent"] = null;

                   
                }




            }
            else
            {
                if (idTipoAgendamentoVincularEntidadeHiddenField.Value != "")
                {

                    ObjCalendarEvent.IdTipoAgendamento = Convert.ToInt32(idTipoAgendamentoVincularEntidadeHiddenField.Value);//Pega o Id do Tipo de Evento
                    idTipoAgendamentoVincularEntidadeHiddenField.Value = "";//Limpa Campo Temporario
                    Session["ObjCalendarEvent"] = ObjCalendarEvent; //Carrega Session
                    Response.Redirect("FrmCalendarioEntidade.aspx?indmnu=3"); // Chama Tela para Consultar a Entidade que deseja fazer Agendamento
                }
            }

        }




        #region WebServic Calendario

        //this method only updates title and description
        //this is called when a event is clicked on the calendar
        [System.Web.Services.WebMethod(true)]
        public static ImproperCalendarEvent UpdateEvent(ImproperCalendarEvent improperEvent)
        {
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
            if (idList != null && idList.Contains(improperEvent.id))
            {

                // FullCalendar 2.x
                CalendarEvent cevent = new CalendarEvent()
                {
                    id = improperEvent.id,
                    IDAgendamento = improperEvent.id,
                    title = improperEvent.title,
                    description = improperEvent.description,
                    start = Convert.ToDateTime(improperEvent.start),//.ToUniversalTime(),
                    end = Convert.ToDateTime(improperEvent.end),//.ToUniversalTime(),
                    //allDay = improperEvent.allDay
                    DataInicio = Convert.ToDateTime(improperEvent.start),
                    DataFinal = Convert.ToDateTime(improperEvent.end),
                    UsuCod = HttpContext.Current.Session["usuarioAgendamento"].ToString(),//Usuario selecionado no Combo
                    UsuCodGestor = HttpContext.Current.Session["usuario"].ToString(),//Usuario Logado
                    CondicaoVisita = improperEvent.CondicaoVisita,
                    idLembreteUm = improperEvent.idLembreteUm,
                    //idLembreteDois 
                    DescricaoCompromisso = improperEvent.DescricaoCompromisso,
                    LinhaProdutoQuantidadeStretch = Convert.ToDecimal(improperEvent.LinhaProdutoQuantidadeStretch),
                    LinhaProdutoQuantidadeFitaPP = Convert.ToDecimal(improperEvent.LinhaProdutoQuantidadeFitaPP),
                    LinhaProdutoQuantidadeFitaImpressa = Convert.ToDecimal(improperEvent.LinhaProdutoQuantidadeFitaImpressa),

                    EstimativaVendaFitaImpressa = Convert.ToInt32(improperEvent.EstimativaVendaFitaImpressa),
                    EstimativaVendaFitaPP = Convert.ToInt32(improperEvent.EstimativaVendaFitaPP),
                    EstimativaVendaStretch = Convert.ToInt32(improperEvent.EstimativaVendaStretch),
                    ComRepresentante = improperEvent.ComRepresentante,
                    MeioTransporte = improperEvent.MeioTransporte,
                    Km = Convert.ToDecimal(improperEvent.Km),
                    ValorEstimadoViagem = Convert.ToDecimal(improperEvent.ValorEstimadoViagem),
                    ClasseCliente = improperEvent.ClasseCliente
                };


                improperEvent =  EventDAO.updateEvent(cevent);

               

            }

            return improperEvent;
        }

        //called when delete button is pressed
        [System.Web.Services.WebMethod(true)]
        public static String deleteEvent(ImproperCalendarEvent improperEvent)
        {
            //idList is stored in Session by JsonResponse.ashx for security reasons
            //whenever any event is update or deleted, the event id is checked
            //whether it is present in the idList, if it is not present in the idList
            //then it may be a malicious user trying to delete someone elses events
            //thus this checking prevents misuse
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
            if (idList != null && idList.Contains(improperEvent.id))
            {



                // FullCalendar 2.x
                CalendarEvent cevent = new CalendarEvent()
                {
                    id = improperEvent.id,
                    IDAgendamento = improperEvent.id,
                    UsuCod = HttpContext.Current.Session["usuarioAgendamento"].ToString(),//Usuario selecionado no combo
                    UsuCodGestor = HttpContext.Current.Session["usuario"].ToString(),//Usuario Logado
                    
                };


                EventDAO.deleteEvent(cevent);



                return "deleted event with id:" + improperEvent.id;
            }

            return "unable to delete event with id: " + improperEvent.id;
        }

        //called when Add button is clicked
        //this is called when a mouse is clicked on open space of any day or dragged 
        //over mutliple days
        [System.Web.Services.WebMethod(true)]
        public static ImproperCalendarEvent addEvent(ImproperCalendarEvent improperEvent)
        {
            // FullCalendar 1.x
            //CalendarEvent cevent = new CalendarEvent()
            //{
            //    title = improperEvent.title,
            //    description = improperEvent.description,
            //    start = DateTime.ParseExact(improperEvent.start, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
            //    end = DateTime.ParseExact(improperEvent.end, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)
            //};

            // FullCalendar 2.x
            CalendarEvent cevent = new CalendarEvent()
            {
                title = improperEvent.title,
                description = improperEvent.description,
                start = Convert.ToDateTime(improperEvent.start),//.ToUniversalTime(),
                end = Convert.ToDateTime(improperEvent.end),//.ToUniversalTime(),
                //allDay = improperEvent.allDay
                DataInicio = Convert.ToDateTime(improperEvent.start),
                DataFinal = Convert.ToDateTime(improperEvent.end),
                UsuCod = HttpContext.Current.Session["usuarioAgendamento"].ToString(),//Usuario Selecionado no Combo
                UsuCodGestor = HttpContext.Current.Session["usuario"].ToString(),//Usuario Logado
                IdTipoAgendamento = improperEvent.IdTipoAgendamento,
                CondicaoVisita = improperEvent.CondicaoVisita,
                idLembreteUm = improperEvent.idLembreteUm,
                //idLembreteDois 
                DescricaoCompromisso = improperEvent.DescricaoCompromisso,
                EntCod = improperEvent.EntCod,
                LinhaProdutoQuantidadeStretch = Convert.ToDecimal(improperEvent.LinhaProdutoQuantidadeStretch),
                LinhaProdutoQuantidadeFitaPP = Convert.ToDecimal(improperEvent.LinhaProdutoQuantidadeFitaPP),
                LinhaProdutoQuantidadeFitaImpressa = Convert.ToDecimal(improperEvent.LinhaProdutoQuantidadeFitaImpressa),


                EstimativaVendaFitaImpressa = Convert.ToInt32(improperEvent.EstimativaVendaFitaImpressa),
                EstimativaVendaFitaPP = Convert.ToInt32(improperEvent.EstimativaVendaFitaPP),
                EstimativaVendaStretch = Convert.ToInt32(improperEvent.EstimativaVendaStretch),
                ComRepresentante = improperEvent.ComRepresentante,
                MeioTransporte = improperEvent.MeioTransporte,
                Km = Convert.ToDecimal(improperEvent.Km),
                ValorEstimadoViagem = Convert.ToDecimal(improperEvent.ValorEstimadoViagem),
                ClasseCliente = improperEvent.ClasseCliente
                
            };

            
            //Salvar no Banco
            improperEvent = EventDAO.addEvent(cevent);


            //Esse ID eh importante para localizar no UPDATE
            int key = improperEvent.id;

            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];

            if (idList != null)
            {
                idList.Add(key);
            }


            //Retorna Objeto todo para carregamento dos dados na tela
            return improperEvent; //return the primary key of the added cevent object

        }

        //Evento Chamado para Buscar dados Adicionar da Entidade a Ser vinculada
        [System.Web.Services.WebMethod(true)]
        public static ImproperCalendarEvent ConsultaEntidadeAdd(ImproperCalendarEvent improperEvent)
        {
           
            // FullCalendar 2.x
            CalendarEvent cevent = new CalendarEvent()
            {
               
                EntCod = improperEvent.EntCod,
                IdTipoAgendamento = improperEvent.IdTipoAgendamento 
            };

            
            //Consulta no Banco no Banco
            improperEvent = EventDAO.ConsultaEntidadeAdd(cevent);


           
            //Retorna Objeto todo para carregamento dos dados na tela
            return improperEvent; 

        }



        
        #endregion

       

        protected void FiltrarLinkButton_Click(object sender, EventArgs e)
        {
            Session["usuarioAgendamento"] = UsuarioDropDownList.Value;

            ListidTipoAgendamento = new List<string>();
            
            for (int i = 0; i < TipoAgendamentoCheckBoxList.Items.Count; i++)
            {

                if (TipoAgendamentoCheckBoxList.Items[i].Selected == true)
                {
                    //Cria Uma Lista Com os Tipos Agendamento para Filtro
                    ListidTipoAgendamento.Add(TipoAgendamentoCheckBoxList.Items[i].Value);
                    Session["ListidTipoAgendamento"] = ListidTipoAgendamento;
                }
            }

        }

        protected void RelatorioLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../telasRelatorio/FrmRelCalendario.aspx?indmnu=3");
        }

       
        
       

      

    }
}