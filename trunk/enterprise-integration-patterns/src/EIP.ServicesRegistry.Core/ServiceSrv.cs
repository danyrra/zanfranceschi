using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core
{
	public class ServiceSrv
	{
		private IServiceDAO dao;

		public ServiceSrv(IServiceDAO dao)
		{
			this.dao = dao;
		}

		public EventRegistry CreateEventRegistry(EventRegistry obj)
		{
			return dao.Insert<EventRegistry>(obj);
		}

		public void UpdateEventRegistry(EventRegistry obj)
		{
			dao.Update(obj);
		}

		public void RemoveEventRegistry(string id)
		{
			dao.Remove(id);
		}

		public EventRegistry FindEventByDataType(string dataTypeFullName)
		{
			return dao.FindOneByProperty<EventRegistry>("CanonicalDataType", dataTypeFullName);
		}

		public EventRegistry[] SearchEvents(string term)
		{
			return dao.SearchEvents(term);
		}

		public WebServiceRegistry[] SearchWebServices(string term)
		{
			return dao.SearchWebServices(term);
		}
	}
}
