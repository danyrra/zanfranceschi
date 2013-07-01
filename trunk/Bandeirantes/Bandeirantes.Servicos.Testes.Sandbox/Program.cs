using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			Console.WriteLine("'Client (c)' or 'Server (s)':");
			string type = Console.ReadLine();
			string title = null;
			Action method = null;

			if (type.ToLower() == "c")
			{
				title = "Client";
				method = Client;

			}
			else if (type.ToLower() == "s")
			{
				title = "Server";
				method = Server;
			}
			else
			{
				title = "Error";
				method = Error;
			}

			Console.Title = title;
			method();
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

					channel.BasicPublish(
						string.Empty,
						rpcQueueName,
						requestProperties,
						Encoding.UTF8.GetBytes("Request"));

					BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
					Log("response received: {0}", Encoding.UTF8.GetString(ea.Body));
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
					channel.BasicQos(1, 1, true);
					QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
					channel.BasicConsume(rpcQueueName, false, consumer);

					Log("Serving...");

					while (true)
					{
						BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
						IBasicProperties replyProperties = channel.CreateBasicProperties();
						replyProperties.CorrelationId = ea.BasicProperties.CorrelationId;
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
