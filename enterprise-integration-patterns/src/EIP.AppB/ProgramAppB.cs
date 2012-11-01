using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.CanonicalDomain.Events;
using System.Threading;
using MassTransit;
using EIP.AppB.ServicesRegistry;
using EIP.CanonicalModels;

namespace EIP.AppB
{
	class ProgramAppB
	{
		static void Main(string[] args)
		{
			ConfigureEndpoint();
		}

		static IServiceBus bus;

		static void ConfigureEndpoint()
		{
			IServiceRegistry service = new ServiceRegistryClient();
			var _event = service.FindOneByDataType("EIP.CanonicalDomain.Events.EmployeeHired");
			bus = ServiceBusFactory.New(sbc =>
			{
				sbc.UseMsmq();
				sbc.UseMulticastSubscriptionClient();
				sbc.VerifyMsmqConfiguration();
				sbc.ReceiveFrom(string.Format("msmq://{0}/PublisherEmployeeHired_AppB", _event.Address));
				sbc.Subscribe(subs =>
				{
					subs.Handler<EmployeeHired>(obj => Console.WriteLine("Novo funcionário contratado: {0}", obj.Employee.Name));
				});
			});
			Console.Read();
		}
	}
}
