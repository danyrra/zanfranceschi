using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using EIP.CanonicalModel.Events;
using System.Threading;
using EIP.AppA.ServiceRegistry;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;
using EIP.CanonicalModel.Requests;
using MassTransit.Exceptions;

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

				try
				{
					bus.Publish(new TestOccurred { Text = word });
					if (num % 2 == 0)
					{

						Guid transactionId = Guid.NewGuid();
						
						bus.PublishRequest(new TestRequest { CorrelationId = transactionId, Request = word }, x =>
						{
							x.Handle<TestResponse>(HandleResponse);
							x.HandleFault(HandleFaultRequest);
							x.HandleTimeout(TimeSpan.FromSeconds(4), c => Console.WriteLine("request timeout"));
						});
					}
				}
				catch (PublishException pex)
				{
					Console.WriteLine(pex.Message);
				}

				Console.WriteLine(word);
				Thread.Sleep(1000);
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
			});
		}
	}
}
