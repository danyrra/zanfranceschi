using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.CanonicalModels;

namespace EIP.CanonicalDomain.Events
{
	public class EmployeeHired
		: BaseEvent
	{
		public EmployeeHired() : base() { }
		
		public Employee Employee { get; set; }

		public override float Event_Version
		{
			get { return 0.1f; }
		}
	}
}