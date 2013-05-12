using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SAP.CRM.Gateway;
using SAP.CRM.Core.BL.Contracts;
using SAP.CRM.Core.SQLite;

namespace SAP.CRM.Core.BL
{
	public class ActivityDetail : BusinessEntityBase
	{
		private string activityId;
		
		public string ActivityId {
			get {
				return activityId;
			}
			set {
				activityId = value;
			}
		}

		private  List<ActivityPartner> partners;

		[IgnoreAttribute]
		public List<ActivityPartner> Partners {
			get {
				return partners;
			}
			set {
				partners = value;
			}
		}

		private List<ActivityText> texts;

		[IgnoreAttribute]
		public List<ActivityText> Texts {
			get {
				return texts;
			}
			set {
				texts = value;
			}
		}
	}
}

