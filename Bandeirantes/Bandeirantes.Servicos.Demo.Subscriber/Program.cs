using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Bandeirantes.Servicos.Corporativo.Comercial;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Bandeirantes.Servicos.Demo.Subscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Nome exclusivo da fila:");
			string id = Console.ReadLine();

			Bus.Receber<NegociacaoBloqueadaNotificacao>(id, n =>
			{
				Console.WriteLine(n.NegociacaoId);
			});
		}
	}

	static class Bus
	{
		public static void Receber<T>(string subscriptionId, Action<T> action)
		{
			IModel model;
			IConnection connection;
			var connectionFactory = new ConnectionFactory();
			connectionFactory.HostName = "localhost";
			connection = connectionFactory.CreateConnection();
			model = connection.CreateModel();

			IDictionary args = new Dictionary<string, string>();
			args.Add("ha", "true");

			string exchange = typeof(T).FullName;

			string queue = string.Format("{0}-{1}", exchange, subscriptionId);

			model.ExchangeDeclare(exchange, ExchangeType.Topic, true, false, args);
			model.QueueDeclare(queue, true, false, false, args);
			model.QueueBind(queue, exchange, "*");

			QueueingBasicConsumer consumer = new QueueingBasicConsumer(model);
			String consumerTag = model.BasicConsume(queue, false, consumer);
			while (true)
			{
				try
				{
					BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
					IBasicProperties props = e.BasicProperties;
					byte[] body = e.Body;

					string serializedObject = Encoding.Default.GetString(body);
					T @object = JsonConvert.DeserializeObject<T>(serializedObject);
					action(@object);
					model.BasicAck(e.DeliveryTag, false);
				}
				catch (OperationInterruptedException ex)
				{
					break;
				}
				catch (Exception ex)
				{
					break;
				}
			}

			connection.Close();
			model.Close();
		}
	}
}
