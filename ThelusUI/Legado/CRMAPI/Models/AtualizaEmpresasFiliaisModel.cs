using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaEmpresasFiliaisModel
    {
        public string CodigoSAP { get; set; }

        List<EmpresasFiliaisClass> EmpresasFiliais = new List<EmpresasFiliaisClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaEmpresasFiliais()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select BPLId CodigoSAP, BPLName NomeEmpresa, TaxIdNum CNPJ from OBPL ");

                stringSQL.AppendLine("where (BPLId = '" + CodigoSAP + "' or '' = '" + CodigoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                EmpresasFiliais = objUtilClass.ConvertDataTable<EmpresasFiliaisClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar as filiais da empresas do SAP.");
            }
        }

        public string AtualizaEmpresasFiliais()
        {
            string erro = "";

            try
            {
                CarregaEmpresasFiliais();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (EmpresasFiliaisClass EmpresaFilial in EmpresasFiliais)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_EMPRESA_FILIAL", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeEmpresa", SqlDbType.VarChar, 100, "NomeEmpresa"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 32, "CNPJ"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = EmpresaFilial.CodigoSAP;
                        dbCommand.Parameters["@NomeEmpresa"].Value = EmpresaFilial.NomeEmpresa ?? "";
                        dbCommand.Parameters["@CNPJ"].Value = EmpresaFilial.CNPJ ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação das filiais da empresas.";
            }

            return erro;
        }
    }
}