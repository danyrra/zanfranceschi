using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bandeirantes.Servicos.Bus.Implementations.RabbitMq
{
	internal class RabbitMqPublishChannel
		: IPublishChannel
	{
		private IModel channel;

		internal RabbitMqPublishChannel(IModel channel)
		{
			this.channel = channel;
		}

		public void Publish<T>(T mensagem)
		{
			Publish<T>(mensagem, "*");
		}

		public void Publish<T>(T message, string routingKey)
		{
			string exchange = typeof(T).FullName;
			channel.ExchangeDeclare(exchange, ExchangeType.Topic, true, false, null);
			string obj = JsonConvert.SerializeObject(message);
			IBasicProperties basicProperties = channel.CreateBasicProperties();
			channel.BasicPublish(exchange, routingKey, basicProperties, Encoding.UTF8.GetBytes(obj));
		}

		public void Request<T>(T message)
		{
			Publish<T>(message);
		}

		public void BatchRequest<RequestType, ResponseType>(IEnumerable<RequestType> requests, Action<ResponseType> handler)
		{

			string requestExchange = "bandeirantes.rpc";
			string requestRoutingKey = typeof(RequestType).FullName;
			IBasicProperties requestProperties = channel.CreateBasicProperties();
			QueueingBasicConsumer responseConsumer = new QueueingBasicConsumer(channel);

			ConfigureRequest(requestExchange, requestRoutingKey, requestProperties, responseConsumer);

			foreach (var request in requests)
			{
				// request publication
				byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
				channel.BasicPublish(requestExchange, requestRoutingKey, requestProperties, message);

				// start response awaiting
				BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)responseConsumer.Queue.Dequeue();
				ResponseType response = JsonConvert.DeserializeObject<ResponseType>(Encoding.UTF8.GetString(delivery.Body));
				handler(response);
				channel.BasicAck(delivery.DeliveryTag, false);
			}
		}

		public void Request<RequestType, ResponseType>(RequestType request, Action<ResponseType> handler)
		{

			string requestExchange = "bandeirantes.rpc";
			string requestRoutingKey = typeof(RequestType).FullName;
			IBasicProperties requestProperties = channel.CreateBasicProperties();
			QueueingBasicConsumer responseConsumer = new QueueingBasicConsumer(channel);

			ConfigureRequest(requestExchange, requestRoutingKey, requestProperties, responseConsumer);

			// request publication
			byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
			channel.BasicPublish(requestExchange, requestRoutingKey, requestProperties, message);

			// start response awaiting
			BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)responseConsumer.Queue.Dequeue();
			ResponseType response = JsonConvert.DeserializeObject<ResponseType>(Encoding.UTF8.GetString(delivery.Body));
			handler(response);
			channel.BasicAck(delivery.DeliveryTag, false);
		}

		private void ConfigureRequest(string requestExchange, string requestRoutingKey, IBasicProperties requestProperties, QueueingBasicConsumer responseConsumer)
		{
			// response declaration
			string responseQueueName = string.Format("bandeirantes.resposta.{0}", Guid.NewGuid());
			channel.QueueDeclare(responseQueueName, false, true, true, null);

			// request declaration
			channel.ExchangeDeclare(requestExchange, ExchangeType.Topic, true);
			string requestCorrelationId = Guid.NewGuid().ToString();
			requestProperties.CorrelationId = requestCorrelationId;
			requestProperties.ReplyTo = responseQueueName;
			requestProperties.Expiration = "1";

			// set all for response
			channel.BasicConsume(responseQueueName, false, responseConsumer);
		}
		
		public void Dispose()
		{
			this.channel.Close();
		}
	}
}