using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class clsFretesEstado : clsConexao 
    {
        public string CodigoCidade { get; set;}
        public string NomeCidade { get; set; }
        public float PercentualFretes { get; set; }

        public DataTable Lista_Cidade()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_LISTA_FRETES_CIDADES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 10, "CidCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidNome",SqlDbType.VarChar,100,"CidNome"));
                    
                    dbCommand.Parameters["@CidCod"].Value = this.CodigoCidade;
                    dbCommand.Parameters["@CidNome"].Value = this.NomeCidade;


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


        public DataTable Altera_Percentual_Cidade()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_ALTERA_PERCENTUAL_FRETES_CIDADE", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 8, "CidCod"));
                dbCommand.Parameters.Add(new SqlParameter("@PercentualCidade", SqlDbType.Decimal, 0, "PercentualCidade"));
               

                dbCommand.Parameters["@CidCod"].Value = this.CodigoCidade;
                dbCommand.Parameters["@PercentualCidade"].Value = this.Percentual;
              

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }


        public object Percentual { get; set; }
    }

}