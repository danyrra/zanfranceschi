using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.DeliveryMonitor.Messages
{
	[Serializable]
	public class Subscription
	{
		public string Id { get; set; }
		public string SubscriptionType { get; set; }
		public override bool Equals(object obj)
		{
			return ((Subscription)obj).Id.Equals(Id);
		}
	}
}
