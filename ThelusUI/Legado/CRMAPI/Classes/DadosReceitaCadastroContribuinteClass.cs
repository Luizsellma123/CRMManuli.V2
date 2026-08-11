using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class DadosReceitaCadastroContribuinteClass
    {
        public string updated { get; set; }
        public string taxId { get; set; }
        public string originState { get; set; }
        public List<DadosReceitaCadastroContribuinteRegistroClass> registrations { get; set; }
    }
}