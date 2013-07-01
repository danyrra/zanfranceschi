using System;
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
			channel.QueueDeclare(requestQueue, false, false, false, null);
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
			string requestQueue = typeof(TipoRequisicao).FullName;
			channel.QueueDeclare(requestQueue, false, false, false, null);
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
			string requestQueue = typeof(TipoRequisicao).FullName;
			channel.QueueDeclare(requestQueue, false, false, false, null);
			QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(requestQueue, false, consumer);

			while (true)
			{
				BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
				IBasicProperties replyProperties = channel.CreateBasicProperties();
				byte[] replyMessage = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(func(requisicao)));
				channel.BasicPublish(string.Empty, ea.BasicProperties.ReplyTo, replyProperties, replyMessage);
				channel.BasicAck(ea.DeliveryTag, false);
			}		
		}

		public void Dispose()
		{
			connection.Close();
			channel.Close();
		}
	}
}
