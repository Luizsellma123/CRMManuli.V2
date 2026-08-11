using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;
namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WsHubClienteContatoClass
    {

        public string id { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }


        public List<WsHubClienteContatoClass> ExportaDadosClienteContato(int _IDCliente)
        {
            string Retorno = "";

            List<WsHubClienteContatoClass> ListWsHubClienteContatoClass = new List<WsHubClienteContatoClass>();
            WsHubClienteContatoClass ObjWsHubClienteContatoClass = new WsHubClienteContatoClass();

            clsConexao ObjclsConexao = new clsConexao();

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(ObjclsConexao.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXPORTA_CLIENTE_CONTATO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = _IDCliente;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {

                                ObjWsHubClienteContatoClass = new WsHubClienteContatoClass();

                                ObjWsHubClienteContatoClass.id = row["id"].ToString();
                                ObjWsHubClienteContatoClass.nome = row["nome"].ToString();
                                ObjWsHubClienteContatoClass.telefone = row["telefone"].ToString();
                                ObjWsHubClienteContatoClass.email = row["email"].ToString();

                                ListWsHubClienteContatoClass.Add(ObjWsHubClienteContatoClass);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Retorno = ex.Message;
            }


            return ListWsHubClienteContatoClass;

        }


    }
}