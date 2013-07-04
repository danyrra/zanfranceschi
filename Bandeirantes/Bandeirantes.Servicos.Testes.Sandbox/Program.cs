using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EasyNetQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bandeirantes.Servicos.Testes.Sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WindowHeight = 10;
			Console.WindowWidth = 50;

			ConnectionTests();


			Console.WriteLine("'Client (c)' or 'Server (s)':");
			string type = Console.ReadLine();
			string title = null;
			Action method = null;

			if (type.ToLower() == "c")
			{
				title = "Client";
				method = RawClient;

			}
			else if (type.ToLower() == "s")
			{
				title = "Server";
				method = RawServer;
			}
			else
			{
				title = "Error";
				method = Error;
			}

			Console.Title = title;
			method();
		}

		static void ConnectionTests()
		{
			IConnection connection = new ConnectionFactory { HostName = "localhost" }.CreateConnection();
			IModel channel = connection.CreateModel();

			//connection.ConnectionShutdown += connection_ConnectionShutdown;
			//channel.ModelShutdown += channel_ModelShutdown;

			Console.Read();
		}

		static void channel_ModelShutdown(IModel model, ShutdownEventArgs reason)
		{
			
		}

		static void connection_ConnectionShutdown(IConnection connection, ShutdownEventArgs reason)
		{
			
		}

		static void RawClient()
		{
			while (true)
			{
				using (IConnection connection = new ConnectionFactory { HostName = "localhost" }.CreateConnection())
				{
					using (IModel channel = connection.CreateModel())
					{
						// response declaration
						string responseQueueName = string.Format("bandeirantes.resposta.{0}", Guid.NewGuid());
						channel.QueueDeclare(responseQueueName, false, true, true, null);

						// request declaration
						string requestExchange = "bandeirantes.rpc";
						channel.ExchangeDeclare(requestExchange, ExchangeType.Direct, true);
						string requestRoutingKey = typeof(string).FullName;
						string requestCorrelationId = Guid.NewGuid().ToString();
						IBasicProperties requestProperties = channel.CreateBasicProperties();
						requestProperties.CorrelationId = requestCorrelationId;
						requestProperties.ReplyTo = responseQueueName;
						// request publication
						Console.WriteLine("Enter your request:");
						string request = Console.ReadLine();
						channel.BasicPublish(requestExchange, requestRoutingKey, requestProperties, Encoding.UTF8.GetBytes(request));

						// response awaiting
						QueueingBasicConsumer responseConsumer = new QueueingBasicConsumer(channel);
						channel.BasicConsume(responseQueueName, false, responseConsumer);
						BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)responseConsumer.Queue.Dequeue();
						Console.WriteLine("Got response '{0}'", Encoding.UTF8.GetString(delivery.Body));
					}
				}
			}
		}

		static void RawServer()
		{
			using (IConnection connection = new ConnectionFactory { HostName = "localhost" }.CreateConnection())
			{
				using (IModel channel = connection.CreateModel())
				{
					string requestExchange = "bandeirantes.rpc";
					channel.ExchangeDeclare(requestExchange, ExchangeType.Direct, true);

					string requestQueueName = typeof(string).FullName;
					channel.QueueDeclare(requestQueueName, true, false, false, null);
					channel.QueueBind(requestQueueName, requestExchange, requestQueueName);

					QueueingBasicConsumer requestConsumer = new QueueingBasicConsumer(channel);
					channel.BasicConsume(requestQueueName, false, requestConsumer);

					while (true)
					{
						BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)requestConsumer.Queue.Dequeue();
						IBasicProperties responseProperties = channel.CreateBasicProperties();
						responseProperties.CorrelationId = delivery.BasicProperties.CorrelationId;
						byte[] response = Encoding.UTF8.GetBytes(string.Format("Responded '{0}' @ {1}", Encoding.UTF8.GetString(delivery.Body), DateTime.Now));
						channel.BasicPublish(string.Empty, delivery.BasicProperties.ReplyTo, responseProperties, response);
						channel.BasicAck(delivery.DeliveryTag, false);
						Console.WriteLine(Encoding.UTF8.GetString(response));
					}
				}
			}
		}

		static void EasyNetQServer()
		{
			using (IBus bus = RabbitHutch.CreateBus("host=localhost"))
			{
				bus.Respond<string, string>(r =>
				{
					return string.Format("{0} - Sua responsta para {1}", DateTime.Now, r);
				});
				Console.Read();
			}
		}

		static void EasyNetQClient()
		{
			using (IBus bus = RabbitHutch.CreateBus("host=localhost"))
			{
				using (IPublishChannel pubChannel = bus.OpenPublishChannel())
				{
					while (true)
					{
						pubChannel.Request<string, string>("oi", (res) =>
						{
							Console.WriteLine(res);
						});
						Console.Read();
					}
				}
			}
		}

		static void Error()
		{
			Console.WriteLine("Error...");
		}

		static string rpcQueueName = "rpc";

		static void Client()
		{
			using (IConnection connection = new ConnectionFactory { HostName = "localhost" }.CreateConnection())
			{
				using (IModel channel = connection.CreateModel())
				{
					channel.QueueDeclare(rpcQueueName, false, false, false, null);

					string replyQueue = channel.QueueDeclare();
					QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
					channel.BasicConsume(replyQueue, true, consumer);
					string correlationId = Guid.NewGuid().ToString();
					IBasicProperties requestProperties = channel.CreateBasicProperties();
					requestProperties.CorrelationId = correlationId;
					requestProperties.ReplyTo = replyQueue;

					while (true)
					{
						Console.WriteLine("Your request:");
						string msg = Console.ReadLine();

						channel.BasicPublish(
							string.Empty,
							rpcQueueName,
							requestProperties,
							Encoding.UTF8.GetBytes(msg));

						BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
						Log("response received: {0}", Encoding.UTF8.GetString(ea.Body));
					}
				}
			}
		}

		static void Server()
		{
			using (IConnection connection = new ConnectionFactory { HostName = "localhost" }.CreateConnection())
			{
				using (IModel channel = connection.CreateModel())
				{
					channel.QueueDeclare(rpcQueueName, false, false, false, null);
					//channel.BasicQos(1, 1, true);
					QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
					channel.BasicConsume(rpcQueueName, false, consumer);

					Log("Serving...");

					while (true)
					{
						BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
						IBasicProperties replyProperties = channel.CreateBasicProperties();
						//replyProperties.CorrelationId = ea.BasicProperties.CorrelationId;
						byte[] replyMessage = Encoding.UTF8.GetBytes(string.Format("Resposta para {0}", Encoding.UTF8.GetString(ea.Body)));
						channel.BasicPublish(string.Empty, ea.BasicProperties.ReplyTo, replyProperties, replyMessage);
						channel.BasicAck(ea.DeliveryTag, false);
						Log(Encoding.UTF8.GetString(replyMessage));
					}
				}
			}
		}

		static void Log(string message, params object[] args)
		{
			Console.WriteLine(message, args);
		}

		static void Log(object message)
		{
			Console.WriteLine(message);
		}
	}
}
