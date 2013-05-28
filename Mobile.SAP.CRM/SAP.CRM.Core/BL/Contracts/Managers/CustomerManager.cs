using SAP.CRM.Core.BL.Contracts;
using SAP.CRM.Core.DAL;
using SAP.CRM.Core.SAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.BL.Managers
{
    public static class CustomerManager
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

        static CustomerManager()
        {
        }

        public static void GetRemoteCustomerData()
        {
            // make this a critical section to ensure that access is serial
            lock (locker)
            {
                Debug.WriteLine("Update started");
                UpdateStarted(null, EventArgs.Empty);
                IsUpdating = true;

                Debug.WriteLine("Begin process service application layer");
                ServiceHelper helper = new ServiceHelper();
                // Wire events 
                helper.OnGetCustomersCompleted += delegate
                {
                    // Secure database sync
                    lock (locker)
                    {
                        // Sync with existing entities
                        foreach (var item in helper.Entities.OfType<Customer>())
                        {
                            var customer = ApplicationRepository.GetCustomerWithExternalId(item);
                            if (customer != null) item.ID = customer.ID;
                            ApplicationRepository.SaveCustomer(item);
                        }
                        foreach (var item in helper.Entities.OfType<CustomerContact>())
                        {
                            var customerContact = ApplicationRepository.GetCustomerContactWithExternalId(item);
                            if (customerContact != null) item.ID = customerContact.ID;
                            ApplicationRepository.SaveCustomerContact(item);
                        }         
                    }
                    List<IBusinessEntity> entities = new List<IBusinessEntity>();
                    foreach (var item in helper.Entities)
                    {
                        entities.Add(item);
                    }

                    Debug.WriteLine("Update completed");
                    IsUpdating = false;
                    UpdateFinished(entities, EventArgs.Empty);
                };
                helper.OnGetActivitiesFailed += delegate
                {
                    Debug.WriteLine("Update failed");
                    IsUpdating = false;
                    UpdateFinished(null, EventArgs.Empty);
                };

                try
                {
                    // Do service calls
                    helper.GetCustomers(customerIdList, myCustomers, searchCriteria);
                }
                catch (Exception ex)
                {
                    // Service call could not be peformed due to unknown reason
                    // Propagate exeception to caller
                    throw ex;
                }

            }
        }

        public static void SaveRemoteCustomerData(List<IBusinessEntity> customerData)
        {
            // make this a critical section to ensure that storage is serial
            lock (locker)
            {
                // Persist entities to database
                foreach (var item in customerData.OfType<Customer>())
                {
                    ApplicationRepository.SaveCustomer(item);
                }
                foreach (var item in customerData.OfType<CustomerContact>())
                {
                    ApplicationRepository.SaveCustomerContact(item);
                }
            }
        }

        public static List<IBusinessEntity> GetCustomerData()
        {
            List<IBusinessEntity> customerData = new List<IBusinessEntity>();
            customerData.AddRange(ApplicationRepository.GetCustomers().Cast<IBusinessEntity>());
            customerData.AddRange(ApplicationRepository.GetCustomerContacts().Cast<IBusinessEntity>());
            //customerData.Sort();
            return customerData;
        }
    }
}
