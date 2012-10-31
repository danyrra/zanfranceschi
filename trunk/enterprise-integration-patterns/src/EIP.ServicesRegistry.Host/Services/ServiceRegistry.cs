using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core;

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

		public Service Create(Service service)
		{
			return coreService.Create(service);
		}

		public Service CreateRequestService(
			string name,
			string description,
			string address,
			string dataType,
			string definitionUrl)
		{
			return coreService.CreateRequest(
				name,
				description,
				address,
				dataType,
				definitionUrl);
		}

		public Service CreateEventService(
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
			string dataType, 
			string definitionUrl)
		{
			coreService.UpdateRequestService(
				id, 
				name, 
				description, 
				address, 
				dataType, 
				definitionUrl);
		}

		public void Remove(string id)
		{
			coreService.Remove(id);
		}
	}
}
