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
            // Map SAP response into new response object and return response
            var response = new GetSalesActivitiesResponse();
            
            response.ActivityGeneralData = new List<ActivityGeneralData>();
            foreach (var activity in sapResponse.Generaldata)
            {

                response.ActivityGeneralData.Add(new ActivityGeneralData
                {
                    ActivityCommentField = activity.ActivityComment,
                    ActivityTypeField = activity.ActivityType,
                    ContactField = activity.Contact,
                    ContactRoleField = activity.ContactRole,
                    Descrpt01Field = activity.Descrpt01,
                    Descrpt02Field = activity.Descrpt02,
                    Descrpt03Field = activity.Descrpt03,
                    Descrpt04Field = activity.Descrpt04,
                    Descrpt05Field = activity.Descrpt05,
                    Descrpt06Field = activity.Descrpt06,
                    Descrpt07Field = activity.Descrpt07,
                    Descrpt08Field = activity.Descrpt08,
                    Descrpt09Field = activity.Descrpt09,
                    Descrpt10Field = activity.Descrpt10,
                    DirectionField = activity.Direction,
                    DistrChanField = activity.DistrChan,
                    DivisionField = activity.Division,
                    DocNumberField = activity.DocNumber,
                    FollowUpDateField = activity.FollowUpDate,
                    FollowUpTypeField = activity.FollowUpType,
                    FromDateField = activity.FromDate,
                    FromTimeField = activity.FromTime,
                    LanguField = activity.Langu,
                    LanguIsoField = activity.LanguIso,
                    PartnIdField = activity.PartnId,
                    PartnRoleField = activity.PartnRole,
                    ReasonField = activity.Reason,
                    RefdoctypeField = activity.Refdoctype,
                    RefobjectkeyField = activity.Refobjectkey,
                    RefobjecttypeField = activity.Refobjecttype,
                    RefreltypeField = activity.Refobjecttype,
                    ResultField = activity.Result,
                    ResultExplanationField = activity.ResultExplanation,
                    SalesGrpField = activity.SalesGrp,
                    SalesOffField = activity.SalesOff,
                    SalesorgField = activity.Salesorg,
                    StateField = activity.State,
                    ToDateField = activity.ToDate,
                    ToTimeField = activity.ToTime,
                    TxtKonseField = activity.TxtKonse           
                });
            };

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

            return response;
        }

        //public GetClientsInfoResponse GetClientsInfo()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
