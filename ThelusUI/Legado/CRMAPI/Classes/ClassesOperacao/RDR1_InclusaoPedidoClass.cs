using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class RDR1_InclusaoPedidoClass
    {
        public string ItemCode { get; set; }
        public double Price { get; set; }
        public double UnitPrice { get; set; }
        public int Usage { get; set; }
        public string UomCode { get; set; }
        public string MeasureUnit { get; set; }
        public string FreeText { get; set; }
        public string U_IB_NAT_DESTINACAO { get; set; }
        public string U_IB_Cliche { get; set; }
        public string U_IB_Arruela { get; set; }
        public double Quantity { get; set; }
        public string U_xPed { get; set; }
        public string U_nItem { get; set; }
        public string WarehouseCode { get; set; }
    }
}