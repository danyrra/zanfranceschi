using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Host.Services
{
	public class ServiceRegistry 
		: IServiceRegistry
	{
		ServiceSrv coreService;

		public ServiceRegistry()
		{
			this.coreService = SrvFactory.GetServiceSrv();
		}

		public Service[] GetAll()
		{
			return coreService.GetAll();
		}

		public RequestService[] GetAllRequestServices()
		{
			return coreService.GetAllRequestServices();
		}

		public EventService[] GetAllEventServices()
		{
			return coreService.GetAllEventServices();
		}

		public Service[] Search(string term)
		{
			return coreService.Search(term);
		}

		public Service GetById(string id)
		{
			return coreService.GetById(id);
		}

		public string Create(Service service)
		{
			return coreService.Create(service);
		}

		public string CreateRequestService(
			string name,
			string description,
			string address,
			string definitionUrl)
		{
			return coreService.CreateRequest(
				name,
				description,
				address,
				definitionUrl);
		}

		public string CreateEventService(
				string name,
				string description,
				string address,
				string dataType)
		{
			return coreService.CreateEventService(
				name,
				description,
				address,
				dataType);
		}

		public void Update(Service service)
		{
			coreService.Update(service);
		}

		public void UpdateEventService(
			string id, 
			string name, 
			string description, 
			string address, 
			string dataType)
		{
			coreService.UpdateEventService(
				id, 
				name, 
				description, 
				address, 
				dataType);
		}

		public void UpdateRequestService(
			string id, 
			string name, 
			string description, 
			string address, 
			string definitionUrl)
		{
			coreService.UpdateRequestService(
				id, 
				name, 
				description, 
				address, 
				definitionUrl);
		}

		public void Remove(string id)
		{
			coreService.Remove(id);
		}


		public EventService FindOneByDataType(string dataTypeFullName)
		{
			return coreService.FindOneByDataType(dataTypeFullName);
		}
	}
}
