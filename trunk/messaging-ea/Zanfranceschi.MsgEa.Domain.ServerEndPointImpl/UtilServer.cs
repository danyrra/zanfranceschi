namespace Zanfranceschi.MsgEa.Domain.ServerEndPointImpl
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using RabbitMQ.Client;
	using RabbitMQ.Client.Events;
	using Zanfranceschi.MsgEa.Domain.Impls.Services;
	using Zanfranceschi.MsgEa.Messages.Requests;
	using Zanfranceschi.MsgEa.Messages.Responses;
	using Zanfranceschi.MsgEa.Model;
	using Zanfranceschi.MsgEa.Domain.Services;

	internal class UtilServer
	{
		private IConnection connection;
		private IModel channel;

		private const string queueName = "zanfranceschi.msgea.utils";

		private IUtilServices services;

		internal UtilServer(IUtilServices services)
		{
			this.services = services;

			ConnectionFactory factory = new ConnectionFactory
			{
				HostName = "localhost"
			};

			connection = factory.CreateConnection();
			channel = connection.CreateModel();

			IDictionary args = new Dictionary<string, string>();
			args.Add("x-ha-policy", "all");

			channel.QueueDeclare(queueName, true, false, false, args);
		}

		public void Start()
		{
			QueueingBasicConsumer consumer = MakeConsumer();

			bool done = false;
			while (!done)
			{
				ProcessAMessage(consumer);

				done = this.WasQuitKeyPressed();
			}

			connection.Close();
			connection.Dispose();
			connection = null;
		}

		private QueueingBasicConsumer MakeConsumer()
		{
			QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(queueName, false, consumer);
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

		private void ProcessAMessage(QueueingBasicConsumer consumer)
		{
			BasicDeliverEventArgs messageInEnvelope = DequeueMessage(consumer);
			if (messageInEnvelope == null)
			{
				return;
			}

			try
			{
				object messageObject = SerializationHelper.FromByteArray(messageInEnvelope.Body);
				GetAddressServiceRequest request = messageObject as GetAddressServiceRequest;

				if (request != null)
				{
					Console.WriteLine("Received request for address: {0}", request.CEP);

					GetAddressServiceResponse responseMessage = GetResponse(request);

					IBasicProperties requestProperties = messageInEnvelope.BasicProperties;
					IBasicProperties responseProperties = consumer.Model.CreateBasicProperties();
					responseProperties.CorrelationId = requestProperties.CorrelationId;
					SendResponse(requestProperties.ReplyTo, responseProperties, responseMessage);
					channel.BasicAck(messageInEnvelope.DeliveryTag, false);

					Console.WriteLine("reply sent");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed message: {0}", ex);
			}
		}


		private void SendResponse(string replyQueueName, IBasicProperties responseProperties, Response responseMessage)
		{
			channel.BasicPublish(string.Empty, replyQueueName, responseProperties, responseMessage.ToByteArray());
		}

		private GetAddressServiceResponse GetResponse(GetAddressServiceRequest request)
		{
			Message message;
			User user = null;

			Address address = services.GetAddressByCEP(user, request.CEP, out message);

			return new GetAddressServiceResponse(address, message);
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
