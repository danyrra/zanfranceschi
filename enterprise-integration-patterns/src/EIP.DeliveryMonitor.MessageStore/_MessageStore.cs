using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using EIP.DeliveryMonitor.Messages;

namespace EIP.DeliveryMonitor.MessageStore
{
	class _MessageStore
	{

		static IList<TrackingMessage> messages = new List<TrackingMessage>();

		static void Main(string[] args)
		{
			Console.Title = "Message Store";
			Consume();
		}

		static void Consume()
		{
			string queueName = @".\private$\message_store";

			MessageQueue queue = null;

			if (!MessageQueue.Exists(queueName))
			{
				queue = MessageQueue.Create(queueName);
			}
			else
			{
				queue = new MessageQueue(queueName);
			}

			Console.WriteLine("ready...");

			while (true)
			{
				try
				{
					Message message = queue.Receive();
					message.Formatter = new XmlMessageFormatter(new String[] { "EIP.DeliveryMonitor.Messages.TrackingMessage, EIP.DeliveryMonitor.Messages" });
					TrackingMessage tracking = message.Body as TrackingMessage;
					messages.Add(tracking);
					Console.WriteLine("message stored: {0}/{1}", tracking.Id, tracking.TrackingMessageType);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
