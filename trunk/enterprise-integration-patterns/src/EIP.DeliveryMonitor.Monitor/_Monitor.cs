using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;

namespace EIP.DeliveryMonitor.Monitor
{
	class _Monitor
	{
		static void Main(string[] args)
		{
			Console.Title = "Delivery Monitor (Requestor)";

			MessageQueue requestQueue = new MessageQueue(@".\private$\request_queue");
			MessageQueue responseQueue = new MessageQueue(@".\private$\response_queue");
			
			string body = null;

			while (true)
			{
				Console.WriteLine("Message:");
				body = Console.ReadLine();

				Message requestMessage = new Message(body, new XmlMessageFormatter(new String[] { "System.String,mscorlib" }));
				requestMessage.ResponseQueue = responseQueue;
				requestMessage.Body = body;
				requestQueue.Send(requestMessage);

				try
				{
					Message responseMessage = responseQueue.ReceiveByCorrelationId(requestMessage.Id, new TimeSpan(0, 0, 3));
					responseMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
					Console.WriteLine("response: {0}", responseMessage.Body);
				}
				catch (MessageQueueException ex)
				{
					Console.WriteLine("error: {0}", ex.Message);
				}
			}
		}
	}
}
