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


			IServiceRegistry service = new ServiceRegistryClient();
			var events = service.GetAllEventServices();

			foreach (var item in events)
			{
				Console.WriteLine(item.Name);
			}


			var requests = service.GetAllRequestServices();

			foreach (var item in requests)
			{
				Console.WriteLine(item.Name);
			}

			
			//IServiceRegistry service = new ServiceRegistryClient();

			string id = service.CreateEventService("EmployeeHired", "Event triggered when a employee is hired", "localhost", "EIP.CanonicalDomain.Events.EmployeeHired");

			EventService es = service.FindOneByDataType("EIP.CanonicalDomain.Events.EmployeeHired");
			
			ConfigureEndpoint();
			
			int i = 0;

			while (true)
			{

				bus.Publish(new EmployeeHired { Employee = new Employee { Name = "mensagem " + i.ToString() } });
				Console.WriteLine("mensagem {0}", i++);
				Thread.Sleep(500);
			}
		}

		static IServiceBus bus;

		static void ConfigureEndpoint()
		{

			IServiceRegistry service = new ServiceRegistryClient();

			service.CreateEventService("EmployeeHired", "Event triggered when a employee is hired", "localhost", "EIP.CanonicalDomain.Events.EmployeeHired");

			//service.FindOneByDataType("EIP.CanonicalDomain.Events.EmployeeHired");
			
			
			bus = ServiceBusFactory.New(sbc =>
			{
				sbc.UseMsmq();
				sbc.UseMulticastSubscriptionClient();
				sbc.VerifyMsmqConfiguration();
				sbc.ReceiveFrom("msmq://localhost/PublisherEmployeeHired");
			});




		}
	}


}
