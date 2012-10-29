using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using MassTransit;
using EIP.CanonicalDomain.Events;
using EIP.CanonicalModels;

namespace EIP.Sandbox
{
	class X : Consumes<EmployeeHired>.Selected
	{
		public bool Accept(EmployeeHired message)
		{
			return true;
		}

		public void Consume(EmployeeHired message)
		{
			Console.WriteLine(message.Employee.Name);
		}
	}

	
	class Program
	{
		const string Queue = @"INFO1301040072\TestQueue";

		static void Main(string[] args)
		{
			Console.Title = "Receiver";

			Console.WriteLine("Entre com o nome da fila:");
			var queue = Console.ReadLine();

			var bus = ServiceBusFactory.New(sbc =>
			{
				sbc.UseMsmq();
				//sbc.UseRabbitMq();
				sbc.UseMulticastSubscriptionClient();
				sbc.UseJsonSerializer();
				sbc.VerifyMsmqConfiguration();
				sbc.ReceiveFrom("msmq://localhost/ReceiverEmployeeHired" + queue);
				sbc.Subscribe(subs =>
				{
					subs.Handler<EmployeeHired>(obj => Console.WriteLine(obj.Employee.Name));
				});
			});

			//bus.SubscribeHandler<EmployeeHired>(obj => Console.WriteLine("..."));

			//Bus.Initialize(sbc =>
			//{
			//    sbc.UseMsmq();
			//    sbc.VerifyMsmqConfiguration();
			//    sbc.UseMulticastSubscriptionClient();
			//    sbc.ReceiveFrom("msmq://localhost/testqueue");
			//    sbc.Subscribe(subs =>
			//    {
			//        subs.Handler<EmployeeHired>(msg => Console.WriteLine(msg.Employee.Name));
			//        subs.Consumer<IConsumer>().Permanent();
			//    });
			//});

			Console.Read();
			
			Console.WriteLine("Ouvindo mensagens...");

			var receiver = new Receiver();
			receiver.Start();
		}
	}
}
