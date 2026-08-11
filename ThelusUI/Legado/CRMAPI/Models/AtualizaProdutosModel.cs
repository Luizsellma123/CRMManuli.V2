using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaProdutosModel
    {
        public string CodigoProdutoSAP { get; set; }

        List<CRMAPI.Classes.ProdutoClass> Produtos = new List<CRMAPI.Classes.ProdutoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaProdutos()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select  ");
                stringSQL.AppendLine("isnull(OITM.ItemCode, '') CodigoProdutoSAP, ");
                stringSQL.AppendLine("isnull(OITM.ItemName, '') Nome, ");
                stringSQL.AppendLine("isnull(OITM.SalUnitMsr, '') UnidadeVenda, ");
                stringSQL.AppendLine("isnull(OITM.validFor, '') AtivoSAP, ");
                stringSQL.AppendLine("isnull(OITM.ItmsGrpCod, 0) GrupoMateriaisSAP, ");
                stringSQL.AppendLine("isnull(OITM.MatType, '') TipoMaterialFiscal, ");
                stringSQL.AppendLine("isnull(OITM.PicturName, '') ImagemProduto, ");
                stringSQL.AppendLine("isnull(OITM.U_MF_PD_CLI, '') CodigoCliente, ");
                //stringSQL.AppendLine("isnull(OITM.U_MF_SUB_GRP, '') SubGrupo, ");
                stringSQL.AppendLine("isnull(OITM.U_MF_TipGrup, '') TipoProduto ");
                stringSQL.AppendLine("from OITM ");
                stringSQL.AppendLine("where convert(date, isnull(OITM.UpdateDate, OITM.CreateDate)) = convert(date, getdate()) ");
                stringSQL.AppendLine("and (OITM.ItemCode = '" + CodigoProdutoSAP + "' or '' = '" + CodigoProdutoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                Produtos = objUtilClass.ConvertDataTable<CRMAPI.Classes.ProdutoClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os produtos do SAP.");
            }
        }

        public string AtualizaProdutos()
        {
            string erro = "";

            try
            {
                CarregaProdutos();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (CRMAPI.Classes.ProdutoClass Produto in Produtos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
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
                        //dbCommand.Parameters.Add(new SqlParameter("@SubGrupo", SqlDbType.NVarChar, 300, "SubGrupo"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoProduto", SqlDbType.NVarChar, 300, "TipoProduto"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoProdutoSAP"].Value = Produto.CodigoProdutoSAP ?? "";
                        dbCommand.Parameters["@Nome"].Value = Produto.Nome ?? "";
                        dbCommand.Parameters["@UnidadeVenda"].Value = Produto.UnidadeVenda ?? "";
                        dbCommand.Parameters["@AtivoSAP"].Value = Produto.AtivoSAP ?? "";
                        dbCommand.Parameters["@GrupoMateriaisSAP"].Value = Produto.GrupoMateriaisSAP;
                        dbCommand.Parameters["@TipoMaterialFiscal"].Value = Produto.TipoMaterialFiscal ?? "";
                        dbCommand.Parameters["@ImagemProduto"].Value = Produto.ImagemProduto ?? "";
                        dbCommand.Parameters["@CodigoClienteSAP"].Value = Produto.CodigoCliente ?? "";
                        //dbCommand.Parameters["@SubGrupo"].Value = Produto.SubGrupo ?? "";
                        dbCommand.Parameters["@TipoProduto"].Value = Produto.TipoProduto ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;

                erro = "Erro na importação dos produtos.";
            }

            return erro;
        }
    }
}