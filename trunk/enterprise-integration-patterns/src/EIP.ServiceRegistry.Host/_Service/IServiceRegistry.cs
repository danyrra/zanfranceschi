using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EIP.ServicesRegistry.Core;

namespace EIP.ServiceRegistry.Host._Service
{
	[ServiceContract]
	public interface IServiceRegistry
	{
		[OperationContract]
		Service[] GetAll();
		[OperationContract]
		RequestService[] GetAllRequest();
		[OperationContract]
		EventService[] GetAllEvent();
		[OperationContract]
		Service[] Search(string term);
		[OperationContract]
		Service GetById(string id);
	}
}
