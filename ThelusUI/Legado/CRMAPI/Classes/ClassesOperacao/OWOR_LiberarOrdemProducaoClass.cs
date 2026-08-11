using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class OWOR_LiberarOrdemProducaoClass
    {
        public string ProductionOrderStatus { get; set; }
        public DateTime U_IB_DtLiberacao { get; set; }
        public string U_IB_HoraLiberacao { get; set; }
        public string Remarks { get; set; }
    }
}