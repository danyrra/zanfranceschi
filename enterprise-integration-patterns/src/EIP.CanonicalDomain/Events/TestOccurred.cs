using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.CanonicalDomain.Events
{
	[Serializable]
	public class TestOccurred
		: BaseEvent
	{
		public string Text { get; set; }
		
		public override float Event_Version
		{
			get { return 1f; }
		}
	}
}
