using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaUtilizacaoModel
    {
        public string CodigoSAP { get; set; }

        List<UtilizacaoClass> Utilizacoes = new List<UtilizacaoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaUtilizacoes()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select ID CodigoSAP, Usage utilizacao from OUSG ");

                stringSQL.AppendLine("where (ID = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                Utilizacoes = objUtilClass.ConvertDataTable<UtilizacaoClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar as utilizacoes do SAP.");
            }
        }

        public string AtualizaUtilizacoes()
        {
            string erro = "";

            try
            {
                CarregaUtilizacoes();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (UtilizacaoClass Utilizacao in Utilizacoes)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_UTILIZACAO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@utilizacao", SqlDbType.VarChar, 20, "utilizacao"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoSAP"].Value = Utilizacao.CodigoSAP;
                        dbCommand.Parameters["@utilizacao"].Value = Utilizacao.utilizacao ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos utilizacoes.";
            }

            return erro;
        }
    }
}