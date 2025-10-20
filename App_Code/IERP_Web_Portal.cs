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


    }
}

