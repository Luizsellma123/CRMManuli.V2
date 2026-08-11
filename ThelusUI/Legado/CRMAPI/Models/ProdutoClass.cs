using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class ProdutoClass : ConexaoClass
    {
        public string CodigoProdutoSAP { get; set; }
        public bool ImportaTodos { get; set; }

        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        DataTable OBJDataTable = new DataTable();

        public string ImportacaoDepositoPadraoProdutos()
        {
            string erro = "";

            //Atribui variavel Global para local 
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }

            #region Importa Depositos Padrão
            OBJDataTable = this.RecuperaDepositosPadraoProdutoSAP();

            if (OBJDataTable.Rows.Count > 0)
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    dbConnection.Open();
                    try
                    {
                        foreach (DataRow row in OBJDataTable.Rows)
                        {
                            SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_DEPOSITOS_PADRAO_PRODUTOS", dbConnection);

                            dbCommand.CommandType = CommandType.StoredProcedure;
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoDepositoSAP", SqlDbType.VarChar, 100, "CodigoDepositoSAP"));
                            dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 8000, "CodigoProdutoSAP"));
                            dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                            dbCommand.Parameters["@CodigoDepositoSAP"].Value = Convert.ToString(row["CodigoDepositoSAP"]);
                            dbCommand.Parameters["@CodigoProdutoSAP"].Value = Convert.ToString(row["CodigoProdutoSAP"]);

                            dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                            dbCommand.ExecuteNonQuery();

                            erro = (string)dbCommand.Parameters["@vErro"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        erro = "Erro atualização depósito padrão.";
                    }
                }
            }

            #endregion

            return erro;
        }

        public DataTable RecuperaDepositosPadraoProdutoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select ToWH CodigoDepositoSAP, Code CodigoProdutoSAP from OITT WHERE ''='' ";

            if (this.CodigoProdutoSAP != "" && this.CodigoProdutoSAP != null)
            {
                StringSQL += "AND OITT.Code='" + this.CodigoProdutoSAP + "' ";
            }

            if (ImportaTodos == false)
            {
                StringSQL += "AND convert(date, isnull(OITT.UpdateDate, OITT.CreateDate))= convert(date, Dateadd(day, -1, getdate()))";
            }

            OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }
    }
}