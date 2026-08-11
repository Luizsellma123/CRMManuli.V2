using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaClientesContatoModel
    {
        public string CodigoSAP { get; set; }

        List<ClienteContatoClass> ClientesContato = new List<ClienteContatoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaClientesContato()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select OCPR.CntctCode CodigoSAP, OCPR.CardCode CodigoClienteSAP, ");
                stringSQL.AppendLine("ISNULL(OCPR.FirstName,'') Nome, ISNULL(OCPR.Tel1,'') Telefone, ");
                stringSQL.AppendLine("ISNULL(OCPR.E_MailL,'') Email, ISNULL(OCPR.[Name],'') TipoContato ");
                stringSQL.AppendLine("from OCPR  ");
                stringSQL.AppendLine("INNER JOIN OCRD ");
                stringSQL.AppendLine("	ON OCRD.CardCode = OCPR.CardCode ");
                stringSQL.AppendLine("where convert(date, isnull(OCRD.UpdateDate, OCRD.CreateDate))= convert(date, getdate()) ");                                  
                stringSQL.AppendLine("and (OCPR.CntctCode = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                ClientesContato = objUtilClass.ConvertDataTable<ClienteContatoClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os contatos dos clientes do SAP.");
            }
        }

        public string AtualizaClientesContato()
        {
            string erro = "";

            try
            {
                CarregaClientesContato();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (ClienteContatoClass ClienteContato in ClientesContato)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLIENTE_CONTATO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoContato", SqlDbType.NVarChar, 50, "TipoContato"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.NVarChar, 50, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.NVarChar, 20, "Telefone"));
                        dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100, "Email"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteContato.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@TipoContato"].Value = ClienteContato.TipoContato ?? "";
                        dbCommand.Parameters["@Nome"].Value = ClienteContato.Nome ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = ClienteContato.CodigoSAP;
                        dbCommand.Parameters["@Telefone"].Value = ClienteContato.Telefone ?? "";
                        dbCommand.Parameters["@Email"].Value = ClienteContato.Email ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos contatos dos clientes.";
            }

            return erro;
        }
    }
}