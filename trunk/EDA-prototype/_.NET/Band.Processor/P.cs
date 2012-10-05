using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;
using Band.Messages;

namespace Band.Processor
{
	class P
	{
		static void Main(string[] args)
		{
			var request = new RequestMessage { Text = "mensagem de teste" };

			IBus bus = RabbitHutch.CreateBus("host=localhost");

			bus.Respond<RequestMessage, ResponseMessage>(r => new ResponseMessage { Text = string.Format("ooopa recebi sua mensagem {0}", r.Text) });
		}
	}
}