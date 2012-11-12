using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using EIP.CanonicalModel.Events;
using System.Threading;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;
using EIP.CanonicalModel.Requests;
using MassTransit.Exceptions;
using RabbitMQ.Client.Exceptions;

namespace EIP.AppA
{
	class ProgramAppA
	{
		static IServiceBus bus;
		static string address = string.Empty;
		static IList<TestOccurred> unpublishedMessages = new List<TestOccurred>();
		
		static void Main(string[] args)
		{
			Console.WindowWidth = 60;
			Console.WindowHeight = 10;
			
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
				string word = string.Format("({0}) | {1} - {2}", DateTime.Now.ToString("HH:mm:ss"), i.ToString(), words[num]);

				var message = new TestOccurred { Text = word };

				try
				{

					int configure_count = 1;
					while (bus == null)
					{
						try
						{
							Console.WriteLine("Trying to bring the Bus up. (attempt nº {0})", configure_count++);
							ConfigureEndpoint();
							Thread.Sleep(1000);
						}
						catch (MassTransit.Exceptions.ConfigurationException cex)
						{
							if (
								cex.InnerException != null &&
								cex.InnerException.InnerException != null &&
								cex.InnerException.InnerException.GetType() == typeof(BrokerUnreachableException))
							{
								Exception brokerException = cex.InnerException.InnerException;
								Console.WriteLine();
								Console.WriteLine("-------------------------");
								Console.WriteLine(brokerException.Message);
								Console.WriteLine("Check if RabbitMQ service is running.");
								Console.WriteLine("-------------------------");
								Console.WriteLine();
							}
							throw cex;
						}
						catch (Exception ex)
						{
							Console.WriteLine("could not be published: {0}", message.Text);
							Console.WriteLine(ex.Message);
							unpublishedMessages.Add(message);
						}
					}

					if (unpublishedMessages.Count > 0)
					{
						foreach (var msg in unpublishedMessages)
						{
							bus.Publish(msg);
							Console.WriteLine("published: {0}", msg.Text);
						}

						unpublishedMessages.Clear();
					}

					bus.Publish(message);

					Console.WriteLine("published: {0}", message.Text);

					#region Request/Reply
					if (num % 2 == 0 && false)
					{

						Guid transactionId = Guid.NewGuid();

						bus.PublishRequest(new TestRequest { CorrelationId = transactionId, Request = word }, x =>
						{
							x.Handle<TestResponse>(HandleResponse);
							x.HandleFault(HandleFaultRequest);
							x.HandleTimeout(TimeSpan.FromSeconds(4), c => Console.WriteLine("request timeout"));
						});
					}
					#endregion
				
				}
				catch (Exception ex)
				{
					Console.WriteLine("could not be published: {0}", message.Text);
					Console.WriteLine(ex.Message);
					unpublishedMessages.Add(message);
				}

				Thread.Sleep(1000);
			}
		}
		
		static void ConfigureEndpoint()
		{
			string queueUniqueName = ConfigurationManager.AppSettings["queue-unique_name"];

			string queueProtocol = ConfigurationManager.AppSettings["queue-protocol"];

			bus = ServiceBusFactory.New(sbc =>
			{
				sbc.SetNetwork("eip");

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
			});
		}
		
		static void HandleResponse(TestResponse response)
		{
			Console.WriteLine("response: {0}", response.Response);
		}

		static void HandleFaultRequest(Fault<TestRequest> fault)
		{
			Console.WriteLine("fault request");
		}
		
	}
}
