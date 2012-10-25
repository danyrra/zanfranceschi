using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core;

namespace EIP.ServiceRegistry.WinService
{
	public class ServiceRegistryImpl
		: IServiceRegistry
	{
		public Service[] GetAll()
		{
			return ServiceRegistrySrv.GetAll();
		}

		public Service[] Search(string term)
		{
			return ServiceRegistrySrv.Search(term);
		}

		public Service GetById(string id)
		{
			return ServiceRegistrySrv.GetById(id);
		}

		public Service Insert(Service service)
		{
			return ServiceRegistrySrv.Insert(service);
		}

		public void Update(Service service)
		{
			ServiceRegistrySrv.Update(service);
		}

		public void Remove(string id)
		{
			ServiceRegistrySrv.Remove(id);
		}
	}
}
