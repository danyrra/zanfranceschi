using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.ServicesRegistry.Core.DAL
{
	internal interface IEventSubscriptionDAO
	{
		EventSubscription Insert(EventSubscription subscription);
		void Remove(string id);
		EventSubscription GetById(string id);
		EventSubscription[] Search(string term);
		EventSubscription[] GetAll();
	}
}