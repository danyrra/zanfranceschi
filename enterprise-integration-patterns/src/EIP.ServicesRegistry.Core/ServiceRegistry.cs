using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.ServicesRegistry.Core
{
	public class ServiceRegistry
	{
		public ServiceRegistrant[] GetRegistrants()
		{
			return new ServiceRegistrant[] 
			{ 
				new ServiceRegistrant
				{ 
					Id = "1", 
					Name = "Test", 
					Address = @".\private$\TestQueue"
				}
			};
		}
	}
}
