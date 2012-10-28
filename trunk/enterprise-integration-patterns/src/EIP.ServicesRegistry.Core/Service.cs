using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace EIP.ServicesRegistry.Core
{
	[DataContract]
	public abstract class Service
		: IMongoDbEntity
	{
		[DataMember]
		public ObjectId Id { get; internal set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public string Address { get; set; }
		[DataMember]
		public string DataType { get; set; }
		[DataMember]
		public abstract string ServiceType { get; set;  }
	}
}