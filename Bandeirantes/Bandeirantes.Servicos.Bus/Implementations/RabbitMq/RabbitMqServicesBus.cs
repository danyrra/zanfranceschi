using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bandeirantes.Servicos.Bus.Implementations.RabbitMq
{
	internal class RabbitMqServicesBus
		: IServicesBus
	{
		private IModel channel;
		private IConnection connection;

		public IPublishChannel OpenPublishChannel()
		{
			return new RabbitMqPublishChannel(connection.CreateModel());
		}

		public RabbitMqServicesBus(string hostname)
		{
			var connectionFactory = new ConnectionFactory();
			connectionFactory.HostName = hostname;
			connection = connectionFactory.CreateConnection();
			channel = connection.CreateModel();
		}

		public void Respond<RequestType, ResponseType>(Func<RequestType, ResponseType> func)
		{
			string requestExchange = "bandeirantes.rpc";
			channel.ExchangeDeclare(requestExchange, ExchangeType.Topic, true);

			string requestQueueName = typeof(RequestType).FullName;

			IDictionary args = new Dictionary<object, object> 
			{ 
				{ "x-dead-letter-exchange", "dead-letter-exchange" }, 
				{ "x-dead-letter-routing-key", "*" }
			};

			channel.QueueDeclare(requestQueueName, true, false, false, args);
			channel.QueueBind(requestQueueName, requestExchange, requestQueueName);

			QueueingBasicConsumer requestConsumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(requestQueueName, false, requestConsumer);

			while (true)
			{
				BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)requestConsumer.Queue.Dequeue();
				IBasicProperties responseProperties = channel.CreateBasicProperties();
				responseProperties.CorrelationId = delivery.BasicProperties.CorrelationId;

				RequestType request = JsonConvert.DeserializeObject<RequestType>(Encoding.UTF8.GetString(delivery.Body));
				ResponseType response = func(request);
				string responseString = JsonConvert.SerializeObject(response);

				byte[] responseBytes = Encoding.UTF8.GetBytes(responseString);
				channel.BasicPublish(string.Empty, delivery.BasicProperties.ReplyTo, responseProperties, responseBytes);
				channel.BasicAck(delivery.DeliveryTag, false);
			}
		}

		public void Subscribe<NotificationType>(string subscriptionId, Action<NotificationType> handler)
		{
			Subscribe<NotificationType>(subscriptionId, "*", handler);
		}

		public void Subscribe<NotificationType>(string subscriptionId, string routingKey, Action<NotificationType> handler)
		{
			channel.QueueDeclare(subscriptionId, true, false, false, null);
			string exchangeName = typeof(NotificationType).FullName;
			channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true);
			channel.QueueBind(subscriptionId, exchangeName, routingKey);

			QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(subscriptionId, false, consumer);

			while (true)
			{
				BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
				NotificationType notification = JsonConvert.DeserializeObject<NotificationType>(Encoding.UTF8.GetString(delivery.Body));
				handler(notification);
				channel.BasicAck(delivery.DeliveryTag, false);
			}
		}

		public void Dispose()
		{
			connection.Close();
			channel.Close();
		}
	}
}