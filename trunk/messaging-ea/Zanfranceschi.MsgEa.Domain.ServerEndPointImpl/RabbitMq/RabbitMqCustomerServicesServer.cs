﻿namespace Zanfranceschi.MsgEa.Domain.ServerEndPointImpl.RabbitMq
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using RabbitMQ.Client;
	using RabbitMQ.Client.Events;
	using Zanfranceschi.MsgEa.Domain.Services;
	using Zanfranceschi.MsgEa.Messages.Requests;
	using Zanfranceschi.MsgEa.Messages.Responses;
	using Zanfranceschi.MsgEa.Model;

	public class CustomerServicesServer
		: RabbitMqServicesServer
	{
		private const string queueName = "zanfranceschi.msgea.customer";
		private ICustomerServices services;
		private ExampleNotificationPublisher notificationPublisher;

		public CustomerServicesServer(ICustomerServices services, IServicesServerLogger logger)
			: base("localhost", logger)
		{
			this.services = services;
			notificationPublisher = new ExampleNotificationPublisher();
			notificationPublisher.Connect();
		}

		protected override void OnChannelCreated(IModel channel)
		{
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
				CustomerServiceRequest request = messageObject as CustomerServiceRequest;

				if (request != null)
				{
					logger.Log(string.Format("Received message: {0}", request.OperationType));

					Response responseMessage = GetResponse(request);

					IBasicProperties requestProperties = messageInEnvelope.BasicProperties;
					IBasicProperties responseProperties = consumer.Model.CreateBasicProperties();
					responseProperties.CorrelationId = requestProperties.CorrelationId;
					SendResponse(requestProperties.ReplyTo, responseProperties, responseMessage);
					channel.BasicAck(messageInEnvelope.DeliveryTag, false);
					notificationPublisher.SendMessage(string.Format("Request replied @ {0}", DateTime.Now));
					logger.Log(string.Format("sent reply to: {0}", request.OperationType));
				}
			}
			catch (Exception ex)
			{
				logger.Log(string.Format("Failed message: {0}", ex));
			}
		}

		private void SendResponse(string replyQueueName, IBasicProperties responseProperties, Response responseMessage)
		{
			channel.BasicPublish(string.Empty, replyQueueName, responseProperties, responseMessage.ToByteArray());
		}

		private Response GetResponse(CustomerServiceRequest request)
		{
			Message message;

			switch (request.OperationType)
			{
				case CustomerOperationTypeRequest.Register:
					var customer = services.RegisterCustomer(request.Requestor, request.CustomerName, out message);
					return new CustomerRegisterServiceResponse(customer, message);

				case CustomerOperationTypeRequest.Update:
					services.UpdateCustomer(request.Requestor, request.CustomerId, request.CustomerName, out message);
					return new CustomerUpdateOrDeleteServiceResponse(message);

				case CustomerOperationTypeRequest.Delete:
					services.ExcludeCustomer(request.Requestor, request.CustomerId, out message);
					return new CustomerUpdateOrDeleteServiceResponse(message);

				case CustomerOperationTypeRequest.Search:
					var customers = services.SearchCustomers(request.Requestor, request.NameLike, out message);
					return new CustomerSearchServiceResponse(customers, message);
			}

			return new ErrorResponse(new Message("Ooops"));
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