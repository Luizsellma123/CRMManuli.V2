using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class DocumentoPSIUClass : clsConexao
    {
        public string nome { get; set; }
        public string endereco { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public DataTable Insere_Documento()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();


                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_REGISTRA_DOCUMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@NomeDocumento", SqlDbType.VarChar, 300, "NomeDocumento"));
                    dbCommand.Parameters.Add(new SqlParameter("@endereco", SqlDbType.VarChar, 300, "endereco"));


                    dbCommand.Parameters["@NomeDocumento"].Value = nome;
                    dbCommand.Parameters["@endereco"].Value = endereco;



                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();

                }
            }
            catch
            {

            }
            return outputTable;
        }

        public DataTable Exibir_Documento()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_BUSCA_ARQUIVO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 300, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.Date, 0, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.Date, 0, "DataFinal"));


                    dbCommand.Parameters["@Nome"].Value = nome;
                    dbCommand.Parameters["@DataInicial"].Value = DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = DataFinal;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
            }
            catch
            {

            }

            return outputTable;
        }

        public void Deleta_Documento(int ID)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_DELETA_PSIU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 0,"ID"));

                    dbCommand.Parameters["@ID"].Value = ID;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    dataReader.Close();
                }
            }

            catch
            {

            }
        }

    }
}
