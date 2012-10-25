using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL;

namespace EIP.ServicesRegistry.Core
{
	internal static class ServiceRegistrySrv
	{
		private static IServiceDAO dao = DAOFactory.ServiceDAO;

		internal static Service[] GetAll()
		{
			return dao.GetAll();
		}

		internal static Service[] Search(string term)
		{
			return dao.Search(term);
		}

		internal static Service GetById(string id)
		{
			return dao.GetById(id);
		}

		internal static Service Insert(Service service)
		{
			return dao.Insert(service);
		}

		internal static void Update(Service service)
		{
			dao.Update(service);
		}

		internal static void Remove(string id)
		{
			dao.Remove(id);
		}
	}
}
