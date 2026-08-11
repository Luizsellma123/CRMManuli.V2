using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class ClienteFiscalModel
    {
        public string cnpj { get; set; }
        public string inscricaoEstadual { get; set; }
        public int cnae { get; set; }
        public string suframa { get; set; }
        public string address { get; set; }
        public string tipoEndereco { get; set; }
    }
}