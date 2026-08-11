using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class WOR1_InserirOrdemProducaoLinhasClass
    {
        public string ItemNo { get; set; }
        public string Warehouse { get; set; }
        public double BaseQuantity { get; set; }
        public double PlannedQuantity { get; set; }
        public int ItemType { get; set; }
    }
}