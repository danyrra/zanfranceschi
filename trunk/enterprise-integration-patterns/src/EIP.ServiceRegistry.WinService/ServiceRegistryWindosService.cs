using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.ServiceModel;

namespace EIP.ServiceRegistry.WinService
{
	public class ServiceRegistryWindosService
		: ServiceBase
	{
		public ServiceHost serviceHost = null;
		
		public ServiceRegistryWindosService()
		{
			// Name the Windows Service
			ServiceName = "ServiceRegistry";
		}

		public static void Main()
		{
			ServiceBase.Run(new ServiceRegistryWindosService());
		}

		protected override void OnStart(string[] args)
		{
			if (serviceHost != null)
			{
				serviceHost.Close();
			}
			
			serviceHost = new ServiceHost(typeof(ServiceRegistryImpl));
			serviceHost.Open();
		}

		protected override void OnStop()
		{
			if (serviceHost != null)
			{
				serviceHost.Close();
				serviceHost = null;
			}
		}
	}
}
