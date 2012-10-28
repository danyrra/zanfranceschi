using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.ServicesRegistry.Core
{
	public class RequestService
		: Service
	{
		public string DefinitionUrl { get; set; }

		public override string ServiceType
		{
			get { return "request"; }
			set { }
		}
	}
}
