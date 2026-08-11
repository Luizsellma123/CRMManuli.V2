using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;


namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WsHubClienteEnderecoClass
    {

        public string id_endereco { get; set; }
        public string tipo_endereco { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string municipio { get; set; }
        public string pais { get; set; }
        public string tipo_logradouro { get; set; }



        public List<WsHubClienteEnderecoClass> ExportaDadosClienteEndereco(int _IDCliente)
        {
            string Retorno = "";

            List<WsHubClienteEnderecoClass> ListWsHubClienteEnderecoClass = new List<WsHubClienteEnderecoClass>();
            WsHubClienteEnderecoClass ObjWsHubClienteEnderecoClass = new WsHubClienteEnderecoClass();

            clsConexao ObjclsConexao = new clsConexao();

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(ObjclsConexao.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXPORTA_CLIENTE_ENDERECO", dbConnection);

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

                                ObjWsHubClienteEnderecoClass = new WsHubClienteEnderecoClass();

                                ObjWsHubClienteEnderecoClass.id_endereco = row["id_endereco"].ToString();
                                ObjWsHubClienteEnderecoClass.tipo_endereco = row["tipo_endereco"].ToString();
                                ObjWsHubClienteEnderecoClass.tipo_logradouro = row["tipo_logradouro"].ToString();
                                ObjWsHubClienteEnderecoClass.rua = row["rua"].ToString();
                                ObjWsHubClienteEnderecoClass.numero = row["numero"].ToString();
                                ObjWsHubClienteEnderecoClass.complemento = row["complemento"].ToString();
                                ObjWsHubClienteEnderecoClass.cep = row["cep"].ToString();
                                ObjWsHubClienteEnderecoClass.bairro = row["bairro"].ToString();
                                ObjWsHubClienteEnderecoClass.cidade = row["Cidade"].ToString();
                                ObjWsHubClienteEnderecoClass.estado = row["estado"].ToString();
                                ObjWsHubClienteEnderecoClass.municipio = row["municipio"].ToString();
                                ObjWsHubClienteEnderecoClass.pais = row["pais"].ToString();
                                
                                ListWsHubClienteEnderecoClass.Add(ObjWsHubClienteEnderecoClass);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Retorno = ex.Message;
            }


            return ListWsHubClienteEnderecoClass;

        }

    }
}