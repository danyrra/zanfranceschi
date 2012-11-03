using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using EIP.CanonicalDomain.Events;
using System.Threading;
using EIP.AppA.ServiceRegistry;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;
using EIP.CanonicalDomain.Requests;

namespace EIP.AppA
{
	class ProgramAppA
	{
		static void Main(string[] args)
		{
			Console.Title = "Publisher/Requester (A)";

			Console.WriteLine("Starting to publish test messages...");

			try
			{
				ConfigureEndpoint();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.WriteLine("publishing at {0}", address);
			
			Thread.Sleep(1000);

			IList<string> words = new List<string>{
							 "bomb",
							 "plumb",
							 "die",
							 "ich",
							 "dank",
							 "gracias",
							 "ciao",
							 "teste",
							 "APAOSPDO",
							 "APOO",
							 "SOA",
							 "São Paulo",
							 "102938",
							 "Should go home to rest"
							};

			int i = 0;

			while (true)
			{
				i++;
				Random rnd = new Random();
				int num = rnd.Next(0, words.Count - 1);
				string word = i.ToString() + " - " + words[num];

				bus.Publish(new TestOccurred { Text = word });

				if (num % 2 == 0)
				{
					bus.PublishRequest(new TestRequest { Request = word }, x =>
					{
						x.Handle<TestResponse>(HandleResponse);
						x.HandleFault(HandleFaultRequest);
						x.HandleTimeout(TimeSpan.FromSeconds(4), c => Console.WriteLine("request timeout"));
					});
				}

				Console.WriteLine(word);
				Thread.Sleep(10);
			}
		}

		static void HandleResponse(TestResponse response)
		{
			Console.WriteLine("response: {0}", response.Response);
		}

		static void HandleFaultRequest(Fault<TestRequest> fault)
		{
			Console.WriteLine("fault request");
		}

		static IServiceBus bus;
		static string address = string.Empty;

		static void ConfigureEndpoint()
		{
			EventService eventService = null;

			string LatestEventServiceFilePath = ConfigurationManager.AppSettings["LatestEventServiceFilePath"];

			IServiceRegistry service = new ServiceRegistryClient();

			bool autoCreateServiceRegistry = Convert.ToBoolean(ConfigurationManager.AppSettings["autoCreateServiceRegistry"]);

			try
			{
				string dataType = typeof(TestOccurred).FullName;

				eventService = service.FindOneByDataType(typeof(TestOccurred).FullName);

				if (eventService == null && !autoCreateServiceRegistry)
				{
					throw new Exception(string.Format("Could not find the service registry for type '{0}'.", dataType));
				}
				else if (autoCreateServiceRegistry)
				{
					string serviceRegistryAddress = ConfigurationManager.AppSettings["ServiceRegistryAddress"];
					service.CreateEventService("Test", "", serviceRegistryAddress, typeof(TestOccurred).FullName);
				}

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
			});
		}
	}
}
