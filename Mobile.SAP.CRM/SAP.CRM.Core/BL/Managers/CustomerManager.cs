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
        public static event EventHandler<UpdateEventArgs> UpdateFinished = delegate { };

        private static bool _isUpdating = false;

        public static bool IsUpdating
        {
            get { return _isUpdating; }
            set { _isUpdating = value; }
        }

		private static bool _isMoreData = true;

        static CustomerManager()
        {
        }

        private static List<IBusinessEntity> entityCache;

        public static void GetRemoteCustomerData(List<Customer> customerList, bool myCustomers, string searchCriteria)
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
                helper.OnGetCustomersCompleted += delegate(GetDataEventArgs eventArgs)
                {
                    List<Customer> customers = new List<Customer>();
                    UpdateEventArgs args = new UpdateEventArgs();

                    if (eventArgs.Error != null)
                    {
                        args.Error = eventArgs.Error;
                        Debug.WriteLine("Update failed");
                    }
                    else
                    {
                        //Cache entities
                        entityCache = helper.Entities;
                        // Secure database sync
                        lock (locker)
                        {
                            // Sync with existing customer data
                            foreach (var item in entityCache.OfType<Customer>())
                            {
                                var customer = ApplicationRepository.GetCustomerWithExternalId(item);
                                if (customer != null)
                                {
                                    item.ID = customer.ID;
                                    SyncCustomerData(item);
                                }
                            }
                        }
                        Debug.WriteLine("Update completed");
                    }
                    IsUpdating = false;
                    UpdateFinished(null, args);
                };

                // Prepare request params 
//                List<string> customerIdList = new List<string>();
//                foreach (var item in customerList)
//                {
//                    customerIdList.Add(item.CustomerField);
//                }

                try
                {
                    // Do service calls
                    helper.GetCustomers(myCustomers, searchCriteria);
                }
                catch (Exception ex)
                {
                    // Service call could not be peformed due to unknown reason
                    // Propagate exeception to caller
                    throw ex;
                }
            }
        }

        public static void SyncCustomerData(Customer customer)
        {
            // make this a critical section to ensure that storage is serial
            lock (locker)
            {
                ApplicationRepository.SaveCustomer(customer);
                // Sync customer contacts
                var customerContactList = ApplicationRepository.GetCustomerContactsWithExternalCustomerId(customer.CustomerField).ToList();
                foreach (var item in entityCache.OfType<CustomerContact>().Where(c => c.CustomerField == customer.CustomerField))
                {
                    var customerContact = customerContactList.FirstOrDefault(c => c.PartneremployeeidField == item.PartneremployeeidField);
                    if (customerContact != null) item.ID = customerContact.ID;
                    ApplicationRepository.SaveCustomerContact(item);
                    customerContactList.Remove(customerContact);
                }
                foreach (var item in customerContactList)
                {
                    ApplicationRepository.DeleteCustomerContact(item.ID);
                }
            }
        }

        public static void RemoveCustomerData(Customer customer)
        {
            // make this a critical section to ensure that storage is serial
            lock (locker)
            {
                var customerContacts = ApplicationRepository.GetCustomerContactsWithExternalCustomerId(customer.CustomerField).ToList();
                foreach (var item in customerContacts)
                {
                    ApplicationRepository.DeleteCustomerContact(item.ID);
                }
                ApplicationRepository.DeleteCustomer(customer.ID);
                if (entityCache != null)
                {
                    foreach (var item in entityCache.OfType<CustomerContact>().Where(c => c.CustomerField == customer.CustomerField))
                    {
                        item.ID = 0;
                    }
                    foreach (var item in entityCache.OfType<Customer>().Where(c => c.CustomerField == customer.CustomerField))
                    {
                        item.ID = 0;
                    }
                }
            }
        }

        public static void ClearCache()
        {
            entityCache = null;
        }

		public static List<Customer> GetCustomerDataFromCache()
		{
			return entityCache.OfType<Customer>().ToList();
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
