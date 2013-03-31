namespace Zanfranceschi.MsgEa.ServerEndPointHost
{
	using System;
	using System.Threading;
	using Ninject;
	using Zanfranceschi.MsgEa.Domain.Impl.DAL;
	using Zanfranceschi.MsgEa.Domain.Impl.Services;
	using Zanfranceschi.MsgEa.Domain.Impls.DAL.Impls.Memory;
	using Zanfranceschi.MsgEa.Domain.Impls.Services;
	using Zanfranceschi.MsgEa.Domain.ServerEndPointImpl;
	using Zanfranceschi.MsgEa.Domain.ServerEndPointImpl.RabbitMq;
	using Zanfranceschi.MsgEa.Domain.Services;
	using System.IO;
	using System.Reflection;

	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Server";
			Console.WindowHeight = 10;
			Console.WindowWidth = 80;

			Console.WriteLine("Select the service to start.");
			Console.WriteLine("Type 1 for Customer and Utility Services.");
			Console.WriteLine("Type anything else for Subscriber Services.");

			string input = Console.ReadLine();

			if (input == "1")
			{
				Thread serverT = new Thread(StartServer);
				Thread utilServerT = new Thread(StartUtilServer);
				serverT.Start();
				Console.WriteLine("Customer Services started.");
				utilServerT.Start();
				Console.WriteLine("Utility Services started.");
			}
			else
			{
				Thread subsT = new Thread(StartSubscriber);
				subsT.Start();
				Console.WriteLine("Subscriber started.");
			}

			Thread fileWatcherThread = new Thread(StartFileWatcher);
			fileWatcherThread.Start();
		}

		static void StartFileWatcher()
		{
			FileSystemWatcher watcher = new FileSystemWatcher(AppDomain.CurrentDomain.BaseDirectory);
			watcher.NotifyFilter =
				  NotifyFilters.Attributes
				| NotifyFilters.CreationTime
				| NotifyFilters.DirectoryName
				| NotifyFilters.FileName
				| NotifyFilters.LastAccess
				| NotifyFilters.LastWrite
				| NotifyFilters.Security
				| NotifyFilters.Size;

			watcher.Changed += new FileSystemEventHandler(watcher_Changed);
			watcher.Deleted += new FileSystemEventHandler(watcher_Changed);
			watcher.Created += new FileSystemEventHandler(watcher_Changed);
			watcher.Filter = "*.dll";
			watcher.EnableRaisingEvents = true;
			
			Console.WriteLine("file watcher.");
		}

		static void watcher_Changed(object sender, FileSystemEventArgs e)
		{
			DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
			foreach (var item in info.GetFiles("*.dll"))
			{
				Console.WriteLine(item.Name);
			}
		}

		static void StartSubscriber()
		{
			ExampleNotificationSubscriber subscriber = new ExampleNotificationSubscriber();
			subscriber.Connect();
			subscriber.ConsumeMessages();
		}

		static void StartServer()
		{
			using (IKernel kernel = new StandardKernel())
			{
				kernel.Bind<ICustomerDAO>().To<MemoryCustomerDAO>();
				ICustomerServices services = kernel.Get<CustomerServices>();
				CustomerServicesServer server = new CustomerServicesServer(services, new Logger());
				server.Start();
			}
		}

		static void StartUtilServer()
		{
			UtilServices services = new UtilServices();
			RabbitMqUtilServicesServer server = new RabbitMqUtilServicesServer(services);
			server.Start();
		}
	}

	class Logger
		: IServicesServerLogger
	{
		public void Log(string log)
		{
			Console.WriteLine(log);
		}
	}
}
