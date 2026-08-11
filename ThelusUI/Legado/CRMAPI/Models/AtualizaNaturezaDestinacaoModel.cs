using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaNaturezaDestinacaoModel
    {
        public string CodigoSAP { get; set; }

        List<NaturezaDestinacaoClass> NaturezaDestinacao = new List<NaturezaDestinacaoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaNaturezaDestinacao()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select Code CodigoSAP, name Nome from [@IB_NAT_DESTINACAO] ");

                stringSQL.AppendLine("where (Code = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                NaturezaDestinacao = objUtilClass.ConvertDataTable<NaturezaDestinacaoClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar as naturezas de destinação do SAP.");
            }
        }

        public string AtualizaNaturezaDestinacao()
        {
            string erro = "";

            try
            {
                CarregaNaturezaDestinacao();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (NaturezaDestinacaoClass Natureza in NaturezaDestinacao)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_NATUREZA_DESTINACAO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.NVarChar, 50, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = Natureza.CodigoSAP ?? "";
                        dbCommand.Parameters["@Nome"].Value = Natureza.Nome ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação das naturezas de destinação.";
            }

            return erro;
        }
    }
}