using Newtonsoft.Json;
using System.Collections.Generic;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class PARTICIPACOESPARTDET
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("NMAN-PANTE")]
        public string NMANPANTE { get; set; }

       //[JsonProperty("VÍNCULO-PANTE")]
        public string VNCULOPANTE { get; set; }

       //[JsonProperty("CDEB-PANTE")]
        public string CDEBPANTE { get; set; }

       //[JsonProperty("CDEB-DESCR-PANTE")]
        public string CDEBDESCRPANTE { get; set; }

       //[JsonProperty("CDEB-UF-PANTE")]
        public string CDEBUFPANTE { get; set; }

       //[JsonProperty("PERCAP-PANTE")]
        public string PERCAPPANTE { get; set; }

       //[JsonProperty("RESTRI-PANTE")]
        public string RESTRIPANTE { get; set; }

       //[JsonProperty("CNPJ-CPF-PANTE")]
        public string CNPJCPFPANTE { get; set; }

       //[JsonProperty("CNPJ-SEQ-PANTE")]
        public string CNPJSEQPANTE { get; set; }

       //[JsonProperty("DIG-PANTE")]
        public string DIGPANTE { get; set; }

       //[JsonProperty("IDENT-PANTE")]
        public string IDENTPANTE { get; set; }

       //[JsonProperty("CDSITRF-PANTE")]
        public string CDSITRFPANTE { get; set; }
    }
}