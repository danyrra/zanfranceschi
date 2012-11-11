using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Runtime.Caching;
using MassTransit;
using System.Configuration;
using EIP.CanonicalModel.Events;
using System.IO;
using System.ServiceModel;
using Newtonsoft.Json;

namespace EIP.AppC.Models
{
	/// <summary>
	/// AppC specific representation of the Employee model
	/// </summary>
	public class TestModel
	{
		public TestModel()
		{
			Id = IdGenerator.Instance.GetNextId();
		}
		public int Id { get; set; }
		public string Name { get; set; }
	}

	/// <summary>
	/// Mock id generator
	/// </summary>
	internal sealed class IdGenerator
	{
		private int currentId = 1;

		internal int GetNextId()
		{
			return currentId++;
		}

		private static volatile IdGenerator instance;
		private static object syncRoot = new Object();

		private IdGenerator() { }

		public static IdGenerator Instance
		{
			get
			{
				if (instance == null)
				{
					instance = MemoryCache.Default["IdGenerator"] as IdGenerator;

					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new IdGenerator();
							MemoryCache.Default["IdGenerator"] = instance;
						}
					}
				}
				return instance;
			}
		}
	}

	/// <summary>
	/// Service locator for IServiceBus (please, don't use a service locator in production)
	/// </summary>
	internal static class ServiceBusProvider
	{
		internal static IServiceBus Bus
		{
			get
			{
				IServiceBus bus = MemoryCache.Default["IServiceBus"] as IServiceBus;

				if (bus == null)
					bus = ServiceBusConfigurator.ConfigureEndpoint();

				return bus;
			}
		}
	}

	internal static class ServiceBusConfigurator
	{
		/// <summary>
		/// Lots of hard-code and high coupling here!
		/// Don't use it as it is. It's just an example code.
		/// </summary>
		internal static IServiceBus ConfigureEndpoint()
		{
			string queueUniqueName = ConfigurationManager.AppSettings["queue-unique_name"];
			
			string queueProtocol = ConfigurationManager.AppSettings["queue-protocol"];

			IServiceBus bus = ServiceBusFactory.New(sbc =>
			{
				if (queueProtocol == "msmq")
				{
					sbc.UseMsmq();
					sbc.UseMulticastSubscriptionClient();
					sbc.VerifyMsmqConfiguration();
				}
				else if (queueProtocol == "rabbitmq")
				{
					sbc.UseRabbitMq();
				}
				string address = string.Format("{0}://localhost/{1}__{2}", queueProtocol, Environment.MachineName, queueUniqueName);
				sbc.ReceiveFrom(address);
			});

			MemoryCache.Default["IServiceBus"] = bus;

			return bus;
		}
	}
}