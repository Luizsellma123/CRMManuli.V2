using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaGrupoEconomicoModel
    {
        public string CodigoSAP { get; set; }

        List<GrupoEconomicoClass> GruposEconomicos = new List<GrupoEconomicoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaGruposEconomicos()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("SELECT Code CodigoSAP, Name NomeGrupo FROM [@MF_GRP_ECONOMICO] ");

                stringSQL.AppendLine("where (Code = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                GruposEconomicos = objUtilClass.ConvertDataTable<GrupoEconomicoClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os grupos economicos do SAP.");
            }
        }

        public string AtualizaGruposEconomicos()
        {
            string erro = "";

            try
            {
                CarregaGruposEconomicos();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (GrupoEconomicoClass GrupoEconomico in GruposEconomicos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_GRUPO_ECONOMICO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.VarChar, 50, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeGrupo", SqlDbType.VarChar, 100, "NomeGrupo"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoSAP"].Value = GrupoEconomico.CodigoSAP;
                        dbCommand.Parameters["@NomeGrupo"].Value = GrupoEconomico.NomeGrupo ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos grupos economicos.";
            }

            return erro;
        }
    }
}