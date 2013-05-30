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
			Entities = new List<IBusinessEntity> ();
		}

        public delegate void GetDataCompleted(GetDataEventArgs args);

        #region Meta Data Service

        public delegate void GetActivityMetaDataCompleted();
        public event GetActivityMetaDataCompleted OnGetActivityMetaDataCompleted;
        public event GetActivityMetaDataCompleted OnGetActivityMetaDataFailed;

        public void GetMetaData()
        {
            throw new NotImplementedException();
        }

        public void GetMetaData_Completed()
        {

        }

        #endregion

        #region Customer Service
     
        public event GetDataCompleted OnGetCustomersCompleted;

        public void GetCustomers(bool myCustomers, string searchCriteria )
        {
            // Get target system settings
            TargetService targetService = ApplicationRepository.GetActiveTargetService();
            if (targetService == null) throw new Exception("Target service information is missing");

            // Prepare request
            GetCustomerInformationRequest request = new GetCustomerInformationRequest
            {
                CustomerIds = new List<string>(),
                MaxCustomerRecords = targetService.MaxRecord,
                MyCustomers = myCustomers,
                SearchCriteria = searchCriteria
            };
            string payload = JsonConvert.SerializeObject(request);

            // Prepare the WebClient
            CustomWebClient client = new CustomWebClient(Constants.WEBCLIENT_TIMEOUT);
			Uri uri = new Uri(string.Format("{0}{1}", targetService.Uri, "CustomerInfo"));

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";

            client.Encoding = System.Text.Encoding.UTF8;
            client.UploadStringCompleted += GetCustomers_Completed;

            //  Call the service    
            client.UploadStringAsync(uri, "PUT", payload); 
        }

        private void GetCustomers_Completed(object sender, UploadStringCompletedEventArgs e)
        {
            Console.WriteLine("GetCustomers_Completed");
            if (e.Error != null)
            {
                if (OnGetCustomersCompleted != null)
                {
                    var args = new GetDataEventArgs();
                    args.Error = e.Error;
                    OnGetCustomersCompleted(args);
                }
            }
            else
            {
                // Serialize result and  map into entities 
                string json = e.Result;
                var response = JsonConvert.DeserializeObject<GetCustomerInformationResponse>(json);

                if (response.Customers != null)
                {
                    foreach (var item in response.Customers)
                    {
                        IBusinessEntity entity = (IBusinessEntity)new SAP.CRM.Core.BL.Customer
                        {
                            AddressField = item.AddressField,
                            CityField = item.CityField,
                            CountryField = item.CountryField,
                            CountryisoField = item.CountryisoField,
                            CustomerField = item.CustomerField,
                            FaxNumberField = item.FaxNumberField,
                            NameField = item.NameField,
                            PostlCod1Field = item.PostlCod1Field,
                            RegionField = item.RegionField,
                            Sort1Field = item.Sort1Field,
                            StreetField = item.StreetField,
                            Tel1NumbrField = item.Tel1NumbrField
                        };

                        Entities.Add(entity);
                    }
                }

                if (response.CustomerContacts != null)
                {
                    foreach (var item in response.CustomerContacts)
                    {
                        Entities.Add(new SAP.CRM.Core.BL.CustomerContact
                        {
                            AddressField = item.AddressField,
                            CityField = item.CityField,
                            CountryField = item.CountryField,
                            CountryisoField = item.CountryisoField,
                            CustomerField = item.CustomerField,
                            EMailField = item.EMailField,
                            FaxNumberField = item.FaxNumberField,
                            FirstnameField = item.FirstnameField,
                            FunctionField = item.FunctionField,
                            LanguPField = item.LanguPField,
                            LangupIsoField = item.LangupIsoField,
                            LastnameField = item.LastnameField,
                            PartneremployeeidField = item.PartneremployeeidField,
                            PersNoField = item.PersNoField,
                            PostlCod1Field = item.PostlCod1Field,
                            RegionField = item.RegionField,
                            SexField = item.SexField,
                            Sort1PField = item.Sort1PField,
                            StreetField = item.StreetField,
                            Tel1NumbrField = item.Tel1NumbrField,
                            TitlePField = item.TitlePField
                        });
                    }
                }

                if (OnGetCustomersCompleted != null)
                    OnGetCustomersCompleted(new GetDataEventArgs());
            }     
        }

        #endregion

        #region Activity Services

        public event GetDataCompleted OnGetActivitiesCompleted;

        public void GetActivities(bool detail, List<Activity> activityData)
        {
            // Get target system settings
            TargetService targetService = ApplicationRepository.GetActiveTargetService();
            if (targetService == null) throw new Exception("Target service information is missing");

            // Get search parameters from DB
            ActivitySearchSettings activitySearchSettings  = ApplicationRepository.GetActivitiesSearchSettings().FirstOrDefault<ActivitySearchSettings>();
            if (activitySearchSettings == null) throw new Exception("Activity search settings is missing");

            // Prepare request
			List<string> customerSelection = new List<string> ();
			foreach (var item in ApplicationRepository.GetCustomers()) {
				customerSelection.Add (item.CustomerField);
			}

            GetSalesActivitiesRequest request = new GetSalesActivitiesRequest
            {
                DaysBackward = activitySearchSettings.DaysBackward,
                DaysForward = activitySearchSettings.DaysForward,
                Customers = customerSelection,
                MyActivities = activitySearchSettings.MyActivities,
                Detail = detail,
                StatusSelection = new StatusMap
                {
                    Open = activitySearchSettings.StatusOpen,
                    InProgress = activitySearchSettings.StatusInProgress,
                    Completed = activitySearchSettings.StatusCompleted
                }
            };
            string payload = JsonConvert.SerializeObject(request);

            // Prepare the WebClient
            CustomWebClient client = new CustomWebClient(Constants.WEBCLIENT_TIMEOUT);
			Uri uri = new Uri(string.Format("{0}{1}", targetService.Uri, "SalesActivities"));

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
         
            client.Encoding = System.Text.Encoding.UTF8;
            client.UploadStringCompleted += GetActivities_Completed;

            //  Call the service    
            client.UploadStringAsync(uri, "PUT", payload);    
        }

		private void GetActivities_Completed(object sender, UploadStringCompletedEventArgs e)
        {
            Console.WriteLine ("GetActivities_Completed");
            if (e.Error != null)
            {
                if (OnGetActivitiesCompleted != null)
                {
                    var args = new GetDataEventArgs();
                    args.Error = e.Error;
                    OnGetActivitiesCompleted(args);
                }
            }
            else
            {
                // Serialize result and  map into entities 
                string json = e.Result;
                var response = JsonConvert.DeserializeObject<GetSalesActivitiesResponse>(json);

                foreach (var item in response.ActivityGeneralData)
                {
					IBusinessEntity entity = (IBusinessEntity)new Activity {
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
					};
                    Entities.Add(entity);
                }

				if (response.BusinessPartners != null)
				{
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
				}

                if (OnGetActivitiesCompleted != null)
                    OnGetActivitiesCompleted(new GetDataEventArgs());
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

    }
}
