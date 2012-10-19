using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.CanonicalDomain.Events
{
	public abstract class BaseEvent
	{
		protected BaseEvent()
		{
			Event_Id = Guid.NewGuid();
		}
		public Guid Event_Id { get; protected set; }
		public abstract float Event_Version { get; }
		public DateTime Event_Occurred { get; set; }
	}
}
