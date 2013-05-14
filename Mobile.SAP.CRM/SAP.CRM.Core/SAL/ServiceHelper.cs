using Newtonsoft.Json;
using SAP.CRM.Core.BL;
using SAP.CRM.Core.DAL;
using SAP.CRM.Gateway;
using System;
using System.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SAP.CRM.Core.BL.Contracts;

namespace SAP.CRM.Core.SAL
{
	public class ServiceHelper
	{

        public List<IBusinessEntity> Entities { get; set; }

		public ServiceHelper ()
		{
		}

        #region Activity Services

        public delegate void GetActivityMetaDataCompleted();
        public event GetActivityMetaDataCompleted OnGetActivityMetaDataCompleted;
        public event GetActivityMetaDataCompleted OnGetActivityMetaDataFailed;

        public void GetActivityMetaData()
        {
            throw new NotImplementedException();
        }

        public delegate void GetActivitiesCompleted();
        public event GetActivitiesCompleted OnGetActivitiesCompleted;
        public event GetActivitiesCompleted OnGetActivitiesFailed;

        public void GetActivities(bool deep)
        {
            // Get target system settings
            TargetService targetService = ApplicationRepository.GetActiveTargetService();
            if (targetService == null) throw new Exception("Target service information is missing");

            // Get search parameters from DB
            ActivitySearchSettings activitySearchSettings  = ApplicationRepository.GetActivitiesRequestSettings().FirstOrDefault<ActivitySearchSettings>();
            if (activitySearchSettings == null) throw new Exception("Activity search settings is missing");

            // Prepare the WebClient
            CustomWebClient client = new CustomWebClient(Constants.WEBCLIENT_TIMEOUT);
            Uri uri = new Uri(string.Format("{0}/?customer={1}&daysbackward={2}&daysforward={3}" +
                                            "&mycustomers={4}&myactivities={5}&statusopen={6}&statusinprogress={7}" +
                                            "&statuscompleted={8}&deep={9}",
                                            targetService.Uri,
                                            "",
                                            activitySearchSettings.DaysBackward,
                                            activitySearchSettings.DaysForward,
                                            activitySearchSettings.MyCustomers,
                                            activitySearchSettings.MyActivities,
                                            activitySearchSettings.StatusOpen,
                                            activitySearchSettings.StatusInProgress,
                                            activitySearchSettings.StatusCompleted,
                                            deep));

            client.Headers[HttpRequestHeader.Accept] = "application/json";

            client.Encoding = System.Text.Encoding.UTF8;
            client.DownloadStringCompleted += GetActivities_Completed;

            //  Call the service    
            client.DownloadStringAsync(uri);      
        }

        private void GetActivities_Completed(object sender, DownloadStringCompletedEventArgs e)
        {
            Console.WriteLine ("GetActivities_Completed");
            if (e.Error != null)
            {
                if (OnGetActivitiesFailed != null)
                    OnGetActivitiesFailed();
            }
            else
            {
                // Serialize result and  map into entities 
                string json = e.Result;
                var response = JsonConvert.DeserializeObject<GetSalesActivitiesResponse>(json);

                foreach (var item in response.ActivityGeneralData)
                {
                    Entities.Add(new Activity
                    {
                         ActivityCommentField = item.ActivityCommentField,
                         ActivityTypeField = item.ActivityTypeField,
                         ContactField = item.ContactField,
                         ContactRoleField = item.ContactRoleField,
                         Descrpt01Field = item.Descrpt01Field,
                         Descrpt02Field = item.Descrpt02Field,
                         Descrpt03Field = item.Descrpt03Field,
                         Descrpt04Field = item.Descrpt04Field,
                         Descrpt05Field = item.Descrpt05Field,
                         Descrpt06Field = item.Descrpt06Field,
                         Descrpt07Field = item.Descrpt07Field,
                         Descrpt08Field = item.Descrpt08Field,
                         Descrpt09Field = item.Descrpt09Field,
                         Descrpt10Field = item.Descrpt10Field,
                         DirectionField = item.DirectionField,
                         DistrChanField = item.DistrChanField,
                         DivisionField = item.DivisionField,
                         DocNumberField = item.DocNumberField,
                         FollowUpDateField = item.FollowUpDateField,
                         FollowUpTypeField = item.FollowUpTypeField,
                         FromDateField = item.FromDateField,
                         FromTimeField = item.FromTimeField,
                         LanguField = item.LanguField,
                         LanguIsoField = item.LanguIsoField,
                         PartnIdField = item.PartnIdField,
                         PartnRoleField = item.PartnRoleField,
                         ReasonField = item.ReasonField,
                         RefdoctypeField = item.RefdoctypeField,
                         RefobjectkeyField = item.RefobjectkeyField,
                         RefobjecttypeField = item.RefobjecttypeField,
                         RefreltypeField = item.RefreltypeField,
                         ResultExplanationField = item.ResultExplanationField,
                         ResultField = item.ResultField,
                         SalesGrpField = item.SalesGrpField,
                         SalesOffField = item.SalesOffField,
                         SalesorgField = item.SalesorgField,
                         StateField = item.StateField,
                         ToDateField = item.ToDateField,
                         ToTimeField = item.ToTimeField,
                         TxtKonseField = item.TxtKonseField
                    });
                }

                foreach (var item in response.BusinessPartners)
                {
                    Entities.Add(new ActivityPartner
                    {
                         AddrLinkField = item.AddrLinkField,
                         AddrNoField = item.AddrNoField,
                         AddrOriginField = item.AddrOriginField,
                         AddrtypeField = item.AddrtypeField,
                         CalendarUpdateField = item.CalendarUpdateField,
                         CountParvwField = item.CountParvwField,
                         DocNumberField = item.DocNumberField,
                         ItmNumberField = item.ItmNumberField,
                         PartnIdField = item.PartnIdField,
                         PartnIdOldField = item.PartnIdOldField,
                         //PartnNameField = item.PartnNameField,
                         PartnRoleField = item.PartnRoleField,
                         PartnRoleOldField = item.PartnRoleOldField,
                         PersNoField = item.PersNoField,
                         RefobjectkeyField = item.RefobjectkeyField,
                         RefobjecttypeField = item.RefobjecttypeField,
                         UnloadPtField = item.UnloadPtField,
                         
                    });
                }

                if (OnGetActivitiesCompleted != null)
                    OnGetActivitiesCompleted();
            }     
        }

        public void GetActivitiesDetailed(Action action)
        {
            throw new NotImplementedException();
        }

        public void SyncActivities(Action action)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Customer Services

        public void GetCustomerMetaData(Action action)
        {
            throw new NotImplementedException();
        }

        public void GetCustomers(Action action)
        {
            throw new NotImplementedException();
        }

        public void GetCustomersDetailed(Action action)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Contact Services

        public void GetContactsMetaData(Action action)
        {
            throw new NotImplementedException();
        }
        
        public void GetContacts(Action action)
        {
            throw new NotImplementedException();
        }

        public void GetContactsDetailed(Action action)
        {
            throw new NotImplementedException();
        }

        public void SyncContacts(Action action)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

