using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class ODRF_AtualizaEsbocoClass
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DocDueDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CancelDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DocDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string U_MF_ApProd { get; set; }
    }
}