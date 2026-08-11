using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VendasWeb.GerencialVendas;

/// <summary>
/// EventDAO class is the main class which interacts with the database. SQL Server express edition
/// has been used.
/// the event information is stored in a table named 'event' in the database.
///
/// Here is the table format:
/// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
/// event_id is the primary key
/// </summary>
public class EventDAO : clsConexao
{
    //change the connection string as per your database connection.
    //private static string connectionString = ConfigurationManager.AppSettings["DBConnString"];

    //this method retrieves all events within range start-end
    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {

        CalendarEvent ObjCalendarEvent = new CalendarEvent();
        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(strConec);
        string IDAgendamentoFiltro = "";


        DataTable EventsTable = new DataTable();

        //Busca dados da Agenda
        ObjCalendarEvent.UsuCod = HttpContext.Current.Session["usuarioAgendamento"].ToString(); 
        ObjCalendarEvent.DataInicio = start;
        ObjCalendarEvent.DataFinal = end;

        if (HttpContext.Current.Session["ListidTipoAgendamento"] != "")
        {
            List<string> ListidTipoAgendamento = (List<string>)HttpContext.Current.Session["ListidTipoAgendamento"];

            for (int i = 0; i < ListidTipoAgendamento.Count; i++)
            {
                IDAgendamentoFiltro += ListidTipoAgendamento[i].ToString() + ",";
            }


        }

        ObjCalendarEvent.IDAgendamentoFiltro = IDAgendamentoFiltro;

        EventsTable = ObjCalendarEvent.Consulta_Agendamentos_UsuCod_Data();


        if (EventsTable.Rows.Count > 0)
        {
            foreach (DataRow row in EventsTable.Rows)
            {

                CalendarEvent cevent = new CalendarEvent();

                #region Valores Base // Esses Valores são a Base para o Calendario
                cevent.id = (int)row["IDAgendamento"];// reader["event_id"];
                cevent.title = (string)row["DescricaoTipoAgendamento"];// reader["title"];
                cevent.description = (string)row["DescricaoCompromisso"];// reader["description"];
                cevent.start = Convert.ToDateTime(row["DataInicio"]);// reader["event_start"];
                cevent.end = Convert.ToDateTime(row["DataFinal"]);// reader["event_end"];
                cevent.allDay = false;// reader["all_day"];
                cevent.className = (string)row["Cor"];
                #endregion

                cevent.IDAgendamento = (int)row["IDAgendamento"];
                cevent.IdTipoAgendamento = Convert.ToInt32(row["IdTipoAgendamento"]);
                cevent.CondicaoVisita = row["CondicaoVisita"].ToString();
                cevent.idLembreteUm = Convert.ToDecimal(row["idLembreteUm"]);
                cevent.idLembreteDois = Convert.ToDecimal(row["idLembreteDois"]);
                cevent.EntCod = row["EntCod"].ToString();
                cevent.EntNome = row["EntNome"].ToString();
                cevent.EntCpfCgc = row["EntCpfCgc"].ToString();
                cevent.Endereco = row["Endereco"].ToString();
                cevent.ContatoNome = row["ContatoNome"].ToString();
                cevent.ContatoTelefone = row["ContatoTelefone"].ToString();
                cevent.ContatoEmail = row["ContatoEmail"].ToString();
                cevent.LinhaProdutoQuantidadeStretch = Convert.ToDecimal(row["LinhaProdutoQuantidadeStretch"]);
                cevent.LinhaProdutoQuantidadeFitaPP = Convert.ToDecimal(row["LinhaProdutoQuantidadeFitaPP"]);
                cevent.LinhaProdutoQuantidadeFitaImpressa = Convert.ToDecimal(row["LinhaProdutoQuantidadeFitaImpressa"]);
                cevent.DataInicio = Convert.ToDateTime(row["DataInicio"]);
                cevent.DataFinal = Convert.ToDateTime(row["DataFinal"]);
                cevent.VinculaEntidade = Convert.ToBoolean(row["VinculaEntidade"]);

                cevent.ComRepresentante = row["ComRepresentante"].ToString();
                cevent.MeioTransporte = row["MeioTransporte"].ToString();
                cevent.Km = Convert.ToDecimal(row["Km"]);
                cevent.ValorEstimadoViagem = Convert.ToDecimal(row["ValorEstimadoViagem"]);
                cevent.StatEntComercial = row["StatEntComercial"].ToString();
                cevent.EntStatDescr = row["EntStatDescr"].ToString();

                cevent.DataUltimaVisita = Convert.ToDateTime(row["DataUltimaVisita"]);
                
                cevent.EstimativaVendaStretch = Convert.ToInt32(row["EstimativaVendaStretch"]);
                cevent.EstimativaVendaFitaPP = Convert.ToInt32(row["EstimativaVendaFitaPP"]);
                cevent.EstimativaVendaFitaImpressa = Convert.ToInt32(row["EstimativaVendaFitaImpressa"]);
                cevent.NFHoraSaida = Convert.ToDateTime(row["NFHoraSaida"]);
                cevent.NFValTotNota = Convert.ToDecimal(row["NFValTotNota"]);
                
                cevent.ClasseCliente = row["ClasseCliente"].ToString();
                cevent.TotalVendaAnual = Convert.ToDecimal(row["TotalVendaAnual"]);
                cevent.ItensNF = row["ItensNF"].ToString();



                events.Add(cevent);
            }
        }




        return events;
        //side note: if you want to show events only related to particular users,
        //if user id of that user is stored in session as Session["userid"]
        //the event table also contains an extra field named 'user_id' to mark the event for that particular user
        //then you can modify the SQL as:
        //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
        //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
    }

