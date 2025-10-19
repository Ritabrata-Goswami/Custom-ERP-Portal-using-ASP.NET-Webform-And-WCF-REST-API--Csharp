using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for Cls_ERP_Portal
/// </summary>

namespace Cls_ERP_Web_Portal
{
    [Serializable]
    [DataContract]
    public class Cls_Response
    {
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember]
        public string ResponseMsg { get; set; }
        [DataMember]
        public string Token { get; set; }
    }


    [Serializable]
    public class Cls_PurchaseOrderHdr
    {
        [DataMember]
        public string PONumber { get; set; }
        [DataMember]
        public DateTime PODate { get; set; }
        [DataMember]
        public DateTime ExpectedDlvDate { get; set; }
        [DataMember]
        public string PaymentTerms { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpId { get; set; }
        [DataMember]
        public string RowId { get; set; }
        [DataMember]
        public List<Cls_PurchaseOrderDtl> DtlPO { get; set; }
    }

    [Serializable]
    public class Cls_PurchaseOrderDtl
    {
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string VendorCode { get; set; }
        [DataMember]
        public string WarehouseCode { get; set; }
        [DataMember]
        public decimal Quantity { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal Discount { get; set; }
        [DataMember]
        public decimal Tax { get; set; }
        [DataMember]
        public string RemarksDtl { get; set; }
    }



}