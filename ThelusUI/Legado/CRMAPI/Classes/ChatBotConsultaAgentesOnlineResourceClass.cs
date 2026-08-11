using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ChatBotConsultaAgentesOnlineResourceClass
    {
        public int total { get; set; }
        public string itemType { get; set; }
        public List<ChatBotConsultaAgentesOnlineResourceItemClass> items { get; set; }
    }
}