using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using EIP.DeliveryMonitor.Messages;
using System.Threading;

namespace EIP.DeliveryMonitor.SubscribersList
{
	class _SubscribersList
	{
		static IList<Subscription> subscriptions = new List<Subscription>();

		static void Main(string[] args)
		{
			Console.Title = "Subscriber List";
			Console.WindowWidth = 50;
			Console.WindowHeight = 10;

			Thread subscriptionThread = new Thread(new ThreadStart(StartSubscriptionService));
			subscriptionThread.Start();

			Thread subscribersThread = new Thread(new ThreadStart(StartSubscribersQueryService));
			subscribersThread.Start();


			Console.Read();

		}

		static void StartSubscribersQueryService()
		{
			string queueName = @".\private$\subscriber_list_request";
			
			MessageQueue requestQueue = null;

			if (!MessageQueue.Exists(queueName))
			{
				requestQueue = MessageQueue.Create(queueName);
			}
			else
			{
				requestQueue = new MessageQueue(queueName);
			}
			
			requestQueue.Formatter = new BinaryMessageFormatter();

			MessagePropertyFilter filter = new MessagePropertyFilter();
			filter.SetAll();
			requestQueue.MessageReadPropertyFilter = filter;

			Console.WriteLine("Started subscribers query service.");

			string type = null;

			while (true)
			{
				try
				{
					Message requestMessage = requestQueue.Receive();

					type = requestMessage.Body.ToString();

					IList<Subscription> response = (from subscription in subscriptions 
												   where subscription.SubscriptionType.ToLower().Equals(type.ToLower()) 
													select subscription).ToList();

					MessageQueue responseQueue = requestMessage.ResponseQueue;
					Message responseMessage = new Message(response, new BinaryMessageFormatter());
					responseMessage.CorrelationId = requestMessage.Id;
					responseQueue.Send(responseMessage);
					Console.WriteLine("Responded @ {0}.", requestMessage.ResponseQueue.Path);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		static void StartSubscriptionService()
		{
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

			Console.WriteLine("Started subscription service.");

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
