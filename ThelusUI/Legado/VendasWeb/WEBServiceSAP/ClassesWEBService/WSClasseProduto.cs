using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseProduto
    {
        public string CodigoProdutoSAP { get; set; }
        public string Nome { get; set; }
        public string UnidadeVenda { get; set; }
        public string AtivoSAP { get; set; }
        public string GrupoMateriaisSAP { get; set; }
        public string TipoMaterialFiscal { get; set; }
        public string ImagemProduto { get; set; }
        public string CodigoCliente { get; set; }
    }
}