using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;
using System.Data;
using System.Threading.Tasks;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClassePedidoInclusao : clsConexao
    {
        public int BPL_IDAssignedToInvoice { get; set; }
        //public string DocObjectCode { get; set; }
        public string cod_cliente { get; set; }
        public string cod_esboco { get; set; }
        public int cod_vendedor { get; set; }
        public int cond_pag { get; set; }
        public int crm_cod_pedido { get; set; }
        public DateTime data_entrega { get; set; }
        public DateTime data_lancamento { get; set; }
        public string descricao { get; set; }
        public string num_ref_cliente { get; set; }
        public string obs_nf { get; set; }
        public string ped_cliente { get; set; }

        //Campos do CRM
        public int IDEmpresa { get; set; }
        public int IDPedido { get; set; }
        public string CodigoUsuarioCRM { get; set; }

        public List<WSClassePedidoInclusaoTransportadora> TaxExtension { get; set; }
        public List<WSClassePedidoInclusaoItens> Document_Lines { get; set; }
        public List<WSClassePedidoInclusaoDespesas> DocumentsAdditionalExpenses { get; set; }

        //Metodo para inserir dados items do pedido
        public void incluiTransportadora(string tipo_frete, string cod_transp)
        {
            WSClassePedidoInclusaoTransportadora PedidoTransportadora = new WSClassePedidoInclusaoTransportadora();

            //Atribuição de valores
            PedidoTransportadora.tipo_frete = RecuperaCodigoSAP(tipo_frete).ToString();
            PedidoTransportadora.cod_transp = cod_transp;

            //Verifica se esta instanciado
            if (this.TaxExtension == null)
            {
                this.TaxExtension = new List<WSClassePedidoInclusaoTransportadora>();
            }
            this.TaxExtension.Add(PedidoTransportadora);
        }

        //Metodo para inserir dados items do pedido
        public void incluiItens(string cod_item, decimal quantidade, decimal preco, string Usage,
            string cod_uni_med, string nome_uni_med, string texto_livre,
            string nat_dest, string cliche_prod, string arruela, string xPedPedido, string nItemPedido,
            string CodigoDepositoSAP)
        {
            WSClassePedidoInclusaoItens PedidoItens = new WSClassePedidoInclusaoItens();

            //Atribuição de valores
            PedidoItens.cod_item = cod_item;
            PedidoItens.quantidade = Convert.ToDouble(quantidade);
            PedidoItens.preco = Convert.ToDouble(preco);
            PedidoItens.Usage = Convert.ToInt32(Usage);
            PedidoItens.cod_uni_med = cod_uni_med;
            PedidoItens.nome_uni_med = nome_uni_med;
            PedidoItens.texto_livre = texto_livre ?? "";
            PedidoItens.nat_dest = nat_dest;
            PedidoItens.cliche_prod = cliche_prod;
            PedidoItens.arruela = arruela;
            PedidoItens.xPed = xPedPedido;
            PedidoItens.nItem = nItemPedido;
            PedidoItens.Dep = CodigoDepositoSAP;

            //Verifica se esta instanciado
            if (this.Document_Lines == null)
            {
                this.Document_Lines = new List<WSClassePedidoInclusaoItens>();
            }
            this.Document_Lines.Add(PedidoItens);
        }

        //Metodo para inserir dados items do pedido
        public void incluiDespesas(string ExpenseCode, string valor_frete)
        {
            WSClassePedidoInclusaoDespesas PedidoDespesas = new WSClassePedidoInclusaoDespesas();

            //Atribuição de valores
            PedidoDespesas.ExpenseCode = ExpenseCode;
            PedidoDespesas.valor_frete = valor_frete;

            //Verifica se esta instanciado
            if (this.DocumentsAdditionalExpenses == null)
            {
                this.DocumentsAdditionalExpenses = new List<WSClassePedidoInclusaoDespesas>();
            }
            this.DocumentsAdditionalExpenses.Add(PedidoDespesas);
        }
        /*
        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }
            return response;
        }
        */

        public int RecuperaCodigoSAP(string tipo_frete)
        {
            DataTable outputTable = new DataTable();

            int CodigoSAP = 0;
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RECUPERA_CODIGO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@tipo_frete", SqlDbType.VarChar, 8000, "tipo_frete"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "CodigoSAP", DataRowVersion.Default, null));

                    dbCommand.Parameters["@tipo_frete"].Value = tipo_frete;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    CodigoSAP = (int)dbCommand.Parameters["@CodigoSAP"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return CodigoSAP;
        }

    }
}