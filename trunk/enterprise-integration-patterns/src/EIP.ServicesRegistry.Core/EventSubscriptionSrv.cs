using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core
{
	public class EventSubscriptionSrv
	{
		private IEventSubscriptionDAO dao;

		public EventSubscriptionSrv(IEventSubscriptionDAO dao)
		{
			this.dao = dao;
		}

		internal EventSubscription Create(EventSubscription subscription)
		{
			return dao.Insert(subscription);
		}

		internal void Remove(string id)
		{
			dao.Remove(id);
		}

		internal EventSubscription GetById(string id)
		{
			return dao.GetById(id);
		}

		internal EventSubscription[] Search(string term)
		{
			return dao.Search(term);
		}

		internal EventSubscription[] GetAll()
		{
			return dao.GetAll();
		}

		internal void Update(EventSubscription subscription)
		{
			dao.Update(subscription);
		}

		internal void Update(string subscriptionId, string name, string subscriberAddress)
		{
			var subscription = dao.GetById(subscriptionId);
			subscription.Name = name;
			subscription.SubscriberAddress = subscriberAddress;
			dao.Update(subscription);
		}

		internal void UpdateSubscriberAddress(string subscriptionId, string subscriberAddress)
		{
			var subscription = dao.GetById(subscriptionId);
			subscription.SubscriberAddress = subscriberAddress;
			dao.Update(subscription);
		}
	}
}
