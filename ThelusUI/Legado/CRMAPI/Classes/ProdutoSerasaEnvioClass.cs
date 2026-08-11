using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ProdutoSerasaEnvioClass : ConexaoClass
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string NomeCampo { get; set; }
        public string Descricao { get; set; }
        public int PosicaoInicial { get; set; }
        public int PosicaoFinal { get; set; }
        public int Tamanho { get; set; }
        public List<ProdutoSerasaEnvioFilhoClass> ProdutoSerasaEnvioFilho = new List<ProdutoSerasaEnvioFilhoClass>();

        public string RecuperaConfiguracaoEnvioProdutoFilho()
        {
            DataTable outputTable = new DataTable();
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_API_SERASA_CONFIG_PROD_ENVIO_FILHO", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDConfiguracao", SqlDbType.Int, 0, "IDConfiguracao"));

                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@IDConfiguracao"].Value = this.IDConfiguracao;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                if (erro == "")
                                {
                                    ProdutoSerasaEnvioFilhoClass OBJProdutoSerasaRetornoFilho = new ProdutoSerasaEnvioFilhoClass();

                                    OBJProdutoSerasaRetornoFilho.IDProduto = Convert.ToInt32(row["IDProduto"]);
                                    OBJProdutoSerasaRetornoFilho.IDConfiguracao = Convert.ToInt32(row["IDConfiguracao"]);
                                    OBJProdutoSerasaRetornoFilho.IDConfiguracaoFilho = Convert.ToInt32(row["IDConfiguracaoFilho"]);
                                    OBJProdutoSerasaRetornoFilho.NomeCampo = Convert.ToString(row["NomeCampo"]);
                                    OBJProdutoSerasaRetornoFilho.Descricao = Convert.ToString(row["Descricao"]);
                                    OBJProdutoSerasaRetornoFilho.PosicaoInicial = Convert.ToInt32(row["PosicaoInicial"]);
                                    OBJProdutoSerasaRetornoFilho.PosicaoFinal = Convert.ToInt32(row["PosicaoFinal"]);
                                    OBJProdutoSerasaRetornoFilho.Tamanho = Convert.ToInt32(row["Tamanho"]);
                                    OBJProdutoSerasaRetornoFilho.Valor = Convert.ToString(row["Valor"]);
                                    OBJProdutoSerasaRetornoFilho.RecuperaValorProduto = Convert.ToBoolean(row["RecuperaValorProduto"]);

                                    ProdutoSerasaEnvioFilho.Add(OBJProdutoSerasaRetornoFilho);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = "erro ao recuperar configuração produto SERASA.";
            }

            return erro;
        }
    }
}