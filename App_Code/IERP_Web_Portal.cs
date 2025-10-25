using Cls_ERP_Web_Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IERP_Web_Portal" in both code and config file together.
namespace ERP_Web_Portal_WCF
{
    [ServiceContract]
    public interface IERP_Web_Portal
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/DoWork",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Cls_Response DoWork();


        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/PostPO",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle =WebMessageBodyStyle.Wrapped)]
        Cls_Response PostPO(Cls_PurchaseOrderHdr A_Cls_Po);

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/GetPO?PoNum={PoNum}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Cls_ShowPurchaseOrder GetPO(string PoNum);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/PostGRPO",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Cls_Response PostGRPO(Cls_GRPO_Hdr A_Cls_Grpo);

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/GetGRPO?GrpoNum={GrpoNum}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Cls_ShowGRPO GetGRPO(string GrpoNum);

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/PostAPInv",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Cls_Response PostApInv(Cls_AccountPurchaseInvHdr A_Cls_ApInv);

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "/GetAPInv?InvNum={InvNum}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Cls_ShowAccountPurchaseInv GetAPInvoice(string InvNum);

        [OperationContract]
        [WebInvoke(
            Method = "PATCH",
            UriTemplate = "/UpdateAPInv",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Cls_Response UpdateAPInv(Cls_UpdateAPInv sendObj);



    }
}

