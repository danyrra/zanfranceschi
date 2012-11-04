using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using EIP.ServicesRegistry.Core.DAL;
using EIP.ServicesRegistry.Core.DAL._MongoDB;
using MongoDB.Driver;
using System.Configuration;
using Ninject.Modules;

namespace EIP.ServicesRegistry.Core
{
	public static class SrvFactory
	{
		public static ServiceSrv GetServiceSrv()
		{
			return kernel.Get<ServiceSrv>();
		}

		static IKernel kernel = new StandardKernel(new ServicesRegistryModule());

		private class ServicesRegistryModule
			: NinjectModule
		{
			public override void Load()
			{
				string connectionString = ConfigurationManager.AppSettings["mongodb-connection_string"];
				string database = ConfigurationManager.AppSettings["mongodb-database"];
				MongoServer server = MongoServer.Create(connectionString);

				Bind<IServiceDAO>()
						  .To<MongoDbServiceDAO>()
						  .WithConstructorArgument("server", server)
						  .WithConstructorArgument("database", database);
			}
		}
	}
}
