using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace VendasWeb.GerencialVendas
{
    public class clsDocumentosWeb : clsConexao
    {

        public int UserDocumentoID { get; set; }
        public string UserDocXUsuarioID { get; set; }
        public string Url { get; set; }
        public string Nome { get; set; }
        public string Usucod { get; set; }


        public DataTable Insere_Documento()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();


                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_insere_documento", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@Url", SqlDbType.VarChar, 8000, "Url"));
                dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));


                dbCommand.Parameters["@Url"].Value = Url;
                dbCommand.Parameters["@Nome"].Value = Nome;



                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();

            }

            return outputTable;
        }

        public DataTable Deleta_Documento()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();


                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_deleta_documento", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@UserDocumentoID", SqlDbType.Int, 0, "UserDocumentoID"));

                dbCommand.Parameters["@UserDocumentoID"].Value = UserDocumentoID;



                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();



            }

            return outputTable;
        }

        public DataTable Mostra_Documento()
        {

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("user_sp_mostra_documento", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;


                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();



            }

            return outputTable;
        }

        public DataTable Mostra_Documento_Usuario()
        {

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("User_Sp_Consulta_Documento_Web_Usuario", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@Usucod", SqlDbType.VarChar, 100, "Usucod"));
                dbCommand.Parameters.Add(new SqlParameter("@UserDocumentoID", SqlDbType.Int, 0, "UserDocumentoID"));

                dbCommand.Parameters["@Usucod"].Value = Usucod;
                dbCommand.Parameters["@UserDocumentoID"].Value = UserDocumentoID;

                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();



            }

            return outputTable;
        }

        public DataTable Insere_Documento_Usuario()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();


                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("User_Sp_Insere_Documento_Web_x_Usuario", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;


                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@UserDocumentoID", SqlDbType.Int, 150, "UserDocumentoID"));

                dbCommand.Parameters["@UsuCod"].Value = Usucod;
                dbCommand.Parameters["@UserDocumentoID"].Value = UserDocumentoID;


                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();

            }

            return outputTable;
        }

        public DataTable Remove_Documento_Usuario()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();


                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("Remove_User_Documento_Web_x_Usuario", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@UserDocXUsuarioID", SqlDbType.VarChar, 150, "UserDocXUsuarioID"));



                dbCommand.Parameters["@UserDocXUsuarioID"].Value = UserDocXUsuarioID;




                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();

            }

            return outputTable;
        }

    }
}