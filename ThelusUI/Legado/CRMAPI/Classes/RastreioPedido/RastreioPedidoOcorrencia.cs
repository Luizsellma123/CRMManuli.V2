using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.RastreioPedido
{
    public class RastreioPedidoOcorrencia
    {    
        public string IDTipo { get; set; }

        public string IDEvento { get; set; }

        public string IDCategoria { get; set; }

        public string CodigoOcorrencia { get; set; }

        public string Descricao { get; set; }
    }
}