using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class CrmTabelaPrecoProdutoClass : clsConexao
    {

        public string CodigoUsuario { get; set; }
        public int IDProduto { get; set; }
        public int IDTabela { get; set; }
        public decimal ValorUnitario { get; set; }
        public string Status { get; set; }


        public string CodigoProdutoSAP { get; set; }
        public string NomeProduto { get; set; }



        public DataTable RetornaTabelaPrecoProd()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TABELA_PRECO_PROD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IdProduto", SqlDbType.Int, 0, "IdProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 8000, "CodigoProdutoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeProduto", SqlDbType.VarChar, 8000, "NomeProduto"));


                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;
                    dbCommand.Parameters["@IdProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@CodigoProdutoSAP"].Value = this.CodigoProdutoSAP;
                    dbCommand.Parameters["@NomeProduto"].Value = this.NomeProduto;



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


        public string GravaTabelaPrecoProd()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TABELA_PRECO_PROD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorUnitario", SqlDbType.Decimal, 0, "ValorUnitario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 100, "Status"));
                    
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;
                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@ValorUnitario"].Value = this.ValorUnitario;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    
                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na GravaTabelaPrecoProd: " + ex.Message;
                }
            }

            return erro;
        }


        public string ExcluiTabelaPrecoProd()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_TABELA_PRECO_PROD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;
                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    
                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na ExcluiTabelaPrecoProd: " + ex.Message;
                }
            }

            return erro;
        }



        public DataTable RetornaTabelaPrecoProdLog()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TABELA_PRECO_PROD_LOG", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));

                    
                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;
                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;



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


    }
}