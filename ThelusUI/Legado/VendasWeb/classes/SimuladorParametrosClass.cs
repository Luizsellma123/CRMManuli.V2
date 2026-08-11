using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class SimuladorParametrosClass : clsConexao
    {
        public string idparametro { get; set; }
        public string codempresa { get; set; }
        public string alcada { get; set; }
        public string nomeEmpresa { get; set; }
        public string Percentual { get; set; }


        public DataTable Consulta_Parametros()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_BUSCA_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@codempresa", SqlDbType.VarChar, 50, "codempresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@alcada", SqlDbType.VarChar, 300, "alcada"));

                    dbCommand.Parameters["@codempresa"].Value = codempresa;
                    dbCommand.Parameters["@alcada"].Value = alcada;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                }
            }

            catch (Exception ex)
            {

            }
            return outputTable;

        }

        public string Atualiza_Porcentagem()
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_MUDA_PERCENTUAL_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar, 30, "id"));
                    dbCommand.Parameters.Add(new SqlParameter("@percent", SqlDbType.Decimal, 1, "percent"));

                    dbCommand.Parameters["@id"].Value = idparametro;
                    dbCommand.Parameters["@percent"].Value = Percentual;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                }
            }

            catch (Exception ex)
            {
                return ("Erro");
            }
            return ("");
        }

    }
}