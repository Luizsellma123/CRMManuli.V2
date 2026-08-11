using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class CarteiraClasse : clsConexao
    {
        public int IDCliente { get; set; }

        public DataTable CarregaCarteiraDetalheCliente()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CARTEIRA_DETALHE_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    
                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;

        }

        public DataTable CarregaCarteiraVendedorCliente()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CARTEIRA_VENDEDOR_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));


                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;

        }
    }
}