using SAP.CRM.Core.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL
{
    public class ActivityPartner : BusinessEntityBase
    {
        private string refobjecttypeField;

        
        public string RefobjecttypeField
        {
            get { return refobjecttypeField; }
            set { refobjecttypeField = value; }
        }

        private string refobjectkeyField;

        
        public string RefobjectkeyField
        {
            get { return refobjectkeyField; }
            set { refobjectkeyField = value; }
        }

        private string docNumberField;

        
        public string DocNumberField
        {
            get { return docNumberField; }
            set { docNumberField = value; }
        }

        private string itmNumberField;

        
        public string ItmNumberField
        {
            get { return itmNumberField; }
            set { itmNumberField = value; }
        }

        private string countParvwField;

        
        public string CountParvwField
        {
            get { return countParvwField; }
            set { countParvwField = value; }
        }

        private string partnRoleField;

        
        public string PartnRoleField
        {
            get { return partnRoleField; }
            set { partnRoleField = value; }
        }

        private string partnRoleOldField;

        
        public string PartnRoleOldField
        {
            get { return partnRoleOldField; }
            set { partnRoleOldField = value; }
        }

        private string partnIdField;

        
        public string PartnIdField
        {
            get { return partnIdField; }
            set { partnIdField = value; }
        }

        private string partnIdOldField;

        
        public string PartnIdOldField
        {
            get { return partnIdOldField; }
            set { partnIdOldField = value; }
        }

        private string addrNoField;

        
        public string AddrNoField
        {
            get { return addrNoField; }
            set { addrNoField = value; }
        }

        private string persNoField;

        
        public string PersNoField
        {
            get { return persNoField; }
            set { persNoField = value; }
        }

        private string addrtypeField;

        
        public string AddrtypeField
        {
            get { return addrtypeField; }
            set { addrtypeField = value; }
        }

        private string addrOriginField;

        
        public string AddrOriginField
        {
            get { return addrOriginField; }
            set { addrOriginField = value; }
        }

        private string unloadPtField;

        
        public string UnloadPtField
        {
            get { return unloadPtField; }
            set { unloadPtField = value; }
        }

        private string calendarUpdateField;

        
        public string CalendarUpdateField
        {
            get { return calendarUpdateField; }
            set { calendarUpdateField = value; }
        }

        private string addrLinkField;

        
        public string AddrLinkField
        {
            get { return addrLinkField; }
            set { addrLinkField = value; }
        }

        //private string partnNameField;


        //public string PartnNameField
        //{
        //    get { return partnNameField; }
        //    set { partnNameField = value; }
        //}
    }
}
