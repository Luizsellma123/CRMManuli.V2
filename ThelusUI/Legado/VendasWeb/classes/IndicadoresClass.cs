using System;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb
{
    public class IndicadoresClass : GerencialVendas.clsConexao
    {
        #region Campos

        public int IDUsuarioResponsavel { get; set; }

        public int IDUsuarioSolicitante { get; set; }

        public string DataInicial { get; set; }

        public string DataFinal { get; set; }
        
        public string Sistema { get; set; }

        #endregion

        #region Procedures

        public DataTable RetornaResponsavel()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_RESPONSAVEL_TI", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaSolicitante()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_SOLICITANTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaStatus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_STATUS_INDICADORES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaSistema()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_SISTEMA_INDICADORES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaListaIndicadoresTI()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_INDICADORES_TI", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioSolicitante", SqlDbType.Int, 0, "IDUsuarioSolicitante"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@Sistema", SqlDbType.VarChar, 8000, "Sistema"));

                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;
                    dbCommand.Parameters["@IDUsuarioSolicitante"].Value = this.IDUsuarioSolicitante;
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    dbCommand.Parameters["@Sistema"].Value = this.Sistema;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

    }
}