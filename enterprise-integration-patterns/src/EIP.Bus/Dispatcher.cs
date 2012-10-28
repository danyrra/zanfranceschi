using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace EIP.Bus
{
	public class Dispatcher
	{
		MessageQueue InputQueue { get; set; }
		IList<MessageQueue> outputQueues { get; set; }



	}
}
