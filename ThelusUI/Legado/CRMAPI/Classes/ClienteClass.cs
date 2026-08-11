using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ClienteClass
    {
        public string NomeCliente { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string ObservacaoSimples { get; set; }
        public string ObservacaoCompleta { get; set; }
        public int CodigoVendedorSAP { get; set; }
        public string TipoCliente { get; set; }
        public string NaturezaJuridica { get; set; }
        public string IndicadorIndIEDest { get; set; }
        public string IndicadorNatureza { get; set; }
        public string IndicadorOpConsumidor { get; set; }
        public string EnquadramentoTributario { get; set; }
        public string CartaIPI { get; set; }
        public string DataCarta { get; set; }
        public string SimplesNacional { get; set; }
        public string ProdutorRural { get; set; }
        public string CodigoClienteSAP { get; set; }
        public string GrupoEconomico { get; set; }
        public int GrupoClientes { get; set; }
        public int IdAnexoSAP { get; set; }
        public string CPOM { get; set; }
        public decimal LimiteCredito { get; set; }
        public int CondicaoPagamentoPadraoSAP { get; set; }
        public string ClassificacaoComercial { get; set; }
    }
}