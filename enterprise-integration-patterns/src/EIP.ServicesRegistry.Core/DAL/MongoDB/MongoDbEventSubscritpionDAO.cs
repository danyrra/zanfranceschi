using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;

namespace EIP.ServicesRegistry.Core.DAL._MongoDB
{
	internal class MongoDbEventSubscritpionDAO
		: MongoDbBaseDAO<EventSubscription>, IEventSubscriptionDAO
	{
		internal MongoDbEventSubscritpionDAO(MongoServer server, string database, string collection)
			: base(server, database, collection) { }

		protected override bool SearchCriteria(EventSubscription obj, string term)
		{
			term = term.ToLower();

			return
				obj.SubscriberQueuePath.ToLower().Contains(term)
				|| obj.EventService.DataType.ToLower().Contains(term)
				|| obj.EventService.Name.ToLower().Contains(term);
		}
	}
}
