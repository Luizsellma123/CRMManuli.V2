using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaClientesEnderecoModel
    {
        public string CodigoClienteSAP { get; set; }

        List<ClienteEnderecoClass> ClientesEnderecos = new List<ClienteEnderecoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaClientesEnderecos()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select CRD1.CardCode CodigoClienteSAP, isnull(CRD1.[Address],'') DescricaoEndereco, ");
                stringSQL.AppendLine("isnull(CRD1.AddrType, '') TipoLogradouro, ");
                stringSQL.AppendLine("isnull(CRD1.Street, '') Rua, isnull(CRD1.StreetNo, '') NumeroRua, ");
                stringSQL.AppendLine("isnull(CRD1.Building, '') Complemento, isnull(CRD1.ZipCode, '') CEP, ");
                stringSQL.AppendLine("isnull(CRD1.Block,'') Bairro, isnull(CRD1.City, '') Cidade, ");
                stringSQL.AppendLine("isnull(CRD1.Country, '') PaisSAP, isnull(CRD1.[State], '') EstadoSAP, ");
                stringSQL.AppendLine("isnull(CRD1.County, '') MunicipioSAP ");
                stringSQL.AppendLine("from CRD1 INNER JOIN OCRD ON CRD1.CardCode = OCRD.CardCode ");
                stringSQL.AppendLine("where convert(date, isnull(OCRD.UpdateDate, OCRD.CreateDate)) = convert(date, getdate())  ");                
                stringSQL.AppendLine("and (CRD1.CardCode = '" + CodigoClienteSAP + "' or '' = '" + CodigoClienteSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                ClientesEnderecos = objUtilClass.ConvertDataTable<ClienteEnderecoClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os endereços dos clientes do SAP.");
            }
        }

        public string AtualizaClientesEnderecos()
        {
            string erro = "";

            try
            {
                CarregaClientesEnderecos();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (ClienteEnderecoClass ClienteEndereco in ClientesEnderecos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLIENTE_ENDERECO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoEndereco", SqlDbType.NVarChar, 50, "DescricaoEndereco"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoLogradouro", SqlDbType.NVarChar, 100, "TipoLogradouro"));
                        dbCommand.Parameters.Add(new SqlParameter("@Rua", SqlDbType.NVarChar, 100, "Rua"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroRua", SqlDbType.NVarChar, 100, "NumeroRua"));
                        dbCommand.Parameters.Add(new SqlParameter("@Complemento", SqlDbType.NText, 0, "Complemento"));
                        dbCommand.Parameters.Add(new SqlParameter("@CEP", SqlDbType.VarChar, 20, "CEP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.NVarChar, 100, "Bairro"));
                        dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.NVarChar, 100, "Cidade"));
                        dbCommand.Parameters.Add(new SqlParameter("@PaisSAP", SqlDbType.NVarChar, 3, "PaisSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@EstadoSAP", SqlDbType.NVarChar, 3, "EstadoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@MunicipioSAP", SqlDbType.NVarChar, 100, "MunicipioSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteEndereco.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@DescricaoEndereco"].Value = ClienteEndereco.DescricaoEndereco ?? "";
                        dbCommand.Parameters["@TipoLogradouro"].Value = ClienteEndereco.TipoLogradouro ?? "";
                        dbCommand.Parameters["@Rua"].Value = ClienteEndereco.Rua ?? "";
                        dbCommand.Parameters["@NumeroRua"].Value = ClienteEndereco.NumeroRua ?? "";
                        dbCommand.Parameters["@Complemento"].Value = ClienteEndereco.Complemento ?? "";
                        dbCommand.Parameters["@CEP"].Value = ClienteEndereco.CEP ?? "";
                        dbCommand.Parameters["@Bairro"].Value = ClienteEndereco.Bairro ?? "";
                        dbCommand.Parameters["@Cidade"].Value = ClienteEndereco.Cidade ?? "";
                        dbCommand.Parameters["@PaisSAP"].Value = ClienteEndereco.PaisSAP ?? "";
                        dbCommand.Parameters["@EstadoSAP"].Value = ClienteEndereco.EstadoSAP ?? "";
                        dbCommand.Parameters["@MunicipioSAP"].Value = ClienteEndereco.MunicipioSAP ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos endereços dos clientes do SAP.";
            }

            return erro;
        }
    }
}