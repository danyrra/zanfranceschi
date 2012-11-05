using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

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

		static void Publisher()
		{
			MessageQueue queue = new MessageQueue("FormatName:MULTICAST=235.109.116.117:7784");
			//MessageQueue queue = new MessageQueue(@".\private$\test_pub");
			//queue.MulticastAddress = "235.109.116.117:7784";
			//BinaryMessageFormatter f = new BinaryMessageFormatter();
			//Message msg = new Message("oi");
			//f.Write(msg, "i");
			queue.Send("xxx");
		}

		static void Subscriber()
		{
			MessageQueue queue = new MessageQueue(@".\private$\test_sub");
			BinaryMessageFormatter f = new BinaryMessageFormatter();
			//queue.MulticastAddress = "235.109.116.117:7784";
			queue.MulticastAddress = @"FormatName:MULTICAST=235.109.116.117:7784\private$\test_pub";
			
			while (true)
			{
				Message message = queue.Receive();
				//var msg = f.Read(message);
				Console.WriteLine(message);
			}
		}
	}
}
