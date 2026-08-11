using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class CrmTabelaPrecoClass : clsConexao
    {

        #region Campos De Filtro
        public string PesquisarPorDropDownList { get; set; }
        public string PesquisarPorTextBox { get; set; }
        #endregion
        public string CodigoUsuario { get; set; }
        public int IDTabela { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }



        public string GravaTabelaPreco()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TABELA_PRECO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 800, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 800, "Status"));

                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vIDTabela", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "vIDTabela", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;
                    dbCommand.Parameters["@Nome"].Value = this.Nome;
                    dbCommand.Parameters["@Status"].Value = this.Status;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.IDTabela = (Int32)dbCommand.Parameters["@vIDTabela"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro GravaTabelaPreco:" + ex.Message;
                }
            }

            return erro;
        }

        public DataTable RetornaTabelaPreco()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TABELA_PRECO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 800, "Nome"));


                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;
                    dbCommand.Parameters["@Nome"].Value = this.Nome;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;

        }

        public DataTable ManutencaoTabelaPreco()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_MANUTENCAO_TABELA_PRECO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));

                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDTabela = Convert.ToInt32(row["IDTabela"]);
                                this.Nome = row["Nome"].ToString();
                                this.DataCriacao = Convert.ToDateTime(row["DataCriacao"].ToString());
                                this.Status = row["Status"].ToString();
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;

        }

        public string AtualizacaoGeral()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_WS_IMPORTACAO_SAP_CRM", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    dbCommand.ExecuteNonQuery();

                }
            }
            catch
            {
                Retorno = "Erro na Funcao Altera_CondPagCod_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }



    }
}