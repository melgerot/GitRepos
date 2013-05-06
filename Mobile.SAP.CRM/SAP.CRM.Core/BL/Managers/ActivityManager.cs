using SAP.CRM.Core.BL;
using SAP.CRM.Core.DAL;
using SAP.CRM.Core.SAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL.Managers
{
    public static class ActivityManager
    {
        static object locker = new object();

        public static event EventHandler UpdateStarted = delegate { };
        public static event EventHandler UpdateFinished = delegate { };

        private static bool _isUpdating = false;

        public static bool IsUpdating
        {
            get { return _isUpdating; }
            //set { _isUpdating = value; }
        }

        static ActivityManager()
        {
        }

        public static void Uppdate()
        {
            // make this a critical section to ensure that access is serial
            lock (locker)
            {
                Debug.WriteLine("Update started");
                UpdateStarted(null, EventArgs.Empty);

                Debug.WriteLine("Begin process Servicce Application Layer - Async");
                ServiceHelper helper = new ServiceHelper();
   
                _isUpdating = true;
                helper.GetSalesActivities(delegate
                {
                    Debug.WriteLine("Processing update in delegate");
                    var list = helper.BusinessEntityList.FirstOrDefault();
                    if (list != null)
                    {
                        Debug.WriteLine("Data exist and is updated in database");
                        // TODO: Delete cascade all old activity data

                        // TODO: Save all new data

                    } 
                      Debug.WriteLine("Update finished");
                     UpdateFinished(null, EventArgs.Empty);
                     _isUpdating = false;
                });           
            }
        }

        public static Activity GetActivity(int id)
        {
            return ActivityRepository.GetActivity(id);
        }

        public static IList<Activity> GetActivities()
        {
            return new List<Activity>(ActivityRepository.GetActivities());
        }
    }
}
