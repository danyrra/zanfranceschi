using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace EIP.ServicesRegistry.Core
{
	public class EventSubscription
		: IMongoDbEntity
	{
		public MongoDB.Bson.ObjectId Id { get; set; }

		public MongoDB.Bson.ObjectId EventServiceId { get; set; }

		private EventService eventService;
		
		[BsonIgnore]
		public EventService EventService 
		{ 
			get 
			{
				if (eventService == null)
					eventService = ServiceRegistrySrv.GetById(EventServiceId.ToString()) as EventService;

				return eventService;
			}
			private set { eventService = value; }
		}
		
		public string SubscriberQueuePath { get; private set; }
		
		internal EventSubscription(EventService service, string subscriberQueuePath)
		{
			this.EventService = service;
			this.SubscriberQueuePath = subscriberQueuePath;
			this.EventServiceId = service.Id;
		}
	}
}