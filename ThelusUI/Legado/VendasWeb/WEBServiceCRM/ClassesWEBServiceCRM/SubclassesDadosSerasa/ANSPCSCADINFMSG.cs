using Newtonsoft.Json;
using System.Collections.Generic;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class ANSPCSCADINFMSG
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }
        public string SEQ { get; set; }
        public string MENSAGEM { get; set; }
    }
}