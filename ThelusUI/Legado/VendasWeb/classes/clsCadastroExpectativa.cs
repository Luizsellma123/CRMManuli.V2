using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.GerencialVendas
{
    public class clsCadastroExpectativa : clsConexao
    {
        public string VendCod { get; set; }
        public string VendCodNum { get; set; }
        public string VendClasseCod { get; set; }
        public string LinhaProduto { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
        public int ID_Expectativa { get; set; }



        public DataTable Lista_Expectativa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_Busca_Expectativas", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    /*
                    dbCommand.Parameters.Add(new SqlParameter("@VendNome", SqlDbType.VarChar, 100, "VendNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 100, "LinhaProduto"));


                    dbCommand.Parameters["@LinhaProduto"].Value = this.Dashboard;

                    dbCommand.Parameters["@VendNome"].Value = this.UsuCod;


                    */

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;

        }



        public DataTable Linha_Lista()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("User_SP_lista_expectativa_linhas", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 10, "VendClasseCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 20, "LinhaProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@Mes", SqlDbType.VarChar, 100, "Mes"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ano", SqlDbType.VarChar, 100, "Ano"));



                    dbCommand.Parameters["@VendClasseCod"].Value = this.VendClasseCod;
                    dbCommand.Parameters["@LinhaProduto"].Value = this.LinhaProduto;
                    dbCommand.Parameters["@Mes"].Value = this.Mes;
                    dbCommand.Parameters["@Ano"].Value = this.Ano;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;

        }



        //Vou deixar aqui oq vc fez de eletar para nao perder
        public DataTable Linha_Deleta()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_Deleta_Linha_Expectativas", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@ID_Expectativa", SqlDbType.Int, 0, "ID_Expectativa"));


                    dbCommand.Parameters["@ID_Expectativa"].Value = this.ID_Expectativa;



                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;

        }


        public DataTable Classe_Lista()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("User_SP_lista_Classe_lista", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 10, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 20, "LinhaProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@Mes", SqlDbType.VarChar, 100, "Mes"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ano", SqlDbType.VarChar, 100, "Ano"));



                    dbCommand.Parameters["@VendCod"].Value = this.VendCod;
                    dbCommand.Parameters["@LinhaProduto"].Value = this.LinhaProduto;
                    dbCommand.Parameters["@Mes"].Value = this.Mes;
                    dbCommand.Parameters["@Ano"].Value = this.Ano;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;

        }



        public DataTable Classe_Deleta()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_Deleta_Classe_Expectativas", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@ID_Expectativa", SqlDbType.Int, 0, "ID_Expectativa"));


                    dbCommand.Parameters["@ID_Expectativa"].Value = this.ID_Expectativa;



                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;

        }



    }
}