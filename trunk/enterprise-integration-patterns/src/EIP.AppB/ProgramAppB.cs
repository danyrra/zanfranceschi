using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.CanonicalModel.Events;
using System.Threading;
using MassTransit;
using EIP.AppB.ServicesRegistry;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using EIP.CanonicalModel.Requests;

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
			EventRegistry eventRegistry = null;

			string LatestEventServiceFilePath = ConfigurationManager.AppSettings["LatestEventServiceFilePath"];

			IServiceRegistry service = new ServiceRegistryClient();

			try
			{
				string dataType = typeof(TestOccurred).FullName;

				eventRegistry = service.FindEventByDataType(typeof(TestOccurred).FullName);

				if (eventRegistry == null)
					throw new Exception(string.Format("Could not find the service registry for type '{0}'.", dataType));

				using (StreamWriter file = new StreamWriter(LatestEventServiceFilePath, false))
				{
					string serializedEvent = JsonConvert.SerializeObject(eventRegistry);
					file.Write(serializedEvent);
				}
			}
			catch (EndpointNotFoundException) // Service Registry unreachable...
			{
				using (StreamReader file = new StreamReader(LatestEventServiceFilePath))
				{
					eventRegistry = JsonConvert.DeserializeObject<EventRegistry>(file.ReadToEnd());
				}
			}

			string queueUniqueName = ConfigurationManager.AppSettings["QueueUniqueName"];
			string queueProtocol = ConfigurationManager.AppSettings["queue-protocol"];

			bus = ServiceBusFactory.New(sbc =>
			{
				sbc.SetNetwork("eip");

				sbc.UseControlBus();

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
				address = string.Format("{0}://{1}/{2}__{3}", queueProtocol, eventRegistry.Address, Environment.MachineName, queueUniqueName);
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
