using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL;

namespace EIP.ServicesRegistry.Core
{
	public static class ServiceRegistrySrv
	{
		private static IServiceDAO dao = DAOFactory.ServiceDAO;
		
		public static Service[] GetAll()
		{
			return dao.GetAll();
		}

		public static Service[] Search(string term)
		{
			return dao.Search(term);
		}

		public static Service GetById(string id)
		{
			return dao.GetById(id);
		}

		public static Service Insert(Service service)
		{
			return dao.Insert(service);
		}

		public static void Update(Service service)
		{
			dao.Update(service);
		}

		public static void Remove(string id)
		{
			dao.Remove(id);
		}
	}
}
