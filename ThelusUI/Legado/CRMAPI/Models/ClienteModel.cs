using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class ClienteModel
    {
        public int CardCode { get; set; }

        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        public string GravaCliente()
        {
            this.CarregaApplication();

            string erro = "";

            OBJComunicacaoServiceLayerSAP.RetornaInformacaoGET();

            return erro;
        }

        public void CarregaApplication()
        {
            //Atribui variavel Global para local Service Layer
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }
        }
    }
}