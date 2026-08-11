using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSClienteDetalhes : clsConexao
    {
        public int IDCliente { get; set; }
        public string Cliente { get; set; }
        public string Cidade { get; set; }
        public string Vendedor { get; set; }
        public string Classe { get; set; }
        public string UltimoHistorico { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }

        public DataTable CarregaCarteiraDetalheCliente()
        {
            string erro = "";

            DataTable OBJDataTable = new DataTable();

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
                        OBJDataTable.Load(dataReader);
                    }

                    if (OBJDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in OBJDataTable.Rows)
                        {
                            this.Cliente = Convert.ToString(row["NomeCliente"]);
                            this.Cidade = Convert.ToString(row["Cidade"]);
                            this.UltimoHistorico = Convert.ToString(row["Historico"]);
                            this.CNPJ = Convert.ToString(row["CNPJ"]);
                            this.Telefone = Convert.ToString(row["Telefone"]);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return OBJDataTable;

        }

        public DataTable CarregaCarteiraVendedorCliente()
        {
            string erro = "";

            DataTable OBJDataTable = new DataTable();

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
                        OBJDataTable.Load(dataReader);
                    }

                    if (OBJDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in OBJDataTable.Rows)
                        {
                            this.Vendedor = Convert.ToString(row["NomeVendedor"]);
                            this.Classe = Convert.ToString(row["NomeClasse"]);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return OBJDataTable;

        }
    }
}