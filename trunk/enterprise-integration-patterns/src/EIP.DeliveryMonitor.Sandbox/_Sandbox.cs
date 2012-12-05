using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;

namespace EIP.DeliveryMonitor.Sandbox
{
	class _Sandbox
	{
		static void Main(string[] args)
		{
			Console.Title = "Replier";

			MessagePropertyFilter filter = new MessagePropertyFilter();
			filter.SetAll();

			MessageQueue requestQueue = new MessageQueue(@".\private$\request_queue");
			requestQueue.MessageReadPropertyFilter = filter;
			requestQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

			Console.WriteLine("Waiting for replies...");

			while (true)
			{
				Message requestMessage = requestQueue.Receive();

				requestMessage.CorrelationId = requestMessage.Id;
				requestMessage.Body = requestMessage.Body.ToString().ToUpper();
				MessageQueue responseQueue = requestMessage.ResponseQueue;
				//Thread.Sleep(3000);
				responseQueue.Send(requestMessage);
				Console.WriteLine("replied: {0}", requestMessage.Body);
			}
		}
	}
}
