using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class ODRF_AdicionarDocumentoClass
    {
        public int DocEntry { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DateFormatConverterClass), "yyyy-MM-ddTHH:mm:ss")]
        public DateTime? DocDueDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DateFormatConverterClass), "yyyy-MM-ddTHH:mm:ss")]
        public DateTime? CancelDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(DateFormatConverterClass), "yyyy-MM-ddTHH:mm:ss")]
        public DateTime? DocDate { get; set; }
    }
}