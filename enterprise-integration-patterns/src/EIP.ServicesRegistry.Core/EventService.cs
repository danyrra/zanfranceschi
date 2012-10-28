using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.ServicesRegistry.Core
{
	public class EventService
		: Service
	{
		public override string ServiceType
		{
			get { return "event"; }
			set { }
		}
	}
}
