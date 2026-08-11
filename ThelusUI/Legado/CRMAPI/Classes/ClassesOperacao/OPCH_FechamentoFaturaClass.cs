using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class OPCH_FechamentoFaturaClass
    {
        public string U_IB_NumeroFaturaDDA { get; set; }
        public DateTime DocDueDate { get; set; }
        public List<PCH6_FechamentoFaturaClass> DocumentInstallments { get; set; }
    }
}