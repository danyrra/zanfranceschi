using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace EIP.ServicesRegistry.Core.Entities
{
	[DataContract]
	public abstract class Service
		: IEntity
	{
		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		[DataMember]
		public string Id { get; internal set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public string Address { get; set; }
		[DataMember]
		public abstract string ServiceType { get; set; }
	}
}