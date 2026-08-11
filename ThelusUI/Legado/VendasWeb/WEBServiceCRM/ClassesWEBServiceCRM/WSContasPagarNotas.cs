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
    public class WSContasPagarNotas
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

        public void RecuperaNotasContasPagar()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            if (this.ObjType == 18)
            {
                StringSQL += "select OPCH.DocEntry, OCRD.CardCode CodigoCliente, OCRD.CardName NomeCliente, ";
                StringSQL += "OPCH.BPLId Empresa, OBPL.BPLName NomeEmpresa, OPCH.DocTotal TotalNota, ";
                StringSQL += "OPCH.Serial NotaFiscal, JDT1.SourceLine Parcela, ";
                StringSQL += "JDT1.DueDate DataVencimento, isnull(convert(varchar(10), JDT1.MthDate,103),'') as DataPagamento, ";
                StringSQL += "Credit ValorPagar, OPCH.DocDate DataEmissao, ";
                StringSQL += "isnull(ODSC.BankCode, '') CodigoBanco, isnull(ODSC.BankName, '') NomeBanco, ";
                StringSQL += "isnull(OPYM.DflAccount, '') Agencia ";
                StringSQL += "from JDT1 ";
                StringSQL += "INNER JOIN OPCH ON JDT1.BaseRef = OPCH.DocEntry and TransType = '18' ";
                StringSQL += "INNER JOIN OPCH T0 ON T0.DocEntry = OPCH.DocEntry ";
                StringSQL += "INNER JOIN OBPL ON OPCH.BPLId = OBPL.BPLId ";
                StringSQL += "INNER JOIN OCRD ON OPCH.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
                StringSQL += "INNER JOIN OCRD T1 ON OCRD.CardCode = T1.CardCode ";
                StringSQL += "LEFT JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
                StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
                StringSQL += "LEFT JOIN PCH6 ON OPCH.DocEntry=PCH6.DocEntry and PCH6.InstLmntID=JDT1.SourceLine ";
                StringSQL += "LEFT JOIN PCH6 T2 ON OPCH.DocEntry=T2.DocEntry and T2.InstLmntID=JDT1.SourceLine ";
                StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OPCH.PeyMethod ";
                StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
                StringSQL += "where OPCH.DocEntry= '" + this.DocEntry + "' ";
            }
            else
            {
                StringSQL += "select OVPM.DocEntry, OCRD.CardCode CodigoCliente, OCRD.CardName NomeCliente, ";
                StringSQL += "OVPM.BPLId Empresa, OBPL.BPLName NomeEmpresa, OVPM.DocTotal TotalNota, ";
                StringSQL += "'' NotaFiscal, '1' Parcela, ";
                StringSQL += "JDT1.DueDate DataVencimento, isnull(JDT1.MthDate, '') as DataPagamento, ";
                StringSQL += "Credit ValorPagar, OVPM.DocDate DataEmissao, ";
                StringSQL += "'' CodigoBanco, '' NomeBanco, ";
                StringSQL += "'' Agencia ";
                StringSQL += "from JDT1 ";
                StringSQL += "INNER JOIN OVPM ON JDT1.BaseRef = OVPM.DocNum ";
                StringSQL += "INNER JOIN OVPM T0 ON JDT1.BaseRef = T0.DocNum ";
                StringSQL += "INNER JOIN OBPL ON OVPM.BPLId = OBPL.BPLId ";
                StringSQL += "INNER JOIN OCRD ON OVPM.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
                StringSQL += "LEFT JOIN OCRD T1 ON OCRD.CardCode = T1.CardCode ";
                StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
                StringSQL += "where OVPM.DocEntry='" + this.DocEntry + "' ";
            }

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