using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WsHubClienteFormaPagamentoClass
    {
        public string codFormaPagamento { get; set; }


        public List<WsHubClienteFormaPagamentoClass> ExportaDadosClienteFormaPagamento(int _IDCliente)
        {
            string Retorno = "";

            List<WsHubClienteFormaPagamentoClass> ListWsHubClienteFormaPagamentoClass = new List<WsHubClienteFormaPagamentoClass>();
            WsHubClienteFormaPagamentoClass ObjWsHubClienteFormaPagamentoClass = new WsHubClienteFormaPagamentoClass();

            clsConexao ObjclsConexao = new clsConexao();

            DataTable outputTable = new DataTable();
            
           
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(ObjclsConexao.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXPORTA_CLIENTE_FORMAS_PAGAMENTO", dbConnection);

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

                                ObjWsHubClienteFormaPagamentoClass = new WsHubClienteFormaPagamentoClass();

                                ObjWsHubClienteFormaPagamentoClass.codFormaPagamento = row["codFormaPagamento"].ToString();

                                ListWsHubClienteFormaPagamentoClass.Add(ObjWsHubClienteFormaPagamentoClass);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Retorno = ex.Message;
            }

            

            return ListWsHubClienteFormaPagamentoClass;

        }
    }
}