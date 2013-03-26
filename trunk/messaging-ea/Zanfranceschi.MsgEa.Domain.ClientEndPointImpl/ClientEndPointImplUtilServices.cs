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

	public class ClientEndPointImplUtilServices
		: IUtilServices
	{
		private IConnection connection;
		private IModel channel;
		private string replyQueueName;
		private QueueingBasicConsumer responseConsumer;
		private User user;
		private const string queueName = "zanfranceschi.msgea.utils";

		public ClientEndPointImplUtilServices()
		{
			try
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
			catch (Exception ex)
			{
				Console.WriteLine("Error: {0}", ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
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

		public Address GetAddressByCEP(User user, string CEP, out Message message)
		{
			GetAddressServiceRequest request = new GetAddressServiceRequest(user, CEP);
			byte[] messageBody = request.ToByteArray();

			IBasicProperties requestProperties = channel.CreateBasicProperties();
			requestProperties.CorrelationId = Guid.NewGuid().ToString();
			requestProperties.ReplyTo = replyQueueName;

			channel.BasicPublish(string.Empty, queueName, requestProperties, messageBody);
			BasicDeliverEventArgs replyInEnvelope = responseConsumer.Queue.Dequeue() as BasicDeliverEventArgs;
			if (replyInEnvelope != null)
			{
				object responseObject = SerializationHelper.FromByteArray(replyInEnvelope.Body);
				GetAddressServiceResponse response = responseObject as GetAddressServiceResponse;
				message = response.Message;
				return response.Address;
			}
			message = null;
			return null;
		}
	}
}
