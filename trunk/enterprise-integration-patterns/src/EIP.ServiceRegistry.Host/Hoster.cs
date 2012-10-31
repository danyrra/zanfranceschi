﻿using System.Configuration;
using EIP.ServiceRegistry.Host._Service;
using Topshelf;
using EIP.ServicesRegistry.Core;

namespace EIP.ServiceRegistry.Host
{
	class Hoster
	{
		static void Main(string[] args)
		{
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
