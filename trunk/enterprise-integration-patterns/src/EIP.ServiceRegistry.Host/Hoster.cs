using System.Configuration;
using EIP.ServiceRegistry.Host._Service;
using Topshelf;
using EIP.ServicesRegistry.Core;

namespace EIP.ServiceRegistry.Host
{
	class Hoster
	{
		static void Test()
		{
			EventService service = new EventService
			{
				Address = @".\private$\testqueue",
				DataType = typeof(Hoster).FullName,
				Description = "short desc",
				Name = "EventOccurred"
			};

			EventService service2 = new EventService
			{
				Address = @".\private$\xx",
				DataType = typeof(Hoster).FullName,
				Description = "short desc",
				Name = "XXOccurred"
			};

			RequestService rservice = new RequestService 
			{
				Address = "http://xxx",
				Name = "."
			};

			ServiceRegistrySrv.Create(service2);

			ServiceRegistrySrv.Create(rservice);

			var serviços = ServiceRegistrySrv.GetAll();
			var eventos = ServiceRegistrySrv.GetAllEvent();
			var requests = ServiceRegistrySrv.GetAllRequest();

			EventSubscription subscriber = new EventSubscription(service2, @".\pathtomyqueue");

			var x = EventSubscriptionService.GetAll();

			EventSubscriptionService.Create(subscriber);


			var list = EventSubscriptionService.GetAll();
		}
		
		static void Main(string[] args)
		{
			Test();
			
			string baseWebAddress = ConfigurationManager.AppSettings["baseWebAddress"];
			string baseTcpAddress = ConfigurationManager.AppSettings["baseTcpAddress"];

			HostFactory.Run(x =>
			{
				x.Service<WcfServiceWrapper<ServiceRegistry.Host._Service.ServiceRegistry, IServiceRegistry>>(s =>
				{
					s.ConstructUsing(name =>

						new WcfServiceWrapper<ServiceRegistry.Host._Service.ServiceRegistry, IServiceRegistry>(
							"ServiceRegistry",
							baseWebAddress,
							baseTcpAddress));

					s.WhenStarted(tc => tc.Start());
					s.WhenStopped(tc => tc.Stop());
				});
				x.RunAsLocalSystem();

				x.SetDescription("ESB Service Registry");
				x.SetDisplayName("ServiceRegistry");
				x.SetServiceName("ServiceRegistry");
			});
		}
	}
}
