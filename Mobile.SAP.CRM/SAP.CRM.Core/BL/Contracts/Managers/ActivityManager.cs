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
            set { _isUpdating = value; }
        }

        static ActivityManager()
        {
        }

        public static void RefreshActivityList(List<Activity> refreshList)
        {
            // TODO: Check that ActivityManager is not updating

            // TODO: Check that new and changed data has been synced to backend.
 
            // TODO: Perform check for last refresh date

            // make this a critical section to ensure that access is serial
            lock (locker)
            {               
                Debug.WriteLine("Update started");                           
                UpdateStarted(null, EventArgs.Empty);
                IsUpdating = true;
                
                Debug.WriteLine("Begin process service application layer");
                ServiceHelper helper = new ServiceHelper();
                // Wire events 
                helper.OnGetActivitiesCompleted += delegate
                {
                    lock (locker)
                    {
                        // TODO: Delete existing activity data
						// ApplicationRepository.DeleteActivities();
					    List<Activity> activityList = ApplicationRepository.GetActivities().ToList();
						foreach (var item in activityList) {
							ApplicationRepository.DeleteActivity(item.ID);
						}

                        // Persist entities to database
                        foreach (var item in helper.Entities.OfType<Activity>())
                        {
                            ApplicationRepository.SaveActivity(item);
                        }

                        foreach (var item in helper.Entities.OfType<ActivityPartner>())
                        {
                            ApplicationRepository.SaveActivityPartner(item);
                        } 

                        Debug.WriteLine("Update completed");
                        IsUpdating = false;
                    }
                    UpdateFinished(null, EventArgs.Empty);         
                };
                helper.OnGetActivitiesFailed += delegate
                {
                    Debug.WriteLine("Update failed");      
                    IsUpdating = false;
                    UpdateFinished(null, EventArgs.Empty);                  
                };
                
				try {
					// Do service calls
					helper.GetActivities(false);
				} catch (Exception ex) {
					// Service call could not be peformed due to unknown reason
					// Propagate exeception to caller
					throw ex;
				}
                
            }
        }

        public static List<Activity> GetActivityList()
        {
            return ApplicationRepository.GetActivities().ToList<Activity>();
        }

		public static Activity GetActivity (int currentActivity)
		{
			return ApplicationRepository.GetActivity (currentActivity);
		}

		public static ActivitySearchSettings GetActivitySearchSettings ()
		{
			return ApplicationRepository.GetFirstActivitySearchSettings();
		}

		public static int SaveActivitySearchSettings (ActivitySearchSettings item)
		{
			lock(locker)
			{
				return ApplicationRepository.SaveActivitySearchSettings(item);
			}
		}

        //public static Activity GetActivity(int id)
        //{
        //    return ActivityRepository.GetActivity(id);
        //}

        //public static IList<Activity> GetActivities()
        //{
        //    return new List<Activity>(ActivityRepository.GetActivities());
        //}


    }
}
