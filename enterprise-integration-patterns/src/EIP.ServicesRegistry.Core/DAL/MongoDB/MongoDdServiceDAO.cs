using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core.DAL._MongoDB
{
	public class MongoDbServiceDAO
		: MongoDbBaseDAO<ServiceRegistry>, IServiceDAO
	{
		public MongoDbServiceDAO(MongoServer server, string database)
			: base(server, database, "services")
		{
			if (!BsonClassMap.IsClassMapRegistered(typeof(ServiceRegistry)))
			{
				BsonClassMap.RegisterClassMap<ServiceRegistry>(cm =>
				{
					cm.AutoMap();
					cm.SetIsRootClass(true);
					cm.AddKnownType(typeof(EventRegistry));
					cm.AddKnownType(typeof(WebServiceRegistry));
				});
			}
		}

		protected override bool SearchCriteria(ServiceRegistry obj, string term)
		{
			term = term.ToLower();

			bool result =
				   obj.Address.ToLower().Contains(term)
				|| obj.Description.ToLower().Contains(term)
				|| obj.Name.ToLower().Contains(term)
				|| obj.TechnicalDetails.ToLower().Contains(term);

			return result;
		}

		public T Insert<T>(ServiceRegistry service) where T : ServiceRegistry
		{
			return (T)base.Insert(service);
		}

		public T GetById<T>(string id) where T : ServiceRegistry
		{
			return (T)base.GetById(id);
		}

		public T FindOneByProperty<T>(string propertyName, object propertyValue) where T : ServiceRegistry
		{
			return (T)collection.Find(Query.EQ(propertyName, BsonValue.Create(propertyValue))).FirstOrDefault();
		}

		public EventRegistry[] SearchEvents(string term)
		{
			term = term.ToLower();

			return (from obj in collection.AsQueryable<EventRegistry>()
					where
						   obj.Address.ToLower().Contains(term)
						|| obj.Description.ToLower().Contains(term)
						|| obj.Name.ToLower().Contains(term)
						|| obj.TechnicalDetails.ToLower().Contains(term)
						|| obj.CanonicalDataType.ToLower().Contains(term)
						|| obj.CanonicalDataTypeVersion.ToLower().Contains(term)
						|| obj.Protocol.ToLower().Contains(term)
					select obj).ToArray();
		}

		public WebServiceRegistry[] SearchWebServices(string term)
		{
			term = term.ToLower();

			return (from obj in collection.AsQueryable<WebServiceRegistry>()
					where
						   obj.Address.ToLower().Contains(term)
						|| obj.Description.ToLower().Contains(term)
						|| obj.Name.ToLower().Contains(term)
						|| obj.TechnicalDetails.ToLower().Contains(term)
						|| obj.WsdlUrl.ToLower().Contains(term)
					select obj).ToArray();
		}
	}
}
