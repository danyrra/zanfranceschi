using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using EIP.CanonicalDomain.Events;
using System.Threading;
using EIP.CanonicalModels;
using EIP.AppA.ServiceRegistry;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;

namespace EIP.AppA
{
	class ProgramAppA
	{
		static void Main(string[] args)
		{
			Console.Title = "App A";

			Console.WriteLine("Starting to hire away!!!");

			ConfigureEndpoint();
			
			Thread.Sleep(1000);

			Console.WriteLine("Endpoint configured...");

			IList<string> names = new List<string>{
							 "Francisco",
							 "Sandra",
							 "Ana",
							 "João",
							 "Fernanda"
							};

			int i = 0;

			while (true)
			{
				i++;
				Random rnd = new Random();
				int num = rnd.Next(0, 4);
				string name = i.ToString() + " - " + names[num];

				Employee employee = new Employee { Name = name };

				bus.Publish(new EmployeeHired { Employee = employee });
				Console.WriteLine(name);
				Thread.Sleep(1000);
			}
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
			});
		}
	}
}
