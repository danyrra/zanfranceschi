using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using EIP.DeliveryMonitor.Messages;

namespace EIP.DeliveryMonitor.Subscriber
{
	public class _Subscriber
	{
		static void Main(string[] args)
		{
			Console.Title = "Subscriber";
			var subcriber = new _Subscriber();
			subcriber.Configure();
			subcriber.Start();
		}

		static string MULTICAST_QUEUE_ADDRESS = "236.1.1.2:8001";
		static string MULTICAST_QUEUE_FORMATTED = string.Format("FormatName:MULTICAST={0}", MULTICAST_QUEUE_ADDRESS);

		private string queueId;
		private MessageQueue queue = null;

		void Configure()
		{
			Console.WriteLine("Please, enter the subscriber Id (alphanumeric only):");
			queueId = Console.ReadLine();
			Console.Title = "Subscriber - " + queueId;

			string queueName = string.Format(@".\private$\{0}", queueId);

			if (!MessageQueue.Exists(queueName))
			{
				queue = MessageQueue.Create(queueName);
			}
			else
			{
				queue = new MessageQueue(queueName);
			}

			queue.MulticastAddress = MULTICAST_QUEUE_ADDRESS;

			MessagePropertyFilter filter = new MessagePropertyFilter();
			filter.CorrelationId = true;
			filter.Body = true;
			filter.Id = true;

			queue.MessageReadPropertyFilter = filter;

			NotifyToSubscriberList();
		}

		void Start()
		{
			Console.WriteLine("started listening for messages...");

			while (true)
			{
				try
				{
					Message message = queue.Receive();
					message.Formatter = new XmlMessageFormatter(new String[] { "System.String, mscorlib" });
					Console.WriteLine("received: '{0}' - {1}", message.Body, message.Id);

					MessageQueue trackingQueue = new MessageQueue(@".\private$\message_store");
					Message trackingMessage = new Message(new TrackingMessage { Id = message.Id, TrackingMessageType = TrackingMessageType.Received }, new XmlMessageFormatter(new String[] { "EIP.DeliveryMonitor.Messages.TrackingMessage, EIP.DeliveryMonitor.Messages" }));
					trackingQueue.Send(trackingMessage);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		void NotifyToSubscriberList()
		{
			Subscription subscription = new Subscription { Id = queueId, SubscriptionType = "string" };
			MessageQueue queue = new MessageQueue(@".\private$\subscriber_list");
			Message message = new Message(subscription, new XmlMessageFormatter(new String[] { "EIP.DeliveryMonitor.Messages.Subscription, EIP.DeliveryMonitor.Messages" }));
			queue.Send(message);
		}
	}
}
