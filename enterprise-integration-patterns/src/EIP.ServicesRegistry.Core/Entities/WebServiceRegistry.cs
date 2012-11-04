using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EIP.ServicesRegistry.Core.Entities
{
	/// <summary>
	/// Representation of a WebService
	/// </summary>
	[DataContract]
	public class WebServiceRegistry
		: ServiceRegistry
	{
		/// <summary>
		/// Definition Url
		/// </summary>
		[DataMember]
		public string WsdlUrl { get; set; }
	}
}
