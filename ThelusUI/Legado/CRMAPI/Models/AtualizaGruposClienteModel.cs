using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaGruposClienteModel
    {
        public string CodigoSAP { get; set; }

        List<GruposClientesClass> GruposCliente = new List<GruposClientesClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaGruposCliente()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select GroupCode CodigoGrupoSAP, GroupName Nome from OCRG ");

                stringSQL.AppendLine("where (GroupCode = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                GruposCliente = objUtilClass.ConvertDataTable<GruposClientesClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os grupos do cliente.");
            }
        }

        public string AtualizaGruposCliente()
        {
            string erro = "";

            try
            {
                CarregaGruposCliente();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (GruposClientesClass GrupoCliente in GruposCliente)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_GRUPO_CLIENTE", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoGrupoSAP", SqlDbType.Int, 0, "CodigoGrupoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 20, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoGrupoSAP"].Value = GrupoCliente.CodigoGrupoSAP;
                        dbCommand.Parameters["@Nome"].Value = GrupoCliente.Nome ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos grupos dos clientes.";
            }

            return erro;
        }
    }
}