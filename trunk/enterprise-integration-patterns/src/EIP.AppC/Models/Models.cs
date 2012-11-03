using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Runtime.Caching;
using MassTransit;
using EIP.AppC.ServicesRegistry;
using System.Configuration;
using EIP.CanonicalDomain.Events;
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
			EventService eventService = null;

			string LatestEventServiceFilePath = ConfigurationManager.AppSettings["LatestEventServiceFilePath"];

			IServiceRegistry service = new ServiceRegistryClient();

			try
			{
				string dataType = typeof(TestOccurred).FullName;

				eventService = service.FindOneByDataType(typeof(TestOccurred).FullName);

				if (eventService == null)
					throw new Exception(string.Format("Could not find the service registry for type '{0}'.", dataType));

				using (StreamWriter file = new StreamWriter(LatestEventServiceFilePath, false))
				{
					string serializedEvent = JsonConvert.SerializeObject(eventService);
					file.Write(serializedEvent);
				}
			}
			catch (EndpointNotFoundException) // Service Registry unreachable...
			{
				using (StreamReader file = new StreamReader(LatestEventServiceFilePath))
				{
					eventService = JsonConvert.DeserializeObject<EventService>(file.ReadToEnd());
				}
			}

			string queueUniqueName = ConfigurationManager.AppSettings["QueueUniqueName"];
			string queueProtocol = ConfigurationManager.AppSettings["queue-protocol"];

			IServiceBus bus = ServiceBusFactory.New(sbc =>
			{
				if (queueProtocol == "msmq")
				{
					sbc.UseMsmq();
					sbc.UseMulticastSubscriptionClient();
					sbc.VerifyMsmqConfiguration();
				}
				else
				{
					sbc.UseRabbitMq();
				}
				sbc.ReceiveFrom(string.Format("{0}://{1}/{2}", queueProtocol, eventService.Address, queueUniqueName));
			});

			MemoryCache.Default["IServiceBus"] = bus;

			return bus;
		}
	}
}