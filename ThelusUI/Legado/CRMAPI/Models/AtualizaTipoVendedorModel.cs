using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaTipoVendedorModel
    {
        public string CodigoTipoSAP { get; set; }

        List<TipoVendedorClass> TiposVendedor = new List<TipoVendedorClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaTiposVendedor()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select Code as CodigoTipoSAP, [Name] DescricaoTipo from [@MF_TIPO_VENDEDOR] ");

                stringSQL.AppendLine("where (Code = '" + CodigoTipoSAP + "' or '' = '" + CodigoTipoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                TiposVendedor = objUtilClass.ConvertDataTable<TipoVendedorClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os tipos de vendedor.");
            }
        }

        public string AtualizaTiposVendedor()
        {
            string erro = "";

            try
            {
                CarregaTiposVendedor();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (TipoVendedorClass TipoVendedor in TiposVendedor)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_TIPO_VENDEDOR", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoTipoSAP", SqlDbType.VarChar, 50, "@CodigoTipoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoTipo", SqlDbType.VarChar, 100, "@DescricaoTipo"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoTipoSAP"].Value = TipoVendedor.CodigoTipoSAP ?? "";
                        dbCommand.Parameters["@DescricaoTipo"].Value = TipoVendedor.DescricaoTipo ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;
                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos tipos de vendedor.";
            }

            return erro;
        }
    }
}