using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class OCRD_IncluirClienteClass
    {
        public int Series { get; set; }
        public string CardName { get; set; }
        public string CardType { get; set; }
        public string Phone1 { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }
        public int SalesPersonCode { get; set; }
        public string AliasName { get; set; }
        public string U_IB_NAT_JURIDICA { get; set; }
        public string U_TX_IndIEDest { get; set; }
        public string U_TX_IndNat { get; set; }
        public string U_TX_IndFinal { get; set; }
        public string U_IB_Enquadr_Trib { get; set; }
        public string U_IB_CartaIPI { get; set; }
        public string U_TX_SN { get; set; }
        public string U_TX_ProdRural { get; set; }
        public string U_IB_CPOM { get; set; }
        public int PayTermsGrpCode { get; set; }
        public string FreeText { get; set; }
        public string SinglePayment { get; set; }
        public string CollectionAuthorization { get; set; }
        public double CreditLimit { get; set; }
        public string U_IB_DataCartaIPI { get; set; }
        public string Properties1 { get; set; }
        public string Properties2 { get; set; }
        public string Properties3 { get; set; }
        public string Properties4 { get; set; }
        public string Properties5 { get; set; }
        public string Properties6 { get; set; }
        public string Properties7 { get; set; }
        public string Properties8 { get; set; }
        public string Properties9 { get; set; }
        public string Properties10 { get; set; }
        public string Properties11 { get; set; }
        public string Properties12 { get; set; }
        public string Properties13 { get; set; }
        public string Properties14 { get; set; }
        public string Properties15 { get; set; }
        public string Properties16 { get; set; }
        public string Properties17 { get; set; }
        public string Properties18 { get; set; }
        public string Properties19 { get; set; }
        public string Properties20 { get; set; }
        public string Properties21 { get; set; }
        public string Properties22 { get; set; }
        public string Properties23 { get; set; }
        public string Properties24 { get; set; }
        public string Properties25 { get; set; }
        public string Properties26 { get; set; }
        public string Properties27 { get; set; }
        public string Properties28 { get; set; }
        public string Properties29 { get; set; }
        public string Properties30 { get; set; }
        public string Properties31 { get; set; }
        public string Properties32 { get; set; }
        public string Properties33 { get; set; }
        public string Properties34 { get; set; }
        public string Properties35 { get; set; }
        public string Properties36 { get; set; }
        public string Properties37 { get; set; }
        public string Properties38 { get; set; }
        public string Properties39 { get; set; }
        public string Properties40 { get; set; }
        public string Properties41 { get; set; }
        public string Properties42 { get; set; }
        public string Properties43 { get; set; }
        public string Properties44 { get; set; }
        public string Properties45 { get; set; }
        public string Properties46 { get; set; }
        public string Properties47 { get; set; }
        public string Properties48 { get; set; }
        public string Properties49 { get; set; }
        public string Properties50 { get; set; }
        public string Properties51 { get; set; }
        public string Properties52 { get; set; }
        public string Properties53 { get; set; }
        public string Properties54 { get; set; }
        public string Properties55 { get; set; }
        public string Properties56 { get; set; }
        public string Properties57 { get; set; }
        public string Properties58 { get; set; }
        public string Properties59 { get; set; }
        public string Properties60 { get; set; }
        public string Properties61 { get; set; }
        public string Properties62 { get; set; }
        public string Properties63 { get; set; }
        public string Properties64 { get; set; }

        public List<CRD1_IncluirClienteEnderecoClass> BPAddresses { get; set; }
        public List<OCPR_IncluirClienteContatoClass> ContactEmployees { get; set; }
        public List<CRD2_IncluirClienteFormasPagamentoClass> BPPaymentMethods { get; set; }
        public List<CRD7_IncluirClienteFiscalClass> BPFiscalTaxIDCollection { get; set; }

        public OCRD_IncluirClienteClass()
        {
            this.Series = 73;
            this.BPAddresses = new List<CRD1_IncluirClienteEnderecoClass>();
            this.ContactEmployees = new List<OCPR_IncluirClienteContatoClass>();
            this.BPPaymentMethods = new List<CRD2_IncluirClienteFormasPagamentoClass>();
            this.BPFiscalTaxIDCollection = new List<CRD7_IncluirClienteFiscalClass>();

            for (int i = 1; i <= 64; i++)
            {
                GetType().GetProperty($"Properties{i}")?.SetValue(this, "N");
            }
        }
    }
}