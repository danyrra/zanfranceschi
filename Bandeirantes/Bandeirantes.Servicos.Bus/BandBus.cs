using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bandeirantes.Servicos.Comum;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Util;

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
			string exchange = typeof(T).FullName;
			channel.ExchangeDeclare(exchange, ExchangeType.Topic, true, false, null);
			string obj = JsonConvert.SerializeObject(mensagem);
			IBasicProperties basicProperties = channel.CreateBasicProperties();
			channel.BasicPublish(exchange, "*", basicProperties, Encoding.UTF8.GetBytes(obj));
		}

		public void Request<T>(T message)
		{
			Publish<T>(message);
		}

		public void BatchRequest<TipoRequisicao, TipoResposta>(IEnumerable<TipoRequisicao> requisicoes, Action<TipoResposta> handler)
			where TipoRequisicao : MensagemBarramento
			where TipoResposta : MensagemBarramento
		{
			string requestQueue = typeof(TipoRequisicao).FullName;

			IDictionary args = new Dictionary<string, string>();
			args.Add("x-ha-policy", "all");

			channel.QueueDeclare(requestQueue, false, false, false, args);
			string replyQueue = channel.QueueDeclare();
			QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(replyQueue, true, consumer);
			string correlationId = Guid.NewGuid().ToString();
			IBasicProperties requestProperties = channel.CreateBasicProperties();
			requestProperties.ReplyTo = replyQueue;

			foreach (var requisicao in requisicoes)
			{
				byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requisicao));

				channel.BasicPublish(
					string.Empty,
					requestQueue,
					requestProperties,
					message);

				BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
				TipoResposta resposta = JsonConvert.DeserializeObject<TipoResposta>(Encoding.UTF8.GetString(ea.Body));
				handler(resposta);
			}
		}

		public void Request<TipoRequisicao, TipoResposta>(TipoRequisicao requisicao, Action<TipoResposta> handler)
			where TipoRequisicao : MensagemBarramento
			where TipoResposta : MensagemBarramento
		{
			
			//string responseQueueName = string.Format("bandeirantes.resposta.{0}", Guid.NewGuid());
			//string responseQueue = channel.QueueDeclare(responseQueueName, false, true, false, null);

			string requestExchange = "bandeirantes.rpc";
			channel.ExchangeDeclare(requestExchange, ExchangeType.Direct);
			string requestRoutingKey = typeof(TipoRequisicao).FullName;
			string requestCorrelationId = Guid.NewGuid().ToString();
			IBasicProperties requestProperties = channel.CreateBasicProperties();
			requestProperties.CorrelationId = requestCorrelationId;
			channel.BasicPublish(requestExchange, requestRoutingKey, requestProperties, null);
			
			/*
			string requestQueue = typeof(TipoRequisicao).FullName;

			IDictionary args = new Dictionary<string, string>();
			args.Add("x-ha-policy", "all");

			channel.QueueDeclare(requestQueue, false, false, false, args);
			string replyQueue = channel.QueueDeclare();
			QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(replyQueue, true, consumer);
			string correlationId = Guid.NewGuid().ToString();
			IBasicProperties requestProperties = channel.CreateBasicProperties();
			requestProperties.ReplyTo = replyQueue;

			byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requisicao));

			channel.BasicPublish(
				string.Empty,
				requestQueue,
				requestProperties,
				message);

			BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
			TipoResposta resposta = JsonConvert.DeserializeObject<TipoResposta>(Encoding.UTF8.GetString(ea.Body));
			handler(resposta);
			*/ 
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
			where TipoRequisicao : IRequisicaoComResposta
			where TipoResposta : MensagemBarramento
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
				byte[] response = Encoding.UTF8.GetBytes(string.Format("Responded '{0}' @ {1}", Encoding.UTF8.GetString(delivery.Body), DateTime.Now));
				channel.BasicPublish(string.Empty, delivery.BasicProperties.ReplyTo, responseProperties, response);
				channel.BasicAck(delivery.DeliveryTag, false);
				Console.WriteLine(Encoding.UTF8.GetString(response));
			}
		}

		public void Dispose()
		{
			connection.Close();
			channel.Close();
		}
	}
}
