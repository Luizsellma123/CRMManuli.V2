using Newtonsoft.Json;
using System.Collections.Generic;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class PARTICIPACOESDET
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("CNPJ-CPF-PADA")]
        public string CNPJCPFPADA { get; set; }

       //[JsonProperty("DIG-PADA")]
        public string DIGPADA { get; set; }

       //[JsonProperty("EMP-LIG-PADA")]
        public string EMPLIGPADA { get; set; }

       //[JsonProperty("RESTRI-PADA")]
        public string RESTRIPADA { get; set; }

       //[JsonProperty("CNPJ-SEQ-PADA")]
        public string CNPJSEQPADA { get; set; }

       //[JsonProperty("IDENT-PADA")]
        public string IDENTPADA { get; set; }
        public string CDSITRF { get; set; }
    }
}