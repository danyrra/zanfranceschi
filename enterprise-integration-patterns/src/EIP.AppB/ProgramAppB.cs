using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.CanonicalDomain.Events;
using System.Threading;
using MassTransit;
using EIP.AppB.ServicesRegistry;
using EIP.CanonicalModels;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;

namespace EIP.AppB
{
	class ProgramAppB
	{
		static void Main(string[] args)
		{
			Console.Title = "App B";

			Console.WriteLine("Starting to listen for new employees hired...");

			ConfigureEndpoint();

			Console.WriteLine("Ready. Press <Enter> to quit.");

			Console.Read();
		}

		static IServiceBus bus;

		static void ConfigureEndpoint()
		{
			EventService eventService = null;

			string LatestEventServiceFilePath = ConfigurationManager.AppSettings["LatestEventServiceFilePath"];

			IServiceRegistry service = new ServiceRegistryClient();

			try
			{
				string dataType = typeof(EmployeeHired).FullName;

				eventService = service.FindOneByDataType(typeof(EmployeeHired).FullName);

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
				sbc.ReceiveFrom(string.Format("{0}://{1}/{2}", queueProtocol, eventService.Address, queueUniqueName));
				sbc.Subscribe(subs => subs.Consumer<MessageHandler>().Permanent());
			});
		}
	}

	class MessageHandler
		: Consumes<EmployeeHired>.All
	{
		public void Consume(EmployeeHired message)
		{
			Console.WriteLine("New employee hired: {0}", message.Employee.Name);
		}
	}
}
