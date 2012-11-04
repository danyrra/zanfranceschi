using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core.DAL
{
	public interface IServiceDAO
	{
		T Insert<T>(ServiceRegistry service) where T : ServiceRegistry;
		T GetById<T>(string id) where T : ServiceRegistry;
		T FindOneByProperty<T>(string propertyName, object propertyValue) where T : ServiceRegistry;
		EventRegistry[] SearchEvents(string term);
		WebServiceRegistry[] SearchWebServices(string term);
		void Update(ServiceRegistry service);
		void Remove(string id);
	}
}