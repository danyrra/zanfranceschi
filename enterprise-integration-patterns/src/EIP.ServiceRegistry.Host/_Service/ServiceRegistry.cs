using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core;

namespace EIP.ServiceRegistry.Host._Service
{
	public class ServiceRegistry
		: IServiceRegistry
	{
		ServiceSrv coreService;

		public ServiceRegistry()
		{
			this.coreService = SrvFactory.GetServiceSrv();
		}
		
		public Service[] GetAll()
		{
			return coreService.GetAll();
		}

		public RequestService[] GetAllRequest()
		{
			return coreService.GetAllRequest();
		}

		public EventService[] GetAllEvent()
		{
			return coreService.GetAllEvent();
		}

		public Service[] Search(string term)
		{
			return coreService.Search(term);
		}

		public Service GetById(string id)
		{
			return coreService.GetById(id);
		}

		public Service Create(Service service)
		{
			return coreService.Create(service);
		}

		public void Update(Service service)
		{
			coreService.Update(service);
		}

		public void Remove(string id)
		{
			coreService.Remove(id);
		}
	}
}
