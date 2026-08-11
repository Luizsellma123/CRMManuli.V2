using Newtonsoft.Json;
using System.Collections.Generic;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class ANSPCSCADINF1
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }
        public string SEQ { get; set; }
        public string PESS { get; set; }
        public string DOC { get; set; }
        public string FIL { get; set; }
        public string DIG { get; set; }

       //[JsonProperty("SEQ-SOC")]
        public string SEQSOC { get; set; }
        public string VINC { get; set; }
        public string NOME { get; set; }
        public string QTDE { get; set; }
        public string SITUAC { get; set; }
    }
}