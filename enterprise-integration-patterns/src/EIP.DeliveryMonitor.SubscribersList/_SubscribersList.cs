using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using EIP.DeliveryMonitor.Messages;

namespace EIP.DeliveryMonitor.SubscribersList
{
	class _SubscribersList
	{
		static IList<Subscription> subscriptions = new List<Subscription>();
		
		static void Main(string[] args)
		{
			Console.Title = "Subscriber List";
			
			string queueName = @".\private$\subscriber_list";

			MessageQueue queue = null;

			if (!MessageQueue.Exists(queueName))
			{
				queue = MessageQueue.Create(queueName);
			}
			else
			{
				queue = new MessageQueue(queueName);
			}

			Console.WriteLine("Ready...");

			while (true)
			{
				try
				{
					Message message = queue.Receive();
					message.Formatter = new XmlMessageFormatter(new String[] { "EIP.DeliveryMonitor.Messages.Subscription, EIP.DeliveryMonitor.Messages" });
					Subscription subscription = message.Body as Subscription;

					if (!subscriptions.Contains(subscription))
					{
						subscriptions.Add(subscription);
						Console.WriteLine("received subscription: {0} of type {1}", subscription.Id, subscription.SubscriptionType);	
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
