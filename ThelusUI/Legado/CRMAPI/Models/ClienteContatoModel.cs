using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class ClienteContatoModel
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
    }
}