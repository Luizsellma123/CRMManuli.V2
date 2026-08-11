using Newtonsoft.Json;
using System.Collections.Generic;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class HPCANCOMPPAG1
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("ANC-ANO")]
        public string ANCANO { get; set; }

       //[JsonProperty("ANC-MÊS")]
        public string ANCMS { get; set; }

       //[JsonProperty("ANC-MES-DESP")]
        public string ANCMESDESP { get; set; }

       //[JsonProperty("ANC-COD-FXA-AV")]
        public string ANCCODFXAAV { get; set; }

       //[JsonProperty("ANC-DESC-FAIXA-AV")]
        public string ANCDESCFAIXAAV { get; set; }

       //[JsonProperty("ANC-VLR-FAIXA-DE-AV")]
        public string ANCVLRFAIXADEAV { get; set; }

       //[JsonProperty("ANC-VLR-FAIXA-ATE-AV")]
        public string ANCVLRFAIXAATEAV { get; set; }

       //[JsonProperty("ANC-COD-FXA-PZ")]
        public string ANCCODFXAPZ { get; set; }

       //[JsonProperty("ANC-DES-FXA-PZ")]
        public string ANCDESFXAPZ { get; set; }

       //[JsonProperty("ANC-VLR-FXA-DE-PZ")]
        public string ANCVLRFXADEPZ { get; set; }

       //[JsonProperty("ANC-VLR-FXA-ATE-PZ")]
        public string ANCVLRFXAATEPZ { get; set; }

       //[JsonProperty("SEG-INFO")]
        public string SEGINFO { get; set; }
        public string RESERVADO { get; set; }
    }
}