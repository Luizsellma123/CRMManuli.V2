using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaFreteIncontermsModel
    {
        public string CodigoSAP { get; set; }

        List<FreteIncontermsClass> FretesInconterms = new List<FreteIncontermsClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaFretesInconterms()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select Code CodigoSAP, name Descricao from [@IB_INCOTERMS] ");

                stringSQL.AppendLine("where (Code = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                FretesInconterms = objUtilClass.ConvertDataTable<FreteIncontermsClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os fretes inconterms do SAP.");
            }
        }

        public string AtualizaFretesInconterms()
        {
            string erro = "";

            try
            {
                CarregaFretesInconterms();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (FreteIncontermsClass Frete in FretesInconterms)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_FRETE_INCOTERMS", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.NVarChar, 50, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 100, "Descricao"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = Frete.CodigoSAP ?? "";
                        dbCommand.Parameters["@Descricao"].Value = Frete.Descricao ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos fretes inconterms.";
            }

            return erro;
        }
    }
}