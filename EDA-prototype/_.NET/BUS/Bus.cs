using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;

namespace BUS
{
	public delegate void MessageReceivedHandler(string message);
	public delegate void BusNotificationHandler(string message);

	/// <summary>
	/// Implementação inocente de um framework de BUS.
	/// O evento de recebimento de mensagem não está atrelado ao consumo de um determinado exchange, etc, etc...
	/// </summary>
	public class Bus
	{
		public event MessageReceivedHandler OnMessageReceived;
		public event BusNotificationHandler OnBusNotificationOccured;

		public void Publish(string host, string exchange, string message)
		{
			ConnectionFactory factory = new ConnectionFactory();
			factory.HostName = host;
			using (IConnection connection = factory.CreateConnection())
			using (IModel channel = connection.CreateModel())
			{
				channel.ExchangeDeclare(exchange, "fanout");

				byte[] body = System.Text.Encoding.UTF8.GetBytes(message);
				channel.BasicPublish(exchange, "", null, body);
				
				if (OnBusNotificationOccured != null)
					OnBusNotificationOccured.Invoke(string.Format("Mensagem enviada: {0}", message));
			}
		}

		public void StartConsuming(string host, string exchange)
		{
			if (OnBusNotificationOccured != null)
				OnBusNotificationOccured.Invoke("Configurando inscrição...");
			
			Thread thread = new Thread(obj => SetQueueConsumption(host, exchange));
			thread.Start();
			
			if (OnBusNotificationOccured != null)
				OnBusNotificationOccured.Invoke("Inscrição configurada.");
		}

		private void SetQueueConsumption(string host, string exchange)
		{
			ConnectionFactory factory = new ConnectionFactory { HostName = host };

			using (IConnection connection = factory.CreateConnection())
			{
				using (IModel channel = connection.CreateModel())
				{
					channel.ExchangeDeclare(exchange, "fanout");
					string queueName = channel.QueueDeclare();
					channel.QueueBind(queueName, exchange, "");
					QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
					channel.BasicConsume(queueName, true, consumer);

					while (true)
					{
						BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
						string msg = Encoding.ASCII.GetString(e.Body);
						
						if (OnMessageReceived != null)
							OnMessageReceived.Invoke(msg);
						
						if (OnBusNotificationOccured != null)
							OnBusNotificationOccured.Invoke("Mensagem recebida.");
					}
				}
			}
		}
	}
}
