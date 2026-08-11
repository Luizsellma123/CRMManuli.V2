using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaEstadosModel
    {
        public string CodigoEstadoSAP { get; set; }

        List<EstadoClass> Estados = new List<EstadoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaEstados()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select Code CodigoEstadoSAP, Country PaisSap, Name Nome from OCST");

                stringSQL.AppendLine("where (Code = '" + CodigoEstadoSAP + "' or '' = '" + CodigoEstadoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                Estados = objUtilClass.ConvertDataTable<EstadoClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os estados do SAP.");
            }
        }

        public string AtualizaEstados()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                CarregaEstados();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (EstadoClass Estado in Estados)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_ESTADO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoEstadoSAP", SqlDbType.VarChar, 3, "CodigoEstadoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoPaisSAP", SqlDbType.VarChar, 3, "CodigoPaisSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoEstadoSAP"].Value = Estado.CodigoEstadoSAP.ToString();
                        dbCommand.Parameters["@CodigoPaisSAP"].Value = Estado.PaisSap.ToString();
                        dbCommand.Parameters["@Nome"].Value = Estado.Nome.ToString();

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;
                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos estados.";
            }

            return erro;
        }
    }
}