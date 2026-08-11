using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class CrmProdutoClass : clsConexao
    {
        #region Campos De Filtro

        public string PesquisarPorDropDownList { get; set; }
        public string PesquisarPorTextBox { get; set; }

        #endregion

        public string CodigoUsuario { get; set; }
        public int IDProduto { get; set; }
        public string CodigoProdutoSAP { get; set; }
        public string Nome { get; set; }
        public string UnidadeVenda { get; set; }

        public DataTable RetornaProduto()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PRODUTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 800, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 800, "CodigoProdutoSAP"));


                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@Nome"].Value = this.Nome;
                    dbCommand.Parameters["@CodigoProdutoSAP"].Value = this.CodigoProdutoSAP;


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

        public DataTable RetornaProdutoPorCodigoProdutoSAP()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PRODUTO_CodigoProdutoSAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 800, "CodigoProdutoSAP"));

                    dbCommand.Parameters["@CodigoProdutoSAP"].Value = this.CodigoProdutoSAP;

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

        public DataTable ManutencaoProduto()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_MANUTENCAO_PRODUTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));

                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDProduto = Convert.ToInt32(row["IDProduto"]);
                                this.Nome = row["Nome"].ToString();
                                this.CodigoProdutoSAP = row["CodigoProdutoSAP"].ToString();
                                this.UnidadeVenda = row["UnidadeVenda"].ToString();

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

        public decimal RetornaProdutoFatorConversao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PRODUTO_FATORCONVERSAOFITAPESO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));

                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return Convert.ToDecimal(row["FatorConversao"]);
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return 0;
        }

        public decimal RetornaQuantidadeConvertida(decimal Quantidade)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PRODUTO_QUANTIDADECONVERTIDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 8000, "CodigoProdutoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 0, "Quantidade"));

                    dbCommand.Parameters["@CodigoProdutoSAP"].Value = this.CodigoProdutoSAP;
                    dbCommand.Parameters["@Quantidade"].Value = Quantidade;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return Convert.ToDecimal(row["Quantidade"]);
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return 0;
        }

    }
}