using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaPaisModel
    {
        public string CodigoSAP { get; set; }

        List<PaisClass> Paises = new List<PaisClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaPaises()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select Code CodigoSAP, Name Nome from OCRY ");

                stringSQL.AppendLine("where (Code = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                Paises = objUtilClass.ConvertDataTable<PaisClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os paises do SAP.");
            }
        }
        
        public string AtualizaPaises()
        {
            string erro = "";
            
            try
            {
                CarregaPaises();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (PaisClass Pais in Paises)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_PAIS", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.VarChar, 10, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoSAP"].Value = Pais.CodigoSAP.ToString();
                        dbCommand.Parameters["@Nome"].Value = Pais.Nome.ToString();

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos paises.";
            }

            return erro;
        }
    }
}