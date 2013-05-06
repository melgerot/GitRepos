using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.CRM.Gateway;
using SAP.CRM.Core.BL.Contracts;
using SAP.CRM.Core.SQLite;

namespace SAP.CRM.Core.BL
{
    public class Activity : BusinessEntityBase
    {
        [IgnoreAttribute]
        public List<ActivityPartner> Partners { get; set; }

        [IgnoreAttribute]
        public List<ActivityText> Texts { get; set; }
    }
}
