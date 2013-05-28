using SAP.CRM.Core.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL
{
    public class Customer : BusinessEntityBase
    {
        private string customerField;

        
        public string CustomerField
        {
            get { return customerField; }
            set { customerField = value; }
        }

        private string sort1Field;

        
        public string Sort1Field
        {
            get { return sort1Field; }
            set { sort1Field = value; }
        }

        private string nameField;

        
        public string NameField
        {
            get { return nameField; }
            set { nameField = value; }
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

        private string addressField;

        
        public string AddressField
        {
            get { return addressField; }
            set { addressField = value; }
        }
 
    }
}
