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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class GatewayService : IGatewayService
    {
        const string SALES_ACTIVITY_STATUS_OPEN = "0";
        const string SALES_ACTIVITY_STATUS_INPROGRESS = "1";
        const string SALES_ACTIVITY_STATUS_COMPLETED = "5";


        public GetSalesActivitiesResponse GetSalesActivities(string sessionid, string customer, string datefrom, 
            string mycustomers, string myactivities, bool statusopen, bool statusinprogress, bool statuscompleted)
        {
            ZIgnBpcontactGetlistResponse sapResponse = null;

            // Prepare request object and call SAP
            using (ZIGNMOBILESALESACTIVITYClient client = new ZIGNMOBILESALESACTIVITYClient("binding_SOAP12"))
            {
                // TODO - service user should be changed to SAML Token
                client.ClientCredentials.UserName.UserName = "DEVELOPER";
                client.ClientCredentials.UserName.Password = "ZignMob2017";

                var StatusList = new List<Rangesktast>();
                if (statusopen) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_OPEN });
                if (statusinprogress) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_INPROGRESS });
                if (statuscompleted) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_COMPLETED });

                var sapRequest = new ZIgnBpcontactGetlist
                {
                    Customer = customer,
                    FromDate = datefrom,
                    MyCustomers = mycustomers,
                    MySalesActivity = myactivities,
                    Status = StatusList.ToArray<Rangesktast>()
                };
                sapResponse = client.ZIgnBpcontactGetlist(sapRequest);
            }
            // Map SAP response into service response object and return response
            var response = new GetSalesActivitiesResponse();

            foreach (var activity in sapResponse.Generaldata)
            {
                // Get business partners by for sales activity
                var businesspartners = from partner in sapResponse.Businesspartner
                                      where partner.Refobjecttype == activity.Refobjecttype
                                         && partner.Refobjectkey == activity.Refobjectkey
                                         && partner.DocNumber == activity.DocNumber                               
                                      select partner;
                // Get texts for sales activity
                var texts = from text in sapResponse.Text
                                       where text.Refobjecttype == activity.Refobjecttype
                                          && text.Refobjectkey == activity.Refobjectkey
                                          && text.DocNumber == activity.DocNumber
                                       select text;
                          
               response.SalesActivities.Add(new SalesActivity
               {

               };
            };

            return response;
        }

        //public GetClientsInfoResponse GetClientsInfo()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
