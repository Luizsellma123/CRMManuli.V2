using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class HistoricoCRMClass : clsConexao
    {
        public string CodigoPai { get; set; }
        public int Codigo { get; set; }
        public int CodigoEvento { get; set; }
        public int CodigoCategoria { get; set; }
        public string EntCod { get; set; }
        public string EntNome { get; set; }
        public string EntCNPJ { get; set; }
        public string UserEntFoneNome { get; set; }
        public string EntFone { get; set; }
        public string EntEmail { get; set; }
        public string DataCad { get; set; }
        public string UsuCod { get; set; }
        public string Historico { get; set; }
        public string DataAgenda { get; set; }
        public string Classificacao { get; set; }

        public DataTable Lista_Evento()
        {
            string strSql = "";
            strSql = "select Codigo, Descricao from User_tb_CRM_Evento where CodigoPai = 0 ";
            //strSql = strSql + "union all select 0, 'Selecione' ";

            return Executa_DataTable(strSql, "Lista_Evento");
        }

        public DataTable Lista_Categoria()
        {
            string strSql = "";
            strSql = "select Codigo, Descricao from User_tb_CRM_Evento where CodigoPai = '" + CodigoPai.ToString() + "' ";
            //strSql = strSql + "union all select 0, 'Selecione' "; 
            return Executa_DataTable(strSql, "Lista_Categoria");
        }

        public DataTable Lista_Evento_Filtro()
        {
            string strSql = "";
            strSql = "select Codigo, Descricao from User_tb_CRM_Evento where CodigoPai = 0 ";
            //strSql = strSql + "union all select 0, 'Todos' ";

            return Executa_DataTable(strSql, "Lista_Evento_Filtro");
        }

        public DataTable Lista_Categoria_Filtro()
        {
            string strSql = "";
            strSql = "select Codigo, Descricao from User_tb_CRM_Evento where CodigoPai = '" + CodigoPai.ToString() + "' ";
            //strSql = strSql + "union all select 0, 'Todos' ";
            return Executa_DataTable(strSql, "Lista_Categoria_Filtro");
        }

        public DataTable Executa_DataTable(String strSql, string Metodo)
        {
            DataTable outputTable = new DataTable();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand(strSql, dbConnection))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Deixa o Timeout da consulta com cerca de 4 minutos
                        dbCommand.CommandTimeout = 340;

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no método " + Metodo);
                }
                return outputTable;
            }
        }

        public string Historico_Inserir()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("user_sp_CRM_Historico_Inserir", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoEvento", SqlDbType.Int, 0, "CodigoEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoCategoria", SqlDbType.Int, 0, "CodigoCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataCad", SqlDbType.DateTime, 19, "DataCad"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 31, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 8000, "Historico"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataAgenda", SqlDbType.DateTime, 19, "DataAgenda"));

                    dbCommand.Parameters["@Codigo"].Value = Codigo;
                    dbCommand.Parameters["@CodigoEvento"].Value = CodigoEvento;
                    dbCommand.Parameters["@CodigoCategoria"].Value = CodigoCategoria;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@DataCad"].Value = DataCad;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@Historico"].Value = Historico;
                    if (DataAgenda == "" || DataAgenda == null)
                    {
                        dbCommand.Parameters["@DataAgenda"].Value = DBNull.Value;
                    }
                    else
                    {
                        dbCommand.Parameters["@DataAgenda"].Value = DataAgenda;
                    }

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                }
                catch
                {
                    erro = "Erro ao inserir histórico!";
                }
            }



            #region Adiciona no Calendario informações
            if (erro == "")
            {


                if (DataAgenda == "" || DataAgenda == null)
                {
                    DataAgenda = DateTime.Now.ToString();
                }

                try
                {
                    /*
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


                        dbCommand.Parameters["@DataInicio"].Value = DataAgenda;
                        dbCommand.Parameters["@DataFinal"].Value = DataAgenda;
                        dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                        dbCommand.Parameters["@UsuCodGestor"].Value = UsuCod;
                        dbCommand.Parameters["@IdTipoAgendamento"].Value = 17;/*idTipoAgendamento = 17 pois eh Historico CRM*/
                        /*dbCommand.Parameters["@CondicaoVisita"].Value = "Manutenção";
                        dbCommand.Parameters["@idLembreteUm"].Value = null;
                        dbCommand.Parameters["@idLembreteDois"].Value = null;
                        dbCommand.Parameters["@DescricaoCompromisso"].Value = Historico;
                        dbCommand.Parameters["@EntCod"].Value = EntCod;
                        dbCommand.Parameters["@LinhaProdutoQuantidadeStretch"].Value = 0;
                        dbCommand.Parameters["@LinhaProdutoQuantidadeFitaPP"].Value = 0;
                        dbCommand.Parameters["@LinhaProdutoQuantidadeFitaImpressa"].Value = 0;

                        dbCommand.Parameters["@ComRepresentante"].Value = "Não";
                        dbCommand.Parameters["@MeioTransporte"].Value = "";
                        dbCommand.Parameters["@Km"].Value = 0;
                        dbCommand.Parameters["@ValorEstimadoViagem"].Value = 0;
                        dbCommand.Parameters["@EstimativaVendaStretch"].Value = 0;
                        dbCommand.Parameters["@EstimativaVendaFitaPP"].Value = 0;
                        dbCommand.Parameters["@EstimativaVendaFitaImpressa"].Value = 0;

                        //Aumentando o timeout do command
                        dbCommand.CommandTimeout = 999999;

                        SqlDataReader dataReader = dbCommand.ExecuteReader();

                        dataReader.Close();


                    


                    }
                    */
                }
                catch(Exception ex)
                {

                    erro = "Erro ao inserir histórico Carteira!";
                }
            }
            #endregion Adiciona no Calendario informações




            return erro;
        }

        public string Classificacao_Cliente_Alterar()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("user_sp_CRM_Atendimento_Cliente_Alterar", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Classificacao", SqlDbType.VarChar, 30, "Classificacao"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@Classificacao"].Value = Classificacao;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                }
                catch
                {
                    erro = "Erro ao inserir Classificação!";
                }
            }

            return erro;
        }

        /*public void Historico_Listar()
        {

            string strSQL = "";
            this.Historico = "";
            DataTable Historicos = new DataTable();

            strSQL = "select His.DataCad, His.UsuCod, His.Historico, Eve.Descricao as Evento, Cat.Descricao as Categoria from User_tb_CRM_Historico His ";
            strSQL = strSQL + "left join User_tb_CRM_Evento Eve on His.CodigoEvento = Eve.Codigo and Eve.CodigoPai = '0' ";
            strSQL = strSQL + "left join User_tb_CRM_Evento Cat on His.CodigoCategoria = Cat.Codigo and Cat.CodigoPai = Eve.Codigo ";
            strSQL = strSQL + "where EntCod='" + EntCod.ToString() + "' order by DataCad desc";

            Historicos = Executa_DataTable(strSQL, "Historico_Listar");

            if (Historicos.Rows.Count > 0)
            {
                foreach (DataRow row in Historicos.Rows)
                {
                    this.Historico = this.Historico + "\n" + (string)row["DataCad"].ToString() + " - " +
                        (string)row["UsuCod"].ToString() + " - " +
                        (string)row["Evento"].ToString() + " - " +
                        (string)row["Categoria"].ToString() + " - " +
                        (string)row["Historico"].ToString();
                }
            }
        }*/

        public DataTable Historico_Listar()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Historico_CRM_Listar", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoEvento", SqlDbType.Int, 0, "CodigoEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoCategoria", SqlDbType.Int, 0, "CodigoCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 31, "UsuCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@CodigoEvento"].Value = CodigoEvento;
                    dbCommand.Parameters["@CodigoCategoria"].Value = CodigoCategoria;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return outputTable;
        }

        /*public void Entidade_Listar()
        {
            string strSQL = "";
            DataTable Entidade = new DataTable();

            strSQL = "select top 1 Ent.EntCod, EntNome, EntCpfCgc, Efo.EntFoneDDD, Efo.EntFoneNum, Efo.UserEntFoneNome, Ewe.EntWebEMail from Entidade Ent ";
            strSQL = strSQL + "left join ENT_FONE Efo on Efo.EntCod = Ent.EntCod and Efo.EntFonePrinc = 'Sim' ";
            strSQL = strSQL + "left join ENT_WEB Ewe on Ewe.EntCod = Ent.EntCod and Ewe.EntWebTipo = 'Comercial' ";
            strSQL = strSQL + "where Ent.EntCod='" + EntCod.ToString() + "'";

            Entidade = Executa_DataTable(strSQL, "Entidade_Listar");

            if (Entidade.Rows.Count > 0)
            {
                foreach (DataRow row in Entidade.Rows)
                {
                    this.EntNome = (string)row["EntNome"].ToString();
                    this.EntCNPJ = (string)row["EntCpfCgc"].ToString();
                    this.EntFone = "(" + (string)row["EntFoneDDD"].ToString() + ")" + (string)row["EntFoneNum"].ToString();
                    this.EntEmail = (string)row["EntWebEMail"].ToString();
                    this.UserEntFoneNome = (string)row["UserEntFoneNome"].ToString();
                }
            }
        }*/
    }
}