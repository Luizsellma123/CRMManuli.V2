using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaClienteIdentificacaoFiscalModel
    {
        public string CodigoClienteSAP { get; set; }

        List<ClienteIdentificacaoFiscalClass> ClientesIdentificacaoFiscal = new List<ClienteIdentificacaoFiscalClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaClientesIdentificacaoFiscal()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select CRD7.CardCode CodigoClienteSAP, ");
                stringSQL.AppendLine("ISNULL(CRD7.[Address],'') DescricaoEndereco, ");
                stringSQL.AppendLine("ISNULL(TaxId0,'') CNPJ, ISNULL(TaxId1,'') InscricaoEstadual, ");
                stringSQL.AppendLine("ISNULL(TaxId8,'') Suframa, CONVERT(VARCHAR(MAX),ISNULL(CNAEId,'')) IDCNAE ");
                stringSQL.AppendLine("from CRD7  ");
                stringSQL.AppendLine("INNER JOIN CRD1 ");
                stringSQL.AppendLine("	ON CRD1.CardCode=CRD7.CardCode and CRD7.[Address]= CRD1.[Address] ");
                stringSQL.AppendLine("INNER JOIN OCRD ");
                stringSQL.AppendLine("	ON OCRD.CardCode= CRD1.CardCode ");
                stringSQL.AppendLine("where convert(date, isnull(OCRD.UpdateDate, OCRD.CreateDate))=convert(date, getdate()) ");
                stringSQL.AppendLine("and (CRD7.CardCode = '" + CodigoClienteSAP + "' or '' = '" + CodigoClienteSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                ClientesIdentificacaoFiscal = objUtilClass.ConvertDataTable<ClienteIdentificacaoFiscalClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar a identificação fiscal dos clientes do SAP.");
            }
        }

        public string AtualizaClientesIdentificacaoFiscal()
        {
            string erro = "";

            ClienteIdentificacaoFiscalClass ClienteIdentificacaoFiscalTeste = new ClienteIdentificacaoFiscalClass();

            CarregaClientesIdentificacaoFiscal();

            ConexaoClass objConexaoClass = new ConexaoClass();

            foreach (ClienteIdentificacaoFiscalClass ClienteIdentificacaoFiscal in ClientesIdentificacaoFiscal)
            {                
                using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                {
                    try
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_IDENTIFICACAO_FISCAL", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoEndereco", SqlDbType.NVarChar, 50, "DescricaoEndereco"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNAESAP", SqlDbType.Int, 0, "CNAESAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.NVarChar, 100, "CNPJ"));
                        dbCommand.Parameters.Add(new SqlParameter("@InscricaoEstadual", SqlDbType.NVarChar, 100, "InscricaoEstadual"));
                        dbCommand.Parameters.Add(new SqlParameter("@Suframa", SqlDbType.NVarChar, 100, "Suframa"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteIdentificacaoFiscal.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@DescricaoEndereco"].Value = ClienteIdentificacaoFiscal.DescricaoEndereco ?? "";
                        dbCommand.Parameters["@CNAESAP"].Value = Convert.ToInt32(ClienteIdentificacaoFiscal.IDCNAE ?? "0");
                        dbCommand.Parameters["@CNPJ"].Value = ClienteIdentificacaoFiscal.CNPJ ?? "";
                        dbCommand.Parameters["@InscricaoEstadual"].Value = ClienteIdentificacaoFiscal.InscricaoEstadual ?? "";
                        dbCommand.Parameters["@Suframa"].Value = ClienteIdentificacaoFiscal.Suframa ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;
                    }
                    catch (Exception ex)
                    {
                        erro = ex.Message;

                        erro = "Erro na importação das identificações fiscais dos clientes do SAP.";
                    }

                }
            }

            return erro;
        }
    }
}