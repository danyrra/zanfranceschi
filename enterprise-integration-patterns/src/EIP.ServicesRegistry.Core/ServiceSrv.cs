using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core
{
	public class ServiceSrv
	{
		private IServiceDAO dao;

		public EventService FindOneByDataType(string dataTypeFullName)
		{
			return dao.FindOneByProperty("DataType", dataTypeFullName);
		}

		public ServiceSrv(IServiceDAO dao)
		{
			this.dao = dao;
		}

		public EventService[] GetAllEventServices()
		{
			return dao.GetAllEvent();
		}

		public RequestService[] GetAllRequestServices()
		{
			return dao.GetAllRequest();
		}

		public Service[] Search(string term)
		{
			return dao.Search(term);
		}

		public Service GetById(string id)
		{
			return dao.GetById(id);
		}

		public string Create(Service service)
		{
			return dao.Insert(service).Id;
		}

		public void Update(Service service)
		{
			dao.Update(service);
		}

		public void Remove(string id)
		{
			dao.Remove(id);
		}

		public string CreateRequest(
			string name,
			string description,
			string address, 
			string definitionUrl)
		{
			RequestService request = new RequestService 
			{
				Address = address,
				DefinitionUrl = definitionUrl,
				Description = description,
				Name = name
			};
			return dao.Insert(request).Id;
		}

		public string CreateEventService(
			string name,
			string description,
			string address,
			string dataType)
		{
			EventService @event = new EventService
			{
				Address = address,
				DataType = dataType,
				Description = description,
				Name = name,
			};

			return dao.Insert(@event).Id;
		}

		public void UpdateRequestService(
			string id,
			string name,
			string description,
			string address,
			string definitionUrl)
		{
			RequestService request = new RequestService
			{
				Address = address,
				DefinitionUrl = definitionUrl,
				Description = description,
				Name = name
			};

			dao.Update(request);
		}

		public void UpdateEventService(
			string id,
			string name,
			string description,
			string address,
			string dataType)
		{
			EventService @event = new EventService
			{
				Address = address,
				DataType = dataType,
				Description = description,
				Name = name,
			};

			dao.Update(@event);
		}
	}
}
