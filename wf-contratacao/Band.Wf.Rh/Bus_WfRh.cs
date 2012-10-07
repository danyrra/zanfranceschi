using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;
using Band.Mensagens.Wf;
using System.Threading;
using Topshelf;

namespace Band.Wf.Rh
{
	public class Program
	{
		public static void Main(string[] args)
		{
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
			bus = RabbitHutch.CreateBus("host=localhost");
		}

		public void Start()
		{
			channel = bus.OpenPublishChannel();
			while (bus.IsConnected)
			{
				Thread.Sleep(2000);
				var c = new ColaboradorContratado { Departamento = "Projetos TI", InicioColaboracao = DateTime.Now.AddDays(10), Nome = "Francisco", Sobrenome = "Zanfranceschi" };
				channel.Publish<ColaboradorContratado>(c);
			}
		}

		public void Stop()
		{
			Console.WriteLine("Stopping...");
			channel.Dispose();
			bus.Dispose();
		}
	}
}
