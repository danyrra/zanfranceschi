using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EIP.ServicesRegistry.Core;

namespace EIP.ServiceRegistry.WinService
{
	[ServiceContract]
	public interface IServiceRegistry
	{
		[OperationContract]
		Service[] GetAll();
		[OperationContract]
		Service[] Search(string term);
		[OperationContract]
		Service GetById(string id);
		//[OperationContract]
		//Service Insert(Service service);
		//[OperationContract]
		//void Update(Service service);
		//[OperationContract]
		//void Remove(string id);
	}

}
