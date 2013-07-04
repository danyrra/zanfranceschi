using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bandeirantes.Servicos.Bus.Implementations.RabbitMq;

namespace Bandeirantes.Servicos.Bus
{
	public class ServiceBusFactory
	{
		public static IServicesBus CreateServiceBus(string hostname)
		{
			return new RabbitMqServicesBus(hostname);
		}
	}
}