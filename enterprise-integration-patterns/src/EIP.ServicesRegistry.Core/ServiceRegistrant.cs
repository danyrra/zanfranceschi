using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace EIP.ServicesRegistry.Core
{
	public class ServiceRegistrant
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public ServiceEndPoint EndPoint { get; set; }
		public bool IsAlive { get; set; }
	}

	public enum ServiceRegistrantType
	{ 
		Queue,
		WebService
	}
}
