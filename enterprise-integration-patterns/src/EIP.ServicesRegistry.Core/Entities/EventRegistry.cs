using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace EIP.ServicesRegistry.Core.Entities
{
	/// <summary>
	/// Represents a message handled by Queueing Systems (MSMQ, RabbitMQ, etc.)
	/// </summary>
	[DataContract]
	public class EventRegistry
		: ServiceRegistry
	{
		/// <summary>
		/// msmq, rabbitmq, etc.
		/// </summary>
		[DataMember]
		public string Protocol { get; set; }

		/// <summary>
		/// Should be formatted as typeof(MyMessage).FullName
		/// </summary>
		[DataMember]
		public string CanonicalDataType { get; set; }

		/// <summary>
		/// Version of the Canonical Data Type
		/// </summary>
		[DataMember]
		public string CanonicalDataTypeVersion { get; set; }
	}
}