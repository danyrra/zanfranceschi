using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace EIP.Deployment.MTQueueCreator
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				string machineName = Environment.MachineName;

				Console.WriteLine("Enter the Endpoint name (Leave blank for '{0}'):", machineName);
				string endpoint = Console.ReadLine();

				if (string.IsNullOrEmpty(endpoint))
					endpoint = machineName;

				string queue_subscriptions = string.Format("{0}_subscriptions", endpoint);
				string queue_subscriptions_error = string.Format("{0}_subscriptions_error", endpoint);

				string[] queues = { endpoint, queue_subscriptions, queue_subscriptions_error };

				foreach (string queue in queues)
				{
					string queue_path = string.Format(@".\private$\{0}", queue);

					if (!MessageQueue.Exists(queue_path))
					{
						MessageQueue.Create(queue_path, true);
						Console.WriteLine("Queue '{0}', created...", queue);
					}
					else
					{
						Console.WriteLine("Queue '{0}' already exists...", queue);
					}
				}

				Console.WriteLine("Queues creation finished. Enter 'exit' to exit or press any key to create more queues.");
				if (Console.ReadLine().ToLower().Equals("exit"))
					break;
			}
		}
	}
}