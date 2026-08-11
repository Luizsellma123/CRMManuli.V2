using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaClassesVendedoresModel
    {
        public string CodigoClasse { get; set; }

        List<ClassesVendedoresClass> ClassesVendedores = new List<ClassesVendedoresClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaClassesVendedores()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select Code CodigoClasse, Name NomeClasse from [@MF_CLASSE_VENDEDOR] ");

                stringSQL.AppendLine("where (Code = '" + CodigoClasse + "' or '' = '" + CodigoClasse + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                ClassesVendedores = objUtilClass.ConvertDataTable<ClassesVendedoresClass>(ConsultaSAP);
            }
            catch //(Exception ex)
            {
                throw new Exception("Erro ao carregar as classes dos vendedores.");
            }
        }

        public string AtualizaClassesVendedores()
        {
            string erro = "";

            try
            {
                CarregaClassesVendedores();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (ClassesVendedoresClass ClasseVendedor in ClassesVendedores)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLASSE_VENDEDOR", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClasseSAP", SqlDbType.NVarChar, 50, "CodigoClasseSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeClasse", SqlDbType.NVarChar, 100, "NomeClasse"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoClasseSAP"].Value = ClasseVendedor.CodigoClasse ?? "";
                        dbCommand.Parameters["@NomeClasse"].Value = ClasseVendedor.NomeClasse ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação das classes dos vendedores.";
            }

            return erro;
        }
    }
}