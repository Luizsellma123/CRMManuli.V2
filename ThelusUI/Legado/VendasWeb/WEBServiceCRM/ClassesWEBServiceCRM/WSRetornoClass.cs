using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSRetornoClass
    {
        public string MsgRetorno { get; set; }
        public int UsuCodOperacao { get; set; }
        public string OperacaoVia { get; set; }
        public string JSONRetorno { get; set; }

        /***Retorno Nota Fiscal SAP****/
        public int NumeroPrimarioNotaSAP { get; set; }

        public WSRetornoClass()
        {
            this.MsgRetorno = "";
            this.UsuCodOperacao = 0;
            this.OperacaoVia = "";
            this.NumeroPrimarioNotaSAP = 0;
        }
    }
}