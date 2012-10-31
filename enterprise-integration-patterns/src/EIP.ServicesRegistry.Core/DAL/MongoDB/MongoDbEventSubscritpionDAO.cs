using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core.DAL._MongoDB
{
	public class MongoDbEventSubscritpionDAO
		: MongoDbBaseDAO<EventSubscription>, IEventSubscriptionDAO
	{
		public MongoDbEventSubscritpionDAO(MongoServer server, string database)
			: base(server, database, "subscriptions") 
		{
			if (!BsonClassMap.IsClassMapRegistered(typeof(EventSubscription)))
			{
				BsonClassMap.RegisterClassMap<EventSubscription>(cm =>
				{
					cm.AutoMap();
				});
			}
		}

		protected override bool SearchCriteria(EventSubscription obj, string term)
		{
			term = term.ToLower();

			return
				obj.SubscriberAddress.ToLower().Contains(term)
				|| obj.EventService.DataType.ToLower().Contains(term)
				|| obj.EventService.Name.ToLower().Contains(term)
				|| obj.Name.ToLower().Contains(term);
		}
	}
}
