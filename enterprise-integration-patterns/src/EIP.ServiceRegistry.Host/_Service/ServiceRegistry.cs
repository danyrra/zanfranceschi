﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core;

namespace EIP.ServiceRegistry.Host._Service
{
	public class ServiceRegistry
		: IServiceRegistry
	{
		public Service[] GetAll()
		{
			return ServiceRegistrySrv.GetAll();
		}

		public RequestService[] GetAllRequest()
		{
			return ServiceRegistrySrv.GetAllRequest();
		}

		public EventService[] GetAllEvent()
		{
			return ServiceRegistrySrv.GetAllEvent();
		}

		public Service[] Search(string term)
		{
			return ServiceRegistrySrv.Search(term);
		}

		public Service GetById(string id)
		{
			return ServiceRegistrySrv.GetById(id);
		}

		public Service Create(Service service)
		{
			return ServiceRegistrySrv.Create(service);
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