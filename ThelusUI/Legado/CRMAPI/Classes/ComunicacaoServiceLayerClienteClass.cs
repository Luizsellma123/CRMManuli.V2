using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ComunicacaoServiceLayerClienteClass
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
        public List<ComunicacaoServiceLayerClienteContatoClass> ContactEmployees { get; set; }
        public List<ComunicacaoServiceLayerClienteEnderecoClass> BPAddresses { get; set; }
        public List<ComunicacaoServiceLayerClientePagamentoClass> BPPaymentMethods { get; set; }
        public List<ComunicacaoServiceLayerClienteFiscalClass> BPFiscalTaxIDCollection { get; set; }

        public void LimparDados()
        {
            this.CardName = null;
            this.CardType = null;
            this.Phone1 = null;
            this.Fax = null;
            this.EmailAddress = null;
            this.Notes = null;
            this.SalesPersonCode = 0;
            this.AliasName = null;
            this.U_IB_NAT_JURIDICA = null;
            this.U_TX_IndIEDest = null;
            this.U_TX_IndNat = null;
            this.U_TX_IndFinal = null;
            this.U_IB_Enquadr_Trib = null;
            this.U_IB_CartaIPI = null;
            this.U_TX_SN = null;
            this.U_TX_ProdRural = null;
            this.U_IB_CPOM = null;
            this.PayTermsGrpCode = 0;
            this.FreeText = null;
            this.SinglePayment = null;
            this.CollectionAuthorization = null;
            this.CreditLimit = 0;
            this.U_IB_DataCartaIPI = null;
            this.Properties1 = "N";
            this.Properties2 = "N";
            this.Properties3 = "N";
            this.Properties4 = "N";
            this.Properties5 = "N";
            this.Properties6 = "N";
            this.Properties7 = "N";
            this.Properties8 = "N";
            this.Properties9 = "N";
            this.Properties10 = "N";
            this.Properties11 = "N";
            this.Properties12 = "N";
            this.Properties13 = "N";
            this.Properties14 = "N";
            this.Properties15 = "N";
            this.Properties16 = "N";
            this.Properties17 = "N";
            this.Properties18 = "N";
            this.Properties19 = "N";
            this.Properties20 = "N";
            this.Properties21 = "N";
            this.Properties22 = "N";
            this.Properties23 = "N";
            this.Properties24 = "N";
            this.Properties25 = "N";
            this.Properties26 = "N";
            this.Properties27 = "N";
            this.Properties28 = "N";
            this.Properties29 = "N";
            this.Properties30 = "N";
            this.Properties31 = "N";
            this.Properties32 = "N";
            this.Properties33 = "N";
            this.Properties34 = "N";
            this.Properties35 = "N";
            this.Properties36 = "N";
            this.Properties37 = "N";
            this.Properties38 = "N";
            this.Properties39 = "N";
            this.Properties40 = "N";
            this.Properties41 = "N";
            this.Properties42 = "N";
            this.Properties43 = "N";
            this.Properties44 = "N";
            this.Properties45 = "N";
            this.Properties46 = "N";
            this.Properties47 = "N";
            this.Properties48 = "N";
            this.Properties49 = "N";
            this.Properties50 = "N";
            this.Properties51 = "N";
            this.Properties52 = "N";
            this.Properties53 = "N";
            this.Properties54 = "N";
            this.Properties55 = "N";
            this.Properties56 = "N";
            this.Properties57 = "N";
            this.Properties58 = "N";
            this.Properties59 = "N";
            this.Properties60 = "N";
            this.Properties61 = "N";
            this.Properties62 = "N";
            this.Properties63 = "N";
            this.Properties64 = "N";
            this.ContactEmployees?.Clear();
            this.BPAddresses?.Clear();
            this.BPPaymentMethods?.Clear();
            this.BPFiscalTaxIDCollection?.Clear();
        }
    }
}