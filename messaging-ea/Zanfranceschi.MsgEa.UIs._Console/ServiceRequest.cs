namespace Zanfranceschi.MsgEa.UIs._Console
{
	using System;
	using RabbitMQ.Client;
	using RabbitMQ.Client.Events;
	using Zanfranceschi.MsgEa.Messages.Requests;
	using Zanfranceschi.MsgEa.Messages.Responses;
	using Zanfranceschi.MsgEa.Model;

	internal class ServiceRequest
	{
		private IConnection connection;
		private IModel channel;
		private string replyQueueName;
		private QueueingBasicConsumer responseConsumer;

		private User user;

		private const string queueName = "zanfranceschi.msgea.customer";

		public ServiceRequest(User user)
		{
			this.user = user;
		}

		public void Connect()
		{
			ConnectionFactory factory = new ConnectionFactory
			{
				HostName = "localhost"
			};

			connection = factory.CreateConnection();
			channel = connection.CreateModel();
			channel.QueueDeclare(queueName, true, false, false, null);
			MakeReplyConsumer();
		}

		public void Disconnect()
		{
			channel = null;

			if (connection.IsOpen)
			{
				connection.Close();
			}

			connection.Dispose();
			connection = null;
		}

		private void MakeReplyConsumer()
		{
			replyQueueName = this.channel.QueueDeclare();

			responseConsumer = new QueueingBasicConsumer(this.channel);
			channel.BasicConsume(replyQueueName, true, responseConsumer);
		}

		public CustomerRegisterServiceResponse RequestCustomerRegistration(string newCustomerName)
		{
			CustomerServiceRequest request = new CustomerServiceRequest(user, CustomerOperationTypeRequest.Register, null, newCustomerName, null);
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
				return response;
			}
			return null;
		}

		public CustomerSearchServiceResponse RequestCustomersSearch(string nameLike)
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
				return response;
			}
			return null;
		}
	}
}