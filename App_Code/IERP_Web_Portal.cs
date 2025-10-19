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
            UriTemplate = "/DoWork",
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Cls_Response DoWork();
    }


}

