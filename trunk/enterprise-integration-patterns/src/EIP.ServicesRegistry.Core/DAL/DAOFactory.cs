using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL._MongoDB;

namespace EIP.ServicesRegistry.Core.DAL
{
	internal static class DAOFactory
	{
		internal static IServiceDAO ServiceDAO
		{
			get 
			{
				return new MongoDBServiceDAO("mongodb://localhost/?safe=true", "eip", "services");
			}
		}
	}
}