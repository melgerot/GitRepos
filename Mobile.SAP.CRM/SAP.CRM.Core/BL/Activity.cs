using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SAP.CRM.Gateway;
using SAP.CRM.Core.BL.Contracts;
using SAP.CRM.Core.SQLite;

namespace SAP.CRM.Core.BL
{
	public class Activity : BusinessEntityBase
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

        private string refdoctypeField;


        public string RefdoctypeField
        {
            get { return refdoctypeField; }
            set { refdoctypeField = value; }
        }

        private string refreltypeField;


        public string RefreltypeField
        {
            get { return refreltypeField; }
            set { refreltypeField = value; }
        }

        private string activityTypeField;


        public string ActivityTypeField
        {
            get { return activityTypeField; }
            set { activityTypeField = value; }
        }

        private string salesorgField;


        public string SalesorgField
        {
            get { return salesorgField; }
            set { salesorgField = value; }
        }

        private string distrChanField;


        public string DistrChanField
        {
            get { return distrChanField; }
            set { distrChanField = value; }
        }

        private string divisionField;


        public string DivisionField
        {
            get { return divisionField; }
            set { divisionField = value; }
        }

        private string salesOffField;


        public string SalesOffField
        {
            get { return salesOffField; }
            set { salesOffField = value; }
        }

        private string salesGrpField;


        public string SalesGrpField
        {
            get { return salesGrpField; }
            set { salesGrpField = value; }
        }

        private string fromDateField;


        public string FromDateField
        {
            get { return fromDateField; }
            set { fromDateField = value; }
        }

        private string toDateField;


        public string ToDateField
        {
            get { return toDateField; }
            set { toDateField = value; }
        }

        private string fromTimeField;


        public string FromTimeField
        {
            get { return fromTimeField; }
            set { fromTimeField = value; }
        }

        private string toTimeField;


        public string ToTimeField
        {
            get { return toTimeField; }
            set { toTimeField = value; }
        }

        private string reasonField;


        public string ReasonField
        {
            get { return reasonField; }
            set { reasonField = value; }
        }

        private string resultField;


        public string ResultField
        {
            get { return resultField; }
            set { resultField = value; }
        }

        private string resultExplanationField;


        public string ResultExplanationField
        {
            get { return resultExplanationField; }
            set { resultExplanationField = value; }
        }

        private string stateField;


        public string StateField
        {
            get { return stateField; }
            set { stateField = value; }
        }

        private string followUpTypeField;


        public string FollowUpTypeField
        {
            get { return followUpTypeField; }
            set { followUpTypeField = value; }
        }

        private string followUpDateField;


        public string FollowUpDateField
        {
            get { return followUpDateField; }
            set { followUpDateField = value; }
        }

        private string activityCommentField;


        public string ActivityCommentField
        {
            get { return activityCommentField; }
            set { activityCommentField = value; }
        }

        private string descrpt01Field;


        public string Descrpt01Field
        {
            get { return descrpt01Field; }
            set { descrpt01Field = value; }
        }

        private string descrpt02Field;


        public string Descrpt02Field
        {
            get { return descrpt02Field; }
            set { descrpt02Field = value; }
        }

        private string descrpt03Field;


        public string Descrpt03Field
        {
            get { return descrpt03Field; }
            set { descrpt03Field = value; }
        }

        private string descrpt04Field;


        public string Descrpt04Field
        {
            get { return descrpt04Field; }
            set { descrpt04Field = value; }
        }

        private string descrpt05Field;


        public string Descrpt05Field
        {
            get { return descrpt05Field; }
            set { descrpt05Field = value; }
        }

        private string descrpt06Field;


        public string Descrpt06Field
        {
            get { return descrpt06Field; }
            set { descrpt06Field = value; }
        }

        private string descrpt07Field;


        public string Descrpt07Field
        {
            get { return descrpt07Field; }
            set { descrpt07Field = value; }
        }

        private string descrpt08Field;


        public string Descrpt08Field
        {
            get { return descrpt08Field; }
            set { descrpt08Field = value; }
        }

        private string descrpt09Field;


        public string Descrpt09Field
        {
            get { return descrpt09Field; }
            set { descrpt09Field = value; }
        }

        private string descrpt10Field;


        public string Descrpt10Field
        {
            get { return descrpt10Field; }
            set { descrpt10Field = value; }
        }

        private string txtKonseField;


        public string TxtKonseField
        {
            get { return txtKonseField; }
            set { txtKonseField = value; }
        }

        private string directionField;


        public string DirectionField
        {
            get { return directionField; }
            set { directionField = value; }
        }

        private string partnRoleField;


        public string PartnRoleField
        {
            get { return partnRoleField; }
            set { partnRoleField = value; }
        }

        private string partnIdField;


        public string PartnIdField
        {
            get { return partnIdField; }
            set { partnIdField = value; }
        }

        private string contactRoleField;


        public string ContactRoleField
        {
            get { return contactRoleField; }
            set { contactRoleField = value; }
        }

        private string contactField;


        public string ContactField
        {
            get { return contactField; }
            set { contactField = value; }
        }

        private string languField;


        public string LanguField
        {
            get { return languField; }
            set { languField = value; }
        }

        private string languIsoField;


        public string LanguIsoField
        {
            get { return languIsoField; }
            set { languIsoField = value; }
        }
	}
}

