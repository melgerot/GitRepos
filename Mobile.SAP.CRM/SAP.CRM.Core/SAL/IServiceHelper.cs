using System;

namespace SAP.CRM.Core.SAL
{
	public interface IServiceHelper
	{
		void GetActivityMetaData(Action action);

		void GetActivities(Action action);

		void GetActivitiesDetailed(Action action);

		void SyncActivities(Action action);

		void GetCustomerMetaData(Action action);

		void GetCustomers(Action action);

		void GetCustomersDetailed(Action action);

		void GetContacts(Action action);

		void GetContactsDetailed(Action action);

		void SyncContacts(Action action);

	}
}

