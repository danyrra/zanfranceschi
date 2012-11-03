using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;

namespace EIP.CanonicalDomain.Requests
{
	public class TestResponse
		: CorrelatedBy<Guid>
	{
		public Guid CorrelationId { get; set; }
		public string Response { get; set; }
	}
}
