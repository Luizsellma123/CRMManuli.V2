using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaCondicoesPagamentoModel
    {
        public string CodigoSAP { get; set; }

        List<CondicaoPagamentoClass> CondicoesPagamento = new List<CondicaoPagamentoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaCondicoesPagamento()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select GroupNum CodigoSAP, PymntGroup NomeCondicao from OCTG ");

                stringSQL.AppendLine("where (GroupNum = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                CondicoesPagamento = objUtilClass.ConvertDataTable<CondicaoPagamentoClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar as condicoes de pagamento do SAP.");
            }
        }

        public string AtualizaCondicoesPagamento()
        {
            string erro = "";

            try
            {
                CarregaCondicoesPagamento();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (CondicaoPagamentoClass CondicaoPagamento in CondicoesPagamento)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CONDICAO_PAGAMENTO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeCondicao", SqlDbType.VarChar, 100, "NomeCondicao"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoSAP"].Value = CondicaoPagamento.CodigoSAP;
                        dbCommand.Parameters["@NomeCondicao"].Value = CondicaoPagamento.NomeCondicao ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação das Condições Pagamentos.";
            }

            return erro;
        }
    }
}