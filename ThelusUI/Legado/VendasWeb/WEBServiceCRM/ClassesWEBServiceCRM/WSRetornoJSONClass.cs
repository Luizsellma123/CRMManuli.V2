using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSRetornoJSONClass
    {
        public string MsgRetorno { get; set; }
        public int UsuCodOperacao { get; set; }
        public string OperacaoVia { get; set; }
        public string JSONRetorno { get; set; }

        public WSRetornoJSONClass()
        {
            this.MsgRetorno = "";
            this.UsuCodOperacao = 0;
            this.OperacaoVia = "";
            this.JSONRetorno = "";
        }
    }
}