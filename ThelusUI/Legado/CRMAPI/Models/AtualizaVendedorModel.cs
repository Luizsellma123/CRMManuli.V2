using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaVendedorModel
    {
        public string CodigoVendedorSAP { get; set; }

        List<VendedorClass> Vendedores = new List<VendedorClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaVendedores()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select SlpName NomeVendedor, SlpCode CodigoVendedorSAP, ");

                stringSQL.AppendLine("isnull(U_MF_Classe_Vendedor,'0000056') ClasseVendedor, ");

                stringSQL.AppendLine("ISNULL(Email,'') EmailVendedor, ISNULL(U_MF_TIPO_VENDEDOR,'') ");

                stringSQL.AppendLine("TipoVendedor, (case when Active='Y' THEN 'Ativo' ELSE 'Inativo' END) StatusVendedor ");

                stringSQL.AppendLine("from OSLP ");

                stringSQL.AppendLine("where (SlpCode = '" + CodigoVendedorSAP + "' or '' = '" + CodigoVendedorSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                Vendedores = objUtilClass.ConvertDataTable<VendedorClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar os vendedores do SAP.");
            }
        }

        public string AtualizaVendedores()
        {
            string erro = "";

            try
            {
                CarregaVendedores();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (VendedorClass Vendedor in Vendedores)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_VENDEDOR", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoVendedorSAP", SqlDbType.Int, 0, "CodigoVendedorSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeVendedor", SqlDbType.NVarChar, 155, "NomeVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@ClasseVendedor", SqlDbType.NVarChar, 50, "ClasseVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@EmailVendedor", SqlDbType.NVarChar, 100, "EmailVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoVendedorSAP", SqlDbType.NVarChar, 50, "TipoVendedorSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@StatusVendedor", SqlDbType.NVarChar, 50, "StatusVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoVendedorSAP"].Value = Convert.ToInt64(Vendedor.CodigoVendedorSAP);
                        dbCommand.Parameters["@NomeVendedor"].Value = Vendedor.NomeVendedor ?? "";
                        dbCommand.Parameters["@ClasseVendedor"].Value = Vendedor.ClasseVendedor ?? "";
                        dbCommand.Parameters["@EmailVendedor"].Value = Vendedor.EmailVendedor ?? "";
                        dbCommand.Parameters["@TipoVendedorSAP"].Value = Vendedor.TipoVendedor ?? "";
                        dbCommand.Parameters["@StatusVendedor"].Value = Vendedor.StatusVendedor ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos vendedores.";
            }

            return erro;
        }
    }
}