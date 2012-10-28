using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.ServiceModel;
using EIP.Sandbox.ServiceReference1;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Builders;



namespace EIP.Sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Sender";

			IServiceRegistry service = new ServiceRegistryClient();
			var events = service.GetAllEvent();

			foreach (var item in events)
			{
				Console.WriteLine(item.Name);
			}


			var requests = service.GetAllRequest();

			foreach (var item in requests)
			{
				Console.WriteLine(item.Name);
			}

			Console.Read();

			//var sender = new Sender();
			//sender.Start();
		}

		static void MongoDbTests()
		{
			var connectionString = "mongodb://localhost/?safe=true";
			var server = MongoServer.Create(connectionString);
			var database = server.GetDatabase("test");

			var collection = database.GetCollection<A>("sandbox");

			BsonClassMap.RegisterClassMap<A>(cm =>
			{
				cm.AutoMap();
				cm.SetIsRootClass(true);
				cm.AddKnownType(typeof(Aa));
				cm.AddKnownType(typeof(Ab));
			});

			A a1 = new Aa { CustomAa = "aaaa" };
			A a2 = new Ab { CustomAb = "xxxxx" };

			collection.Insert<A>(a1);
			collection.Insert<A>(a2);

			var list1 = collection.FindAll();

		}
	}

	abstract class A
	{
		public MongoDB.Bson.ObjectId Id { get; set; }
		public string Name { get; set; }
		public abstract string Type { get; }
	}

	class Aa : A
	{

		public string CustomAa { get; set; }

		public override string Type
		{
			get { return "Aa"; }
		}
	}

	class Ab : A
	{
		public string CustomAb { get; set; }
		
		public override string Type
		{
			get { return "Ab"; }
		}
	}
}
