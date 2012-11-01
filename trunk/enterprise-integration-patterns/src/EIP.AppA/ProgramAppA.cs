using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using EIP.CanonicalDomain.Events;
using System.Threading;
using EIP.CanonicalModels;
using EIP.AppA.ServiceRegistry;

namespace EIP.AppA
{
	class ProgramAppA
	{
		static void Main(string[] args)
		{
			ConfigureEndpoint();
			
			int i = 0;

			IList<string> names = new List<string>{
							 "Francisco",
							 "Sandra",
							 "Ana",
							 "João",
							 "Fernanda"
							};

			while (true)
			{
				Random rnd = new Random();
				int num = rnd.Next(0, 4);
				string name = names[num];

				Employee employee = new Employee { Name = name };

				bus.Publish(new EmployeeHired { Employee = employee });
				Console.WriteLine(name);
				Thread.Sleep(500);
			}
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
				sbc.ReceiveFrom(string.Format("msmq://{0}/PublisherEmployeeHired_AppA", _event.Address));
			});
		}
	}
}
