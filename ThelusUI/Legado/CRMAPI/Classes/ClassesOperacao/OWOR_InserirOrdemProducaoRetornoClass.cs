using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes.ClassesOperacao
{
    public class OWOR_InserirOrdemProducaoRetornoClass
    {
        [JsonProperty("odata.metadata")]
        public string odatametadata { get; set; }
        public int AbsoluteEntry { get; set; }
        public int DocumentNumber { get; set; }
        public int Series { get; set; }
        public string ItemNo { get; set; }
        public string ProductionOrderStatus { get; set; }
        public string ProductionOrderType { get; set; }
        public double PlannedQuantity { get; set; }
        public double CompletedQuantity { get; set; }
        public double RejectedQuantity { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DueDate { get; set; }
        public int ProductionOrderOriginEntry { get; set; }
        public int ProductionOrderOriginNumber { get; set; }
        public string ProductionOrderOrigin { get; set; }
        public int UserSignature { get; set; }
        public string Remarks { get; set; }
        public object ClosingDate { get; set; }
        public object ReleaseDate { get; set; }
        public string CustomerCode { get; set; }
        public string Warehouse { get; set; }
        public object InventoryUOM { get; set; }
        public string JournalRemarks { get; set; }
        public object TransactionNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string Printed { get; set; }
        public object DistributionRule { get; set; }
        public object Project { get; set; }
        public object DistributionRule2 { get; set; }
        public object DistributionRule3 { get; set; }
        public object DistributionRule4 { get; set; }
        public object DistributionRule5 { get; set; }
        public int UoMEntry { get; set; }
        public DateTime StartDate { get; set; }
        public string ProductDescription { get; set; }
        public int Priority { get; set; }
        public string RoutingDateCalculation { get; set; }
        public string UpdateAllocation { get; set; }
        public object SAPPassport { get; set; }
        public object AttachmentEntry { get; set; }
        public object U_TX_TipoOP { get; set; }
        public int U_IB_SeqPedido { get; set; }
        public object U_IB_DtLiberacao { get; set; }
        public object U_IB_HoraLiberacao { get; set; }
        public int U_MF_NUMOS { get; set; }
        public string U_TX_BlocoK { get; set; }
        //public List<ProductionOrderLine> ProductionOrderLines { get; set; }
        //public List<object> ProductionOrdersSalesOrderLines { get; set; }
        //public List<object> ProductionOrdersStages { get; set; }
        //public List<object> ProductionOrdersDocumentReferences { get; set; }
    }
}