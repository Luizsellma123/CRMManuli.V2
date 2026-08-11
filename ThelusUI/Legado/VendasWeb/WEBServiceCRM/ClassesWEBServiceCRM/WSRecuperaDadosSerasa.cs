using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSRecuperaDadosSerasa
    {
        public string TipoConsulta { get; set; }
        public string NumeroDocumento { get; set; }
        public string Produto { get; set; }
        public int IDCliente { get; set; }
        public int IDUsuario { get; set; }

        public WSRecuperaDadosSerasa()
        {
            this.TipoConsulta = "";
            this.NumeroDocumento = "";
            this.Produto = "";
            this.IDCliente = 0;
            this.IDUsuario = 0;
        }
    }
}