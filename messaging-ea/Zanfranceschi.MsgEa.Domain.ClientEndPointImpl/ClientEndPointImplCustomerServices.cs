namespace Zanfranceschi.MsgEa.Domain.ClientEndPointImpl
{
	using System;
	using RabbitMQ.Client;
	using RabbitMQ.Client.Events;
	using Zanfranceschi.MsgEa.Messages.Requests;
	using Zanfranceschi.MsgEa.Messages.Responses;
	using Zanfranceschi.MsgEa.Model;
	using Zanfranceschi.MsgEa.Domain.Services;
	using System.Collections;
	using System.Collections.Generic;

	public class ClientEndPointImplCustomerServices
		: ICustomerServices
	{
		private IConnection connection;
		private IModel channel;
		private string replyQueueName;
		private QueueingBasicConsumer responseConsumer;
		private User user;
		private const string queueName = "zanfranceschi.msgea.customer";

		public ClientEndPointImplCustomerServices()
		{
			ConnectionFactory factory = new ConnectionFactory
			{
				HostName = "localhost"
			};

			connection = factory.CreateConnection();
			channel = connection.CreateModel();

			IDictionary args = new Dictionary<string, string>();
			args.Add("x-ha-policy", "all");

			channel.QueueDeclare(queueName, true, false, false, args);

			replyQueueName = this.channel.QueueDeclare();
			responseConsumer = new QueueingBasicConsumer(this.channel);
			channel.BasicConsume(replyQueueName, true, responseConsumer);
		}

		public void ExcludeCustomer(User user, string customerId, out Message message)
		{
			throw new NotImplementedException();
		}

		public Customer RegisterCustomer(User user, string customerName, out Message message)
		{
			CustomerServiceRequest request = new CustomerServiceRequest(user, CustomerOperationTypeRequest.Register, null, customerName, null);
			byte[] messageBody = request.ToByteArray();

			IBasicProperties requestProperties = channel.CreateBasicProperties();
			requestProperties.CorrelationId = Guid.NewGuid().ToString();
			requestProperties.ReplyTo = replyQueueName;

			channel.BasicPublish(string.Empty, queueName, requestProperties, messageBody);
			BasicDeliverEventArgs replyInEnvelope = responseConsumer.Queue.Dequeue() as BasicDeliverEventArgs;
			if (replyInEnvelope != null)
			{
				object responseObject = SerializationHelper.FromByteArray(replyInEnvelope.Body);
				CustomerRegisterServiceResponse response = responseObject as CustomerRegisterServiceResponse;
				message = response.Message;
				return response.NewlyRegisteredCustomer;
			}
			message = null;
			return null;
		}

		public Customer[] SearchCustomers(User user, string nameLike, out Message message)
		{
			CustomerServiceRequest request = new CustomerServiceRequest(user, CustomerOperationTypeRequest.Search, null, null, nameLike);
			byte[] messageBody = request.ToByteArray();

			IBasicProperties requestProperties = channel.CreateBasicProperties();
			requestProperties.CorrelationId = Guid.NewGuid().ToString();
			requestProperties.ReplyTo = replyQueueName;

			channel.BasicPublish(string.Empty, queueName, requestProperties, messageBody);
			BasicDeliverEventArgs replyInEnvelope = responseConsumer.Queue.Dequeue() as BasicDeliverEventArgs;
			if (replyInEnvelope != null)
			{
				object responseObject = SerializationHelper.FromByteArray(replyInEnvelope.Body);
				CustomerSearchServiceResponse response = responseObject as CustomerSearchServiceResponse;
				message = response.Message;
				return response.Customers;
			}
			message = null;
			return null;
		}

		public void UpdateCustomer(User user, string customerId, string newCustomerName, out Message message)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			channel = null;

			if (connection.IsOpen)
			{
				connection.Close();
			}

			connection.Dispose();
			connection = null;
		}
	}
}
