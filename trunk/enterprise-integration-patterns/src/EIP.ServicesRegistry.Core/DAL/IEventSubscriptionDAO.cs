using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core.DAL
{
	public interface IEventSubscriptionDAO
	{
		EventSubscription Insert(EventSubscription subscription);
		void Update(EventSubscription subscription);
		void Remove(string id);
		EventSubscription GetById(string id);
		EventSubscription[] Search(string term);
		EventSubscription[] GetAll();
	}
}