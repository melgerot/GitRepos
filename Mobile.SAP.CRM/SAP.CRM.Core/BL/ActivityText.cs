using SAP.CRM.Core.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL
{
    public class ActivityText : BusinessEntityBase
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

		private string textId;

		public string TextId {
			get {
				return textId;
			}
			set {
				textId = value;
			}
		}

    }
}
