using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace EIP.ServicesRegistry.Core.DAL._MongoDB
{
	internal abstract class MongoDbBaseDAO<TCollectionType> 
		where TCollectionType : IMongoDbEntity
	{
		protected MongoServer server;
		protected MongoCollection<TCollectionType> collection;

		protected MongoDbBaseDAO(MongoServer server, string database, string collection)
		{
			this.server = server;
			var db = server.GetDatabase(database);
			this.collection = db.GetCollection<TCollectionType>(collection);
		}

		public TCollectionType Insert(TCollectionType obj)
		{
			collection.Insert(obj);
			return obj;
		}

		public void Update(TCollectionType obj)
		{
			Remove(obj.Id.ToString());
			Insert(obj);
		}

		public void Remove(string id)
		{
			var query = Query.EQ("_id", id);
			collection.Remove(query, RemoveFlags.Single);
		}

		public TCollectionType[] GetAll()
		{
			return collection.FindAll().ToArray();
		}

		public TCollectionType GetById(string id)
		{
			return collection.Find(Query.EQ("_id", id)).FirstOrDefault();
		}

		public TCollectionType[] Search(string term)
		{
			return (from obj in collection.AsQueryable<TCollectionType>()
					where SearchCriteria(obj, term)
					select obj).ToArray();
		}

		protected abstract bool SearchCriteria(TCollectionType obj, string term);
	}
}