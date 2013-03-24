using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Zanfranceschi.MsgEa.Sandbox.AppB
{
	internal class RPCServer
	{
		private string rpcQueueName = "rpc_queue";
		private IConnection connection;
		private IModel channel;
		private QueueingBasicConsumer consumer;

		internal RPCServer() 
		{
			ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };
			connection = factory.CreateConnection();
			channel = connection.CreateModel();
		}

		internal void Start()
		{
			channel.QueueDeclare(rpcQueueName, false, false, false, null);
			channel.BasicQos(0, 1, false);
			consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(rpcQueueName, false, consumer);

			IBasicProperties props = channel.CreateBasicProperties();

			Console.WriteLine("Listening...");

			while (true)
			{
				BasicDeliverEventArgs delivery = consumer.Queue.Dequeue() as BasicDeliverEventArgs;

				Console.WriteLine("{0} - Request received.", DateTime.Now);

				props.CorrelationId = delivery.BasicProperties.CorrelationId;

				string request = Encoding.UTF8.GetString(delivery.Body);
				string response = request.ToUpper();

				channel.BasicPublish("", delivery.BasicProperties.ReplyTo, props, Encoding.UTF8.GetBytes(response));
				channel.BasicAck(delivery.DeliveryTag, false);
			}
		}
	}
}
