using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace EIP.ServicesRegistry.Core.DAL._MongoDB
{
	internal class MongoDBServiceDAO
		: IServiceDAO
	{
		private MongoServer server;
		private MongoCollection<Service> collection;
		
		internal MongoDBServiceDAO(string connectionString, string database, string collection)
		{
			server = MongoServer.Create(connectionString);
			var db = server.GetDatabase(database);
			this.collection = db.GetCollection<Service>(collection);
		}
		
		public Service Insert(Service service)
		{
			collection.Insert(service);
			return service;
		}

		public void Update(Service service)
		{
			Remove(service.Id.ToString());
			Insert(service);
		}

		public void Remove(string id)
		{
			var query = Query.EQ("_id", id);
			collection.Remove(query, RemoveFlags.Single);
		}

		public Service[] GetAll()
		{
			return collection.FindAll().ToArray();
		}

		public Service GetById(string id)
		{
			return collection.Find(Query.EQ("_id", id)).FirstOrDefault();
		}


		public Service[] Search(string term)
		{
			return (from obj in collection.AsQueryable<Service>()
				   where 
						obj.Address.Contains(term) 
					||	obj.DefinitionUrl.Contains(term) 
					||	obj.Description.Contains(term) 
					||	obj.Name.Contains(term)
					||	obj.ServiceType.Contains(term)
				   select obj).ToArray();
		}
	}
}
