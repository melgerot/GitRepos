﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SAP.CRM.Gateway
{

    #region DataContracts

    [DataContract]
    public class GetCustomerInformationRequest
    {
		[DataMember]
        public bool MyCustomers { get; set; }
		[DataMember]
        public string SearchCriteria { get; set; }
		[DataMember]
        public int MaxCustomerRecords { get; set; }
		[DataMember]
        public List<string> CustomerIds { get; set; }
    }

    [DataContract]
    public class SalesUnit
    {
        private string salesOrgField;
        [DataMember]
        public string SalesOrgField
        {
            get { return salesOrgField; }
            set { salesOrgField = value; }
        }

        private string distrChanField;
        [DataMember]
        public string DistrChanField
        {
            get { return distrChanField; }
            set { distrChanField = value; }
        }

        private string divisionField;
        [DataMember]
        public string DivisionField
        {
            get { return divisionField; }
            set { divisionField = value; }
        }

        private string salesOffField;
        [DataMember]
        public string SalesOffField
        {
            get { return salesOffField; }
            set { salesOffField = value; }
        }

        private string salesGrpField;
        [DataMember]
        public string SalesGrpField
        {
            get { return salesGrpField; }
            set { salesGrpField = value; }
        }
    }

    [DataContract]
    public class GetCustomerInformationResponse
    {
        [DataMember]
        public List<Customer> Customers { get; set; }

        [DataMember]
        public List<CustomerContact> CustomerContacts { get; set; }
    }

    [DataContract]
    public class CustomerContact
    {
        private string partneremployeeidField;
        [DataMember]
        public string PartneremployeeidField
        {
            get { return partneremployeeidField; }
            set { partneremployeeidField = value; }
        }

        private string customerField;
        [DataMember]
        public string CustomerField
        {
            get { return customerField; }
            set { customerField = value; }
        }

        private string lastnameField;
        [DataMember]
        public string LastnameField
        {
            get { return lastnameField; }
            set { lastnameField = value; }
        }

        private string firstnameField;
        [DataMember]
        public string FirstnameField
        {
            get { return firstnameField; }
            set { firstnameField = value; }
        }

        private string sexField;
        [DataMember]
        public string SexField
        {
            get { return sexField; }
            set { sexField = value; }
        }

        private string titlePField;
        [DataMember]
        public string TitlePField
        {
            get { return titlePField; }
            set { titlePField = value; }
        }

        private string languPField;
        [DataMember]
        public string LanguPField
        {
            get { return languPField; }
            set { languPField = value; }
        }

        private string langupIsoField;
        [DataMember]
        public string LangupIsoField
        {
            get { return langupIsoField; }
            set { langupIsoField = value; }
        }

        private string countryField;
        [DataMember]
        public string CountryField
        {
            get { return countryField; }
            set { countryField = value; }
        }

        private string countryisoField;
        [DataMember]
        public string CountryisoField
        {
            get { return countryisoField; }
            set { countryisoField = value; }
        }

        private string cityField;
        [DataMember]
        public string CityField
        {
            get { return cityField; }
            set { cityField = value; }
        }

        private string postlCod1Field;
        [DataMember]
        public string PostlCod1Field
        {
            get { return postlCod1Field; }
            set { postlCod1Field = value; }
        }

        private string regionField;
        [DataMember]
        public string RegionField
        {
            get { return regionField; }
            set { regionField = value; }
        }

        private string streetField;
        [DataMember]
        public string StreetField
        {
            get { return streetField; }
            set { streetField = value; }
        }

        private string tel1NumbrField;
        [DataMember]
        public string Tel1NumbrField
        {
            get { return tel1NumbrField; }
            set { tel1NumbrField = value; }
        }

        private string faxNumberField;
        [DataMember]
        public string FaxNumberField
        {
            get { return faxNumberField; }
            set { faxNumberField = value; }
        }

        private string functionField;
        [DataMember]
        public string FunctionField
        {
            get { return functionField; }
            set { functionField = value; }
        }

        private string sort1PField;
        [DataMember]
        public string Sort1PField
        {
            get { return sort1PField; }
            set { sort1PField = value; }
        }

        private string addressField;
        [DataMember]
        public string AddressField
        {
            get { return addressField; }
            set { addressField = value; }
        }

        private string persNoField;
        [DataMember]
        public string PersNoField
        {
            get { return persNoField; }
            set { persNoField = value; }
        }

        private string eMailField;
        [DataMember]
        public string EMailField
        {
            get { return eMailField; }
            set { eMailField = value; }
        }
    }

    [DataContract]
    public class GetSalesActivitiesRequest
    {
        [DataMember]
        public int DaysBackward { get; set; }

        [DataMember]
        public int DaysForward { get; set; }

        [DataMember]
        public StatusMap StatusSelection { get; set; }

        [DataMember]
        public bool Detail { get; set; }

        [DataMember]
        public List<SalesUnit> SalesUnits { get; set; }

        [DataMember]
        public bool MyActivities { get; set; }

        [DataMember]
        public List<string> Customers { get; set; }

        [DataMember]
        List<string> Activities;
    }

    [DataContract]
    public class StatusMap
    {
        [DataMember]
        public bool Open { get; set; }

        [DataMember]
        public bool InProgress { get; set; }

        [DataMember]
        public bool Completed { get; set; }
    }

    [DataContract]
    public class GetSalesActivitiesResponse
    {
        [DataMember]
        public List<ActivityGeneralData> ActivityGeneralData { get; set; }

        [DataMember]
        public List<BusinessPartner> BusinessPartners { get; set; }

        [DataMember]
        public List<Text> Texts { get; set; }

        [DataMember]
        public List<Customer> Customers { get; set; }
    }

    [DataContract]
    public class ActivityGeneralData
    {
        private string refobjecttypeField;

        [DataMember]
        public string RefobjecttypeField
        {
            get { return refobjecttypeField; }
            set { refobjecttypeField = value; }
        }

        private string refobjectkeyField;

        [DataMember]
        public string RefobjectkeyField
        {
            get { return refobjectkeyField; }
            set { refobjectkeyField = value; }
        }

        private string docNumberField;

        [DataMember]
        public string DocNumberField
        {
            get { return docNumberField; }
            set { docNumberField = value; }
        }

        private string refdoctypeField;

        [DataMember]
        public string RefdoctypeField
        {
            get { return refdoctypeField; }
            set { refdoctypeField = value; }
        }

        private string refreltypeField;

        [DataMember]
        public string RefreltypeField
        {
            get { return refreltypeField; }
            set { refreltypeField = value; }
        }

        private string activityTypeField;

        [DataMember]
        public string ActivityTypeField
        {
            get { return activityTypeField; }
            set { activityTypeField = value; }
        }

        private string salesorgField;

        [DataMember]
        public string SalesorgField
        {
            get { return salesorgField; }
            set { salesorgField = value; }
        }

        private string distrChanField;

        [DataMember]
        public string DistrChanField
        {
            get { return distrChanField; }
            set { distrChanField = value; }
        }

        private string divisionField;

        [DataMember]
        public string DivisionField
        {
            get { return divisionField; }
            set { divisionField = value; }
        }

        private string salesOffField;

        [DataMember]
        public string SalesOffField
        {
            get { return salesOffField; }
            set { salesOffField = value; }
        }

        private string salesGrpField;

        [DataMember]
        public string SalesGrpField
        {
            get { return salesGrpField; }
            set { salesGrpField = value; }
        }

        private string fromDateField;

        [DataMember]
        public string FromDateField
        {
            get { return fromDateField; }
            set { fromDateField = value; }
        }

        private string toDateField;

        [DataMember]
        public string ToDateField
        {
            get { return toDateField; }
            set { toDateField = value; }
        }

        private string fromTimeField;

        [DataMember]
        public string FromTimeField
        {
            get { return fromTimeField; }
            set { fromTimeField = value; }
        }

        private string toTimeField;

        [DataMember]
        public string ToTimeField
        {
            get { return toTimeField; }
            set { toTimeField = value; }
        }

        private string reasonField;

        [DataMember]
        public string ReasonField
        {
            get { return reasonField; }
            set { reasonField = value; }
        }

        private string resultField;

        [DataMember]
        public string ResultField
        {
            get { return resultField; }
            set { resultField = value; }
        }

        private string resultExplanationField;

        [DataMember]
        public string ResultExplanationField
        {
            get { return resultExplanationField; }
            set { resultExplanationField = value; }
        }

        private string stateField;

        [DataMember]
        public string StateField
        {
            get { return stateField; }
            set { stateField = value; }
        }

        private string followUpTypeField;

        [DataMember]
        public string FollowUpTypeField
        {
            get { return followUpTypeField; }
            set { followUpTypeField = value; }
        }

        private string followUpDateField;

        [DataMember]
        public string FollowUpDateField
        {
            get { return followUpDateField; }
            set { followUpDateField = value; }
        }

        private string activityCommentField;

        [DataMember]
        public string ActivityCommentField
        {
            get { return activityCommentField; }
            set { activityCommentField = value; }
        }

        private string descrpt01Field;

        [DataMember]
        public string Descrpt01Field
        {
            get { return descrpt01Field; }
            set { descrpt01Field = value; }
        }

        private string descrpt02Field;

        [DataMember]
        public string Descrpt02Field
        {
            get { return descrpt02Field; }
            set { descrpt02Field = value; }
        }

        private string descrpt03Field;

        [DataMember]
        public string Descrpt03Field
        {
            get { return descrpt03Field; }
            set { descrpt03Field = value; }
        }

        private string descrpt04Field;

        [DataMember]
        public string Descrpt04Field
        {
            get { return descrpt04Field; }
            set { descrpt04Field = value; }
        }

        private string descrpt05Field;

        [DataMember]
        public string Descrpt05Field
        {
            get { return descrpt05Field; }
            set { descrpt05Field = value; }
        }

        private string descrpt06Field;

        [DataMember]
        public string Descrpt06Field
        {
            get { return descrpt06Field; }
            set { descrpt06Field = value; }
        }

        private string descrpt07Field;

        [DataMember]
        public string Descrpt07Field
        {
            get { return descrpt07Field; }
            set { descrpt07Field = value; }
        }

        private string descrpt08Field;

        [DataMember]
        public string Descrpt08Field
        {
            get { return descrpt08Field; }
            set { descrpt08Field = value; }
        }

        private string descrpt09Field;

        [DataMember]
        public string Descrpt09Field
        {
            get { return descrpt09Field; }
            set { descrpt09Field = value; }
        }

        private string descrpt10Field;

        [DataMember]
        public string Descrpt10Field
        {
            get { return descrpt10Field; }
            set { descrpt10Field = value; }
        }

        private string txtKonseField;

        [DataMember]
        public string TxtKonseField
        {
            get { return txtKonseField; }
            set { txtKonseField = value; }
        }

        private string directionField;

        [DataMember]
        public string DirectionField
        {
            get { return directionField; }
            set { directionField = value; }
        }

        private string partnRoleField;

        [DataMember]
        public string PartnRoleField
        {
            get { return partnRoleField; }
            set { partnRoleField = value; }
        }

        private string partnIdField;

        [DataMember]
        public string PartnIdField
        {
            get { return partnIdField; }
            set { partnIdField = value; }
        }

        private string contactRoleField;

        [DataMember]
        public string ContactRoleField
        {
            get { return contactRoleField; }
            set { contactRoleField = value; }
        }

        private string contactField;

        [DataMember]
        public string ContactField
        {
            get { return contactField; }
            set { contactField = value; }
        }

        private string languField;

        [DataMember]
        public string LanguField
        {
            get { return languField; }
            set { languField = value; }
        }

        private string languIsoField;

        [DataMember]
        public string LanguIsoField
        {
            get { return languIsoField; }
            set { languIsoField = value; }
        }

        //[DataMember]
        //public List<BusinessPartner> BusinessPartners { get; set; }

        //[DataMember]
        //public List<Text> Texts{ get; set; }
    }

    [DataContract]
    public class BusinessPartner
    {
        private string refobjecttypeField;

        [DataMember]
        public string RefobjecttypeField
        {
            get { return refobjecttypeField; }
            set { refobjecttypeField = value; }
        }

        private string refobjectkeyField;

        [DataMember]
        public string RefobjectkeyField
        {
            get { return refobjectkeyField; }
            set { refobjectkeyField = value; }
        }

        private string docNumberField;

        [DataMember]
        public string DocNumberField
        {
            get { return docNumberField; }
            set { docNumberField = value; }
        }

        private string itmNumberField;

        [DataMember]
        public string ItmNumberField
        {
            get { return itmNumberField; }
            set { itmNumberField = value; }
        }

        private string countParvwField;

        [DataMember]
        public string CountParvwField
        {
            get { return countParvwField; }
            set { countParvwField = value; }
        }

        private string partnRoleField;

        [DataMember]
        public string PartnRoleField
        {
            get { return partnRoleField; }
            set { partnRoleField = value; }
        }

        private string partnRoleOldField;

        [DataMember]
        public string PartnRoleOldField
        {
            get { return partnRoleOldField; }
            set { partnRoleOldField = value; }
        }

        private string partnIdField;

        [DataMember]
        public string PartnIdField
        {
            get { return partnIdField; }
            set { partnIdField = value; }
        }

        private string partnIdOldField;

        [DataMember]
        public string PartnIdOldField
        {
            get { return partnIdOldField; }
            set { partnIdOldField = value; }
        }

        private string addrNoField;

        [DataMember]
        public string AddrNoField
        {
            get { return addrNoField; }
            set { addrNoField = value; }
        }

        private string persNoField;

        [DataMember]
        public string PersNoField
        {
            get { return persNoField; }
            set { persNoField = value; }
        }

        private string addrtypeField;

        [DataMember]
        public string AddrtypeField
        {
            get { return addrtypeField; }
            set { addrtypeField = value; }
        }

        private string addrOriginField;

        [DataMember]
        public string AddrOriginField
        {
            get { return addrOriginField; }
            set { addrOriginField = value; }
        }

        private string unloadPtField;

        [DataMember]
        public string UnloadPtField
        {
            get { return unloadPtField; }
            set { unloadPtField = value; }
        }

        private string calendarUpdateField;

        [DataMember]
        public string CalendarUpdateField
        {
            get { return calendarUpdateField; }
            set { calendarUpdateField = value; }
        }

        private string addrLinkField;

        [DataMember]
        public string AddrLinkField
        {
            get { return addrLinkField; }
            set { addrLinkField = value; }
        }

        //private string partnNameField;

        //[DataMember]
        //public string PartnNameField
        //{
        //    get { return partnNameField; }
        //    set { partnNameField = value; }
        //}

    }

    [DataContract]
    public class Text
    {
        private string refobjecttypeField;

        [DataMember]
        public string RefobjecttypeField
        {
            get { return refobjecttypeField; }
            set { refobjecttypeField = value; }
        }

        private string refobjectkeyField;

        [DataMember]
        public string RefobjectkeyField
        {
            get { return refobjectkeyField; }
            set { refobjectkeyField = value; }
        }

        private string docNumberField;

        [DataMember]
        public string DocNumberField
        {
            get { return docNumberField; }
            set { docNumberField = value; }
        }

        private string textIdField;

        [DataMember]
        public string TextIdField
        {
            get { return textIdField; }
            set { textIdField = value; }
        }

        private string languField;

        [DataMember]
        public string LanguField
        {
            get { return languField; }
            set { languField = value; }
        }

        private string langupIsoField;

        [DataMember]
        public string LangupIsoField
        {
            get { return langupIsoField; }
            set { langupIsoField = value; }
        }

        private string textLineField;

        [DataMember]
        public string TextLineField
        {
            get { return textLineField; }
            set { textLineField = value; }
        }

        private string functionField;

        [DataMember]
        public string FunctionField
        {
            get { return functionField; }
            set { functionField = value; }
        }
    }

    [DataContract]
    public class Customer
    {
        private string customerField;

        [DataMember]
        public string CustomerField
        {
            get { return customerField; }
            set { customerField = value; }
        }

        private string sort1Field;

        [DataMember]
        public string Sort1Field
        {
            get { return sort1Field; }
            set { sort1Field = value; }
        }

        private string nameField;
        
        [DataMember]
        public string NameField
        {
            get { return nameField; }
            set { nameField = value; }
        }

        private string countryField;

        [DataMember]
        public string CountryField
        {
            get { return countryField; }
            set { countryField = value; }
        }

        private string countryisoField;

        [DataMember]
        public string CountryisoField
        {
            get { return countryisoField; }
            set { countryisoField = value; }
        }

        private string cityField;

        [DataMember]
        public string CityField
        {
            get { return cityField; }
            set { cityField = value; }
        }

        private string postlCod1Field;

        [DataMember]
        public string PostlCod1Field
        {
            get { return postlCod1Field; }
            set { postlCod1Field = value; }
        }

        private string regionField;

        [DataMember]
        public string RegionField
        {
            get { return regionField; }
            set { regionField = value; }
        }

        private string streetField;

        [DataMember]
        public string StreetField
        {
            get { return streetField; }
            set { streetField = value; }
        }

        private string tel1NumbrField;

        [DataMember]
        public string Tel1NumbrField
        {
            get { return tel1NumbrField; }
            set { tel1NumbrField = value; }
        }

        private string faxNumberField;

        [DataMember]
        public string FaxNumberField
        {
            get { return faxNumberField; }
            set { faxNumberField = value; }
        }

        private string addressField;

        [DataMember]
        public string AddressField
        {
            get { return addressField; }
            set { addressField = value; }
        }
    }

    [DataContract]
    public class CreateSalesActivityRequest
    {
        [DataMember]
        public List<ActivityGeneralData> ActivityGeneralData { get; set; }

        [DataMember]
        public List<BusinessPartner> BusinessPartners { get; set; }

        [DataMember]
        public List<Text> Texts { get; set; }
    }

    [DataContract]
    public class CreateSalesActivityResponse
    {
        [DataMember]
        public List<ActivityGeneralData> ActivityGeneralData { get; set; }

        [DataMember]
        public List<BusinessPartner> BusinessPartners { get; set; }

        [DataMember]
        public List<Text> Texts { get; set; }
    }

    [DataContract]
    public class UpdateSalesActivityRequest
    {
        [DataMember]
        public List<ActivityGeneralData> ActivityGeneralData { get; set; }

        [DataMember]
        public List<BusinessPartner> BusinessPartners { get; set; }

        [DataMember]
        public List<Text> Texts { get; set; }
    }

    [DataContract]
    public class UpdateSalesActivityResponse
    {
        [DataMember]
        public List<ActivityGeneralData> ActivityGeneralData { get; set; }

        [DataMember]
        public List<BusinessPartner> BusinessPartners { get; set; }

        [DataMember]
        public List<Text> Texts { get; set; }
    }

    [DataContract]
    public class DeleteSalesActivityRequest
    {
        public List<ActivityKeyData> Activities { get; set; }
    }

    public class ActivityKeyData
    {
        //private string refobjecttypeField;
		//
        //[DataMember]
        //public string RefobjecttypeField
        //{
        //    get { return refobjecttypeField; }
        //    set { refobjecttypeField = value; }
        //}

        //private string refobjectkeyField;

        //[DataMember]
        //public string RefobjectkeyField
        //{
        //    get { return refobjectkeyField; }
        //    set { refobjectkeyField = value; }
        //}

        private string docNumberField;

        [DataMember]
        public string DocNumberField
        {
            get { return docNumberField; }
            set { docNumberField = value; }
        }
    }

    #endregion

}
