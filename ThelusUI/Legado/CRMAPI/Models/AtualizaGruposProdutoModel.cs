using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaGruposProdutoModel
    {
        public string CodigoSAP { get; set; }

        List<GruposProdutoClass> GruposProduto = new List<GruposProdutoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaGruposProduto()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select ItmsGrpCod CodigoSAP, ItmsGrpNam Nome from OITB ");

                stringSQL.AppendLine("where (ItmsGrpCod = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                GruposProduto = objUtilClass.ConvertDataTable<GruposProdutoClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os grupos dos produtos do SAP.");
            }
        }

        public string AtualizaGruposProduto()
        {
            string erro = "";

            try
            {
                CarregaGruposProduto();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (GruposProdutoClass GrupoProduto in GruposProduto)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_GRUPO_PRODUTO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = GrupoProduto.CodigoSAP;
                        dbCommand.Parameters["@Nome"].Value = GrupoProduto.Nome;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos grupos dos produtos .";
            }

            return erro;
        }
    }
}