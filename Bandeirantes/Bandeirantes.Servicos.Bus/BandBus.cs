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

		public void Request<TipoRequisicao, TipoResposta>(TipoRequisicao requisicao, Action<TipoResposta> handler, string responseId)
			where TipoRequisicao : MensagemBarramento
			where TipoResposta : MensagemBarramento
		{
			string requestExchange = typeof(TipoRequisicao).FullName;

			string responseExchange = typeof(TipoResposta).FullName;
			string responseQueue = channel.QueueDeclare(responseId, true, false, false, null);
			
			channel.ExchangeDeclare(requestExchange, ExchangeType.Topic, true, false, null);
			channel.ExchangeDeclare(responseExchange, ExchangeType.Topic, true, false, null);

			channel.QueueBind(responseQueue, responseExchange, "*");

			IBasicProperties requestProperties = channel.CreateBasicProperties();

			// set correlation Id
			requestProperties.CorrelationId = ((MensagemBarramento)requisicao).Id.ToString();
			// publish request
			string obj = JsonConvert.SerializeObject(requisicao);
			channel.BasicPublish(requestExchange, "*", requestProperties, Encoding.UTF8.GetBytes(obj));

			// listen response
			QueueingBasicConsumer consumer = new QueueingBasicConsumer();

			channel.BasicConsume(responseQueue, true, consumer);

			BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
			if (e.BasicProperties.CorrelationId.Equals(requestProperties.CorrelationId) || true)
			{
				IBasicProperties replyProperties = e.BasicProperties;
				byte[] body = e.Body;

				string serializedObject = Encoding.Default.GetString(body);
				TipoResposta @object = JsonConvert.DeserializeObject<TipoResposta>(serializedObject);
				handler(@object);
				channel.BasicAck(e.DeliveryTag, false);
			}
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
			string exchange = typeof(TipoRequisicao).FullName;
			string queue = string.Format("{0}-{1}", exchange, "response");

			channel.ExchangeDeclare(exchange, ExchangeType.Topic, true, false, null);
			channel.QueueDeclare(queue, true, false, false, null);
			channel.QueueBind(queue, exchange, "*");

			QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
			string consumerTag = channel.BasicConsume(queue, false, consumer);
			while (true)
			{
				BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
				IBasicProperties props = e.BasicProperties;
				byte[] body = e.Body;
				string serializedObject = Encoding.Default.GetString(body);
				TipoRequisicao @object = JsonConvert.DeserializeObject<TipoRequisicao>(serializedObject);
				TipoResposta resp = func(@object);

				#region respond
				string responseExchange = typeof(TipoResposta).FullName;
				channel.ExchangeDeclare(responseExchange, ExchangeType.Topic, true, false, null);
				string responseObject = JsonConvert.SerializeObject(resp);
				IBasicProperties basicProperties = channel.CreateBasicProperties();
				basicProperties.CorrelationId = props.CorrelationId;
				channel.BasicPublish(responseExchange, "*", basicProperties, Encoding.UTF8.GetBytes(responseObject));
				#endregion

				channel.BasicAck(e.DeliveryTag, false);
			}
		}

		public void Dispose()
		{
			connection.Close();
			channel.Close();
		}
	}
}
