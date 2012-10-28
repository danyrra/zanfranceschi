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
	internal class MongoDbServiceDAO
		: MongoDbBaseDAO<Service>, IServiceDAO
	{
		internal MongoDbServiceDAO(MongoServer server, string database, string collection)
			: base(server, database, collection)
		{
			if (!BsonClassMap.IsClassMapRegistered(typeof(Service)))
			{
				BsonClassMap.RegisterClassMap<Service>(cm =>
				{
					cm.AutoMap();
					cm.SetIsRootClass(true);
					cm.AddKnownType(typeof(EventService));
					cm.AddKnownType(typeof(RequestService));
				});
			}
		}
		
		protected override bool SearchCriteria(Service obj, string term)
		{
			term = term.ToLower();

			return
				obj.Address.ToLower().Contains(term)
				|| obj.Description.ToLower().Contains(term)
				|| obj.Name.ToLower().Contains(term);
		}


		public RequestService[] GetAllRequest()
		{
			return collection.FindAllAs<RequestService>().ToArray();
		}

		public EventService[] GetAllEvent()
		{
			return collection.FindAllAs<EventService>().ToArray();
		}
	}
}
