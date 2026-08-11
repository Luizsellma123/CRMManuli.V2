using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseProdutoPrincipal : clsConexao
    {
        public List<WSClasseProduto> ListaProdutos { get; set; }

        //Importa dados de países do SAP
        public string AtualizaProdutos()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseProduto Produto in ListaProdutos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_PRODUTO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.NVarChar, 50, "CodigoProdutoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@UnidadeVenda", SqlDbType.VarChar, 100, "UnidadeVenda"));
                        dbCommand.Parameters.Add(new SqlParameter("@AtivoSAP", SqlDbType.VarChar, 1, "AtivoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@GrupoMateriaisSAP", SqlDbType.Int, 0, "GrupoMateriaisSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoMaterialFiscal", SqlDbType.NVarChar, 3, "TipoMaterialFisca"));
                        dbCommand.Parameters.Add(new SqlParameter("@ImagemProduto", SqlDbType.NVarChar, 300, "ImagemProduto"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 300, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CodigoProdutoSAP"].Value = Produto.CodigoProdutoSAP ?? "";
                        dbCommand.Parameters["@Nome"].Value = Produto.Nome ?? "";
                        dbCommand.Parameters["@UnidadeVenda"].Value = Produto.UnidadeVenda ?? "";
                        dbCommand.Parameters["@AtivoSAP"].Value = Produto.AtivoSAP ?? "";
                        dbCommand.Parameters["@GrupoMateriaisSAP"].Value = Convert.ToInt32(Produto.GrupoMateriaisSAP ?? "0");
                        dbCommand.Parameters["@TipoMaterialFiscal"].Value = Produto.TipoMaterialFiscal ?? "";
                        dbCommand.Parameters["@ImagemProduto"].Value = Produto.ImagemProduto ?? "";
                        dbCommand.Parameters["@CodigoClienteSAP"].Value = Produto.CodigoCliente ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos produtos.";
            }

            return erro;
        }

    }
}