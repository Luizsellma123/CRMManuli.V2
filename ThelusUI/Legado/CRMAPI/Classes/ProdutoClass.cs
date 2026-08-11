using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ProdutoClass
    {
        public string CodigoProdutoSAP { get; set; }
        public string Nome { get; set; }
        public string UnidadeVenda { get; set; }
        public string AtivoSAP { get; set; }
        public int GrupoMateriaisSAP { get; set; }
        public string TipoMaterialFiscal { get; set; }
        public string ImagemProduto { get; set; }
        public string CodigoCliente { get; set; }
        public string SubGrupo { get; set; }
        public string TipoProduto { get; set; }
    }
}