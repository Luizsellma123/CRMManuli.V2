using Newtonsoft.Json;
using System.Collections.Generic;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class HPCVALORES
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("DES-PER")]
        public string DESPER { get; set; }

       //[JsonProperty("ANO1-PRF")]
        public string ANO1PRF { get; set; }

       //[JsonProperty("MES1-PRF")]
        public string MES1PRF { get; set; }

       //[JsonProperty("MES-DESP")]
        public string MESDESP { get; set; }

       //[JsonProperty("COD-FAIXA")]
        public string CODFAIXA { get; set; }

       //[JsonProperty("DESCR-FAIXA")]
        public string DESCRFAIXA { get; set; }

       //[JsonProperty("VLR-FAIXA-DE")]
        public string VLRFAIXADE { get; set; }

       //[JsonProperty("VLR-FAIXA-ATE")]
        public string VLRFAIXAATE { get; set; }

       //[JsonProperty("PERC-FAIXA-DE")]
        public string PERCFAIXADE { get; set; }

       //[JsonProperty("PERC-FAIXA-ATE")]
        public string PERCFAIXAATE { get; set; }

       //[JsonProperty("PMA-FAIXA-DE")]
        public string PMAFAIXADE { get; set; }

       //[JsonProperty("PMA-FAIXA-ATE")]
        public string PMAFAIXAATE { get; set; }

       //[JsonProperty("RESERVADO-SERASA")]
        public string RESERVADOSERASA { get; set; }
    }
}