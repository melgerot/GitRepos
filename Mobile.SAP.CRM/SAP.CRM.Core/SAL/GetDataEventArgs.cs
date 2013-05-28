using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.SAL
{
    public class GetDataEventArgs : EventArgs
    {
        public Exception Error { get; set; }
    }
}
