using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bandeirantes.Servicos.Bus
{
	public class PublishChannel
		: IDisposable
	{
		private IModel channel;

		public PublishChannel(IModel channel)
		{
			this.channel = channel;
		}

		public void Publish<T>(T mensagem)
		{
			Publish<T>(mensagem, "*");
		}

		public void Publish<T>(T mensagem, string routingKey)
		{
			string exchange = typeof(T).FullName;
			channel.ExchangeDeclare(exchange, ExchangeType.Topic, true, false, null);
			string obj = JsonConvert.SerializeObject(mensagem);
			IBasicProperties basicProperties = channel.CreateBasicProperties();
			channel.BasicPublish(exchange, routingKey, basicProperties, Encoding.UTF8.GetBytes(obj));
		}

		public void Request<T>(T message)
		{
			Publish<T>(message);
		}

		public void BatchRequest<TipoRequisicao, TipoResposta>(IEnumerable<TipoRequisicao> requisicoes, Action<TipoResposta> handler)
		{
			// response declaration
			string responseQueueName = string.Format("bandeirantes.resposta.{0}", Guid.NewGuid());
			channel.QueueDeclare(responseQueueName, false, true, true, null);

			// request declaration
			string requestExchange = "bandeirantes.rpc";
			channel.ExchangeDeclare(requestExchange, ExchangeType.Direct, true);
			string requestRoutingKey = typeof(TipoRequisicao).FullName;
			string requestCorrelationId = Guid.NewGuid().ToString();
			IBasicProperties requestProperties = channel.CreateBasicProperties();
			requestProperties.CorrelationId = requestCorrelationId;
			requestProperties.ReplyTo = responseQueueName;

			// set all for response
			QueueingBasicConsumer responseConsumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(responseQueueName, false, responseConsumer);

			foreach (var requisicao in requisicoes)
			{
				// request publication
				byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requisicao));
				channel.BasicPublish(requestExchange, requestRoutingKey, requestProperties, message);

				// start response awaiting
				BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)responseConsumer.Queue.Dequeue();
				TipoResposta resposta = JsonConvert.DeserializeObject<TipoResposta>(Encoding.UTF8.GetString(delivery.Body));
				handler(resposta);
			}
		}

		public void Request<TipoRequisicao, TipoResposta>(TipoRequisicao requisicao, Action<TipoResposta> handler)
		{
			// response declaration
			string responseQueueName = string.Format("bandeirantes.resposta.{0}", Guid.NewGuid());
			channel.QueueDeclare(responseQueueName, false, true, true, null);

			// request declaration
			string requestExchange = "bandeirantes.rpc";
			channel.ExchangeDeclare(requestExchange, ExchangeType.Direct, true);
			string requestRoutingKey = typeof(TipoRequisicao).FullName;
			string requestCorrelationId = Guid.NewGuid().ToString();
			IBasicProperties requestProperties = channel.CreateBasicProperties();
			requestProperties.CorrelationId = requestCorrelationId;
			requestProperties.ReplyTo = responseQueueName;
			
			// set all for response
			QueueingBasicConsumer responseConsumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(responseQueueName, false, responseConsumer);

			// request publication
			byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requisicao));
			channel.BasicPublish(requestExchange, requestRoutingKey, requestProperties, message);

			// start response awaiting
			BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)responseConsumer.Queue.Dequeue();
			TipoResposta resposta = JsonConvert.DeserializeObject<TipoResposta>(Encoding.UTF8.GetString(delivery.Body));
			handler(resposta);
			channel.BasicAck(delivery.DeliveryTag, false);
		}

		public void Dispose()
		{
			this.channel.Close();
		}
	}

	public class BandBus
		: IDisposable
	{
		IModel channel;
		private IConnection connection;

		public PublishChannel OpenPublishChannel()
		{
			return new PublishChannel(connection.CreateModel());
		}

		public BandBus(string hostname)
		{
			var connectionFactory = new ConnectionFactory();
			connectionFactory.HostName = hostname;
			connection = connectionFactory.CreateConnection();
			channel = connection.CreateModel();
		}

		public void Respond<TipoRequisicao, TipoResposta>(TipoRequisicao requisicao, Func<TipoRequisicao, TipoResposta> func)
		{
			string requestExchange = "bandeirantes.rpc";
			channel.ExchangeDeclare(requestExchange, ExchangeType.Direct, true);

			string requestQueueName = typeof(TipoRequisicao).FullName;
			channel.QueueDeclare(requestQueueName, true, false, false, null);
			channel.QueueBind(requestQueueName, requestExchange, requestQueueName);

			QueueingBasicConsumer requestConsumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(requestQueueName, false, requestConsumer);

			while (true)
			{
				BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)requestConsumer.Queue.Dequeue();
				IBasicProperties responseProperties = channel.CreateBasicProperties();
				responseProperties.CorrelationId = delivery.BasicProperties.CorrelationId;

				TipoRequisicao request = JsonConvert.DeserializeObject<TipoRequisicao>(Encoding.UTF8.GetString(delivery.Body));
				TipoResposta response = func(request);
				string responseString = JsonConvert.SerializeObject(response);

				byte[] responseBytes = Encoding.UTF8.GetBytes(responseString);
				channel.BasicPublish(string.Empty, delivery.BasicProperties.ReplyTo, responseProperties, responseBytes);
				channel.BasicAck(delivery.DeliveryTag, false);
			}
		}

		public void Subscribe<TipoNotificacao>(string subscriptionId, Action<TipoNotificacao> handler)
		{
			Subscribe<TipoNotificacao>(subscriptionId, "*", handler);
		}

		public void Subscribe<TipoNotificacao>(string subscriptionId, string routingKey, Action<TipoNotificacao> handler)
		{
			channel.QueueDeclare(subscriptionId, true, false, false, null);
			string exchangeName = typeof(TipoNotificacao).FullName;
			channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true);
			channel.QueueBind(subscriptionId, exchangeName, routingKey);

			QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(subscriptionId, false, consumer);

			while (true)
			{
				BasicDeliverEventArgs delivery = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
				TipoNotificacao notification = JsonConvert.DeserializeObject<TipoNotificacao>(Encoding.UTF8.GetString(delivery.Body));
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
