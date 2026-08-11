using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

/// <summary>
/// Summary description for CalendarEvent
/// </summary>
public class CalendarEvent : clsConexao
{
    #region Campos Base
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public bool allDay { get; set; }
    public string className { get; set; }
    #endregion

    public int IDAgendamento { get; set; }
    public string IDAgendamentoFiltro { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public string UsuCod { get; set; }
    public string UsuCodGestor { get; set; }
    public int IdTipoAgendamento { get; set; }
    public string DescricaoTipoAgendamento { get; set; }
    public string CondicaoVisita { get; set; }
    public decimal idLembreteUm { get; set; }
    public string LembreteUm { get; set; }
    public decimal idLembreteDois { get; set; }
    public string LembreteDois { get; set; }
    public string DescricaoCompromisso { get; set; }
    public string EntCod { get; set; }
    public string EntNome { get; set; }
    public string EntNomeFant { get; set; }
    public string EntCpfCgc { get; set; }
    public string Endereco { get; set; }
    public string ContatoNome { get; set; }
    public string ContatoTelefone { get; set; }
    public string ContatoEmail { get; set; }
    public decimal LinhaProdutoQuantidadeStretch { get; set; }
    public decimal LinhaProdutoQuantidadeFitaPP { get; set; }
    public decimal LinhaProdutoQuantidadeFitaImpressa { get; set; }
    public bool VinculaEntidade { get; set; }
    public string VendCod { get; set; }


    public string ComRepresentante { get; set; }
    public string MeioTransporte { get; set; }
    public decimal Km { get; set; }
    public decimal ValorEstimadoViagem { get; set; }
    public string StatEntComercial {get;set;}
    public string EntStatDescr{get;set;}
    public DateTime DataUltimaVisita { get; set; }
    public int EstimativaVendaStretch{ get; set; }
    public int EstimativaVendaFitaPP{ get; set; }
    public int EstimativaVendaFitaImpressa{ get; set; }
    public DateTime NFHoraSaida	 { get; set; }
    public decimal NFValTotNota	 { get; set; }
    public decimal TotalVendaAnual { get; set; }

    public string ClasseCliente { get; set; }
    public string ItensNF { get; set; }

    
    
    













    public DataTable Consulta_Tipos_Agendamentos()
    {
        DataTable outputTable = new DataTable();


        using (SqlConnection dbConnection = new SqlConnection(strConec))
        {
            //Abre Conexao
            dbConnection.Open();

            SqlCommand dbCommand = new SqlCommand();

            dbCommand = new SqlCommand("user_sp_crm_consulta_tipos_agendamentos", dbConnection);

            dbCommand.CommandType = CommandType.StoredProcedure;


            SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

            SqlDataReader dataReader = dbCommand.ExecuteReader();
            outputTable.Load(dataReader);


            return outputTable;
        }

    }


    public DataTable Consulta_Agendamentos_UsuCod_Data()
    {
        DataTable outputTable = new DataTable();


        using (SqlConnection dbConnection = new SqlConnection(strConec))
        {
            //Abre Conexao
            dbConnection.Open();

            SqlCommand dbCommand = new SqlCommand();

            dbCommand = new SqlCommand("user_sp_crm_consulta_Agendamento_usucod_data", dbConnection);

            dbCommand.CommandType = CommandType.StoredProcedure;


            dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
            dbCommand.Parameters.Add(new SqlParameter("@DataInicio", SqlDbType.DateTime, 0, "DataInicio"));
            dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 0, "DataFinal"));
            dbCommand.Parameters.Add(new SqlParameter("@IDAgendamentoFiltro", SqlDbType.VarChar, 800, "IDAgendamentoFiltro"));



            dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
            dbCommand.Parameters["@DataInicio"].Value = DataInicio;
            dbCommand.Parameters["@DataFinal"].Value = DataFinal;
            dbCommand.Parameters["@IDAgendamentoFiltro"].Value = IDAgendamentoFiltro;


            SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

            SqlDataReader dataReader = dbCommand.ExecuteReader();
            outputTable.Load(dataReader);


            return outputTable;
        }

    }


    public DataTable Consulta_agenda_usuario_UsuCod()
    {
        DataTable outputTable = new DataTable();


        using (SqlConnection dbConnection = new SqlConnection(strConec))
        {
            //Abre Conexao
            dbConnection.Open();

            SqlCommand dbCommand = new SqlCommand();

            dbCommand = new SqlCommand("user_sp_crm_consulta_agenda_usuario", dbConnection);

            dbCommand.CommandType = CommandType.StoredProcedure;


            dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));

            dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";


            SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

            SqlDataReader dataReader = dbCommand.ExecuteReader();
            outputTable.Load(dataReader);


            return outputTable;
        }

    }


    public DataTable Consulta_Entidade_Agenda()
    {

        DataTable outputTable = new DataTable();

        try
        {

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("User_SP_CRM_Consulta_Entidade_Agenda", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;


                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@EntNomeFant", SqlDbType.VarChar, 800, "EntNomeFant"));
                dbCommand.Parameters.Add(new SqlParameter("@EntNome", SqlDbType.VarChar, 800, "EntNome"));
                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 50, "EntCod"));
                dbCommand.Parameters.Add(new SqlParameter("@EntCpfCgc", SqlDbType.VarChar, 100, "EntCpfCgc"));
                dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));




                dbCommand.Parameters["@UsuCod"].Value = UsuCod ?? "";
                dbCommand.Parameters["@EntNomeFant"].Value = EntNomeFant ?? "";
                dbCommand.Parameters["@EntNome"].Value = EntNome ?? "";
                dbCommand.Parameters["@EntCod"].Value = EntCod ?? "";
                dbCommand.Parameters["@EntCpfCgc"].Value = EntCpfCgc ?? "";
                dbCommand.Parameters["@VendCod"].Value = VendCod ?? "";


                dbCommand.CommandTimeout = 9999999;

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
                dataReader.Close();


            }
        }
        catch
        {


        }

        return outputTable;

    }


    public DataTable Mostra_Agendamento_idAgendamento()
    {

        DataTable outputTable = new DataTable();

        try
        {

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_crm_mostra_idAgendamento", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@idAgendamento", SqlDbType.Int, 0, "idAgendamento"));


                dbCommand.Parameters["@idAgendamento"].Value = IDAgendamento;



                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);
                dataReader.Close();

                /* Caso precis pegar campo a campo
                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {

                        Retorno = Convert.ToInt32(row["idAgendamento"]);


                    }
                }
                */


            }
        }
        catch
        {


        }

        return outputTable;

    }

}
