using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class clsEstadosFretes : clsConexao
    {  
        
        public string EstadoOrigem { get; set; }
        public string EstadoDestino { get; set; }   
        public float PercentualDesconto { get; set; }
       
        public DataTable Lista_Estados()
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_LISTA_FRETES_ESTADO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EstadoOrigem", SqlDbType.VarChar,8, "EstadoOrigem"));
                    dbCommand.Parameters.Add(new SqlParameter("@EstadoDestino", SqlDbType.VarChar,8, "EstadoDestino"));

                    dbCommand.Parameters["@EstadoOrigem"].Value = this.EstadoOrigem;
                    dbCommand.Parameters["@EstadoDestino"].Value = this.EstadoDestino;


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


        public DataTable Retorna_Estado()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_ESTADOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                   
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


        public DataTable Altera_Percentual_Estado()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_ALTERA_PERCENTUAL_FRETES_ESTADO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EstadoOrigem", SqlDbType.VarChar, 8, "EstadoOrigem"));
                dbCommand.Parameters.Add(new SqlParameter("@EstadoDestino", SqlDbType.VarChar, 8, "EstadoDestino"));
                dbCommand.Parameters.Add(new SqlParameter("@PercentualEstado", SqlDbType.Decimal, 0, "PercentualEstado"));
                
                dbCommand.Parameters["@EstadoOrigem"].Value = this.EstadoOrigem;
                dbCommand.Parameters["@EstadoDestino"].Value = this.EstadoDestino;
                dbCommand.Parameters["@PercentualEstado"].Value = this.PercentualDesconto;
              
              


                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

               SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                return outputTable;
            }
        }





       
    }

}
