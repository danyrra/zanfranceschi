using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;
using EasyNetQ;

namespace EIP.Sandbox
{
	class MyMessage
	{
		public string Text { get; set; }
	}
	
	class EasyNetQTest
	{
		static void Main(string[] args)
		{
			if (args.Contains("pub"))
			{
				Publish();
			}
			else if (args.Contains("sub"))
			{
				Subscribe();
			}
			Console.Read();
		}

		static void Subscribe()
		{
			var bus = RabbitHutch.CreateBus("host=localhost");
			bus.Subscribe<MyMessage>("my_subscription_id", msg => Console.WriteLine(msg.Text));
		}

		static void Publish()
		{ 
			var bus = RabbitHutch.CreateBus("host=localhost");

			using (var publishChannel = bus.OpenPublishChannel())
			{
				int i = 0;
				
				while (true)
				{
					publishChannel.Publish(new MyMessage { Text = i.ToString() });
					i++;
				}
			}
        
		}
	}
	
	/// <summary>
	/// Testes com multicast
	/// </summary>
	class Program
	{
		static void _Main(string[] args)
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

			string body;

			while (true)
			{
				body = string.Format("message {0}", i);

				MessageQueue queue = new MessageQueue(MULTICAST_QUEUE_FORMATTED);
				Message message = new Message(body, new XmlMessageFormatter(new String[] { "System.String,mscorlib" }));
				message.Body = body;
				queue.Send(message);
				Console.WriteLine("published: {0}", message.Body);
				i++;
				Thread.Sleep(1000);
			}
		}

		static void Subscriber()
		{
			MessageQueue queue = new MessageQueue(@".\private$\multicast_queue_test");
			queue.MulticastAddress = MULTICAST_QUEUE_ADDRESS;

			while (true)
			{
				try
				{
					Message message = queue.Receive();
					message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
					Console.WriteLine("received: {0}", message.Body);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					
				}
			}
		}
	}
}
