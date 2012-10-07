using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using EasyNetQ;
using System.Threading;

namespace Teste
{
	public class Program
	{
		public static void Main(string[] args)
		{
			new Program().Subscribe();

			//Thread t = new Thread(new ThreadStart(new Program().Publish));
			//t.Start();
			
			//HostFactory.Run(x =>
			//{
			//    x.Service<Program>(s =>
			//    {
			//        s.ConstructUsing(name => new Program());
			//        s.WhenStarted(tc => tc.Subscribe());
			//        s.WhenStopped(tc => Console.WriteLine("serviço terminado"));
			//    });
			//    x.RunAsLocalSystem();

			//    x.SetDescription("Consumidor da Mensagem ColaboradorContratado emitida pelo RH.");
			//    x.SetDisplayName("ProjetosTI.Workflow.Bus");
			//    x.SetServiceName("ProjetosTI.Workflow.Bus");
			//});

		}

		IBus bus;
		IPublishChannel channel;

		public Program()
		{
			bus = RabbitHutch.CreateBus("host=localhost");
			channel = bus.OpenPublishChannel();

		}

		public void Publish()
		{
			int i = 0;
			while (true)
			{
				channel.Publish<string>("teste" + i.ToString(), config => config.WithTopic("test"));
				i++;
				Thread.Sleep(1000);
			}
		}

		public void Subscribe()
		{
			bus.Subscribe<string>("teste-id", HandleMessage, config => config.WithTopic("test"));
		}

		public void HandleMessage(string obj)
		{
			Console.WriteLine(obj);
		}
	}
}