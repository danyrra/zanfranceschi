using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace EIP.ServicesRegistry.Core
{
	[DataContract]
	public class Service
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
		public string DefinitionUrl { get; set; }
		[DataMember]
		public string ServiceType { get; set; }
	}
}