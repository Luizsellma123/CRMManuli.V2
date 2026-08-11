using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ChatBotConsultaAgentesOnlineMetaDataClass
    {
        [JsonProperty("#command.uri")]
        public string commanduri { get; set; }
    }
}