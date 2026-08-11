using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;


namespace VendasWeb.GerencialVendas
{
    public class clsBanner : clsConexao
    {

        public int BannerID { get; set; }
        //public byte[] ImageUrl { get; set; }
        public string ImageUrl { get; set; }
        public string NavigateUrl { get; set; }
        public string AlternateText { get; set; }
        public int Impressions { get; set; }
        public bool? Ativo { get; set; }



        public DataTable Insere_Banner()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();


                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_insere_banner", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@ImageUrl", SqlDbType.VarChar, 8000, "ImageUrl"));
                dbCommand.Parameters.Add(new SqlParameter("@NavigateUrl", SqlDbType.VarChar, 8000, "NavigateUrl"));
                dbCommand.Parameters.Add(new SqlParameter("@AlternateText", SqlDbType.VarChar, 200, "AlternateText"));
                dbCommand.Parameters.Add(new SqlParameter("@Impressions", SqlDbType.Int, 0, "Impressions"));
                dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "Ativo"));


                dbCommand.Parameters["@ImageUrl"].Value = ImageUrl;
                dbCommand.Parameters["@NavigateUrl"].Value = NavigateUrl;
                dbCommand.Parameters["@AlternateText"].Value = AlternateText;
                dbCommand.Parameters["@Impressions"].Value = Impressions;
                dbCommand.Parameters["@Ativo"].Value = Ativo;


                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();

            }

            return outputTable;
        }

        public DataTable Desativa_Banner()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();


                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_desativa_banner", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@BannerID", SqlDbType.Int, 0, "BannerID"));

                dbCommand.Parameters["@BannerID"].Value = BannerID;



                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();

            }

            return outputTable;
        }

        public DataTable Deleta_Banner()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();


                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_deleta_banner", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@BannerID", SqlDbType.Int, 0, "BannerID"));

                dbCommand.Parameters["@BannerID"].Value = BannerID;



                //Aumentando o timeout do command
                dbCommand.CommandTimeout = 999999;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();



            }

            return outputTable;
        }

        public DataTable Mostra_Banner()
        {

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("user_sp_mostra_banner", dbConnection);

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

        public DataTable Atualiza_Sequencia_Banner()
        {

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();


                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_atualiza_sequencia_banner", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@Impressions", SqlDbType.Int, 0, "Impressions"));
                dbCommand.Parameters.Add(new SqlParameter("@BannerID", SqlDbType.Int, 0, "BannerID"));

                dbCommand.Parameters["@Impressions"].Value = Impressions;
                dbCommand.Parameters["@BannerID"].Value = BannerID;



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