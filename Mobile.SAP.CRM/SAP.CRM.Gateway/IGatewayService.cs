using SAP.CRM.Gateway.SAPServiceReference1;
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
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //           ResponseFormat = WebMessageFormat.Json,
        //           BodyStyle = WebMessageBodyStyle.Bare,
        //           UriTemplate = "/SalesActivities?customer={customer}&daysbackward={daysbackward}&daysforward={daysforward}&" +
        //                      "mycustomers={mycustomers}&myactivities={myactivities}&statusopen={statusopen}&" +
        //                      "statusinprogress={statusinprogress}&statuscompleted={statuscompleted}&deep={deep}")]   
        //GetSalesActivitiesResponse GetSalesActivities(string customer, int daysbackward, int daysforward,
        //    string mycustomers, string myactivities, bool statusopen, bool statusinprogress, bool statuscompleted, bool deep);


        /*
         * The purpose of this method is to provide caller with complete client informaton. A client record can be either a 
         * customer or a contact person contected to a customer. In order to provide the correct scope of information the call 
         * must provide inforamtion about sales areas and optional retriction for customer responsible employee. 
         */
        [OperationContract]
        [WebInvoke(Method = "PUT",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "/CustomerInfo")] 
        GetCustomerInformationResponse GetCustomerInformation(GetCustomerInformationRequest request);

        /*
         * 
         */

        [OperationContract]
        [WebInvoke(Method = "PUT",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/SalesActivities")] 
        GetSalesActivitiesResponse GetSalesActivities(GetSalesActivitiesRequest request);

        [OperationContract]
        [WebInvoke(Method = "POST",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/SalesActivity")]
        CreateSalesActivityResponse CreateSalesActivity(CreateSalesActivityRequest request);

        [OperationContract]
        [WebInvoke(Method = "PUT",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/SalesActivity")]
        UpdateSalesActivityResponse UpdateSalesActivity(UpdateSalesActivityRequest request);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare,
                   UriTemplate = "/SalesActivity")]
        void DeleteSalesActivity(DeleteSalesActivityRequest request);

    }

    #endregion


}
