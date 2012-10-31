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
		string Create(Service service);
		[OperationContract]
		string CreateEventService(string name, string description, string address, string dataType);
		[OperationContract]
		string CreateRequestService(string name, string description, string address, string definitionUrl);
		[OperationContract]
		Service[] GetAll();
		[OperationContract]
		EventService[] GetAllEventServices();
		[OperationContract]
		RequestService[] GetAllRequestServices();
		[OperationContract]
		Service GetById(string id);
		[OperationContract]
		EventService FindOneByDataType(string dataTypeFullName);
		[OperationContract]
		void Remove(string id);
		[OperationContract]
		Service[] Search(string term);
		[OperationContract]
		void Update(Service service);
		[OperationContract]
		void UpdateEventService(string id, string name, string description, string address, string dataType);
		[OperationContract]
		void UpdateRequestService(string id, string name, string description, string address, string definitionUrl);
	}
}
