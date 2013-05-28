using SAP.CRM.Core.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL
{
    public class CustomerContact : BusinessEntityBase
    {
        private string partneremployeeidField;
        
        public string PartneremployeeidField
        {
            get { return partneremployeeidField; }
            set { partneremployeeidField = value; }
        }

        private string customerField;
        
        public string CustomerField
        {
            get { return customerField; }
            set { customerField = value; }
        }

        private string lastnameField;
        
        public string LastnameField
        {
            get { return lastnameField; }
            set { lastnameField = value; }
        }

        private string firstnameField;
        
        public string FirstnameField
        {
            get { return firstnameField; }
            set { firstnameField = value; }
        }

        private string sexField;
        
        public string SexField
        {
            get { return sexField; }
            set { sexField = value; }
        }

        private string titlePField;
        
        public string TitlePField
        {
            get { return titlePField; }
            set { titlePField = value; }
        }

        private string languPField;
        
        public string LanguPField
        {
            get { return languPField; }
            set { languPField = value; }
        }

        private string langupIsoField;
        
        public string LangupIsoField
        {
            get { return langupIsoField; }
            set { langupIsoField = value; }
        }

        private string countryField;
        
        public string CountryField
        {
            get { return countryField; }
            set { countryField = value; }
        }

        private string countryisoField;
        
        public string CountryisoField
        {
            get { return countryisoField; }
            set { countryisoField = value; }
        }

        private string cityField;
        
        public string CityField
        {
            get { return cityField; }
            set { cityField = value; }
        }

        private string postlCod1Field;
        
        public string PostlCod1Field
        {
            get { return postlCod1Field; }
            set { postlCod1Field = value; }
        }

        private string regionField;
        
        public string RegionField
        {
            get { return regionField; }
            set { regionField = value; }
        }

        private string streetField;
        
        public string StreetField
        {
            get { return streetField; }
            set { streetField = value; }
        }

        private string tel1NumbrField;
        
        public string Tel1NumbrField
        {
            get { return tel1NumbrField; }
            set { tel1NumbrField = value; }
        }

        private string faxNumberField;
        
        public string FaxNumberField
        {
            get { return faxNumberField; }
            set { faxNumberField = value; }
        }

        private string functionField;
        
        public string FunctionField
        {
            get { return functionField; }
            set { functionField = value; }
        }

        private string sort1PField;
        
        public string Sort1PField
        {
            get { return sort1PField; }
            set { sort1PField = value; }
        }

        private string addressField;
        
        public string AddressField
        {
            get { return addressField; }
            set { addressField = value; }
        }

        private string persNoField;
        
        public string PersNoField
        {
            get { return persNoField; }
            set { persNoField = value; }
        }

        private string eMailField;
        
        public string EMailField
        {
            get { return eMailField; }
            set { eMailField = value; }
        }
    }
}
