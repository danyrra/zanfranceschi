using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EIP.ServicesRegistry.Core;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Host.Services
{
	[ServiceContract]
	public interface IServiceRegistry
	{
		[OperationContract]
		EventRegistry FindEventByDataType(string dataTypeFullName);
		[OperationContract]
		EventRegistry[] SearchEvents(string term);
		[OperationContract]
		WebServiceRegistry[] SearchWebService(string term);
	}
}
