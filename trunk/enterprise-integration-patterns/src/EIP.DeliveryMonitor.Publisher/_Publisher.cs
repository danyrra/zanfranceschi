using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;
using EIP.DeliveryMonitor.Messages;

namespace EIP.DeliveryMonitor.Publisher
{
	class _Publisher
	{
		static void Main(string[] args)
		{
			Console.Title = "Publisher";
			
			decimal i = 0;

			string body = null;

			while (true)
			{
				body = string.Format("message #{0}", i);

				MessageQueue queue = new MessageQueue("FormatName:MULTICAST=236.1.1.2:8001");
				Message message = new Message(body, new XmlMessageFormatter(new String[] { "System.String,mscorlib" }));
				message.Body = body;
				queue.Send(message);
				Console.WriteLine("published: '{0}'", message.Body);


				MessageQueue trackingQueue = new MessageQueue(@".\private$\message_store");
				Message trackingMessage = new Message(new TrackingMessage { Id = message.Id, TrackingMessageType = TrackingMessageType.Published }, new XmlMessageFormatter(new String[] { "EIP.DeliveryMonitor.Messages.TrackingMessage, EIP.DeliveryMonitor.Messages" }));
				trackingQueue.Send(trackingMessage);

				i++;
				Thread.Sleep(2000);
			}
		}
	}
}
