using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.CanonicalModel.Events;
using System.Threading;
using MassTransit;
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
			Console.WindowWidth = 60;
			Console.WindowHeight = 10;

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