    //this method updates the event title and description
    public static ImproperCalendarEvent updateEvent(CalendarEvent cevent)
    {

        ImproperCalendarEvent ObjImproperCalendarEvent = new ImproperCalendarEvent();

        //add event to the database and return the primary key of the added event row

        int Retorno = 0;

        DataTable outputTable = new DataTable();

        try
        {

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_crm_altera_agendamento", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDAgendamento", SqlDbType.Int,0, "IDAgendamento"));
                dbCommand.Parameters.Add(new SqlParameter("@DataInicio", SqlDbType.DateTime, 250, "DataInicio"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 250, "DataFinal"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 250, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCodGestor", SqlDbType.VarChar, 250, "UsuCodGestor"));
                dbCommand.Parameters.Add(new SqlParameter("@CondicaoVisita", SqlDbType.VarChar, 250, "CondicaoVisita"));
                dbCommand.Parameters.Add(new SqlParameter("@idLembreteUm", SqlDbType.Decimal, 250, "idLembreteUm"));
                dbCommand.Parameters.Add(new SqlParameter("@idLembreteDois", SqlDbType.Decimal, 250, "idLembreteDois"));
                dbCommand.Parameters.Add(new SqlParameter("@DescricaoCompromisso", SqlDbType.VarChar, 99991, "DescricaoCompromisso"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProdutoQuantidadeStretch", SqlDbType.Decimal, 0, "LinhaProdutoQuantidadeStretch"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProdutoQuantidadeFitaPP", SqlDbType.Decimal, 0, "LinhaProdutoQuantidadeFitaPP"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProdutoQuantidadeFitaImpressa", SqlDbType.Decimal, 0, "LinhaProdutoQuantidadeFitaImpressa"));
                dbCommand.Parameters.Add(new SqlParameter("@ComRepresentante", SqlDbType.VarChar, 50, "ComRepresentante"));
                dbCommand.Parameters.Add(new SqlParameter("@MeioTransporte", SqlDbType.VarChar, 250, "MeioTransporte"));
                dbCommand.Parameters.Add(new SqlParameter("@Km", SqlDbType.Decimal, 0, "Km"));
                dbCommand.Parameters.Add(new SqlParameter("@ValorEstimadoViagem", SqlDbType.Decimal, 0, "ValorEstimadoViagem"));
                dbCommand.Parameters.Add(new SqlParameter("@EstimativaVendaStretch", SqlDbType.Int, 0, "EstimativaVendaStretch"));
                dbCommand.Parameters.Add(new SqlParameter("@EstimativaVendaFitaPP", SqlDbType.Int, 0, "EstimativaVendaFitaPP"));
                dbCommand.Parameters.Add(new SqlParameter("@EstimativaVendaFitaImpressa", SqlDbType.Int, 0, "EstimativaVendaFitaImpressa"));
                dbCommand.Parameters.Add(new SqlParameter("@ClasseCliente", SqlDbType.VarChar, 50, "ClasseCliente"));


                dbCommand.Parameters["@IDAgendamento"].Value = cevent.IDAgendamento;
                dbCommand.Parameters["@DataInicio"].Value = cevent.DataInicio;
                dbCommand.Parameters["@DataFinal"].Value = cevent.DataFinal;
                dbCommand.Parameters["@UsuCod"].Value = cevent.UsuCod;
                dbCommand.Parameters["@UsuCodGestor"].Value = cevent.UsuCodGestor;
                dbCommand.Parameters["@CondicaoVisita"].Value = cevent.CondicaoVisita;
                dbCommand.Parameters["@idLembreteUm"].Value = cevent.idLembreteUm;
                dbCommand.Parameters["@idLembreteDois"].Value = cevent.idLembreteDois;
                dbCommand.Parameters["@DescricaoCompromisso"].Value = cevent.DescricaoCompromisso;
                dbCommand.Parameters["@LinhaProdutoQuantidadeStretch"].Value = cevent.LinhaProdutoQuantidadeStretch;
                dbCommand.Parameters["@LinhaProdutoQuantidadeFitaPP"].Value = cevent.LinhaProdutoQuantidadeFitaPP;
                dbCommand.Parameters["@LinhaProdutoQuantidadeFitaImpressa"].Value = cevent.LinhaProdutoQuantidadeFitaImpressa;

                dbCommand.Parameters["@ComRepresentante"].Value = cevent.ComRepresentante;
                dbCommand.Parameters["@MeioTransporte"].Value = cevent.MeioTransporte;
                dbCommand.Parameters["@Km"].Value = cevent.Km;
                dbCommand.Parameters["@ValorEstimadoViagem"].Value = cevent.ValorEstimadoViagem;
                dbCommand.Parameters["@EstimativaVendaStretch"].Value = cevent.EstimativaVendaStretch;
                dbCommand.Parameters["@EstimativaVendaFitaPP"].Value = cevent.EstimativaVendaFitaPP;
                dbCommand.Parameters["@EstimativaVendaFitaImpressa"].Value = cevent.EstimativaVendaFitaImpressa;
                dbCommand.Parameters["@ClasseCliente"].Value = cevent.ClasseCliente;



                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
                dataReader.Close();


                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {


                        #region Valores Base // Esses Valores são a Base para o Calendario
                        ObjImproperCalendarEvent.id = (int)row["IDAgendamento"];// reader["event_id"];
                        ObjImproperCalendarEvent.title = (string)row["DescricaoTipoAgendamento"];// reader["title"];
                        ObjImproperCalendarEvent.description = (string)row["DescricaoCompromisso"];// reader["description"];
                        ObjImproperCalendarEvent.start = String.Format("{0:s}", Convert.ToDateTime(row["DataInicio"]));// reader["event_start"];
                        ObjImproperCalendarEvent.end = String.Format("{0:s}", Convert.ToDateTime(row["DataFinal"]));// reader["event_end"];
                        ObjImproperCalendarEvent.allDay = false;// reader["all_day"];
                        ObjImproperCalendarEvent.className = (string)row["Cor"];
                        #endregion

                        ObjImproperCalendarEvent.IDAgendamento = (int)row["IDAgendamento"];
                        ObjImproperCalendarEvent.IdTipoAgendamento = Convert.ToInt32(row["IdTipoAgendamento"]);
                        ObjImproperCalendarEvent.CondicaoVisita = row["CondicaoVisita"].ToString();
                        ObjImproperCalendarEvent.idLembreteUm = Convert.ToDecimal(row["idLembreteUm"]);
                        ObjImproperCalendarEvent.idLembreteDois = Convert.ToDecimal(row["idLembreteDois"]);
                        ObjImproperCalendarEvent.EntCod = row["EntCod"].ToString();
                        ObjImproperCalendarEvent.EntNome = row["EntNome"].ToString();
                        ObjImproperCalendarEvent.EntCpfCgc = row["EntCpfCgc"].ToString();
                        ObjImproperCalendarEvent.Endereco = row["Endereco"].ToString();
                        ObjImproperCalendarEvent.ContatoNome = row["ContatoNome"].ToString();
                        ObjImproperCalendarEvent.ContatoTelefone = row["ContatoTelefone"].ToString();
                        ObjImproperCalendarEvent.ContatoEmail = row["ContatoEmail"].ToString();
                        ObjImproperCalendarEvent.LinhaProdutoQuantidadeStretch = Convert.ToDecimal(row["LinhaProdutoQuantidadeStretch"]).ToString("n2");
                        ObjImproperCalendarEvent.LinhaProdutoQuantidadeFitaPP = Convert.ToDecimal(row["LinhaProdutoQuantidadeFitaPP"]).ToString("n2");
                        ObjImproperCalendarEvent.LinhaProdutoQuantidadeFitaImpressa = Convert.ToDecimal(row["LinhaProdutoQuantidadeFitaImpressa"]).ToString("n2");
                        ObjImproperCalendarEvent.DataInicio = Convert.ToDateTime(row["DataInicio"]).ToString("yyyy-MM-dd");
                        ObjImproperCalendarEvent.DataFinal = Convert.ToDateTime(row["DataFinal"]).ToString("yyyy-MM-dd");
                        ObjImproperCalendarEvent.HoraInicio = Convert.ToDateTime(row["DataInicio"]).ToString("HH:mm");
                        ObjImproperCalendarEvent.HoraFinal = Convert.ToDateTime(row["DataFinal"]).ToString("HH:mm");


                        ObjImproperCalendarEvent.ComRepresentante = row["ComRepresentante"].ToString();
                        ObjImproperCalendarEvent.MeioTransporte = row["MeioTransporte"].ToString();
                        ObjImproperCalendarEvent.Km = Convert.ToDecimal(row["Km"]);
                        ObjImproperCalendarEvent.ValorEstimadoViagem = Convert.ToDecimal(row["ValorEstimadoViagem"]);
                        ObjImproperCalendarEvent.StatEntComercial = row["StatEntComercial"].ToString();
                        ObjImproperCalendarEvent.EntStatDescr = row["EntStatDescr"].ToString();
                        ObjImproperCalendarEvent.DataUltimaVisita = Convert.ToDateTime(row["DataUltimaVisita"]).ToString("dd/MM/yyyy"); 
                        ObjImproperCalendarEvent.EstimativaVendaStretch = Convert.ToInt32(row["EstimativaVendaStretch"]);
                        ObjImproperCalendarEvent.EstimativaVendaFitaPP = Convert.ToInt32(row["EstimativaVendaFitaPP"]);
                        ObjImproperCalendarEvent.EstimativaVendaFitaImpressa = Convert.ToInt32(row["EstimativaVendaFitaImpressa"]);
                        ObjImproperCalendarEvent.NFHoraSaida = Convert.ToDateTime(row["NFHoraSaida"]).ToString("dd/MM/yyyy"); 
                        ObjImproperCalendarEvent.NFValTotNota = Convert.ToDecimal(row["NFValTotNota"]);
                        ObjImproperCalendarEvent.ClasseCliente = row["ClasseCliente"].ToString();
                        ObjImproperCalendarEvent.TotalVendaAnual = Convert.ToDecimal(row["TotalVendaAnual"]);
                        ObjImproperCalendarEvent.ItensNF = row["ItensNF"].ToString();



                    }
                }
                else
                {
                    Retorno = -1;
                }


            }
        }
        catch
        {

            Retorno = -2;
        }
        
        
        return ObjImproperCalendarEvent;

    }

