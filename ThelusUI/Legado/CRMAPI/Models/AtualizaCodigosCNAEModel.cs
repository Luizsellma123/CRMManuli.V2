using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaCodigosCNAEModel
    {
        public string CodigoCNAESap { get; set; }

        List<CodigoCNAEClass> CodigosCNAE = new List<CodigoCNAEClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaCodigosCNAE()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select AbsId AbsIdSAP, CNAECode CodigoCNAESap, Descrip DescricaoCNAE ");

                stringSQL.AppendLine("from OCNA ");

                stringSQL.AppendLine("where (CNAECode = '" + CodigoCNAESap + "' or '' = '" + CodigoCNAESap + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                CodigosCNAE = objUtilClass.ConvertDataTable<CodigoCNAEClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os Códigos CNAE.");
            }
        }

        public string AtualizaCodigosCNAE()
        {
            string erro = "";

            try
            {
                CarregaCodigosCNAE();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (CodigoCNAEClass CodigoCNAE in CodigosCNAE)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CODIGOS_CNAE", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoCNAESap", SqlDbType.VarChar, 9, "CodigoCNAESap"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoCNAE", SqlDbType.NText, 0, "DescricaoCNAE"));
                        dbCommand.Parameters.Add(new SqlParameter("@AbsIdSAP", SqlDbType.Int, 0, "AbsIdSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoCNAESap"].Value = CodigoCNAE.CodigoCNAESap.ToString();
                        dbCommand.Parameters["@DescricaoCNAE"].Value = CodigoCNAE.DescricaoCNAE ?? "";
                        dbCommand.Parameters["@AbsIdSAP"].Value = CodigoCNAE.AbsIdSAP;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos Códigos CNAE.";
            }

            return erro;
        }
    }
}