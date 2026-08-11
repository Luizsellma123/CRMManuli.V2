using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ChatBotConsultaAgentesOnlineClass
    {
        public string type { get; set; }
        public ChatBotConsultaAgentesOnlineResourceClass resource { get; set; }
        public string method { get; set; }
        public string status { get; set; }
        public string id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public ChatBotConsultaAgentesOnlineMetaDataClass metadata { get; set; }
    }
}