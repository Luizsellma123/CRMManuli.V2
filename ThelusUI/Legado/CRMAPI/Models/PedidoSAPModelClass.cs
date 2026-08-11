using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class PedidoSAPModelClass
    {
        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        public string NumeroPrimario { get; set; }
        public string LiberaPedido { get; set; }
        public string HistoricoPedido { get; set; }

        public string LiberaPedidoProducaoSAP()
        {
            this.CarregaApplication();

            string erro = "";

            OBJComunicacaoServiceLayerSAP.NumeroPedidoSAP = Convert.ToInt32(this.NumeroPrimario);
            OBJComunicacaoServiceLayerSAP.HistoricoPedidoSAP = this.HistoricoPedido;
            OBJComunicacaoServiceLayerSAP.LiberarProducaoLiberado = this.LiberaPedido;

            erro = OBJComunicacaoServiceLayerSAP.AtualizaAprovacaoPedidoProducao();            

            return erro;
        }

        public void CarregaApplication()
        {
            //Atribui variavel Global para local DI API
            //if (HttpContext.Current.Application["ApplicationComunicacaoSAP"] != null)
            //{
            //    OBJComunicacaoSAP = (ComunicacaoSAPClass)HttpContext.Current.Application["ApplicationComunicacaoSAP"];
            //}

            //Atribui variavel Global para local Service Layer
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }
        }
    }
}