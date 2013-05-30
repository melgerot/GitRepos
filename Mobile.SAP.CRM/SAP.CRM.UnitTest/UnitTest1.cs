using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Newtonsoft.Json;
using SAP.CRM.Gateway;
//using SAP.CRM.Gateway.SAPServiceReference;
//using SAP.CRM.Core.BL.Managers;

namespace SAP.CRM.UnitTest
{
    [TestClass]
    public class GatewayUnitTest
    {
        public const string GATEWAY_URL = "http://172.20.10.7:54978/GatewayService.svc";
        //public const string GATEWAY_URL = "http://localhost:54978/GatewayService.svc";
        public const string SERVICE_PATH = "SalesActivities";

        [TestMethod]
        public void GatewayTest1()
        {

            GetSalesActivitiesRequest request = new GetSalesActivitiesRequest
            {
                //Customer = "",
                StatusSelection = new StatusMap()
            };
            string payload = JsonConvert.SerializeObject(request);

            // Prepare the WebClient
            WebClient client = new WebClient(); // 15 seconds
            //Uri uri = new Uri(string.Format("{0}/{1}?customer={2}&daysbackward={3}&daysforward={4}" +
            //                                "&mycustomers={5}&myactivities={6}&statusopen={7}&statusinprogress={8}" +
            //                                "&statuscompleted={9}&deep={10}", 
            //                                GATEWAY_URL, 
            //                                SERVICE_PATH, 
            //                                "",
            //                                100,
            //                                100,
            //                                "",
            //                                "",
            //                                true,
            //                                true,
            //                                true,
            //                                true));

            Uri uri = new Uri(string.Format("{0}/{1}", GATEWAY_URL, SERVICE_PATH));

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";

            client.Encoding = System.Text.Encoding.UTF8;
            //  Call the service 
            var json = client.UploadString(uri, "PUT", payload); 
            //string json = client.DownloadString(uri);
            var response = JsonConvert.DeserializeObject<GetSalesActivitiesResponse>(json);
        }

        //[TestMethod]
        //public void GatewayTest2()
        //{
        //    // Prepare the WebClient
        //    WebClient client = new WebClient(); // 15 seconds
        //    Uri uri = new Uri(string.Format("{0}/{1}2?customer={2}&daysbackward={3}&daysforward={4}" +
        //                                    "&mycustomers={5}&myactivities={6}&statusopen={7}&statusinprogress={8}" +
        //                                    "&statuscompleted={9}",
        //                                    GATEWAY_URL,
        //                                    SERVICE_PATH,
        //                                    "",
        //                                    100,
        //                                    100,
        //                                    "",
        //                                    "",
        //                                    true,
        //                                    true,
        //                                    true));
        //    client.Headers[HttpRequestHeader.Accept] = "application/json";
        //    client.Encoding = System.Text.Encoding.UTF8;
        //    //  Call the service 
        //    string json = client.DownloadString(uri);

        //    var response = (ZIgnBpcontactGetlistResponse)JsonConvert.DeserializeObject(json, typeof(ZIgnBpcontactGetlistResponse));

        //    //var response = JsonConvert.DeserializeObject<ZIgnBpcontactGetlistResponse>(json);
        //}

        //[TestMethod]
        //public void CoreTest1()
        //{

        //}
    }
}
