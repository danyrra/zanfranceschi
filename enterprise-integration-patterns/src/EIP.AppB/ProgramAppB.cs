﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.CanonicalDomain.Events;
using System.Threading;
using MassTransit;
using EIP.AppB.ServicesRegistry;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using EIP.CanonicalDomain.Requests;

namespace EIP.AppB
{
	class ProgramAppB
	{
		static void Main(string[] args)
		{
			Console.Title = "Subscriber (B)";

			Console.WriteLine("Starting to listen for events...");

			try
			{
				ConfigureEndpoint();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.WriteLine("listening at {0}", address);
			Console.WriteLine("Ready. Press <Enter> to quit.");

			Console.Read();
		}

		static IServiceBus bus;
		static string address = string.Empty;

		static void ConfigureEndpoint()
		{
			EventService eventService = null;

			string LatestEventServiceFilePath = ConfigurationManager.AppSettings["LatestEventServiceFilePath"];

			IServiceRegistry service = new ServiceRegistryClient();

			try
			{
				string dataType = typeof(TestOccurred).FullName;

				eventService = service.FindOneByDataType(typeof(TestOccurred).FullName);

				if (eventService == null)
					throw new Exception(string.Format("Could not find the service registry for type '{0}'.", dataType));

				using (StreamWriter file = new StreamWriter(LatestEventServiceFilePath, false))
				{
					string serializedEvent = JsonConvert.SerializeObject(eventService);
					file.Write(serializedEvent);
				}
			}
			catch (EndpointNotFoundException) // Service Registry unreachable...
			{
				using (StreamReader file = new StreamReader(LatestEventServiceFilePath))
				{
					eventService = JsonConvert.DeserializeObject<EventService>(file.ReadToEnd());
				}
			}

			string queueUniqueName = ConfigurationManager.AppSettings["QueueUniqueName"];
			string queueProtocol = ConfigurationManager.AppSettings["queue-protocol"];

			bus = ServiceBusFactory.New(sbc =>
			{
				if (queueProtocol == "msmq")
				{
					sbc.UseMsmq();
					sbc.UseMulticastSubscriptionClient();
					sbc.VerifyMsmqConfiguration();
				}
				else
				{
					sbc.UseRabbitMq();
				}
				address = string.Format("{0}://{1}/{2}", queueProtocol, eventService.Address, queueUniqueName);
				sbc.ReceiveFrom(address);
				sbc.Subscribe(subs => subs.Consumer<MessageHandler>().Permanent());
			});
		}
	}

	class MessageHandler
		: Consumes<TestOccurred>.All
	{
		public void Consume(TestOccurred message)
		{
			Console.WriteLine("received: {0}", message.Text);
		}
	}
}
