using Newtonsoft.Json;
using System.Collections.Generic;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class PARTICIPACOESDATATU
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("DATA-ULTAT-PART")]
        public string DATAULTATPART { get; set; }

       //[JsonProperty("ORIGEM-DADOS")]
        public string ORIGEMDADOS { get; set; }
    }
}