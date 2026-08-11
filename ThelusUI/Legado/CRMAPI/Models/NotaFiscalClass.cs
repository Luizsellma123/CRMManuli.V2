using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class NotaFiscalClass
    {
        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        public int NumeroPrimarioNotaSAP { get; set; }
        public string HistoricoNotaSAP { get; set; }

        public string AtualizaHistoricoNotasSAP()
        {
            //Atribui variavel Global para local 
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }

            string erro = "";

            OBJComunicacaoServiceLayerSAP.NumeroPrimarioNotaSAP = this.NumeroPrimarioNotaSAP;
            OBJComunicacaoServiceLayerSAP.HistoricoNotaSAP = this.HistoricoNotaSAP;

            erro = OBJComunicacaoServiceLayerSAP.AtualizaHistoricoNotaSAP();

            return erro;
        }
    }
}