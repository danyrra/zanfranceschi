using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using System.Configuration;
using EIP.AppD.ServicesRegistry;
using EIP.CanonicalDomain.Events;
using System.IO;
using System.ServiceModel;
using EIP.CanonicalDomain.Requests;
using Newtonsoft.Json;

namespace EIP.AppD
{
	class ProgramAppD
	{
		static void Main(string[] args)
		{
			Console.Title = "Responder (D)";

			Console.WriteLine("Getting ready to respond.");

			try
			{
				ConfigureEndpoint();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.WriteLine("Ready to respond at {0}", address);
			Console.WriteLine("Press <Enter> to quit.");

			Console.Read();
		}

		static IServiceBus bus;
		static string address = string.Empty;

		static void ConfigureEndpoint()
		{
			EventService eventService = null;

			string LatestEventServiceFilePath = ConfigurationManager.AppSettings["LatestEventServiceFilePath"];

			IServiceRegistry service = new ServiceRegistryClient();
			
			//service.CreateEventService("Test Requst", "", "zanfranceschi-n", typeof(TestRequest).FullName);

			try
			{
				string dataType = typeof(TestRequest).FullName;

				eventService = service.FindOneByDataType(typeof(TestRequest).FullName);

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
				sbc.Subscribe(subs => subs.Handler<TestRequest>(HandleRequest));
			});
		}

		static void HandleRequest(IConsumeContext<TestRequest> ctx, TestRequest request)
		{
			Console.WriteLine("Responding to '{0}'...", request.Request);
			TestResponse response = new TestResponse();
			response.Response = request.Request.ToUpper();
			ctx.Respond(response);
		}
	}
}
