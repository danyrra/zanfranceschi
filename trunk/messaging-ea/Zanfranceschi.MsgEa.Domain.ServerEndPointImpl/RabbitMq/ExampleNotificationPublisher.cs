namespace Zanfranceschi.MsgEa.Domain.ServerEndPointImpl.RabbitMq
{
	using System;
	using RabbitMQ.Client;
	using Zanfranceschi.MsgEa.Messages.Notifications;
	using Zanfranceschi.MsgEa.Messages.Requests;
	
	public class ExampleNotificationPublisher
	{
		private IConnection connection;
		private IModel channel;

		private string ExchangeName = typeof(ExampleNotification).Name;

		public void Connect()
		{
			ConnectionFactory factory = new ConnectionFactory
			{
				HostName = "localhost"
			};

			connection = factory.CreateConnection();
			channel = connection.CreateModel();
			channel.ExchangeDeclare(ExchangeName, "fanout");
		}

		public void Disconnect()
		{
			channel = null;

			if (connection.IsOpen)
			{
				connection.Close();
			}

			connection.Dispose();
			connection = null;
		}

		private const int MessageCount = 10;

		public void SendMessage(string message)
		{
			ExampleNotification notification = new ExampleNotification(message);
			byte[] messageBody = notification.ToByteArray();
			channel.BasicPublish(ExchangeName, string.Empty, null, messageBody);
		}
	}
}