using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;
using Band.Mensagens.Wf;
using System.Threading;
using Topshelf;
using Band.Bus.Componentes;

namespace Band.Wf.Rh
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.Title = "RH";
			
			HostFactory.Run(x =>
			{
				x.Service<Bus_WfRh>(s =>
				{
					s.ConstructUsing(name => new Bus_WfRh());
					s.WhenStarted(tc => tc.Start());
					s.WhenStopped(tc => tc.Stop());
				});
				x.RunAsLocalSystem();
				x.DependsOn("RabbitMQ");
				x.SetDescription("Emitidor da Mensagem ColaboradorContratado.");
				x.SetDisplayName("Rh.Workflow.Bus");
				x.SetServiceName("Rh.Workflow.Bus");
			});
		}
	}

	public class Bus_WfRh
	{
		IBus bus;
		IPublishChannel channel;

		public Bus_WfRh()
		{
			bus = RabbitHutch.CreateBus("host=localhost", r => r.Register<IEasyNetQLogger>(sp => new BandLogger()));
		}

		public void Start()
		{
			Console.WriteLine("Sistema de RH - 2012");
			channel = bus.OpenPublishChannel();
			while (bus.IsConnected)
			{
				Console.WriteLine("Entre com o nome do novo colaborador ou 0 para sair:");
				var input = Console.ReadLine();
				if (input.Equals("0"))
				{
					Stop();
					break;
				}
				var c = new ColaboradorContratado { Nome = input };
				channel.Publish<ColaboradorContratado>(c);
			}
		}

		public void Stop()
		{
			channel.Dispose();
			bus.Dispose();
		}
	}
}
