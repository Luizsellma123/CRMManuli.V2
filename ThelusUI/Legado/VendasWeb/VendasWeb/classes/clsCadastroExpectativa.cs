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
        public object UsuCod {get; set;}
        public string VendNome {get; set;}
        public string LinhaProduto {get; set;}
        public string Dashboard {get; set;}
        public string Mes {get; set;}
        public string Ano { get; set; }
        public string QuantidadeExpectativa { get; set;}

        public DataTable Lista_Expectativa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_EXPECTATIVA_LISTA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                    dbCommand.Parameters["@UsuCod"].Value = this.UsuCod;





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