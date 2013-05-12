using Newtonsoft.Json;
using SAP.CRM.Core.BL;
using SAP.CRM.Core.BL.Contracts;
using SAP.CRM.Core.DAL;
using SAP.CRM.Gateway;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace SAP.CRM.Core.SAL
{
    abstract class ServiceHelperBase<T>
    {

        //public delegate void GetSalesActivitiesCompleted();

        //public event GetSalesActivitiesCompleted OnGetSalesActivitiesCompleted;
        //public event GetSalesActivitiesCompleted OnGetSalesActivitiesFailed;

		private List<T> entityList;

		public List<T> EntityList {
			get {
				return entityList;
			}
		}

        public ServiceHelperBase()
        {
			entityList = new List<T>();
        }

        public void GetSalesActivities(Action action)
        {
            // Get Request parameters from DB
            var settings = ActivityRepository.GetActivitiesRequestSettings().FirstOrDefault();
            if (settings == null) settings = new ActivityRequestSettings
            {
                DaysBackward = 100,
                DaysForward = 100,
                MyActivities = "",
                MyCustomers = "",
                StatusOpen = true,
                StatusInProgress = true,
                StatusCompleted = true
            };

            // Get active URI from application settings

            // Get valid token from credential cache

            // Call network resource using WebClient
            using (CustomWebClient client = new CustomWebClient(15000))
            {
                Uri uri = new Uri(string.Format("{0}" +
                                                "?customer={2}" +
                                                "&daysbackward={3}" +
                                                "&daysforward={4}" +
                                                "&mycustomers={5}" +
                                                "&myactivities={6}" +
                                                "&statusopen={7}" +
                                                "&statusinprogress={8}" +
                                                "&statuscompleted={9}",
                                                "",
                                                "",
                                                settings.DaysBackward,
                                                settings.DaysForward,
                                                settings.MyCustomers,
                                                settings.MyActivities,
                                                settings.StatusOpen,
                                                settings.StatusInProgress,
                                                settings.StatusCompleted));

                // Set async method
                client.DownloadStringCompleted += (sender, e) =>
                {
                    try
                    {
                        // Serialize data and perform mapping to local model
                        var json = e.Result;
                        var response = JsonConvert.DeserializeObject<GetSalesActivitiesResponse>(json);

                        foreach (var a in response.ActivityGeneralData)
                        {
                            // Get activity 
                            var activity = new Activity 
                            { 
                                //ID = Guid.NewGuid().ToString() 
                            };

                            // Get related partner data
                            var partnerList = (from p in response.BusinessPartners
                                               where p.RefobjecttypeField == a.RefobjecttypeField
                                                  && p.RefobjectkeyField == a.RefobjectkeyField
                                                  && p.DocNumberField == a.DocNumberField                               
                                               select p);
                            foreach (var p in partnerList)
	                        {
//                                activity.Partners.Add(new ActivityPartner
//                                {
//                                    //ID = Guid.NewGuid().ToString(),
//                                    //PartnerId = activity.ID
//                                });
	                        }

                            // Get related text data
                            var textList = (from t in response.Texts
                                               where t.RefobjecttypeField == a.RefobjecttypeField
                                                  && t.RefobjectkeyField == a.RefobjectkeyField
                                                  && t.DocNumberField == a.DocNumberField                               
                                               select t);
                            foreach (var t in textList)
	                        {
//                                activity.Texts.Add(new ActivityText
//                                {
//                                    //ID = Guid.NewGuid().ToString(),
//                                    //PartnerId = activity.ID
//                                });
	                        }
                            entityList.Add(activity);
                        }              
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("ERROR saving downloaded data: " + ex);
                    }
                    action();
                };

                // Call the service
                // client.Headers[HttpRequestHeader.Authorization] = ApplicationState.GetInstance.JsonWebToken;
                client.Headers[HttpRequestHeader.Accept] = "application/json";
                client.Encoding = System.Text.Encoding.UTF8;
                client.DownloadStringAsync(uri);
            }
        }
    }
}
