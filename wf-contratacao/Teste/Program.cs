using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using EasyNetQ;
using System.Threading;
using NGinnBPM.MessageBus;
using NGinnBPM.MessageBus.Windsor;
using System.Data.SqlClient;

namespace Teste
{
	public class TestMessage
	{
		public string Id { get; set; }
		public string Text { get; set; }
	}

	public class TestHandler
		: IMessageConsumer<TestMessage>,
		IMessageConsumer<string>
	{
		void Handle(TestMessage message)
		{
			Console.WriteLine("TestMessage arrived with Id={0}", message.Id);
		}

		void IMessageConsumer<TestMessage>.Handle(TestMessage message)
		{
			Console.WriteLine("TestMessage arrived with Id={0}", message.Id);
		}

		public void Handle(string message)
		{
			Console.WriteLine("Message arrived: {0}", message);
		}
	}

	public class Program
	{
		const string CONNECTION_STRING = "Data Source=sbdsao-vmd060;Initial Catalog=Band_Sandbox;Integrated Security=True";

		static void OnDependencyChange(object sender, SqlNotificationEventArgs e)
		{
			
		}

		public static void Main(string[] args)
		{

			var configurator = MessageBusConfigurator.Begin()
				.AddConnectionString("Band_Sandbox", CONNECTION_STRING)
				.SetEndpoint("sql://Band_Sandbox/MQueue1")
				.SetEndpoint("sql://Band_Sandbox/FilaTeste")
				.AddMessageHandlersFromAssembly(typeof(Program).Assembly)
				.AutoStartMessageBus(true)
				.FinishConfiguration();

			IMessageBus bus = configurator.ServiceResolver.GetInstance<IMessageBus>();

			bus.SubscribeAt("sql://Band_Sandbox/FilaTeste", typeof(string));
			
			//bus.SubscribeAt("sql://Band_Sandbox/MQueue1", typeof(string));

			while (true)
			{

			}
			

			//bus.

			//new Program().Subscribe();

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