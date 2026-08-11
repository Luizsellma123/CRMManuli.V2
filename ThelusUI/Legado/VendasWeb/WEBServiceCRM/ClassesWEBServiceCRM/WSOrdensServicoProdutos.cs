using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSOrdensServicoProdutos : GerencialVendas.clsConexao
    {
        #region Campos Principais

        public int IDEmpresa { get; set; }
        public int IDOrdemServico { get; set; }
        public int IDITemSAP { get; set; }
        public int DocEntry { get; set; }
        public int IDDeposito { get; set; }
        public string NumeroOrdemProducaoSAP { get; set; }
        public string Cliente { get; set; }
        public string Empresa { get; set; }
        public int NumeroPedidoSAP { get; set; }
        public string StatusPedidoSAP { get; set; }
        public string NumeroPedidoCRM { get; set; }
        public string StatusPedidoCRM { get; set; }
        public string DataEmissao { get; set; }
        public string DataEntrega { get; set; }
        public string EmbarqueImediato { get; set; }
        public string NomeVendedor { get; set; }
        public string Produto { get; set; }
        public string ProdutoRelacional { get; set; }
        public string QuantidadePedido { get; set; }
        public string QuantidadePlanejada { get; set; }
        public string StatusOrdemProducao { get; set; }
        public string Deposito { get; set; }
        public string ItensFormatado { get; set; }
        public string DepositoRelacional { get; set; }

        #endregion

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        public void RecuperaOrdensServicoProdutoCabecalho()
        {
            DataTable OBJDataTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_ORDENS_SERVICO_PRODUTO_CABECALHO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDITemSAP", SqlDbType.Int, 0, "IDITemSAP"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.IDOrdemServico;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                    dbCommand.Parameters["@IDITemSAP"].Value = this.IDITemSAP;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        OBJDataTable.Load(dataReader);
                    }
                }

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        this.Cliente = Convert.ToString(row["Cliente"]);
                        this.Empresa = Convert.ToString(row["Empresa"]);
                        this.NumeroPedidoSAP = Convert.ToInt32(row["NumeroPedidoSAP"]);
                        this.StatusPedidoSAP = Convert.ToString(row["StatusPedidoSAP"]);
                        this.NumeroPedidoCRM = Convert.ToString(row["NumeroPedidoCRM"]);
                        this.StatusPedidoCRM = Convert.ToString(row["StatusPedidoCRM"]);
                        this.DataEmissao = Convert.ToString(row["DataEmissao"]);
                        this.DataEntrega = Convert.ToString(row["DataEntrega"]);
                        this.EmbarqueImediato = Convert.ToString(row["EmbarqueImediato"]);
                        this.NomeVendedor = Convert.ToString(row["NomeVendedor"]);
                        this.Produto = Convert.ToString(row["Produto"]);
                        this.StatusOrdemProducao = Convert.ToString(row["StatusOrdemProducao"]);
                        this.NumeroOrdemProducaoSAP = Convert.ToString(row["NumeroOrdemProducaoSAP"]);
                        this.Deposito = Convert.ToString(row["Deposito"]);
                        this.ProdutoRelacional = Convert.ToString(row["ProdutoRelacional"]);
                        this.DepositoRelacional = Convert.ToString(row["DepositoRelacional"]);
                    }
                }
            }

            catch (Exception ex)
            {
                string erro = ex.ToString();
            }
        }


        public void RecuperaOrdensServicoProdutoEstrutura()
        {
            DataTable OBJDataTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_ORDENS_SERVICO_PRODUTO_ESTRUTURA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@DocEntry", SqlDbType.Int, 0, "DocEntry"));

                    dbCommand.Parameters["@DocEntry"].Value = this.DocEntry;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        OBJDataTable.Load(dataReader);
                    }
                }

                //Adiciona Dados na classe
                if (OBJDataTable.Rows.Count > 0)
                {
                    this.ItensFormatado = "";

                    this.ItensFormatado += "<table class=\"table table-condensed table-responsive\">";
                    this.ItensFormatado += "<thead>";
                    this.ItensFormatado += "<tr class=\"bg-gray-light\">";
                    this.ItensFormatado += "<th>Componentes:</th>";
                    this.ItensFormatado += "<th>Planejada</th>";
                    this.ItensFormatado += "</tr>";

                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        this.ItensFormatado += "<tr class=\"bg-gray-light\">";
                        this.ItensFormatado += "<th>" + row["Produto"].ToString() + "</th>";
                        this.ItensFormatado += "<th>" + row["QuantidadePlanejada"].ToString() + "</th>";
                        this.ItensFormatado += "</tr>";
                    }

                    this.ItensFormatado += "</thead>";
                    this.ItensFormatado += "</table>";

                }

            }

            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

        }
    }
}