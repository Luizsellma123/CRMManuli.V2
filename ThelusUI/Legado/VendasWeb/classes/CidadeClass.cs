using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class CidadeClass : clsConexao
    {
        public string Busca { get; set; }

        public DataTable Lista_Cidade()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Lista_Cidade", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Busca", SqlDbType.VarChar, 100, "Busca"));

                    dbCommand.Parameters["@Busca"].Value = this.Busca;

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