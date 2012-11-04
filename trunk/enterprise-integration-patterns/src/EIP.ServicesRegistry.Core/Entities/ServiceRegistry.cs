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
	public abstract class ServiceRegistry
	{
		/// <summary>
		/// Artificial unique identifier
		/// </summary>
		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		[DataMember]
		public string Id { get; set; }
		
		/// <summary>
		/// Descriptive name for this service
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Detailed business description for this service
		/// </summary>
		[DataMember]
		public string Description { get; set; }

		/// <summary>
		/// Detailed technical description for this service
		/// </summary>
		[DataMember]
		public string TechnicalDetails { get; set; }

		/// <summary>
		/// Could be an IP address format or machine name.
		/// </summary>
		[DataMember]
		public string Address { get; set; }
	}
}