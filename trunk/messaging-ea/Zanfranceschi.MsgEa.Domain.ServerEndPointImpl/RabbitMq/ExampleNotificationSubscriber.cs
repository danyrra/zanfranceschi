namespace Zanfranceschi.MsgEa.Domain.ServerEndPointImpl.RabbitMq
{
	using System;
	using RabbitMQ.Client;
	using RabbitMQ.Client.Events;
	using Zanfranceschi.MsgEa.Messages.Notifications;
	using Zanfranceschi.MsgEa.Messages.Requests;

	public class ExampleNotificationSubscriber
	{
		private IConnection connection;
		private IModel channel;

		private string ExchangeName = typeof(ExampleNotification).Name;

		private string queueName;

		public void Connect()
		{
			ConnectionFactory factory = new ConnectionFactory
			{
				HostName = "localhost"
			};

			connection = factory.CreateConnection();
			channel = connection.CreateModel();
			channel.ExchangeDeclare(ExchangeName, "fanout");

			// queue name is generated
			queueName = channel.QueueDeclare();
			channel.QueueBind(queueName, ExchangeName, string.Empty);
		}

		public void ConsumeMessages()
		{
			QueueingBasicConsumer consumer = MakeConsumer();
			bool done = false;
			while (!done)
			{
				ReadAMessage(consumer);

				done = this.WasQuitKeyPressed();
			}

			connection.Close();
			connection.Dispose();
			connection = null;
		}

		private QueueingBasicConsumer MakeConsumer()
		{
			QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(queueName, true, consumer);
			return consumer;
		}

		private bool WasQuitKeyPressed()
		{
			if (Console.KeyAvailable)
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey();

				if (Char.ToUpperInvariant(keyInfo.KeyChar) == 'Q')
				{
					return true;
				}
			}
			return false;
		}

		private static void ReadAMessage(QueueingBasicConsumer consumer)
		{
			BasicDeliverEventArgs delivery = DequeueMessage(consumer);
			if (delivery == null)
			{
				return;
			}

			try
			{
				ExampleNotification notification = (ExampleNotification)SerializationHelper.FromByteArray(delivery.Body);
				Console.WriteLine("Received {0}", notification.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed message: {0}", ex);
			}
		}

		private static BasicDeliverEventArgs DequeueMessage(QueueingBasicConsumer consumer)
		{
			const int timeoutMilseconds = 400;
			object result;

			consumer.Queue.Dequeue(timeoutMilseconds, out result);
			return result as BasicDeliverEventArgs;
		}
	}
}
