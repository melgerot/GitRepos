using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL.Managers
{
    class UpdateEventArgs : EventArgs
    {
        public Exception Error { get; set; }
    }
}
