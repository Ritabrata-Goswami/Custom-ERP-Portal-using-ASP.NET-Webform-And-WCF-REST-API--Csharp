using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Cls_ERP_Web_Portal;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ERP_Web_Portal" in code, svc and config file together.
namespace ERP_Web_Portal_WCF
{
    public class ERP_Web_Portal : IERP_Web_Portal
    {
        public Cls_Response DoWork()
        {
            return new Cls_Response(){ 
                StatusCode = 200, 
                ResponseMsg = "Http connection is ok!", 
                Token="" 
            };
        }

    }
}
