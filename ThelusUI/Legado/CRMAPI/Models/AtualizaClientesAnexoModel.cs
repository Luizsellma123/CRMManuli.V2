using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaClientesAnexoModel
    {
        public string CodigoSAP { get; set; }

        List<ClienteAnexoClass> ClientesAnexo = new List<ClienteAnexoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaClientesAnexo()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select AbsEntry IDAnexoSAP, Line CodigoSAP, trgtPath CaminhoDestino, ");
                stringSQL.AppendLine("[FileName] NomeArquivo, FileExt ExtensaoArquivo, ");
                stringSQL.AppendLine("CONVERT(VARCHAR(MAX),[Date],103) DataAnexo, ISNULL([FreeText],'') TextoLivre ");
                stringSQL.AppendLine("from ATC1 INNER JOIN OCRD ON ATC1.AbsEntry=OCRD.AtcEntry ");
                stringSQL.AppendLine("where convert(date, isnull(OCRD.UpdateDate, OCRD.CreateDate))=convert(date, getdate()) ");
                stringSQL.AppendLine("and (Line = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                ClientesAnexo = objUtilClass.ConvertDataTable<ClienteAnexoClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os anexos dos clientes do SAP.");
            }
        }

        public string AtualizaClientesAnexo()
        {
            string erro = "";

            try
            {
                CarregaClientesAnexo();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (ClienteAnexoClass ClienteAnexo in ClientesAnexo)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLIENTE_ANEXOS", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        //dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CaminhoDestino", SqlDbType.NText, 0, "CaminhoDestino"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeArquivo", SqlDbType.NVarChar, 254, "NomeArquivo"));
                        dbCommand.Parameters.Add(new SqlParameter("@ExtensaoArquivo", SqlDbType.NVarChar, 8, "ExtensaoArquivo"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataAnexo", SqlDbType.Date, 0, "DataAnexo"));
                        dbCommand.Parameters.Add(new SqlParameter("@TextoLivre", SqlDbType.NVarChar, 100, "TextoLivre"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnexoSAP", SqlDbType.Int, 0, "IDAnexoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CaminhoDestino"].Value = ClienteAnexo.CaminhoDestino ?? "";
                        dbCommand.Parameters["@NomeArquivo"].Value = ClienteAnexo.NomeArquivo ?? "";
                        dbCommand.Parameters["@ExtensaoArquivo"].Value = ClienteAnexo.ExtensaoArquivo ?? "";
                        //dbCommand.Parameters["@DataAnexo"].Value = Convert.ToDateTime(ClienteAnexo.DataAnexo ?? "");
                        dbCommand.Parameters["@DataAnexo"].Value = ClienteAnexo.DataAnexo ?? "";
                        dbCommand.Parameters["@TextoLivre"].Value = ClienteAnexo.TextoLivre ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = ClienteAnexo.CodigoSAP;
                        dbCommand.Parameters["@IDAnexoSAP"].Value = ClienteAnexo.IDAnexoSAP;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos anexos dos clientes.";
            }

            return erro;
        }
    }
}