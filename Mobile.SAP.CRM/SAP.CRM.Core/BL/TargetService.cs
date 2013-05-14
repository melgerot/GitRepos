using SAP.CRM.Core.BL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL
{
    public class TargetService : BusinessEntityBase
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public bool Active { get; set; }
    }
}
