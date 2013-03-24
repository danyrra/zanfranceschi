namespace Zanfranceschi.MsgEa.Sandbox.AppA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using RabbitMQ.Client;
	using RabbitMQ.Client.Events;
	
	internal class RPCClient
	{
		private IConnection connection;
		private IModel channel;
		private string requestQueueName = "rpc_queue";
		private string replyQueueName;
		private QueueingBasicConsumer consumer;
		private IBasicProperties props;

		internal RPCClient()
		{
			ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };
			connection = factory.CreateConnection();
			channel = connection.CreateModel();
			replyQueueName = channel.QueueDeclare().QueueName;
			consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(replyQueueName, true, consumer);
			props = channel.CreateBasicProperties();
		}

		internal string Call(string message)
		{
			string response = null;
			string corrId = Guid.NewGuid().ToString();

			props.CorrelationId = corrId;
			props.ReplyTo = replyQueueName;

			byte[] msg = Encoding.UTF8.GetBytes(message);

			channel.BasicPublish("", requestQueueName, props, msg);

			while (true)
			{
				BasicDeliverEventArgs delivery = consumer.Queue.Dequeue() as BasicDeliverEventArgs;

				if (delivery.BasicProperties.CorrelationId.Equals(corrId))
				{ 
					response = UTF8Encoding.UTF8.GetString(delivery.Body);
					break;
				}
			}

			return response;
		}

		internal void Close()
		{
			connection.Close();
		}
	}
}
