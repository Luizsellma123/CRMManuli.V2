using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class EntConcorrenciaClass : clsConexao
    {
        public int Codigo { get; set; }
        public string EntCod { get; set; }
        public string NomeConcorrente { get; set; }
        public string ObservacaoConcorrente { get; set; }

        public string Inserir_Concorrencia()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_CRM_Concorrencia_Cliente_Inserir", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeConcorrente", SqlDbType.VarChar, 50, "NomeConcorrente"));
                    dbCommand.Parameters.Add(new SqlParameter("@ObservacaoConcorrente", SqlDbType.VarChar, 8000, "ObservacaoConcorrente"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@NomeConcorrente"].Value = NomeConcorrente;
                    dbCommand.Parameters["@ObservacaoConcorrente"].Value = ObservacaoConcorrente;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Concorrencia_Inserir. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Alterar_Concorrencia()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_CRM_Concorrencia_Cliente_Alterar", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeConcorrente", SqlDbType.VarChar, 50, "NomeConcorrente"));
                    dbCommand.Parameters.Add(new SqlParameter("@ObservacaoConcorrente", SqlDbType.VarChar, 8000, "ObservacaoConcorrente"));

                    dbCommand.Parameters["@Codigo"].Value = Codigo;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@NomeConcorrente"].Value = NomeConcorrente;
                    dbCommand.Parameters["@ObservacaoConcorrente"].Value = ObservacaoConcorrente;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Alterar_Concorrente. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Remove_Concorrencia()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_CRM_Concorrencia_Cliente_Remover", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));

                    dbCommand.Parameters["@Codigo"].Value = Codigo;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Remove_Concorrente. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable Consulta_Concorrencia_EntCod()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("User_sp_CRM_Concorrencia_Cliente_Consultar", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                dbCommand.Parameters["@EntCod"].Value = EntCod;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }

        public string Concorrencia_Excluir_Todas()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("User_sp_Tb_CRM_Concorrencia_Excluir_Todos", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Concorrencia_Inserir. Contactar o Suporte!";
            }

            return Retorno;
        }
    }
}