    //this mehtod deletes event with the id passed in.
    public static void deleteEvent(CalendarEvent cevent)
    {


        //add event to the database and return the primary key of the added event row

        int Retorno = 0;

        DataTable outputTable = new DataTable();

        try
        {

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_crm_deleta_Agendamento", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDAgendamento", SqlDbType.Int, 0, "IDAgendamento"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 250, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCodGestor", SqlDbType.VarChar, 250, "UsuCodGestor"));
                
                dbCommand.Parameters["@IDAgendamento"].Value = cevent.IDAgendamento;
                dbCommand.Parameters["@UsuCod"].Value = cevent.UsuCod;
                dbCommand.Parameters["@UsuCodGestor"].Value = cevent.UsuCodGestor;
                
                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
                dataReader.Close();

            }
        }
        catch
        {

            Retorno = -1;
        }

      
    }

    //this method adds events to the database
    public static ImproperCalendarEvent addEvent(CalendarEvent cevent)
    {


        ImproperCalendarEvent ObjImproperCalendarEvent = new ImproperCalendarEvent();

        //add event to the database and return the primary key of the added event row

        int Retorno = 0;

        DataTable outputTable = new DataTable();

        try
        {

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_crm_insere_agendamento", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@DataInicio", SqlDbType.DateTime, 250, "DataInicio"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 250, "DataFinal"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 250, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCodGestor", SqlDbType.VarChar, 250, "UsuCodGestor"));
                dbCommand.Parameters.Add(new SqlParameter("@IdTipoAgendamento", SqlDbType.Int, 0, "IdTipoAgendamento"));
                dbCommand.Parameters.Add(new SqlParameter("@CondicaoVisita", SqlDbType.VarChar, 250, "CondicaoVisita"));
                dbCommand.Parameters.Add(new SqlParameter("@idLembreteUm", SqlDbType.Decimal, 250, "idLembreteUm"));
                dbCommand.Parameters.Add(new SqlParameter("@idLembreteDois", SqlDbType.Decimal, 250, "idLembreteDois"));
                dbCommand.Parameters.Add(new SqlParameter("@DescricaoCompromisso", SqlDbType.VarChar, 99991, "DescricaoCompromisso"));
                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 250, "EntCod"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProdutoQuantidadeStretch", SqlDbType.Decimal, 0, "LinhaProdutoQuantidadeStretch"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProdutoQuantidadeFitaPP", SqlDbType.Decimal, 0, "LinhaProdutoQuantidadeFitaPP"));
                dbCommand.Parameters.Add(new SqlParameter("@LinhaProdutoQuantidadeFitaImpressa", SqlDbType.Decimal, 0, "LinhaProdutoQuantidadeFitaImpressa"));

                dbCommand.Parameters.Add(new SqlParameter("@ComRepresentante", SqlDbType.VarChar, 50, "ComRepresentante"));
                dbCommand.Parameters.Add(new SqlParameter("@MeioTransporte", SqlDbType.VarChar, 250, "MeioTransporte"));
                dbCommand.Parameters.Add(new SqlParameter("@Km", SqlDbType.Decimal, 0, "Km"));
                dbCommand.Parameters.Add(new SqlParameter("@ValorEstimadoViagem", SqlDbType.Decimal, 0, "ValorEstimadoViagem"));
                dbCommand.Parameters.Add(new SqlParameter("@EstimativaVendaStretch", SqlDbType.Int, 0, "EstimativaVendaStretch"));
                dbCommand.Parameters.Add(new SqlParameter("@EstimativaVendaFitaPP", SqlDbType.Int, 0, "EstimativaVendaFitaPP"));
                dbCommand.Parameters.Add(new SqlParameter("@EstimativaVendaFitaImpressa", SqlDbType.Int, 0, "EstimativaVendaFitaImpressa"));
                dbCommand.Parameters.Add(new SqlParameter("@ClasseCliente", SqlDbType.VarChar, 50, "ClasseCliente"));


                dbCommand.Parameters["@DataInicio"].Value = cevent.DataInicio;
                dbCommand.Parameters["@DataFinal"].Value = cevent.DataFinal;
                dbCommand.Parameters["@UsuCod"].Value = cevent.UsuCod;
                dbCommand.Parameters["@UsuCodGestor"].Value = cevent.UsuCodGestor;
                dbCommand.Parameters["@IdTipoAgendamento"].Value = cevent.IdTipoAgendamento;
                dbCommand.Parameters["@CondicaoVisita"].Value = cevent.CondicaoVisita;
                dbCommand.Parameters["@idLembreteUm"].Value = cevent.idLembreteUm;
                dbCommand.Parameters["@idLembreteDois"].Value = cevent.idLembreteDois;
                dbCommand.Parameters["@DescricaoCompromisso"].Value = cevent.DescricaoCompromisso;
                dbCommand.Parameters["@EntCod"].Value = cevent.EntCod;
                dbCommand.Parameters["@LinhaProdutoQuantidadeStretch"].Value = cevent.LinhaProdutoQuantidadeStretch;
                dbCommand.Parameters["@LinhaProdutoQuantidadeFitaPP"].Value = cevent.LinhaProdutoQuantidadeFitaPP;
                dbCommand.Parameters["@LinhaProdutoQuantidadeFitaImpressa"].Value = cevent.LinhaProdutoQuantidadeFitaImpressa;

                dbCommand.Parameters["@ComRepresentante"].Value = cevent.ComRepresentante;
                dbCommand.Parameters["@MeioTransporte"].Value = cevent.MeioTransporte;
                dbCommand.Parameters["@Km"].Value = cevent.Km;
                dbCommand.Parameters["@ValorEstimadoViagem"].Value = cevent.ValorEstimadoViagem;
                dbCommand.Parameters["@EstimativaVendaStretch"].Value = cevent.EstimativaVendaStretch;
                dbCommand.Parameters["@EstimativaVendaFitaPP"].Value = cevent.EstimativaVendaFitaPP;
                dbCommand.Parameters["@EstimativaVendaFitaImpressa"].Value = cevent.EstimativaVendaFitaImpressa;
                dbCommand.Parameters["@ClasseCliente"].Value = cevent.ClasseCliente;

                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
                dataReader.Close();


                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {


                        #region Valores Base // Esses Valores são a Base para o Calendario
                        ObjImproperCalendarEvent.id = (int)row["IDAgendamento"];// reader["event_id"];
                        ObjImproperCalendarEvent.title = (string)row["DescricaoTipoAgendamento"];// reader["title"];
                        ObjImproperCalendarEvent.description = (string)row["DescricaoCompromisso"];// reader["description"];
                        ObjImproperCalendarEvent.start =String.Format("{0:s}", Convert.ToDateTime(row["DataInicio"]));// reader["event_start"];
                        ObjImproperCalendarEvent.end = String.Format("{0:s}",Convert.ToDateTime(row["DataFinal"]));// reader["event_end"];
                        ObjImproperCalendarEvent.allDay = false;// reader["all_day"];
                        ObjImproperCalendarEvent.className = (string)row["Cor"];
                        #endregion

                        ObjImproperCalendarEvent.IDAgendamento = (int)row["IDAgendamento"];
                        ObjImproperCalendarEvent.IdTipoAgendamento = Convert.ToInt32(row["IdTipoAgendamento"]);
                        ObjImproperCalendarEvent.CondicaoVisita = row["CondicaoVisita"].ToString();
                        ObjImproperCalendarEvent.idLembreteUm = Convert.ToDecimal(row["idLembreteUm"]);
                        ObjImproperCalendarEvent.idLembreteDois = Convert.ToDecimal(row["idLembreteDois"]);
                        ObjImproperCalendarEvent.EntCod = row["EntCod"].ToString();
                        ObjImproperCalendarEvent.EntNome = row["EntNome"].ToString();
                        ObjImproperCalendarEvent.EntCpfCgc = row["EntCpfCgc"].ToString();
                        ObjImproperCalendarEvent.Endereco = row["Endereco"].ToString();
                        ObjImproperCalendarEvent.ContatoNome = row["ContatoNome"].ToString();
                        ObjImproperCalendarEvent.ContatoTelefone = row["ContatoTelefone"].ToString();
                        ObjImproperCalendarEvent.ContatoEmail = row["ContatoEmail"].ToString();
                        ObjImproperCalendarEvent.LinhaProdutoQuantidadeStretch = Convert.ToDecimal(row["LinhaProdutoQuantidadeStretch"]).ToString("n2");
                        ObjImproperCalendarEvent.LinhaProdutoQuantidadeFitaPP = Convert.ToDecimal(row["LinhaProdutoQuantidadeFitaPP"]).ToString("n2");
                        ObjImproperCalendarEvent.LinhaProdutoQuantidadeFitaImpressa = Convert.ToDecimal(row["LinhaProdutoQuantidadeFitaImpressa"]).ToString("n2");
                        ObjImproperCalendarEvent.DataInicio = Convert.ToDateTime(row["DataInicio"]).ToString("yyyy-MM-dd");
                        ObjImproperCalendarEvent.DataFinal = Convert.ToDateTime(row["DataFinal"]).ToString("yyyy-MM-dd");
                        ObjImproperCalendarEvent.HoraInicio = Convert.ToDateTime(row["DataInicio"]).ToString("HH:mm");
                        ObjImproperCalendarEvent.HoraFinal = Convert.ToDateTime(row["DataFinal"]).ToString("HH:mm");
                        ObjImproperCalendarEvent.VinculaEntidade = Convert.ToString(row["VinculaEntidade"].ToString());

                        ObjImproperCalendarEvent.ComRepresentante = row["ComRepresentante"].ToString();
                        ObjImproperCalendarEvent.MeioTransporte = row["MeioTransporte"].ToString();
                        ObjImproperCalendarEvent.Km = Convert.ToDecimal(row["Km"]);
                        ObjImproperCalendarEvent.ValorEstimadoViagem = Convert.ToDecimal(row["ValorEstimadoViagem"]);
                        ObjImproperCalendarEvent.StatEntComercial = row["StatEntComercial"].ToString();
                        ObjImproperCalendarEvent.EntStatDescr = row["EntStatDescr"].ToString();
                        ObjImproperCalendarEvent.DataUltimaVisita = Convert.ToDateTime(row["DataUltimaVisita"]).ToString("dd/MM/yyyy"); 
                        ObjImproperCalendarEvent.EstimativaVendaStretch = Convert.ToInt32(row["EstimativaVendaStretch"]);
                        ObjImproperCalendarEvent.EstimativaVendaFitaPP = Convert.ToInt32(row["EstimativaVendaFitaPP"]);
                        ObjImproperCalendarEvent.EstimativaVendaFitaImpressa = Convert.ToInt32(row["EstimativaVendaFitaImpressa"]);
                        ObjImproperCalendarEvent.NFHoraSaida = Convert.ToDateTime(row["NFHoraSaida"]).ToString("dd/MM/yyyy"); 
                        ObjImproperCalendarEvent.NFValTotNota = Convert.ToDecimal(row["NFValTotNota"]);
                        ObjImproperCalendarEvent.ClasseCliente = row["ClasseCliente"].ToString();
                        ObjImproperCalendarEvent.TotalVendaAnual = Convert.ToDecimal(row["TotalVendaAnual"]);

                        ObjImproperCalendarEvent.ItensNF = row["ItensNF"].ToString();

                    }
                }
                else
                {
                    Retorno = -1;
                }


            }
        }
        catch
        {

            Retorno = -2;
        }

        return ObjImproperCalendarEvent;
    }


    //Busca os dados da Entidade que sera Vinculada no Banco
    public static ImproperCalendarEvent ConsultaEntidadeAdd(CalendarEvent cevent)
    {


        ImproperCalendarEvent ObjImproperCalendarEvent = new ImproperCalendarEvent();

        //add event to the database and return the primary key of the added event row

        int Retorno = 0;

        DataTable outputTable = new DataTable();

        try
        {

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_crm_consulta_entidade_add", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 250, "EntCod"));
                dbCommand.Parameters["@EntCod"].Value = cevent.EntCod;
                

                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
                dataReader.Close();


                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {

                        ObjImproperCalendarEvent.IdTipoAgendamento = cevent.IdTipoAgendamento;
                        ObjImproperCalendarEvent.EntCod = row["EntCod"].ToString();
                        ObjImproperCalendarEvent.EntNome = row["EntNome"].ToString();
                        ObjImproperCalendarEvent.EntCpfCgc = row["EntCpfCgc"].ToString();
                        ObjImproperCalendarEvent.Endereco = row["Endereco"].ToString();
                        ObjImproperCalendarEvent.ContatoNome = row["ContatoNome"].ToString();
                        ObjImproperCalendarEvent.ContatoTelefone = row["ContatoTelefone"].ToString();
                        ObjImproperCalendarEvent.ContatoEmail = row["ContatoEmail"].ToString();

                        ObjImproperCalendarEvent.StatEntComercial = row["StatEntComercial"].ToString();
                        ObjImproperCalendarEvent.EntStatDescr = row["EntStatDescr"].ToString();
                        ObjImproperCalendarEvent.DataUltimaVisita = Convert.ToDateTime(row["DataUltimaVisita"]).ToString("dd/MM/yyyy"); 
                        ObjImproperCalendarEvent.NFHoraSaida = Convert.ToDateTime(row["NFHoraSaida"]).ToString("dd/MM/yyyy"); 
                        ObjImproperCalendarEvent.NFValTotNota = Convert.ToDecimal(row["NFValTotNota"]);
                        //ObjImproperCalendarEvent.ClasseCliente = row["ClasseCliente"].ToString();
                        ObjImproperCalendarEvent.TotalVendaAnual = Convert.ToDecimal(row["TotalVendaAnual"]);

                        ObjImproperCalendarEvent.ItensNF = row["ItensNF"].ToString();
                        
                    }
                }
                else
                {
                    Retorno = -1;
                }


            }
        }
        catch
        {

            Retorno = -2;
        }

        return ObjImproperCalendarEvent;
    }
}
