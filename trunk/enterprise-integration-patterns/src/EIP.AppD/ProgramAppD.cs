using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using System.Configuration;
using EIP.CanonicalModel.Events;
using System.IO;
using System.ServiceModel;
using EIP.CanonicalModel.Requests;
using Newtonsoft.Json;

namespace EIP.AppD
{
	class ProgramAppD
	{
		static void Main(string[] args)
		{
			Console.WindowWidth = 60;
			Console.WindowHeight = 10;

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
			string queueUniqueName = ConfigurationManager.AppSettings["queue-unique_name"];
			
			string queueProtocol = ConfigurationManager.AppSettings["queue-protocol"];

			bus = ServiceBusFactory.New(sbc =>
			{
				sbc.SetNetwork("eip");

				//sbc.UseControlBus();
				
				if (queueProtocol == "msmq")
				{
					
					sbc.UseMsmq();
					sbc.UseMulticastSubscriptionClient();
					sbc.VerifyMsmqConfiguration();
				}
				else if (queueProtocol == "rabbitmq")
				{
					sbc.UseRabbitMq();
				}
				address = string.Format("{0}://localhost/{1}__{2}?ha=true", queueProtocol, Environment.MachineName, queueUniqueName);
				sbc.ReceiveFrom(address);
				sbc.Subscribe(subs => subs.Handler<TestRequest>(HandleRequest));
			});
		}

		static void HandleRequest(IConsumeContext<TestRequest> ctx, TestRequest request)
		{
			Console.WriteLine("Responding to '{0}'...", request.Request);
			TestResponse response = new TestResponse();
			response.Response = string.Format("{0} - {1}", Environment.MachineName, request.Request.ToUpper());
			ctx.Respond(response);
		}
	}
}
