using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class RDR3_InclusaoPedidoClass
    {
        public string LineTotal { get; set; }
        public string ExpenseCode { get; set; }

        public RDR3_InclusaoPedidoClass()
        {
            this.ExpenseCode = "1";
        }
    }
}