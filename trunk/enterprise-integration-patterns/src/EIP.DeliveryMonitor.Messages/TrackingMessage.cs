﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.DeliveryMonitor.Messages
{
	[Serializable]
	public class TrackingMessage
	{
		public string SenderId { get; set; }
		public string MessageId { get; set; }
		public TrackingMessageType TrackingMessageType { get; set; }

	}

	[Serializable]
	public enum TrackingMessageType
	{ 
		Published,
		Received
	}
}