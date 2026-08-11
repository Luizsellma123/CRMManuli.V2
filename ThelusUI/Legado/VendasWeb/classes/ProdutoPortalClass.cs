using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class ProdutoPortalClass
    {
        string EntCod { get; set; }
        string EmpCod { get; set; }
        string ProdCodEstr { get; set; }
        string ProdNome { get; set; }
        string ProdUnidMedCod { get; set; }
        string ProdUnidMedPos { get; set; }
        decimal ValorUnitario { get; set; }
    }
}