using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class clsIncluirExpectativa : clsConexao
    {
        public string VendCod { get; set; }
        public string LinhaProduto { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
        public string Quantidade { get; set; }



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


                    
                    dbCommand.Parameters.Add(new SqlParameter("@VendNome", SqlDbType.VarChar, 100, "VendNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 100, "LinhaProduto"));


                    dbCommand.Parameters["@LinhaProduto"].Value = this.LinhaProduto;

                    dbCommand.Parameters["@VendNome"].Value = this.VendCod;


                    

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



        public DataTable Inclusao_Salvar()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_Salva_Linha_Expectativas", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 10, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 20, "LinhaProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@Mes", SqlDbType.VarChar, 100, "Mes"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ano", SqlDbType.VarChar, 100, "Ano"));
                    dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.VarChar, 8000, "Quantidade"));





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