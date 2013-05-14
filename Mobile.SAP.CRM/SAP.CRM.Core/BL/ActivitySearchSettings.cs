using SAP.CRM.Core.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL
{
    public class ActivitySearchSettings : BusinessEntityBase
    {
        public int DaysBackward { get; set; }

        public int DaysForward { get; set; }

        public string MyCustomers { get; set; }

        public string MyActivities { get; set; }

        public bool StatusOpen { get; set; }

        public bool StatusInProgress { get; set; }

        public bool StatusCompleted { get; set; }
    }
}
