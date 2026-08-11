using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CRMAPI.Classes
{
    public class UserStoredProcedureManuliFitasaClass
    {
        ComunicacaoSAPClass objComunicacaoSAP = new ComunicacaoSAPClass();

        public string DocDateIni { get; set; }

        public string DocDateFin { get; set; }

        public UserStoredProcedureManuliFitasaClass(string DocDateIni, string DocDateFin)
        {
            this.DocDateIni = DocDateIni;

            this.DocDateFin = DocDateFin;
        }

        public DataTable USP_MF_FATURAMENTO_NOTA_FISCAL()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoSAP.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@DocDateIni", SqlDbType.VarChar, 8000, "DocDateIni"));
                    dbCommand.Parameters.Add(new SqlParameter("@DocDateFin", SqlDbType.VarChar, 8000, "DocDateFin"));

                    dbCommand.Parameters["@DocDateIni"].Value = Convert.ToDateTime(DocDateIni).ToString("yyyy-MM-dd");
                    dbCommand.Parameters["@DocDateFin"].Value = Convert.ToDateTime(DocDateFin).ToString("yyyy-MM-dd");

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable USP_MF_PEDIDOS_PENDENTES_NOVO()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoSAP.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));

                    dbCommand.Parameters["@DataInicial"].Value = Convert.ToDateTime(DocDateIni).ToString("yyyy-MM-dd");
                    dbCommand.Parameters["@DataFinal"].Value = Convert.ToDateTime(DocDateFin).ToString("yyyy-MM-dd");

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable USP_MF_PEDIDOS_PENDENTES_CONV_CAMBIO()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoSAP.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));

                    dbCommand.Parameters["@DataInicial"].Value = Convert.ToDateTime(DocDateIni).ToString("yyyy-MM-dd");
                    dbCommand.Parameters["@DataFinal"].Value = Convert.ToDateTime(DocDateFin).ToString("yyyy-MM-dd");

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable USP_MF_NOTAS_DEVOLUCAO()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoSAP.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand(GetCurrentMethodName(), dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@DocDateIni", SqlDbType.VarChar, 8000, "DocDateIni"));
                    dbCommand.Parameters.Add(new SqlParameter("@DocDateFin", SqlDbType.VarChar, 8000, "DocDateFin"));

                    dbCommand.Parameters["@DocDateIni"].Value = Convert.ToDateTime(DocDateIni).ToString("yyyy-MM-dd");
                    dbCommand.Parameters["@DocDateFin"].Value = Convert.ToDateTime(DocDateFin).ToString("yyyy-MM-dd");

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        static string GetCurrentMethodName()
        {
            // Obtém o stack trace
            StackTrace stackTrace = new StackTrace();

            // Obtém o método atual na pilha de chamadas
            var currentMethod = stackTrace.GetFrame(1).GetMethod();

            // Retorna o nome do método
            return currentMethod.Name;
        }
    }
}