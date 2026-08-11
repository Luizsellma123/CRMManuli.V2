using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaNaturezasJuridicasModel
    {
        public string CodigoSAP { get; set; }

        List<NaturezaJuridicaClass> NaturezasJuridicas = new List<NaturezaJuridicaClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaNaturezasJuridicas()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("SELECT Code CodigoSAP, Name Nome FROM [@IB_NAT_JURIDICA] ");

                stringSQL.AppendLine("where (Code = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                NaturezasJuridicas = objUtilClass.ConvertDataTable<NaturezaJuridicaClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar as naturezas jurídicas.");
            }
        }

        public string AtualizaNaturezasJuridicas()
        {
            string erro = "";

            try
            {
                CarregaNaturezasJuridicas();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (NaturezaJuridicaClass NaturezaJuridica in NaturezasJuridicas)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_NATUREZA_JURIDICA", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.VarChar, 50, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoSAP"].Value = NaturezaJuridica.CodigoSAP;
                        dbCommand.Parameters["@Nome"].Value = NaturezaJuridica.Nome ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;
                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação das naturezas jurídicas.";
            }

            return erro;
        }
    }
}