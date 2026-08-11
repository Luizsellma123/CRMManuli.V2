using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaDepositosMaterialModel
    {
        public string CodigoDepositoSAP { get; set; }

        List<DepositosMaterialClass> DepositosMaterial = new List<DepositosMaterialClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaDepositosMaterial()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select isnull(BPLid,0) CodigoEmpresaSAP, ");
                stringSQL.AppendLine("WhsCode CodigoDepositoSAP, WhsName NomeDepositoSAP from OWHS ");
                stringSQL.AppendLine("WHERE convert(date, isnull(OWHS.UpdateDate, OWHS.CreateDate)) = convert(date, getdate()) ");                                                 
                stringSQL.AppendLine("and (BPLid = '" + CodigoDepositoSAP + "' or '' = '" + CodigoDepositoSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                DepositosMaterial = objUtilClass.ConvertDataTable<DepositosMaterialClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os depositos de material do SAP.");
            }
        }

        public string AtualizaDepositosMaterial()
        {
            string erro = "";

            try
            {
                CarregaDepositosMaterial();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (DepositosMaterialClass Deposito in DepositosMaterial)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_DEPOSITOS_MATERIAL", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoEmpresaSAP", SqlDbType.Int, 0, "CodigoEmpresaSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoDepositoSAP", SqlDbType.NVarChar, 8, "CodigoDepositoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeDepositoSAP", SqlDbType.NVarChar, 100, "NomeDepositoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoEmpresaSAP"].Value = Deposito.CodigoEmpresaSAP;
                        dbCommand.Parameters["@CodigoDepositoSAP"].Value = Deposito.CodigoDepositoSAP ?? "";
                        dbCommand.Parameters["@NomeDepositoSAP"].Value = Deposito.NomeDepositoSAP ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos depósitos materiais.";
            }

            return erro;
        }
    }
}