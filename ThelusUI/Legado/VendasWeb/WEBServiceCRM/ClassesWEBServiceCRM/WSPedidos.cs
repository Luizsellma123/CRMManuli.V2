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
    public class WSPedidos
    {
        #region Campos Principais
        public string CodigoEmpresa { get; set; }
        public string PedidoSAP { get; set; }
        public string PedidoCRM { get; set; }

        public DateTime DataEmissaoAux { get; set; }
        public string DataEmissao { get; set; }

        public decimal TotalPedido { get; set; }
        public string HistoricoPedido { get; set; }
        public string NomeCliente { get; set; }
        public string NomeEmpresa { get; set; }
        public int DocEntry { get; set; }
        #endregion

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        public void RecuperaPedidos()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select ORDR.BPLId CodigoEmpresa, OBPL.BPLName NomeEmpresa, ORDR.DocEntry PedidoSAP, ";
            StringSQL += "OCRD.CardCode CodigoCliente, OCRD.CardName  NomeCliente, ";
            StringSQL += "isnull(ORDR.U_IB_CRM_CodPed, '0') PedidoCRM, isnull(ORDR.DocDate, '') as DataEmissao, ";
            StringSQL += "ORDR.DocTotal TotalPedido, isnull(ORDR.U_IB_HistPedido, '') HistoricoPedido ";
            StringSQL += "from ORDR ";
            StringSQL += "INNER JOIN OBPL ON OBPL.BPLId = ORDR.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OCRD.CardCode = ORDR.CardCode ";
            StringSQL += "where ORDR.DocEntry = '" + this.DocEntry + "' ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.CodigoEmpresa = Convert.ToString(row["CodigoEmpresa"]);
                    this.PedidoSAP = Convert.ToString(row["PedidoSAP"]);
                    this.PedidoCRM = Convert.ToString(row["PedidoCRM"]);
                    if (this.PedidoCRM == "0")
                    {
                        this.PedidoCRM = "";
                    }

                    this.DataEmissaoAux = Convert.ToDateTime(row["DataEmissao"]);
                    this.DataEmissao = this.DataEmissaoAux.ToString("dd/MM/yyyy");
                    if(this.DataEmissao == "01-01-1900")
                    {
                        this.DataEmissao = "";
                    }

                    this.TotalPedido = Convert.ToDecimal(row["TotalPedido"]);
                    this.HistoricoPedido = Convert.ToString(row["HistoricoPedido"]);
                    this.NomeCliente = Convert.ToString(row["NomeCliente"]);
                    this.NomeEmpresa = Convert.ToString(row["NomeEmpresa"]);
                }
            }

        }
    }
}