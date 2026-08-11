using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class OWOR_InserirOrdemProducaoClass
    {
        public string ProductionOrderType { get; set; }
        public string ProductionOrderStatus { get; set; }
        public string ItemNo { get; set; }
        public double PlannedQuantity { get; set; }
        public string Warehouse { get; set; }
        public int Priority { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int ProductionOrderOriginEntry { get; set; }
        public int U_IB_SeqPedido { get; set; }
        public int U_MF_NUMOS { get; set; }
        public string Remarks { get; set; }

        public List<WOR1_InserirOrdemProducaoLinhasClass> ProductionOrderLines { get; set; }
    }
}