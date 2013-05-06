using SAP.CRM.Gateway.SAPServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SAP.CRM.Gateway
{

    #region SeviceContract

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGatewayService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/SalesActivities?customer={customer}&daysbackward={daysbackward}&daysforward={daysforward}&" +
                              "mycustomers={mycustomers}&myactivities={myactivities}&statusopen={statusopen}&" +
                              "statusinprogress={statusinprogress}&statuscompleted={statuscompleted}")]   
        GetSalesActivitiesResponse GetSalesActivities(string customer, int daysbackward, int daysforward, 
            string mycustomers, string myactivities, bool statusopen, bool statusinprogress, bool statuscompleted);

        [WebInvoke(Method = "GET",
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/SalesActivities2?customer={customer}&daysbackward={daysbackward}&daysforward={daysforward}&" +
                              "mycustomers={mycustomers}&myactivities={myactivities}&statusopen={statusopen}&" +
                              "statusinprogress={statusinprogress}&statuscompleted={statuscompleted}")]
        ZIgnBpcontactGetlistResponse GetSalesActivities2(string customer, int daysbackward, int daysforward,
            string mycustomers, string myactivities, bool statusopen, bool statusinprogress, bool statuscompleted);

       
        
        
        //[OperationContract]
        //GetClientsInfoResponse GetClientsInfo();

    }

    #endregion


}
