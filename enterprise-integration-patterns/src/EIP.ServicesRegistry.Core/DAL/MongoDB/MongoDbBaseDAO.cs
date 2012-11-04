using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDBBuilders = MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core.DAL._MongoDB
{
	public abstract class MongoDbBaseDAO<TCollectionType> 
		where TCollectionType : ServiceRegistry
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
			SafeModeResult result = collection.Insert(obj);
			return obj;
		}

		public void Update(TCollectionType obj)
		{
			BsonDocumentWrapper document = BsonDocumentWrapper.Create<TCollectionType>(obj);

			collection.Update(
				Query.EQ("_id", obj.Id),
				MongoDBBuilders.Update.Replace(document)
			);
		}

		public void Remove(string id)
		{
			var query = Query.EQ("_id", id);
			collection.Remove(query, RemoveFlags.Single);
		}

		public TCollectionType GetById(string id)
		{
			var result = collection.Find(Query.EQ("_id", BsonValue.Create(id)));
			return result.FirstOrDefault();
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