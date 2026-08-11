using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class FechamentoFaturaModel : ConexaoClass
    {
        public int IDEmpresa { get; set; }
        public int IDFechamentoFatura { get; set; }
        public string CodigoUsuarioCRM { get; set; }

        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private int IDNota { get; set; }

        public string GravaFechamentoFatura()
        {
            string erro = "";
            DataTable OBJDataFechamentoFatura = new DataTable();

            //Atribui variavel Global para local 
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }

            if (erro == "")
            {

                OBJDataFechamentoFatura = this.RecuperaDadosFechamentoFatura(ref erro);

                if (erro == "")
                {

                    if (OBJDataFechamentoFatura.Rows.Count > 0)
                    {
                        foreach (DataRow row in OBJDataFechamentoFatura.Rows)
                        {
                            if (erro == "")
                            {
                                OBJComunicacaoServiceLayerSAP.NumeroPrimarioNotaSAP = Convert.ToInt32(row["PrimarioNotaSAP"]);
                                OBJComunicacaoServiceLayerSAP.NumeroFatura = Convert.ToString(row["NumeroFatura"]);
                                OBJComunicacaoServiceLayerSAP.DataVencimentoFatura = Convert.ToDateTime(row["DataVencimento"]);

                                erro = OBJComunicacaoServiceLayerSAP.GravaDadosFaturaNotaSAP();

                                if (erro == "")
                                {
                                    this.IDNota = Convert.ToInt32(row["IDNota"]);
                                    erro = this.AtualizaDadosFechamentoNota();
                                }
                            }
                        }

                        if (erro == "")
                        {
                            erro = this.AtualizaDadosFechamento();
                        }

                    }
                    else
                    {
                        erro = "Não existem dados para fechamento informado.";
                    }
                }
            }

            return erro;
        }

        public DataTable RecuperaDadosFechamentoFatura(ref string erro)
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_API_RETORNA_FECHAMENTO_FATURA_NOTAS", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDFechamento", SqlDbType.Int, 0, "IDFechamento"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDFechamento"].Value = this.IDFechamentoFatura;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = "erro ao recuperar fechamento de faturas.";
            }


            return outputTable;
        }

        public string AtualizaDadosFechamentoNota()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_API_GRAVA_FECHAMENTO_FATURA_NOTAS_GERACAO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDFechamento", SqlDbType.Int, 0, "@IDFechamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDNota", SqlDbType.Int, 0, "@IDNota"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDFechamento"].Value = this.IDFechamentoFatura;
                    dbCommand.Parameters["@IDNota"].Value = this.IDNota;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuarioCRM;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização das notas do fechamento da fatura.";
                }
            }

            return erro;
        }

        public string AtualizaDadosFechamento()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("[CRM_API_GRAVA_FECHAMENTO_FATURA_GERACAO_SAP]", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDFechamento", SqlDbType.Int, 0, "@IDFechamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDFechamento"].Value = this.IDFechamentoFatura;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuarioCRM;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização do fechamento da fatura.";
                }
            }

            return erro;
        }


    }
}