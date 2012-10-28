using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL._MongoDB;
using System.Configuration;
using MongoDB.Driver;

namespace EIP.ServicesRegistry.Core.DAL
{
	internal static class DAOFactory
	{
		private static string database;
		private static MongoServer server;

		static DAOFactory()
		{ 
			string connectionString = ConfigurationManager.AppSettings["mongodb-connection_string"];
			database = ConfigurationManager.AppSettings["mongodb-database"];
			if (server == null)
				server = MongoServer.Create(connectionString);
		}
		
		internal static IServiceDAO ServiceDAO
		{
			get 
			{
				return new MongoDbServiceDAO(server, database, "services");
			}
		}
		internal static IEventSubscriptionDAO EventSubscritpionDAO
		{
			get
			{
				return new MongoDbEventSubscritpionDAO(server, database, "subscriptions");
			}
		}
	}
}