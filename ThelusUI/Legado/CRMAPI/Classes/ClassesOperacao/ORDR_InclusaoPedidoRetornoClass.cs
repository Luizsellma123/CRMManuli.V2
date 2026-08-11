using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class ORDR_InclusaoPedidoRetornoClass
    {
        [JsonProperty("odata.metadata")]
        public string odatametadata { get; set; }

        [JsonProperty("odata.etag")]
        public string odataetag { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string DocType { get; set; }
        public string HandWritten { get; set; }
        public string Printed { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Address { get; set; }
        public string NumAtCard { get; set; }
        public double DocTotal { get; set; }
        public object AttachmentEntry { get; set; }
        public string DocCurrency { get; set; }
        public double DocRate { get; set; }
    }
}