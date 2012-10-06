using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;
using Band.Messages;

namespace Band.ProcessorCoordinator
{
	class PC
	{
		static void Main(string[] args)
		{
			var request = new RequestMessage { Text = "XXXXXXXXXXXXXXXXXXX" };
			
			IBus bus = RabbitHutch.CreateBus("host=localhost");

			using (var pc = bus.OpenPublishChannel())
			{
				pc.Request<RequestMessage, ResponseMessage>(request, resp => Console.WriteLine(resp.Text));
			}
		}
	}
}
