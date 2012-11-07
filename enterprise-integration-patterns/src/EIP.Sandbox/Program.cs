using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;

namespace EIP.Sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Contains("pub"))
			{
				Publisher();
			}
			else if (args.Contains("sub"))
			{
				Subscriber();
			}
		}

		static string MULTICAST_QUEUE_ADDRESS = "236.1.1.2:8001";
		static string MULTICAST_QUEUE_FORMATTED = string.Format("FormatName:MULTICAST={0}", MULTICAST_QUEUE_ADDRESS);

		static void Publisher()
		{
			decimal i = 0;
			while (true)
			{
				MessageQueue queue = new MessageQueue(MULTICAST_QUEUE_FORMATTED);
				queue.Send("test" + i.ToString());
				Console.WriteLine("test" + i.ToString());
				i++;
				Thread.Sleep(1000);
			}
}

		static void Subscriber()
		{
			MessageQueue queue = new MessageQueue(@".\private$\multicast_queue_test");
			BinaryMessageFormatter f = new BinaryMessageFormatter();
			queue.MulticastAddress = MULTICAST_QUEUE_ADDRESS;
			
			while (true)
			{
				Message message = queue.Receive();
				//var msg = f.Read(message);
				Console.WriteLine(message);
			}
		}
	}
}
