using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Host.Services
{
	public class ServiceRegistry 
		: IServiceRegistry
	{
		ServiceSrv service;

		public ServiceRegistry()
		{
			this.service = SrvFactory.GetServiceSrv();
		}

		public EventRegistry FindEventByDataType(string dataTypeFullName)
		{
			Tracer.Trace("FindEventByDataType('{0}')", dataTypeFullName);
			return service.FindEventByDataType(dataTypeFullName);
		}

		public EventRegistry[] SearchEvents(string term)
		{
			Tracer.Trace("SearchEvents('{0}')", term);
			return service.SearchEvents(term);
		}

		public WebServiceRegistry[] SearchWebService(string term)
		{
			Tracer.Trace("SearchWebService('{0}')", term);
			return service.SearchWebServices(term);
		}
	}
}
