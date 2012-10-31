using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EIP.ServicesRegistry.Core.Entities
{
	[DataContract]
	public class RequestService
		: Service
	{
		[DataMember]
		public string DefinitionUrl { get; set; }
		[DataMember]
		public override string ServiceType
		{
			get { return "request"; }
			set { }
		}
	}
}
