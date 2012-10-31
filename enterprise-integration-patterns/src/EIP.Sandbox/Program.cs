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
using MassTransit;
using MassTransit;
using EIP.CanonicalDomain.Events;
using EIP.CanonicalModels;
using System.Threading;



namespace EIP.Sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Sender";

			//var bus = ServiceBusFactory.New(sbc =>
			//{
			//    sbc.UseMsmq();
			//    //sbc.UseRabbitMq();
			//    sbc.UseMulticastSubscriptionClient();
			//    sbc.UseJsonSerializer();
			//    sbc.VerifyMsmqConfiguration();
			//    sbc.ReceiveFrom("msmq://localhost/PublisherEmployeeHired");
			//});

			////Bus.Initialize(sbc =>
			////{
			////    sbc.UseMsmq();
			////    sbc.VerifyMsmqConfiguration();
			////    sbc.UseMulticastSubscriptionClient();
			////    //sbc.UseRabbitMq();
			////    sbc.ReceiveFrom("msmq://localhost/PublisherEmployeeHired");
				
			////});

			//int i = 0;

			//while (true)
			//{
				
			//    bus.Publish(new EmployeeHired { Employee = new Employee { Name = "mensagem " + i.ToString() } });
			//    Console.WriteLine("mensagem {0}", i++);
			//    Thread.Sleep(500);
			//}

			IServiceRegistry service = new ServiceRegistryClient();
			var events = service.GetAllEventServices();

			foreach (var item in events)
			{
				Console.WriteLine(item.Name);
			}


			var requests = service.GetAllRequestServices();

			foreach (var item in requests)
			{
				Console.WriteLine(item.Name);
			}

			Console.Read();

			//var sender = new Sender();
			//sender.Start();
		}

	}
}
