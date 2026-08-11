using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ProdutoSerasaEnvioFilhoClass
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public int IDConfiguracaoFilho { get; set; }
        public string NomeCampo { get; set; }
        public string Descricao { get; set; }
        public int PosicaoInicial { get; set; }
        public int PosicaoFinal { get; set; }
        public int Tamanho { get; set; }
        public string Valor { get; set; }
        public bool RecuperaValorProduto { get; set; }
    }
}