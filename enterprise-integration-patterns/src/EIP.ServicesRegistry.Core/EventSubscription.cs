using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace EIP.ServicesRegistry.Core
{
	[DataContract]
	public class EventSubscription
		: IEntity
	{
		[DataMember]
		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		public string Id { get; set; }
		[DataMember]
		public string EventServiceId { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string SubscriberAddress { get; internal set; }
		[DataMember]
		[BsonIgnore]
		public EventService EventService { get; set; }
		
		internal EventSubscription(EventService service, string name, string subscriberAddress)
		{
			this.EventService = service;
			this.SubscriberAddress = subscriberAddress;
			this.EventServiceId = service.Id;
			this.Name = name;
		}
	}
}