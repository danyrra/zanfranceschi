using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace EIP.ServicesRegistry.Core
{
	[DataContract]
	public class EventSubscription
		: IMongoDbEntity
	{
		public MongoDB.Bson.ObjectId Id { get; set; }
		[DataMember]
		public MongoDB.Bson.ObjectId EventServiceId { get; set; }
		[DataMember]
		public string SubscriberQueuePath { get; private set; }
		
		private EventService eventService;

		[DataMember]
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
		
		internal EventSubscription(EventService service, string subscriberQueuePath)
		{
			this.EventService = service;
			this.SubscriberQueuePath = subscriberQueuePath;
			this.EventServiceId = service.Id;
		}
	}
}