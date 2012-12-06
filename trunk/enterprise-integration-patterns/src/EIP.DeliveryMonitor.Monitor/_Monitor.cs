using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;
using EIP.DeliveryMonitor.Messages;

namespace EIP.DeliveryMonitor.Monitor
{
	class _Monitor
	{
		static void Main(string[] args)
		{
			Console.Title = "Delivery Monitor (Requestor)";
			Console.WindowWidth = 60;
			Console.WindowHeight = 10;

			MessageQueue requestQueue = new MessageQueue(@".\private$\subscriber_list_request");
			MessageQueue responseQueue = new MessageQueue(@".\private$\subscriber_list_response");

			string body = null;

			while (true)
			{
				Console.WriteLine("Enter the message type to request list of subscribers:");
				body = Console.ReadLine();

				Message requestMessage = new Message(body, new BinaryMessageFormatter());
				requestMessage.ResponseQueue = responseQueue;
				requestQueue.Send(requestMessage);

				try
				{
					Message responseMessage = responseQueue.ReceiveByCorrelationId(requestMessage.Id, new TimeSpan(0, 0, 20));
					responseMessage.Formatter = new BinaryMessageFormatter();

					IList<Subscription> response = responseMessage.Body as IList<Subscription>;

					if (response != null && response.Count > 0)
					{
						Console.WriteLine("Here is the list for the query '{0}'", body);

						foreach (var subscription in response)
						{
							Console.WriteLine("\t{0}", subscription.Id);
						}
					}
					else
					{
						Console.WriteLine("No subscriber for '{0}'", body);
					}
				}
				catch (MessageQueueException ex)
				{
					Console.WriteLine("error: {0}", ex.Message);
				}
			}
		}
	}
}
