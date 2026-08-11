using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class EntidadePortalClass
    {
        string EntCod { get; set; }
        string EntNome { get; set; }

        /*Lista de entidades que o cliente possui*/
        public List<ProdutoPortalClass> ProdutoPortalList { get; set; }

    }
}