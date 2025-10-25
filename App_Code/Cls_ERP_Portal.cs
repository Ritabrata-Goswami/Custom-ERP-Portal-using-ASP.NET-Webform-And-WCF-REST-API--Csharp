using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml;

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


                    //PO Classes
    [Serializable]
    [DataContract]
    public class Cls_ShowPurchaseOrder
    {
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember]
        public string ResponseMsg { get; set; }
        [DataMember]
        public Cls_PurchaseOrderHdr PoHdr { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Cls_PurchaseOrderHdr
    {
        [DataMember]
        public string PONumber { get; set; }
        [DataMember]
        public string PODate { get; set; }
        [DataMember]
        public string ExpectedDlvDate { get; set; }
        [DataMember]
        public string PaymentTerms { get; set; }
        [DataMember]
        public string TotalAmount { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpId { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string RowId { get; set; }
        [DataMember]
        public string POStatus { get; set; }
        [DataMember]
        public string POCreatedDate { get; set; }
        [DataMember]
        public string ApprovedBy { get; set; }
        [DataMember]
        public string ApprovedDate { get; set; }
        [DataMember]
        public List<Cls_PurchaseOrderDtl> DtlPO { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Cls_PurchaseOrderDtl
    {
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string VendorCode { get; set; }
        [DataMember]
        public string WarehouseCode { get; set; }
        [DataMember]
        public string Quantity { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string Discount { get; set; }
        [DataMember]
        public string Tax { get; set; }
        [DataMember]
        public decimal LineTotal { get; set; }
        [DataMember]
        public string RemarksDtl { get; set; }
    }


                    //GRPO Classes
    [DataContract]
    public class Cls_ShowGRPO
    {
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember]
        public string ResponseMsg { get; set; }
        [DataMember]
        public Cls_GRPO_Hdr GrpoHdr { get; set; }
    }

    [DataContract]
    public class Cls_GRPO_Hdr
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string GRPO_No { get; set; }
        [DataMember]
        public string PO_No { get; set; }
        [DataMember]
        public string VendorCode { get; set; }
        [DataMember]
        public string WarehouseCode { get; set; }
        [DataMember]
        public string WarehouseName { get; set; }
        [DataMember]
        public string ReceivedDt { get; set; }
        [DataMember]
        public string ReceivedBy { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string DocStatus { get; set; }
        [DataMember]
        public string DocDate { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public List<Cls_GRPO_Dtls> GrpoDtls { get; set; }
    }

    [DataContract]
    public class Cls_GRPO_Dtls
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string GRPO_Id { get; set; }
        [DataMember]
        public string LineId { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public string OrderQuantity { get; set; }
        [DataMember]
        public string ReceivedQty { get; set; }
        [DataMember]
        public string UnitPrice { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }
        [DataMember]
        public string BatchNum { get; set; }
        [DataMember]
        public string ExpireDt { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }


                    //A/P Inv Classes
    [DataContract]
    public class Cls_ShowAccountPurchaseInv
    {
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember]
        public string ResponseMsg { get; set; }
        [DataMember]
        public Cls_AccountPurchaseInvHdr ApHdr { get; set; }
    }

    [DataContract]
    public class Cls_AccountPurchaseInvHdr
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string InvNo { get; set; }
        [DataMember]
        public string Vendor_Id { get; set; }
        [DataMember]
        public string PONo { get; set; }
        [DataMember]
        public string GRPONo { get; set; }
        [DataMember]
        public string DocDate { get; set; }
        [DataMember]
        public string PostingDate {get;set;}
        [DataMember]
        public string DueDate {get;set; }
        [DataMember]
        public string Currency { get; set;}
        [DataMember]
        public decimal DocTotal { get; set;}
        [DataMember]
        public decimal TaxAmount { get; set;}
        [DataMember]
        public decimal Discount { get; set;}
        [DataMember]
        public decimal NetAmount { get; set;}
        [DataMember]
        public string Remarks {  get; set;}
        [DataMember]
        public string Status { get; set;}
        [DataMember]
        public string CreatedOn { get; set;}
        [DataMember]
        public string CreatedBy { get; set;}
        [DataMember]
        public string CreatedByName { get; set;}
        [DataMember]
        public string ModifiedOn { get; set;}
        [DataMember]
        public string ModifiedBy { get; set;}
        [DataMember]
        public List<Cls_AccountPurchaseInvDtls> ApInvDtls { get; set;}
    }

    [DataContract]
    public class Cls_AccountPurchaseInvDtls
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string InvNo { get; set; }
        [DataMember]
        public string ItemCode { get; set; }
        [DataMember]
        public decimal Quantity { get; set; }
        [DataMember]
        public decimal UnitPrice { get; set; }
        [DataMember] 
        public decimal LineTotal { get; set; }
        [DataMember]
        public string TaxCode { get; set; }
        [DataMember]
        public string RemarksDtl { get; set; }
    }


    //Update A/P Inv Class.
    public class Cls_UpdateAPInv
    {
        [DataMember]
        public int RowId { get; set; }
        [DataMember]
        public string InvNo { get; set; }
        [DataMember]
        public string DocStatus { get; set; }
    }


}