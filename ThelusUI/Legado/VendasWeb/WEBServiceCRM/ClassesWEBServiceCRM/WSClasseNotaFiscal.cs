using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VendasWeb.classes;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{

    public class WSClasseNotaFiscal
    {
        public int CodigoEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public string CodigoCliente { get; set; }
        public string NumeroCNPJ { get; set; }
        public string NomeCliente { get; set; }
        public string DataDigitacao { get; set; }
        public string DataSaida { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UnidadeFederativa { get; set; }
        public string CEPCliente { get; set; }
        public string CondicaoPagamento { get; set; }
        public string CodigoVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string Frete { get; set; }
        public string CodigoTransportadora { get; set; }
        public string NomeTransportadora { get; set; }
        public string ObservacaoNota { get; set; }
        public string HistoricoPedido { get; set; }
        public decimal TotalICMS { get; set; }
        public decimal TotalIPI { get; set; }
        public decimal TotalDiferimentoICMS { get; set; }
        public decimal TotalMercadorias { get; set; }
        public decimal TotalComIPI { get; set; }
        public string ItensFormatado { get; set; }

        public string PrevisaoEntrega { get; set; }

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        public void RecuperaDadosCabecalhoNota()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "SELECT ";
            StringSQL += "CodigoEmpresa, NomeEmpresa, CodigoCliente, NumeroCNPJ, NomeCliente, DataDigitacao, DataSaida, Endereco, Bairro, Cidade, ";
            StringSQL += "UnidadeFederativa, CEP, CondicaoPagamento, CodigoVendedor, NomeVendedor, ";
            StringSQL += "Frete, CodigoTransportador, NomeTransportador, ObservacaoNota, HistoricoPedido, ";
            StringSQL += "sum(ICMS) as ICMS, sum(IPI) as IPI, sum(DiferimentoICMS) DiferimentoICMS, ";
            StringSQL += "sum(TotalMercadorias) TotalMercadoria, sum(TotalComIPI) TotalComIPI ";
            StringSQL += "from( ";
            StringSQL += "select OBPL.BPLId CodigoEmpresa, OBPL.BPLName NomeEmpresa, ";
            StringSQL += "OCRD.CardCode CodigoCliente, isnull(CRD7.TaxId0, '') NumeroCNPJ, OCRD.CardName NomeCliente, ";
            StringSQL += "OINV.DocDate DataDigitacao, OINV.DocDueDate DataSaida, isnull(CRD1.[AddrType], '') + ' ' + isnull(CRD1.Street, '') + ', ' + isnull(CRD1.StreetNo, '') Endereco, ";
            StringSQL += "CRD1.[Block] Bairro, CRD1.City Cidade, CRD1.[State] UnidadeFederativa, CRD1.ZipCode CEP, ";
            StringSQL += "OCTG.PymntGroup CondicaoPagamento, OSLP.SlpCode CodigoVendedor, OSLP.SlpName NomeVendedor, ";
            StringSQL += "INC.[Name] Frete, T1.CardCode CodigoTransportador, T1.CardName NomeTransportador, ";
            StringSQL += "convert(varchar(max), OINV.Header) + char(10) + char(13) + Convert(varchar(max), OINV.Footer) as ObservacaoNota, ";
            StringSQL += "convert(varchar(max), OINV.U_IB_HistPedido) as HistoricoPedido, ";
            StringSQL += "((case when INV1.Currency = 'R$' then INV1.Price ";
            StringSQL += "else (INV1.Price * INV1.Rate) ";
            StringSQL += "END) * INV1.Quantity) TotalMercadorias, ";
            StringSQL += "isnull((SELECT top 1 case when T00.TaxSum > 0 then T00.TaxSum else ((T00.taxrate * INV1.LineTotal) / 100) end FROM INV4 T00 INNER JOIN OSTA T01 ON T00.stacode = T01.code INNER JOIN OSTT T02 ON T01.type = T02.absid INNER JOIN ONFT T03 ON T02.nftaxid = T03.absid WHERE T00.docentry = INV1.DocEntry AND T00.linenum = INV1.Linenum AND INV1.taxonly = 'N' AND T01.exempt = 'N' AND(T03.code = 'ICMS')),0) ICMS, ";
            StringSQL += "isnull((SELECT top 1 case when T00.TaxSum > 0 then T00.TaxSum else ((T00.taxrate * INV1.LineTotal) / 100) end FROM INV4 T00 INNER JOIN OSTA T01 ON T00.stacode = T01.code INNER JOIN OSTT T02 ON T01.type = T02.absid INNER JOIN ONFT T03 ON T02.nftaxid = T03.absid WHERE T00.docentry = INV1.DocEntry AND T00.linenum = INV1.Linenum AND INV1.taxonly = 'N' AND T01.exempt = 'N' AND(T03.code = 'IPI')),0) IPI, ";
            StringSQL += "isnull((SELECT top 1 T00.U_TX_DIFL FROM INV4 T00 INNER JOIN OSTA T01 ON T00.stacode = T01.code INNER JOIN OSTT T02 ON T01.type = T02.absid INNER JOIN ONFT T03 ON T02.nftaxid = T03.absid WHERE T00.docentry = INV1.DocEntry AND T00.linenum = INV1.Linenum AND INV1.taxonly = 'N' AND T01.exempt = 'N' AND(T03.code = 'ICMS')),0) DiferimentoICMS, ";
            StringSQL += "(((case when INV1.Currency = 'R$' then(INV1.Quantity * INV1.Price) ";
            StringSQL += "else (INV1.Quantity * INV1.Price * INV1.Rate) ";
            StringSQL += "END))+isnull((SELECT top 1 case when T00.TaxSum > 0 then T00.TaxSum else ((T00.taxrate * INV1.LineTotal) / 100) end FROM INV4 T00 INNER JOIN OSTA T01 ON T00.stacode = T01.code INNER JOIN OSTT T02 ON T01.type = T02.absid INNER JOIN ONFT T03 ON T02.nftaxid = T03.absid WHERE T00.docentry = INV1.DocEntry AND T00.linenum = INV1.Linenum AND INV1.taxonly = 'N' AND T01.exempt = 'N' AND(T03.code = 'IPI')),0)) TotalComIPI ";
            StringSQL += "from OINV ";
            StringSQL += "INNER JOIN INV1 ON OINV.DocEntry = INV1.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OBPL.BPLId = OINV.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCTG ON OCTG.GroupNum = OINV.GroupNum ";
            StringSQL += "LEFT JOIN INV12 ON INV12.DocEntry = OINV.DocEntry ";
            StringSQL += "LEFT JOIN[@IB_INCOTERMS] INC ON INC.Code = INV12.Incoterms ";
            StringSQL += "LEFT JOIN OCRD T1 ON T1.CardCode = INV12.Carrier ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OINV.SlpCode ";
            StringSQL += "LEFT JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.[Address]='ENTREGA' ";
            StringSQL += "LEFT JOIN CRD7 ON OCRD.CardCode=CRD7.CardCode and CRD7.[Address]='ENTREGA' ";
            StringSQL += "where OINV.BPLId='" + this.CodigoEmpresa + "' and OINV.DocEntry= '" + this.NumeroNotaFiscal + "' ";
            StringSQL += ") aux ";
            StringSQL += "group by ";
            StringSQL += "CodigoEmpresa, NomeEmpresa, CodigoCliente, NumeroCNPJ, NomeCliente, ";
            StringSQL += "DataDigitacao, DataSaida, Endereco, Bairro, Cidade, ";
            StringSQL += "UnidadeFederativa, CEP, CondicaoPagamento, CodigoVendedor, NomeVendedor, ";
            StringSQL += "CodigoTransportador, NomeTransportador, Frete, ObservacaoNota, HistoricoPedido ";

            //OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsulta(StringSQL);
            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            //Adiciona Dados na classe
            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.NomeEmpresa = row["NomeEmpresa"].ToString();
                    this.CodigoCliente = row["CodigoCliente"].ToString();
                    this.NumeroCNPJ = row["NumeroCNPJ"].ToString();
                    this.NomeCliente = row["NomeCliente"].ToString();
                    this.DataDigitacao = row["DataDigitacao"].ToString();
                    this.DataSaida = row["DataSaida"].ToString();
                    this.Endereco = row["Endereco"].ToString();
                    this.Bairro = row["Bairro"].ToString();
                    this.Cidade= row["Cidade"].ToString();
                    this.UnidadeFederativa = row["UnidadeFederativa"].ToString();
                    this.CEPCliente = row["CEP"].ToString();
                    this.CondicaoPagamento = row["CondicaoPagamento"].ToString();
                    this.CodigoVendedor = row["CodigoVendedor"].ToString();
                    this.NomeVendedor = row["NomeVendedor"].ToString();
                    this.Frete = row["Frete"].ToString();
                    this.CodigoTransportadora = row["CodigoTransportador"].ToString();
                    this.NomeTransportadora = row["NomeTransportador"].ToString();
                    this.ObservacaoNota = row["ObservacaoNota"].ToString();
                    this.HistoricoPedido = row["HistoricoPedido"].ToString();
                    this.TotalICMS = Convert.ToDecimal(row["ICMS"]);
                    this.TotalIPI = Convert.ToDecimal(row["IPI"]);
                    this.TotalDiferimentoICMS = Convert.ToDecimal(row["DiferimentoICMS"]);
                    this.TotalMercadorias = Convert.ToDecimal(row["TotalMercadoria"]);
                    this.TotalComIPI = Convert.ToDecimal(row["TotalComIPI"]);
                }
            }
        }

        public void RecuperaDadosItemsNota()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select INV1.ItemCode CodigoProduto, OITM.ItemName NomeProduto, INV1.unitMsr UnidadeMedida, INV1.Quantity Quantidade, ";
            StringSQL += "(case when INV1.Currency = 'R$' then INV1.Price ";
            StringSQL += "else (INV1.Price * INV1.Rate) ";
            StringSQL += "END) ValorUnitario, ";
            StringSQL += "((case when INV1.Currency = 'R$' then(INV1.Quantity * INV1.Price) ";
            StringSQL += "else (INV1.Quantity * INV1.Price * INV1.Rate) ";
            StringSQL += "END)) TotalSemIPI, ";
            StringSQL += "(((case when INV1.Currency = 'R$' then(INV1.Quantity * INV1.Price) ";
            StringSQL += "else (INV1.Quantity * INV1.Price * INV1.Rate) ";
            StringSQL += "END))+isnull((SELECT top 1 case when T00.TaxSum > 0 then T00.TaxSum else ((T00.taxrate * INV1.LineTotal) / 100) end FROM INV4 T00 INNER JOIN OSTA T01 ON T00.stacode = T01.code INNER JOIN OSTT T02 ON T01.type = T02.absid INNER JOIN ONFT T03 ON T02.nftaxid = T03.absid WHERE T00.docentry = INV1.DocEntry AND T00.linenum = INV1.Linenum AND INV1.taxonly = 'N' AND T01.exempt = 'N' AND(T03.code = 'IPI')),0)) ";
            StringSQL += "TotalComIPI ";
            StringSQL += "from OINV ";
            StringSQL += "INNER JOIN INV1 ON OINV.DocEntry = INV1.DocEntry ";
            StringSQL += "INNER JOIN OITM ON INV1.ItemCode = OITM.ItemCode ";
            StringSQL += "where OINV.BPLId = '" + this.CodigoEmpresa + "' and OINV.DocEntry = '" + this.NumeroNotaFiscal + "' ";

            //OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsulta(StringSQL);
            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            //Adiciona Dados na classe
            if (OBJDataTable.Rows.Count > 0)
            {
                this.ItensFormatado = "";

                this.ItensFormatado += "<table class=\"table table-condensed table-responsive\">";
                this.ItensFormatado += "<thead>";
                this.ItensFormatado += "<tr class=\"bg-gray-light\">";
                this.ItensFormatado += "<th>Código</th>";
                this.ItensFormatado += "<th>Descrição</th>";
                this.ItensFormatado += "<th>UN</th>";
                this.ItensFormatado += "<th>Quantidade</th>";
                this.ItensFormatado += "<th>Valor Unitário</th>";
                this.ItensFormatado += "<th>Total S/IPI</th>";
                this.ItensFormatado += "<th>Total Geral</th>";
                this.ItensFormatado += "</tr>";


                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ItensFormatado += "<tr class=\"bg-gray-light\">";
                    this.ItensFormatado += "<th>" + row["CodigoProduto"].ToString() + "</th>";
                    this.ItensFormatado += "<th>" + row["NomeProduto"].ToString() + "</th>";
                    this.ItensFormatado += "<th>" + row["UnidadeMedida"].ToString() + "</th>";
                    this.ItensFormatado += "<th>" + row["Quantidade"].ToString() + "</th>";
                    this.ItensFormatado += "<th>" + Convert.ToDecimal(row["ValorUnitario"]).ToString("C") + "</th>";
                    this.ItensFormatado += "<th>" + Convert.ToDecimal(row["TotalSemIPI"]).ToString("C") + "</th>";
                    this.ItensFormatado += "<th>" + Convert.ToDecimal(row["TotalComIPI"]).ToString("C") + "</th>";
                    this.ItensFormatado += "</tr>";
                }

                this.ItensFormatado += "</thead>";
                this.ItensFormatado += "</table>";
            }
        }
    }
}