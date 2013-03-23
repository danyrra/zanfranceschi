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


	public class IBus
	{
		private MessageQueue requestQueue;
		private MessageQueue responseQueue;
		
		public IBus(MessageQueue requestQueue, MessageQueue responseQueue)
		{
			this.requestQueue = requestQueue;
			this.responseQueue = responseQueue;

			MessagePropertyFilter filter = new MessagePropertyFilter();
			filter.SetAll();
			
			this.requestQueue.Formatter = new BinaryMessageFormatter();
			this.responseQueue.Formatter = new BinaryMessageFormatter();
			this.requestQueue.MessageReadPropertyFilter = filter;
			this.responseQueue.MessageReadPropertyFilter = filter;
		}
		
		public void Request<TRequest, TResponse>(TRequest request, Action<TResponse> callback)
		{
			Message requestMessage = new Message(request);
			requestMessage.ResponseQueue = responseQueue;
			requestQueue.Send(requestMessage);

			Message responseMessage = responseQueue.ReceiveByCorrelationId(requestMessage.Id, new TimeSpan(0, 5, 0));
			TResponse response = (TResponse)responseMessage.Body;
			callback(response);
		}

		public void Respond<TRequest, TResponse>(Func<TRequest, TResponse> func)
		{
			Message requestMessage = requestQueue.Receive();

			TRequest request = (TRequest)requestMessage.Body;

			TResponse response = func(request);

			Message responseMessage = new Message(response);
			responseMessage.CorrelationId = requestMessage.Id;

			MessageQueue responseQueue = requestMessage.ResponseQueue;
			responseQueue.Send(response);
		}
	}
}
