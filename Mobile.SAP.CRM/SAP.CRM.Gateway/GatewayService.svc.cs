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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class GatewayService : IGatewayService
    {
        const string SALES_ACTIVITY_STATUS_OPEN = "0";
        const string SALES_ACTIVITY_STATUS_INPROGRESS = "1";
        const string SALES_ACTIVITY_STATUS_COMPLETED = "5";

        public GetCustomerInformationResponse GetCustomerInformation(GetCustomerInformationRequest request)
        {
            var response = new GetCustomerInformationResponse();

            try
            {
                if (request == null)
                    throw new WebFaultException(System.Net.HttpStatusCode.NoContent);

                // Force record restriction for free search
                if (request.SearchCriteria != null && request.SearchCriteria.Length >= 3) request.MaxCustomerRecords = 100;

                // Prepare request object and call backend system
                using (ZIGNMOBILESALESACTIVIYClient client = new ZIGNMOBILESALESACTIVIYClient("binding_SOAP122"))
                {
                    string employeeNumber = "";
                    List<BapicustomerIdrange> customerSelection = null;
                    bool maxRecordRestriction = false;

                    // TODO - service user should be changed to SAML Token
                    client.ClientCredentials.UserName.UserName = "DEVELOPER";
                    client.ClientCredentials.UserName.Password = "ZignMob2017";

                    // If personal customers is requested then get employee number
                    if (request.MyCustomers)
                    {
                        // Call to external system
                        var sapResponse = client.ZIgnGetEmployeenumber(new ZIgnGetEmployeenumber
                         {
                             Id = client.ClientCredentials.UserName.UserName
                         });
                        employeeNumber = sapResponse.Employeenumber;
                    }
                    
                    customerSelection = new List<BapicustomerIdrange>();
                    if (request.CustomerIds != null && request.CustomerIds.Count() > 0)
                    {
                        
                        foreach (var item in request.CustomerIds)
                        {
                            customerSelection.Add(new BapicustomerIdrange { Sign = "I", Option = "EQ", Low = item });
                        }
                    }
                    
                    if (employeeNumber != "" || customerSelection.Count() > 0 || request.SearchCriteria.Length >= 3)
                    {
                        // Activate customer records restiction
                        if (request.MaxCustomerRecords > 0) maxRecordRestriction = true;

                        // Call to external system
                        var sapResponse = client.ZIgnCustomerGetlist(new ZIgnCustomerGetlist
                        {
                            Employeenumber = employeeNumber,
                            //Search = request.SearchCriteria,
                            Idrange = customerSelection.ToArray(),
                            Maxrows = request.MaxCustomerRecords,
                            MaxrowsSpecified = maxRecordRestriction
                        });

                        // Clear customer selection
                        customerSelection = new List<BapicustomerIdrange>();

                        // Handle response data
                        foreach (var item in sapResponse.Addressdata)
                        {
                            // Add customer records to main response
                            response.Customers.Add(new Customer
                            {
                                AddressField = item.Address,
                                CityField = item.City,
                                CountryField = item.Country,
                                CountryisoField = item.Countryiso,
                                CustomerField = item.Customer,
                                FaxNumberField = item.FaxNumber,
                                NameField = item.Name,
                                PostlCod1Field = item.PostlCod1,
                                RegionField = item.Region,
                                Sort1Field = item.Sort1,
                                StreetField = item.Street,
                                Tel1NumbrField = item.Tel1Numbr
                            });

                            // Prepare new customer selection 
                            customerSelection.Add(new BapicustomerIdrange { Sign = "I", Option = "EQ", Low = item.Customer });
                        }
                    }
                   
                    // Get contact for all customers that will be returned
                    if (customerSelection.Count() > 0)
                    {
                        // Call external system
                        var sapResponse = client.ZIgnCustomerGetcontactlist(new ZIgnCustomerGetcontactlist
                        {
                            Customerrange = customerSelection.ToArray()
                        });

                        // Handle response data
                        foreach (var item in sapResponse.Contactaddressdata)
	                    {
                            response.CustomerContacts.Add(new CustomerContact
                            {
                                 AddressField = item.Address,
                                 CityField = item.City,
                                 CountryField = item.Country,
                                 CountryisoField = item.Countryiso,
                                 CustomerField = item.Customer,
                                 EMailField = item.EMail,
                                 FaxNumberField = item.FaxNumber,
                                 FirstnameField = item.Firstname,
                                 FunctionField = item.Function,
                                 LanguPField = item.LanguP,
                                 LangupIsoField = item.LangupIso,
                                 LastnameField = item.Lastname,
                                 PartneremployeeidField = item.Partneremployeeid,
                                 PersNoField = item.PersNo,
                                 PostlCod1Field = item.PostlCod1,
                                 RegionField = item.Region,
                                 SexField = item.Sex,
                                 Sort1PField = item.Sort1P,
                                 StreetField = item.Street,
                                 Tel1NumbrField = item.Tel1Numbr,
                                 TitlePField = item.TitleP
                            });
	                    }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.InternalServerError);
            }
            return response;
        }

        //public GetSalesActivitiesResponse GetSalesActivities(string customer, int daysbackward, int daysforward, 
        //    string mycustomers, string myactivities, bool statusopen, bool statusinprogress, bool statuscompleted, bool deep)
        //{
        //    ZIgnBpcontactGetlistResponse sapResponse = null;

        //    // Prepare request object and call SAP
        //    using (ZIGNMOBILESALESACTIVITYClient client = new ZIGNMOBILESALESACTIVITYClient("binding_SOAP12"))
        //    {
        //        // TODO - service user should be changed to SAML Token
        //        client.ClientCredentials.UserName.UserName = "DEVELOPER";
        //        client.ClientCredentials.UserName.Password = "ZignMob2017";

        //        var StatusList = new List<Rangesktast>();
        //        if (statusopen) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_OPEN });
        //        if (statusinprogress) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_INPROGRESS });
        //        if (statuscompleted) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_COMPLETED });

        //        var sapRequest = new ZIgnBpcontactGetlist
        //        {
        //            Customer = customer,
        //            DaysBackward = daysbackward,
        //            DaysForward = daysforward,
        //            MyCustomers = mycustomers,
        //            MySalesActivity = myactivities,
        //            Status = StatusList.ToArray<Rangesktast>()
        //        };
        //        sapResponse = client.ZIgnBpcontactGetlist(sapRequest);
        //    }
        //    // Map SAP response into new response object and return response
        //    var response = new GetSalesActivitiesResponse();
            
        //    response.ActivityGeneralData = new List<ActivityGeneralData>();
        //    foreach (var activity in sapResponse.Generaldata)
        //    {

        //        response.ActivityGeneralData.Add(new ActivityGeneralData
        //        {
        //            ActivityCommentField = activity.ActivityComment,
        //            ActivityTypeField = activity.ActivityType,
        //            ContactField = activity.Contact,
        //            ContactRoleField = activity.ContactRole,
        //            Descrpt01Field = activity.Descrpt01,
        //            Descrpt02Field = activity.Descrpt02,
        //            Descrpt03Field = activity.Descrpt03,
        //            Descrpt04Field = activity.Descrpt04,
        //            Descrpt05Field = activity.Descrpt05,
        //            Descrpt06Field = activity.Descrpt06,
        //            Descrpt07Field = activity.Descrpt07,
        //            Descrpt08Field = activity.Descrpt08,
        //            Descrpt09Field = activity.Descrpt09,
        //            Descrpt10Field = activity.Descrpt10,
        //            DirectionField = activity.Direction,
        //            DistrChanField = activity.DistrChan,
        //            DivisionField = activity.Division,
        //            DocNumberField = activity.DocNumber,
        //            FollowUpDateField = activity.FollowUpDate,
        //            FollowUpTypeField = activity.FollowUpType,
        //            FromDateField = activity.FromDate,
        //            FromTimeField = activity.FromTime,
        //            LanguField = activity.Langu,
        //            LanguIsoField = activity.LanguIso,
        //            PartnIdField = activity.PartnId,
        //            PartnRoleField = activity.PartnRole,
        //            ReasonField = activity.Reason,
        //            RefdoctypeField = activity.Refdoctype,
        //            RefobjectkeyField = activity.Refobjectkey,
        //            RefobjecttypeField = activity.Refobjecttype,
        //            RefreltypeField = activity.Refobjecttype,
        //            ResultField = activity.Result,
        //            ResultExplanationField = activity.ResultExplanation,
        //            SalesGrpField = activity.SalesGrp,
        //            SalesOffField = activity.SalesOff,
        //            SalesorgField = activity.Salesorg,
        //            StateField = activity.State,
        //            ToDateField = activity.ToDate,
        //            ToTimeField = activity.ToTime,
        //            TxtKonseField = activity.TxtKonse           
        //        });
        //    };

        //    if (deep) // Add details if requested
        //    {
        //        response.BusinessPartners = new List<BusinessPartner>();
        //        foreach (var partner in sapResponse.Businesspartner)
        //        {
        //            response.BusinessPartners.Add(new BusinessPartner
        //            {
        //                AddrLinkField = partner.AddrLink,
        //                AddrNoField = partner.AddrNo,
        //                AddrOriginField = partner.AddrOrigin,
        //                AddrtypeField = partner.Addrtype,
        //                CalendarUpdateField = partner.CalendarUpdate,
        //                CountParvwField = partner.CountParvw,
        //                DocNumberField = partner.DocNumber,
        //                ItmNumberField = partner.ItmNumber,
        //                PartnIdField = partner.PartnId,
        //                PartnIdOldField = partner.PartnIdOld,
        //                PartnRoleField = partner.PartnRole,
        //                PartnRoleOldField = partner.PartnRoleOld,
        //                PersNoField = partner.PersNo,
        //                RefobjectkeyField = partner.Refobjectkey,
        //                RefobjecttypeField = partner.Refobjecttype,
        //                UnloadPtField = partner.UnloadPt
        //            });
        //        }

        //        response.Texts = new List<Text>();
        //        foreach (var text in sapResponse.Text)
        //        {
        //            response.Texts.Add(new Text
        //            {
        //                DocNumberField = text.DocNumber,
        //                FunctionField = text.Function,
        //                LanguField = text.Langu,
        //                LangupIsoField = text.LangupIso,
        //                RefobjectkeyField = text.Refobjectkey,
        //                RefobjecttypeField = text.Refobjecttype,
        //                TextIdField = text.TextId,
        //                TextLineField = text.TextLine
        //            });
        //        }
        //    }

        //    return response;
        //}

        public GetSalesActivitiesResponse GetSalesActivities(GetSalesActivitiesRequest request)
        {                
            // Response object
            var response = new GetSalesActivitiesResponse();

            try
            {
                if (request == null)
                    throw new WebFaultException(System.Net.HttpStatusCode.InternalServerError);

                // Prepare request object and call SAP
                using (ZIGNMOBILESALESACTIVIYClient client = new ZIGNMOBILESALESACTIVIYClient("binding_SOAP122"))
                {
                    string employeeNumber = null;
                    var salesActivityList = new List<VbkaBoid>();

                    // TODO - service user should be changed to SAML Token
                    client.ClientCredentials.UserName.UserName = "DEVELOPER";
                    client.ClientCredentials.UserName.Password = "ZignMob2017";

                    var statusSelection = new List<Rangesktast>();
                    if (request.StatusSelection != null)
                    {
                        if (request.StatusSelection.Open)
                            statusSelection.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_OPEN });
                        if (request.StatusSelection.InProgress)
                            statusSelection.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_INPROGRESS });
                        if (request.StatusSelection.Completed)
                            statusSelection.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_COMPLETED });
                    }

                    var salesUnitSelection = new List<Sdorgunits>();
                    if (request.SalesUnits != null && request.SalesUnits.Count() > 0)
                    {
                        foreach (var item in request.SalesUnits)
                        {
                            salesUnitSelection.Add(new Sdorgunits
                            {
                                DistrChan = item.DistrChanField,
                                Division = item.DivisionField,
                                SalesGrp = item.SalesGrpField,
                                SalesOff = item.SalesOffField,
                                SalesOrg = item.SalesOrgField
                            });
                        }
                    }

                    var fromDate = DateTime.Today.AddDays(-(System.Convert.ToDouble(request.DaysBackward)));
                    var toDate = DateTime.Today.AddDays(System.Convert.ToDouble(request.DaysForward));

                    // If personal activities is requested then get employee number
                    if (request.MyActivities)
                    {
                        // Call to external system
                        var sapResponse = client.ZIgnGetEmployeenumber(new ZIgnGetEmployeenumber
                         {
                             Id = client.ClientCredentials.UserName.UserName
                         });
                        employeeNumber = sapResponse.Employeenumber;
                    }

                    if (request.MyActivities)
                    {
                        var employeeSelection = new List<Rangesparnr>();
                        employeeSelection.Add(new Rangesparnr { Sign = "I", Option = "EQ", Low = employeeNumber });

                        // Call to external system
                        var sapResponse = client.ZIgnBpcontactGetlist1(new ZIgnBpcontactGetlist1
                        {
                            ActivityStartDate = fromDate.ToShortDateString(),
                            ActivityEndDate = toDate.ToShortDateString(),
                            Statesel = statusSelection.ToArray(),
                            Employeesel = employeeSelection.ToArray(),
                            Sdorganizationalunits = salesUnitSelection.ToArray()
                        });

                        // Handle reponse
                        foreach (var item in sapResponse.Salesactivityid)
                        {
                            if (!salesActivityList.Contains(item))
                            {
                                salesActivityList.Add(item);
                            }
                        }
                    }

                    // If activities for my customers activities and/or a specific customer
                    if (request.Customers != null && request.Customers.Count() > 0)
                    {
                        var customerSelection = new List<Rangeskunnr>();

                        foreach (var item in request.Customers)
                        {
                            customerSelection.Add(new Rangeskunnr { Sign = "I", Option = "EQ", Low = item });
                        }
                            
                        // Call to external system
                        var sapResponse = client.ZIgnBpcontactGetlist1(new ZIgnBpcontactGetlist1
                        {
                            ActivityStartDate = fromDate.ToShortDateString(),
                            ActivityEndDate = toDate.ToShortDateString(),
                            Statesel = statusSelection.ToArray(),
                            Customersel = customerSelection.ToArray(),
                            Sdorganizationalunits = salesUnitSelection.ToArray()
                        });

                        // Handle reponse
                        foreach (var item in sapResponse.Salesactivityid)
                        {
                            if (!salesActivityList.Contains(item))
                            {
                                salesActivityList.Add(item);
                            }
                        }
                    }

                    // Get sales activity details
                    if (salesActivityList.Count() > 0)
                    {
                        // Call to external system
                        var sapResponse = client.ZIgnBpcontactGetdetail(new ZIgnBpcontactGetdetail
                        {
                            Salesactivityid = salesActivityList.ToArray()
                        });

                        // Handle response
                        response.ActivityGeneralData = new List<ActivityGeneralData>();
                        foreach (var general in sapResponse.Generaldata)
                        {
                            response.ActivityGeneralData.Add(new ActivityGeneralData
                            {
                                ActivityCommentField = general.ActivityComment,
                                ActivityTypeField = general.ActivityType,
                                ContactField = general.Contact,
                                ContactRoleField = general.ContactRole,
                                Descrpt01Field = general.Descrpt01,
                                Descrpt02Field = general.Descrpt02,
                                Descrpt03Field = general.Descrpt03,
                                Descrpt04Field = general.Descrpt04,
                                Descrpt05Field = general.Descrpt05,
                                Descrpt06Field = general.Descrpt06,
                                Descrpt07Field = general.Descrpt07,
                                Descrpt08Field = general.Descrpt08,
                                Descrpt09Field = general.Descrpt09,
                                Descrpt10Field = general.Descrpt10,
                                DirectionField = general.Direction,
                                DistrChanField = general.DistrChan,
                                DivisionField = general.Division,
                                DocNumberField = general.DocNumber,
                                FollowUpDateField = general.FollowUpDate,
                                FollowUpTypeField = general.FollowUpType,
                                FromDateField = general.FromDate,
                                FromTimeField = general.FromTime,
                                LanguField = general.Langu,
                                LanguIsoField = general.LanguIso,
                                PartnIdField = general.PartnId,
                                PartnRoleField = general.PartnRole,
                                ReasonField = general.Reason,
                                RefdoctypeField = general.Refdoctype,
                                RefobjectkeyField = general.Refobjectkey,
                                RefobjecttypeField = general.Refobjecttype,
                                RefreltypeField = general.Refobjecttype,
                                ResultField = general.Result,
                                ResultExplanationField = general.ResultExplanation,
                                SalesGrpField = general.SalesGrp,
                                SalesOffField = general.SalesOff,
                                SalesorgField = general.Salesorg,
                                StateField = general.State,
                                ToDateField = general.ToDate,
                                ToTimeField = general.ToTime,
                                TxtKonseField = general.TxtKonse
                            });
                        };

                        if (request.Detail) // Add details if requested
                        {
                            response.BusinessPartners = new List<BusinessPartner>();
                            foreach (var partner in sapResponse.Businesspartner)
                            {
                                response.BusinessPartners.Add(new BusinessPartner
                                {
                                    AddrLinkField = partner.AddrLink,
                                    AddrNoField = partner.AddrNo,
                                    AddrOriginField = partner.AddrOrigin,
                                    AddrtypeField = partner.Addrtype,
                                    CalendarUpdateField = partner.CalendarUpdate,
                                    CountParvwField = partner.CountParvw,
                                    DocNumberField = partner.DocNumber,
                                    ItmNumberField = partner.ItmNumber,
                                    PartnIdField = partner.PartnId,
                                    PartnIdOldField = partner.PartnIdOld,
                                    PartnRoleField = partner.PartnRole,
                                    PartnRoleOldField = partner.PartnRoleOld,
                                    PersNoField = partner.PersNo,
                                    RefobjectkeyField = partner.Refobjectkey,
                                    RefobjecttypeField = partner.Refobjecttype,
                                    UnloadPtField = partner.UnloadPt
                                });
                            }

                            response.Texts = new List<Text>();
                            foreach (var text in sapResponse.Text)
                            {
                                response.Texts.Add(new Text
                                {
                                    DocNumberField = text.DocNumber,
                                    FunctionField = text.Function,
                                    LanguField = text.Langu,
                                    LangupIsoField = text.LangupIso,
                                    RefobjectkeyField = text.Refobjectkey,
                                    RefobjecttypeField = text.Refobjecttype,
                                    TextIdField = text.TextId,
                                    TextLineField = text.TextLine
                                });
                            }
                        }

                    }
                }
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotImplemented);
            }
            return response;
        }

        //public ZIgnBpcontactGetlistResponse GetSalesActivities2(string customer, int daysbackward, int daysforward, 
        //    string mycustomers, string myactivities, bool statusopen, bool statusinprogress, bool statuscompleted)
        //{
        //    ZIgnBpcontactGetlistResponse sapResponse = null;

        //    // Prepare request object and call SAP
        //    using (ZIGNMOBILESALESACTIVITYClient client = new ZIGNMOBILESALESACTIVITYClient("binding_SOAP12"))
        //    {
        //        // TODO - service user should be changed to SAML Token
        //        client.ClientCredentials.UserName.UserName = "DEVELOPER";
        //        client.ClientCredentials.UserName.Password = "ZignMob2017";

        //        var StatusList = new List<Rangesktast>();
        //        if (statusopen) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_OPEN });
        //        if (statusinprogress) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_INPROGRESS });
        //        if (statuscompleted) StatusList.Add(new Rangesktast { Sign = "I", Option = "EQ", Low = SALES_ACTIVITY_STATUS_COMPLETED });

        //        var sapRequest = new ZIgnBpcontactGetlist
        //        {
        //            Customer = customer,
        //            DaysBackward = daysbackward,
        //            DaysForward = daysforward,
        //            MyCustomers = mycustomers,
        //            MySalesActivity = myactivities,
        //            Status = StatusList.ToArray<Rangesktast>()
        //        };
        //        sapResponse = client.ZIgnBpcontactGetlist(sapRequest);
        //    }

        //    return sapResponse;
        //}

        //public GetClientsInfoResponse GetClientsInfo()
        //{
        //    throw new NotImplementedException();
        //}

        public CreateSalesActivityResponse CreateSalesActivity(CreateSalesActivityRequest request)
        {
            var response = new CreateSalesActivityResponse();

            try
            {
                if (request == null)
                    throw new WebFaultException(System.Net.HttpStatusCode.InternalServerError);

                if (request.ActivityGeneralData == null || request.ActivityGeneralData.Count == 0)
                    throw new WebFaultException(System.Net.HttpStatusCode.InternalServerError);


                // Prepare request object and call SAP
                using (ZIGNMOBILESALESACTIVIYClient client = new ZIGNMOBILESALESACTIVIYClient("binding_SOAP122"))
                {
                    // TODO - service user should be changed to SAML Token
                    client.ClientCredentials.UserName.UserName = "DEVELOPER";
                    client.ClientCredentials.UserName.Password = "ZignMob2017";

                    // Map request data
                    var generalData = new List<Bus1037VbkakomCr>();
                    var generalDatax = new List<Bus1037VbkakomCrx>();
                    foreach (var item in request.ActivityGeneralData)
	                {
                        generalData.Add(new Bus1037VbkakomCr
                        {
                            ActivityComment = item.ActivityCommentField,
                            ActivityType = item.ActivityTypeField,
                            Contact = item.ContactField,
                            ContactRole = item.ContactRoleField,
                            Descrpt01 = item.Descrpt01Field,
                            Descrpt02 = item.Descrpt02Field,
                            Descrpt03 = item.Descrpt03Field,
                            Descrpt04 = item.Descrpt04Field,
                            Descrpt05 = item.Descrpt05Field,
                            Descrpt06 = item.Descrpt06Field,
                            Descrpt07 = item.Descrpt07Field,
                            Descrpt08 = item.Descrpt08Field,
                            Descrpt09 = item.Descrpt09Field,
                            Descrpt10 = item.Descrpt10Field,
                            Direction = item.DirectionField,
                            DistrChan = item.DistrChanField,
                            Division = item.DivisionField,
                            DocNumber = item.DocNumberField,
                            FollowUpDate = item.FollowUpDateField,
                            FollowUpType = item.FollowUpTypeField,
                            FromDate = item.FromDateField,
                            FromTime = item.FromTimeField,
                            Langu = item.LanguField,
                            LanguIso  = item.LanguIsoField,
                            PartnId = item.PartnIdField,
                            PartnRole = item.PartnRoleField,
                            Reason = item.ReasonField,
                            Refdoctype = item.RefdoctypeField,
                            Refobjectkey = item.RefobjectkeyField,
                            Refobjecttype = item.RefobjecttypeField,
                            Refreltype = item.RefreltypeField,
                            Result = item.ResultField,
                            ResultExplanation = item.ResultExplanationField,
                            SalesGrp = item.SalesGrpField,
                            SalesOff = item.SalesOffField,
                            Salesorg = item.SalesorgField,
                            State  = item.StateField,
                            ToDate = item.ToDateField,
                            ToTime = item.ToTimeField,
                            TxtKonse = item.TxtKonseField
                        });
                        generalDatax.Add(new Bus1037VbkakomCrx
                        {
                            ActivityComment = "X",
                            ActivityType = "X",
                            Contact = "X",
                            ContactRole = "X",
                            Descrpt01 = "X",
                            Descrpt02 = "X",
                            Descrpt03 = "X",
                            Descrpt04 = "X",
                            Descrpt05 = "X",
                            Descrpt06 = "X",
                            Descrpt07 = "X",
                            Descrpt08 = "X",
                            Descrpt09 = "X",
                            Descrpt10 = "X",
                            Direction = "X",
                            DistrChan = "X",
                            Division = "X",
                            DocNumber = item.DocNumberField,
                            FollowUpDate = "X",
                            FollowUpType = "X",
                            FromDate = "X",
                            FromTime = "X",
                            Langu = "X",
                            LanguIso = "X",
                            PartnId = "X",
                            PartnRole = "X",
                            Reason = "X",
                            Refdoctype = "X",
                            Refobjectkey = item.RefobjectkeyField,
                            Refobjecttype = item.RefobjecttypeField,
                            Refreltype = "X",
                            Result = "X",
                            ResultExplanation = "X",
                            SalesGrp = "X",
                            SalesOff = "X",
                            Salesorg = "X",
                            State = "X",
                            ToDate = "X",
                            ToTime = "X",
                            TxtKonse = "X"
                        });
	                }

                    var partnerData = new List<VbkaVbpa2kom>();
                    var partnerDatax = new List<VbkaVbpa2komx>();
                    if (request.BusinessPartners != null && request.BusinessPartners.Count > 0)
                    {
                        foreach (var item in request.BusinessPartners)
                        {
                            partnerData.Add(new VbkaVbpa2kom
                            {
                                AddrLink = item.AddrLinkField,
                                AddrNo = item.AddrNoField,
                                AddrOrigin = item.AddrOriginField,
                                Addrtype = item.AddrtypeField,
                                CalendarUpdate = item.CalendarUpdateField,
                                CountParvw = item.CountParvwField,
                                DocNumber = item.DocNumberField,
                                ItmNumber = item.ItmNumberField,
                                PartnId = item.PartnIdField,
                                PartnIdOld = item.PartnIdOldField,
                                PartnRole = item.PartnRoleField,
                                PartnRoleOld = item.PartnRoleOldField,
                                PersNo = item.PersNoField,
                                Refobjectkey = item.RefobjectkeyField,
                                Refobjecttype = item.RefobjecttypeField,
                                UnloadPt = item.UnloadPtField
                            });
                            partnerDatax.Add(new VbkaVbpa2komx
                            {
                                AddrLink = "X",
                                AddrNo = "X",
                                AddrOrigin = "X",
                                Addrtype = "X",
                                CalendarUpdate = "X",
                                CountParvw = "X",
                                DocNumber = item.DocNumberField,
                                ItmNumber = item.ItmNumberField,
                                PartnId = "X",
                                PartnIdOld = "X",
                                PartnRole = "X",
                                PartnRoleOld = "X",
                                PersNo = "X",
                                Refobjectkey = item.RefobjectkeyField,
                                Refobjecttype = item.RefobjecttypeField,
                                UnloadPt = "X"
                            });
                        }
                    }

                    var texts = new List<VbkaTlinekom>();
                    if (request.Texts != null && request.Texts.Count > 0)
                    {
                        foreach (var item in request.Texts)
                        {
                            texts.Add(new VbkaTlinekom
                            {
                                DocNumber = item.DocNumberField,
                                Function = item.FunctionField,
                                Langu = item.LanguField,
                                LangupIso = item.LangupIsoField,
                                Refobjectkey = item.RefobjectkeyField,
                                Refobjecttype = item.RefobjecttypeField,
                                TextId = item.TextIdField,
                                TextLine = item.TextLineField
                            });
                        }
                    }

                    if (true) // Allways call the backend servie
                    {
                        // Call backend service
                        var sapResponse = client.ZIgnBpcontactCreate(new ZIgnBpcontactCreate
                        {
                            Generaldata = generalData.ToArray(),
                            Generaldatax = generalDatax.ToArray(),
                            Businesspartner = partnerData.ToArray(),
                            Businesspartnerx = partnerDatax.ToArray(),
                            Text = texts.ToArray(),
                            RtfItf = "ITF",
                            Testrun = ""                
                        });

                        // Handle response
                        response.ActivityGeneralData = new List<ActivityGeneralData>();
                        if (sapResponse.Generaldata != null || sapResponse.Generaldata.Count() > 0)
                        {
                            foreach (var item in sapResponse.Generaldata)
                            {
                                response.ActivityGeneralData.Add(new ActivityGeneralData
                                {
                                    ActivityCommentField = item.ActivityComment,
                                    ActivityTypeField = item.ActivityType,
                                    ContactField = item.Contact,
                                    ContactRoleField = item.ContactRole,
                                    Descrpt01Field = item.Descrpt01,
                                    Descrpt02Field = item.Descrpt02,
                                    Descrpt03Field = item.Descrpt03,
                                    Descrpt04Field = item.Descrpt04,
                                    Descrpt05Field = item.Descrpt05,
                                    Descrpt06Field = item.Descrpt06,
                                    Descrpt07Field = item.Descrpt07,
                                    Descrpt08Field = item.Descrpt08,
                                    Descrpt09Field = item.Descrpt09,
                                    Descrpt10Field = item.Descrpt10,
                                    DirectionField = item.Direction,
                                    DistrChanField = item.DistrChan,
                                    DivisionField = item.Division,
                                    DocNumberField = item.DocNumber,
                                    FollowUpDateField = item.FollowUpDate,
                                    FollowUpTypeField = item.FollowUpType,
                                    FromDateField = item.FromDate,
                                    FromTimeField = item.FromTime,
                                    LanguField = item.Langu,
                                    LanguIsoField = item.LanguIso,
                                    PartnIdField = item.PartnId,
                                    PartnRoleField = item.PartnRole,
                                    ReasonField = item.Reason,
                                    RefdoctypeField = item.Refdoctype,
                                    RefobjectkeyField = item.Refobjectkey,
                                    RefobjecttypeField = item.Refobjecttype,
                                    RefreltypeField = item.Refreltype,
                                    ResultExplanationField = item.ResultExplanation,
                                    ResultField = item.Result,
                                    SalesGrpField = item.SalesGrp,
                                    SalesOffField = item.SalesOff,
                                    SalesorgField = item.Salesorg,
                                    StateField = item.State,
                                    ToDateField = item.ToDate,
                                    ToTimeField = item.ToTime,
                                    TxtKonseField = item.TxtKonse
                                });
                            }
                        }
                        response.BusinessPartners = new List<BusinessPartner>();
                        if (sapResponse.Businesspartner != null || sapResponse.Businesspartner.Count() > 0)
                        {
                            foreach (var item in sapResponse.Businesspartner)
                            {
                                response.BusinessPartners.Add(new BusinessPartner
                                {
                                    AddrLinkField = item.AddrLink,
                                    AddrNoField = item.AddrNo,
                                    AddrOriginField = item.AddrOrigin,
                                    AddrtypeField = item.Addrtype,
                                    CalendarUpdateField = item.CalendarUpdate,
                                    CountParvwField = item.CountParvw,
                                    DocNumberField = item.DocNumber,
                                    ItmNumberField = item.ItmNumber,
                                    PartnIdField = item.PartnId,
                                    PartnIdOldField = item.PartnIdOld,
                                    PartnRoleField = item.PartnRole,
                                    PartnRoleOldField = item.PartnRoleOld,
                                    PersNoField = item.PersNo,
                                    RefobjectkeyField = item.Refobjectkey,
                                    RefobjecttypeField = item.Refobjecttype,
                                    UnloadPtField = item.UnloadPt
                                });
                            }
                        }
                        response.Texts = new List<Text>();
                        if (sapResponse.Text == null || sapResponse.Text.Count() > 0)
                        {
                            foreach (var item in sapResponse.Text)
                            {
                                response.Texts.Add(new Text
                                {
                                    DocNumberField = item.DocNumber,
                                    FunctionField = item.Function,
                                    LanguField = item.Langu,
                                    LangupIsoField = item.LangupIso,
                                    RefobjectkeyField = item.Refobjectkey,
                                    RefobjecttypeField = item.Refobjecttype,
                                    TextIdField = item.TextId,
                                    TextLineField = item.TextLine
                                });
                            }
                        }
                    }
                }
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotImplemented);
            }

            return response;
        }

        public UpdateSalesActivityResponse UpdateSalesActivity(UpdateSalesActivityRequest request)
        {
            var response = new UpdateSalesActivityResponse();
            try
            {
                if (request == null)
                    throw new WebFaultException(System.Net.HttpStatusCode.InternalServerError);

                // Prepare request object and call SAP
                using (ZIGNMOBILESALESACTIVIYClient client = new ZIGNMOBILESALESACTIVIYClient("binding_SOAP122"))
                {

                    // TODO - service user should be changed to SAML Token
                    client.ClientCredentials.UserName.UserName = "DEVELOPER";
                    client.ClientCredentials.UserName.Password = "ZignMob2017";

                    // Map request data
                    var generalData = new List<Bus1037VbkakomUpd>();
                    var generalDatax = new List<Bus1037VbkakomUpdx>();
                    foreach (var item in request.ActivityGeneralData)
                    {
                        generalData.Add(new Bus1037VbkakomUpd
                        {
                            ActivityComment = item.ActivityCommentField,
                            //ActivityType = item.ActivityTypeField,
                            Contact = item.ContactField,
                            ContactRole = item.ContactRoleField,
                            Descrpt01 = item.Descrpt01Field,
                            Descrpt02 = item.Descrpt02Field,
                            Descrpt03 = item.Descrpt03Field,
                            Descrpt04 = item.Descrpt04Field,
                            Descrpt05 = item.Descrpt05Field,
                            Descrpt06 = item.Descrpt06Field,
                            Descrpt07 = item.Descrpt07Field,
                            Descrpt08 = item.Descrpt08Field,
                            Descrpt09 = item.Descrpt09Field,
                            Descrpt10 = item.Descrpt10Field,
                            Direction = item.DirectionField,
                            //DistrChan = item.DistrChanField,
                            //Division = item.DivisionField,
                            DocNumber = item.DocNumberField,
                            FollowUpDate = item.FollowUpDateField,
                            FollowUpType = item.FollowUpTypeField,
                            FromDate = item.FromDateField,
                            FromTime = item.FromTimeField,
                            Langu = item.LanguField,
                            LanguIso = item.LanguIsoField,
                            //PartnId = item.PartnIdField,
                            //PartnRole = item.PartnRoleField,
                            Reason = item.ReasonField,
                            //Refdoctype = item.RefdoctypeField,
                            Refobjectkey = item.RefobjectkeyField,
                            Refobjecttype = item.RefobjecttypeField,
                            //Refreltype = item.RefreltypeField,
                            Result = item.ResultField,
                            ResultExplanation = item.ResultExplanationField,
                            SalesGrp = item.SalesGrpField,
                            SalesOff = item.SalesOffField,
                            //Salesorg = item.SalesorgField,
                            State = item.StateField,
                            ToDate = item.ToDateField,
                            ToTime = item.ToTimeField,
                            TxtKonse = item.TxtKonseField
                            
                        });
                        generalDatax.Add(new Bus1037VbkakomUpdx
                        {
                            ActivityComment = "X",
                            //ActivityType = "X",
                            Contact = "X",
                            ContactRole = "X",
                            Descrpt01 = "X",
                            Descrpt02 = "X",
                            Descrpt03 = "X",
                            Descrpt04 = "X",
                            Descrpt05 = "X",
                            Descrpt06 = "X",
                            Descrpt07 = "X",
                            Descrpt08 = "X",
                            Descrpt09 = "X",
                            Descrpt10 = "X",
                            Direction = "X",
                            //DistrChan = "X",
                            //Division = "X",
                            DocNumber = item.DocNumberField,
                            FollowUpDate = "X",
                            FollowUpType = "X",
                            FromDate = "X",
                            FromTime = "X",
                            Langu = "X",
                            LanguIso = "X",
                            //PartnId = "X",
                            //PartnRole = "X",
                            Reason = "X",
                            //Refdoctype = "X",
                            Refobjectkey = item.RefobjectkeyField,
                            Refobjecttype = item.RefobjecttypeField,
                            //Refreltype = "X",
                            Result = "X",
                            ResultExplanation = "X",
                            SalesGrp = "X",
                            SalesOff = "X",
                            //Salesorg = "X",
                            State = "X",
                            ToDate = "X",
                            ToTime = "X",
                            TxtKonse = "X"
                        });
                    }

                    var partnerData = new List<VbkaVbpa2kom>();
                    var partnerDatax = new List<VbkaVbpa2komx>();
                    if (request.BusinessPartners != null && request.BusinessPartners.Count > 0)
                    {
                        foreach (var item in request.BusinessPartners)
                        {
                            partnerData.Add(new VbkaVbpa2kom
                            {
                                AddrLink = item.AddrLinkField,
                                AddrNo = item.AddrNoField,
                                AddrOrigin = item.AddrOriginField,
                                Addrtype = item.AddrtypeField,
                                CalendarUpdate = item.CalendarUpdateField,
                                CountParvw = item.CountParvwField,
                                DocNumber = item.DocNumberField,
                                ItmNumber = item.ItmNumberField,
                                PartnId = item.PartnIdField,
                                PartnIdOld = item.PartnIdOldField,
                                PartnRole = item.PartnRoleField,
                                PartnRoleOld = item.PartnRoleOldField,
                                PersNo = item.PersNoField,
                                Refobjectkey = item.RefobjectkeyField,
                                Refobjecttype = item.RefobjecttypeField,
                                UnloadPt = item.UnloadPtField
                            });
                            partnerDatax.Add(new VbkaVbpa2komx
                            {
                                AddrLink = "X",
                                AddrNo = "X",
                                AddrOrigin = "X",
                                Addrtype = "X",
                                CalendarUpdate = "X",
                                CountParvw = "X",
                                DocNumber = item.DocNumberField,
                                ItmNumber = item.ItmNumberField,
                                PartnId = "X",
                                PartnIdOld = "X",
                                PartnRole = "X",
                                PartnRoleOld = "X",
                                PersNo = "X",
                                Refobjectkey = item.RefobjectkeyField,
                                Refobjecttype = item.RefobjecttypeField,
                                UnloadPt = "X"
                            });
                        }
                    }

                    var texts = new List<VbkaTlinekom>();
                    var textsx = new List<VbkaTlinekomx>();
                    if (request.Texts != null && request.Texts.Count > 0)
                    {
                        foreach (var item in request.Texts)
                        {
                            texts.Add(new VbkaTlinekom
                            {
                                DocNumber = item.DocNumberField,
                                Function = item.FunctionField,
                                Langu = item.LanguField,
                                LangupIso = item.LangupIsoField,
                                Refobjectkey = item.RefobjectkeyField,
                                Refobjecttype = item.RefobjecttypeField,
                                TextId = item.TextIdField,
                                TextLine = item.TextLineField
                            });
                            textsx.Add(new VbkaTlinekomx
                            {
                                DocNumber = item.DocNumberField,
                                Function = "X",
                                Langu = "X",
                                LangupIso = "X",
                                Refobjectkey = item.RefobjectkeyField,
                                Refobjecttype = item.RefobjecttypeField,
                                TextId = item.TextIdField,
                                TextLine = item.TextLineField
                            });
                            
                        }
                    }
                    if (true)
                    {
                        var sapResponse = client.ZIgnBpcontactChange(new ZIgnBpcontactChange
                        {
                            Generaldata = generalData.ToArray(),
                            Generaldatax = generalDatax.ToArray(),
                            Businesspartner = partnerData.ToArray(),
                            Businesspartnerx = partnerDatax.ToArray(),
                            Text = texts.ToArray(),
                            Textx = textsx.ToArray(),
                            RtfItf = "ITF",
                            Testrun = ""
                        });

                        // Handle response
                        response.ActivityGeneralData = new List<ActivityGeneralData>();
                        if (sapResponse.Generaldata != null || sapResponse.Generaldata.Count() > 0)
                        {
                            foreach (var item in sapResponse.Generaldata)
                            {
                                response.ActivityGeneralData.Add(new ActivityGeneralData
                                {
                                    ActivityCommentField = item.ActivityComment,
                                    //ActivityTypeField = item.ActivityType,
                                    ContactField = item.Contact,
                                    ContactRoleField = item.ContactRole,
                                    Descrpt01Field = item.Descrpt01,
                                    Descrpt02Field = item.Descrpt02,
                                    Descrpt03Field = item.Descrpt03,
                                    Descrpt04Field = item.Descrpt04,
                                    Descrpt05Field = item.Descrpt05,
                                    Descrpt06Field = item.Descrpt06,
                                    Descrpt07Field = item.Descrpt07,
                                    Descrpt08Field = item.Descrpt08,
                                    Descrpt09Field = item.Descrpt09,
                                    Descrpt10Field = item.Descrpt10,
                                    DirectionField = item.Direction,
                                    //DistrChanField = item.DistrChan,
                                    //DivisionField = item.Division,
                                    DocNumberField = item.DocNumber,
                                    FollowUpDateField = item.FollowUpDate,
                                    FollowUpTypeField = item.FollowUpType,
                                    FromDateField = item.FromDate,
                                    FromTimeField = item.FromTime,
                                    LanguField = item.Langu,
                                    LanguIsoField = item.LanguIso,
                                    //PartnIdField = item.PartnId,
                                    //PartnRoleField = item.PartnRole,
                                    ReasonField = item.Reason,
                                    //RefdoctypeField = item.Refdoctype,
                                    RefobjectkeyField = item.Refobjectkey,
                                    RefobjecttypeField = item.Refobjecttype,
                                    //RefreltypeField = item.Refreltype,
                                    ResultExplanationField = item.ResultExplanation,
                                    ResultField = item.Result,
                                    SalesGrpField = item.SalesGrp,
                                    SalesOffField = item.SalesOff,
                                    //SalesorgField = item.Salesorg,
                                    StateField = item.State,
                                    ToDateField = item.ToDate,
                                    ToTimeField = item.ToTime,
                                    TxtKonseField = item.TxtKonse
                                });
                            }
                        }
                        response.BusinessPartners = new List<BusinessPartner>();
                        if (sapResponse.Businesspartner != null || sapResponse.Businesspartner.Count() > 0)
                        {
                            foreach (var item in sapResponse.Businesspartner)
                            {
                                response.BusinessPartners.Add(new BusinessPartner
                                {
                                    AddrLinkField = item.ADDR_LINK,
                                    AddrNoField = item.ADDR_NO,
                                    AddrOriginField = item.ADDR_ORIGIN,
                                    AddrtypeField = item.ADDRTYPE,
                                    CalendarUpdateField = item.CALENDAR_UPDATE,
                                    CountParvwField = item.COUNT_PARVW,
                                    DocNumberField = item.DOC_NUMBER,
                                    ItmNumberField = item.ITM_NUMBER,
                                    PartnIdField = item.PARTN_ID,
                                    PartnIdOldField = item.PARTN_ID_OLD,
                                    PartnRoleField = item.PARTN_ROLE,
                                    PartnRoleOldField = item.PARTN_ROLE_OLD,
                                    PersNoField = item.PERS_NO,
                                    RefobjectkeyField = item.REFOBJECTKEY,
                                    RefobjecttypeField = item.REFOBJECTTYPE,
                                    UnloadPtField = item.UNLOAD_PT
                                });
                            }
                        }
                        response.Texts = new List<Text>();
                        if (sapResponse.Text == null || sapResponse.Text.Count() > 0)
                        {
                            foreach (var item in sapResponse.Text)
                            {
                                response.Texts.Add(new Text
                                {
                                    DocNumberField = item.DocNumber,
                                    FunctionField = item.Function,
                                    LanguField = item.Langu,
                                    LangupIsoField = item.LangupIso,
                                    RefobjectkeyField = item.Refobjectkey,
                                    RefobjecttypeField = item.Refobjecttype,
                                    TextIdField = item.TextId,
                                    TextLineField = item.TextLine
                                });
                            }
                        }
                    }
                }
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotImplemented);
            }
            return response;
        }

        public void DeleteSalesActivity(DeleteSalesActivityRequest request)
        {
            try
            {
                if (request == null)
                    throw new WebFaultException(System.Net.HttpStatusCode.InternalServerError);

                if(request.Activities == null || request.Activities.Count == 0)
                    throw new WebFaultException(System.Net.HttpStatusCode.InternalServerError);

                // Prepare request object and call SAP
                using (ZIGNMOBILESALESACTIVIYClient client = new ZIGNMOBILESALESACTIVIYClient("binding_SOAP122"))
                {

                    // TODO - service user should be changed to SAML Token
                    client.ClientCredentials.UserName.UserName = "DEVELOPER";
                    client.ClientCredentials.UserName.Password = "ZignMob2017";

                    var activityIdList = new List<VbkaBoid>();
                    foreach (var item in request.Activities)
	                {
                        activityIdList.Add(new VbkaBoid
                        {
                            DocNumber = item.DocNumberField,
                            Objecttype = "",
                            SystemX = ""
                        });
	                }

                    if (true) // This should allways be called
                    {
                        var sapResponse = client.ZIgnBpcontactDelete(new ZIgnBpcontactDelete
                        {
                             Salesactivityid = activityIdList.ToArray(),
                             Testrun = ""
                        });
                    }
                }
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotImplemented);
            }
        }
    }

}
