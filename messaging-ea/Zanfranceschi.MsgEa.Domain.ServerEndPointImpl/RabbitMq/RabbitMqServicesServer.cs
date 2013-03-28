namespace Zanfranceschi.MsgEa.Domain.ServerEndPointImpl.RabbitMq
{
	using RabbitMQ.Client;
	using System;

	/// <summary>
	/// RabbitMQ specific implementation for base services class
	/// </summary>
	public abstract class RabbitMqServicesServer
	{
		protected IConnection connection;
		protected IModel channel;
		protected IServicesServerLogger logger;

		protected RabbitMqServicesServer(string host, IServicesServerLogger logger)
		{
			this.logger = logger;
			
			try
			{
				ConnectionFactory factory = new ConnectionFactory
				{
					HostName = host
				};

				connection = factory.CreateConnection();
				channel = connection.CreateModel();
				OnChannelCreated(channel);
			}
			catch (Exception ex)
			{
				logger.Log(ex.Message);
				logger.Log(ex.StackTrace);
				throw ex;
			}
		}

		protected abstract void OnChannelCreated(IModel channel);
	}
}