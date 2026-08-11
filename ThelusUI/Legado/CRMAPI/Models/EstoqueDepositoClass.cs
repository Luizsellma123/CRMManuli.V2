using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class EstoqueDepositoClass : ConexaoClass
    {
        public string CodigoDepositoSAP { get; set; }
        public bool ImportaTodos { get; set; }

        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        DataTable OBJDataTable = new DataTable();

        public string ImportacaoDepositosSAP()
        {
            string erro = "";

            //Atribui variavel Global para local 
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }

            #region Importa Depositos 
            OBJDataTable = this.RecuperaEstoqueDepositosSAP();

            if (OBJDataTable.Rows.Count > 0)
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    dbConnection.Open();
                    try
                    {
                        foreach (DataRow row in OBJDataTable.Rows)
                        {
                            SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_DEPOSITOS_MATERIAL", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoEmpresaSAP", SqlDbType.Int, 0, "CodigoEmpresaSAP"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoDepositoSAP", SqlDbType.NVarChar, 8, "CodigoDepositoSAP"));
                            dbCommand.Parameters.Add(new SqlParameter("@NomeDepositoSAP", SqlDbType.NVarChar, 100, "NomeDepositoSAP"));
                            dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                            dbCommand.Parameters["@CodigoEmpresaSAP"].Value = Convert.ToString(row["CodigoEmpresaSAP"]);
                            dbCommand.Parameters["@CodigoDepositoSAP"].Value = Convert.ToString(row["CodigoDepositoSAP"]); 
                            dbCommand.Parameters["@NomeDepositoSAP"].Value = Convert.ToString(row["NomeDepositoSAP"]); 

                            dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                            dbCommand.ExecuteNonQuery();

                            erro = (string)dbCommand.Parameters["@vErro"].Value;

                        }
                    }
                    catch (Exception ex)
                    {
                        erro = "Erro atualização depósito.";
                    }
                }
            }

            #endregion

            return erro;
        }

        public DataTable RecuperaEstoqueDepositosSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select isnull(BPLid,'') CodigoEmpresaSAP, WhsCode CodigoDepositoSAP, WhsName NomeDepositoSAP from OWHS WHERE ''='' ";

            if (this.CodigoDepositoSAP != "" && this.CodigoDepositoSAP != null)
            {
                StringSQL += "AND OWHS.WhsCode='" + this.CodigoDepositoSAP + "' ";
            }

            if (ImportaTodos == false)
            {
                StringSQL += "AND convert(date, isnull(OWHS.UpdateDate, OWHS.CreateDate))= convert(date, Dateadd(day, -1, getdate()))";
            }

            OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }
    }
}