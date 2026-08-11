using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class ORDR_InclusaoPedidoClass
    {
        public string CardCode { get; set; }
        public string DocObjectCode { get; set; }
        public int BPL_IDAssignedToInvoice { get; set; }
        public DateTime DocDueDate { get; set; }
        public int SalesPersonCode { get; set; }
        public int PaymentGroupCode { get; set; }
        public DateTime DocDate { get; set; }
        public string U_IB_Pedido_Cliente { get; set; }
        public string U_IB_HistPedido { get; set; }
        public string NumAtCard { get; set; }
        public int U_IB_CRM_CodPed { get; set; }
        public string U_IB_insertOrigem { get; set; }
        public string OpeningRemarks { get; set; }
        public string Comments { get; set; }

        public List<RDR1_InclusaoPedidoClass> DocumentLines { get; set; }
        public RDR12_InclusaoPedidoClass TaxExtension { get; set; }
        public List<RDR3_InclusaoPedidoClass> DocumentAdditionalExpenses { get; set; }

        public ORDR_InclusaoPedidoClass()
        {
            this.DocObjectCode = "oOrders";
            this.U_IB_insertOrigem = "CRM";
            this.Comments = "Pedido Inserido pelo CRM";
            this.DocumentLines = new List<RDR1_InclusaoPedidoClass>();
            this.TaxExtension = new RDR12_InclusaoPedidoClass();
            this.DocumentAdditionalExpenses = new List<RDR3_InclusaoPedidoClass>();
        }
    }
}