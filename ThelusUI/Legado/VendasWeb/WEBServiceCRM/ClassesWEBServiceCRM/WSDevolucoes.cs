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
    public class WSDevolucoes
    {
        #region Campos Principais

        public string CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public string Empresa { get; set; }
        public string NomeEmpresa { get; set; }
        public string NotaFiscal { get; set; }
        public string Parcela { get; set; }

        public DateTime DataVencimentoAux { get; set; }
        public string DataVencimento { get; set; }

        //public DateTime DataPagamentoAux { get; set; }
        public string DataPagamento { get; set; }

        public DateTime DataEmissaoAux { get; set; }
        public string DataEmissao { get; set; }

        public string CodigoBanco { get; set; }
        public string NomeBanco { get; set; }
        public string Agencia { get; set; }

        public decimal ValorPagar { get; set; }
        public decimal TotalNota { get; set; }

        public int DocEntry { get; set; }
        public int ObjType { get; set; }

        #endregion

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        public void RecuperaNotasDevolucoes()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";
                        
                StringSQL += "select ORIN.DocEntry, OCRD.CardCode CodigoCliente, OCRD.CardName NomeCliente, ";
                StringSQL += "ORIN.BPLId Empresa, OBPL.BPLName NomeEmpresa, ORIN.DocTotal TotalNota, ";
                StringSQL += "ORIN.Serial NotaFiscal, JDT1.SourceLine Parcela, ";
                StringSQL += "JDT1.DueDate DataVencimento, isnull(convert(varchar(10), JDT1.MthDate,103),'') as DataPagamento, ";
                StringSQL += "Credit ValorPagar, ORIN.DocDate DataEmissao, ";
                StringSQL += "isnull(ODSC.BankCode, '') CodigoBanco, isnull(ODSC.BankName, '') NomeBanco, ";
                StringSQL += "isnull(OPYM.DflAccount, '') Agencia ";
                StringSQL += "from JDT1 ";
                StringSQL += "INNER JOIN ORIN ON JDT1.BaseRef = ORIN.DocEntry and TransType = '14' ";
                StringSQL += "INNER JOIN ORIN T1 ON T1.DocEntry = ORIN.DocEntry ";
                StringSQL += "INNER JOIN OBPL ON ORIN.BPLId = OBPL.BPLId ";
                StringSQL += "INNER JOIN OCRD ON ORIN.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
                StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
                StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
                StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
                StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
                StringSQL += "LEFT JOIN RIN6 ON ORIN.DocEntry=RIN6.DocEntry and RIN6.InstLmntID=JDT1.SourceLine ";
                StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=ORIN.PeyMethod ";
                StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
                StringSQL += "where ORIN.DocEntry= '" + this.DocEntry + "' ";
                       
            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.CodigoCliente = Convert.ToString(row["CodigoCliente"]);
                    this.NomeCliente = Convert.ToString(row["NomeCliente"]);
                    this.Empresa = Convert.ToString(row["Empresa"]);
                    this.NomeEmpresa = Convert.ToString(row["NomeEmpresa"]);
                    this.NotaFiscal = Convert.ToString(row["NotaFiscal"]);
                    this.Parcela = Convert.ToString(row["Parcela"]);

                    this.DataVencimentoAux = Convert.ToDateTime(row["DataVencimento"]);
                    this.DataVencimento = this.DataVencimentoAux.ToString("dd/MM/yyyy");
                    if (this.DataVencimento == "01-01-1900")
                    {
                        this.DataVencimento = "";
                    }

                    this.DataPagamento = Convert.ToString(row["DataPagamento"]);

                    this.DataEmissaoAux = Convert.ToDateTime(row["DataEmissao"]);
                    this.DataEmissao = this.DataEmissaoAux.ToString("dd/MM/yyyy");
                    if (this.DataEmissao == "01-01-1900")
                    {
                        this.DataEmissao = "";
                    }

                    this.CodigoBanco = Convert.ToString(row["CodigoBanco"]);
                    this.NomeBanco = Convert.ToString(row["NomeBanco"]);
                    this.Agencia = Convert.ToString(row["Agencia"]);

                    this.ValorPagar = Convert.ToDecimal(row["ValorPagar"]);
                    this.TotalNota = Convert.ToDecimal(row["TotalNota"]);
                }
            }

        }

    }
}