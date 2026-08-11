using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WsHubClienteFiscalClass
    {

        public string cnpj { get; set; }
        public string inscricaoEstadual { get; set; }
        public string cnae { get; set; }
        public string suframa { get; set; }
        public string address { get; set; }
        public string tipoEndereco { get; set; }





        public List<WsHubClienteFiscalClass> ExportaDadosClienteFiscal(int _IDCliente)
        {
            string Retorno = "";

            List<WsHubClienteFiscalClass> ListWsHubClienteFiscalClass = new List<WsHubClienteFiscalClass>();
            WsHubClienteFiscalClass ObjWsHubClienteFiscalClass = new WsHubClienteFiscalClass();

            clsConexao ObjclsConexao = new clsConexao();

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(ObjclsConexao.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXPORTA_CLIENTE_FISCAL", dbConnection);

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

                                ObjWsHubClienteFiscalClass = new WsHubClienteFiscalClass();

                                ObjWsHubClienteFiscalClass.cnpj = row["cnpj"].ToString();
                                ObjWsHubClienteFiscalClass.inscricaoEstadual = row["inscricaoEstadual"].ToString();
                                ObjWsHubClienteFiscalClass.cnae = row["cnae"].ToString();
                                ObjWsHubClienteFiscalClass.cnpj = row["cnpj"].ToString();
                                ObjWsHubClienteFiscalClass.suframa = row["suframa"].ToString();
                                ObjWsHubClienteFiscalClass.address = row["address"].ToString();
                                ObjWsHubClienteFiscalClass.tipoEndereco = row["tipoEndereco"].ToString();

                                ListWsHubClienteFiscalClass.Add(ObjWsHubClienteFiscalClass);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Retorno = ex.Message;
            }


            return ListWsHubClienteFiscalClass;

        }

    }
}