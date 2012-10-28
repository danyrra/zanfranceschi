using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL;

namespace EIP.ServicesRegistry.Core
{
	internal static class EventSubscriptionService
	{
		private static IEventSubscriptionDAO dao = DAOFactory.EventSubscritpionDAO;

		internal static EventSubscription Create(EventSubscription subscription)
		{
			return dao.Insert(subscription);
		}

		internal static void Remove(string id)
		{
			dao.Remove(id);
		}

		internal static EventSubscription GetById(string id)
		{
			return dao.GetById(id);
		}

		internal static EventSubscription[] Search(string term)
		{
			return dao.Search(term);
		}

		internal static EventSubscription[] GetAll()
		{
			return dao.GetAll();
		}
	}
}
