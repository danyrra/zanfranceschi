using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.DAL;

namespace EIP.ServicesRegistry.Core
{
	public class ServiceSrv
	{
		private IServiceDAO dao;

		public ServiceSrv(IServiceDAO dao)
		{
			this.dao = dao;
		}

		public Service[] GetAll()
		{
			return dao.GetAll();
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

		public Service Create(Service service)
		{
			return dao.Insert(service);
		}

		public void Update(Service service)
		{
			dao.Update(service);
		}

		public void Remove(string id)
		{
			dao.Remove(id);
		}

		public Service CreateRequest(
			string name,
			string description,
			string address, 
			string dataType, 
			string definitionUrl)
		{
			RequestService request = new RequestService 
			{
				Address = address,
				DataType = dataType,
				DefinitionUrl = definitionUrl,
				Description = description,
				Name = name
			};


			return dao.Insert(request);
		}

		public Service CreateEventService(
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

			return dao.Insert(@event);
		}

		public void UpdateRequestService(
			string id,
			string name,
			string description,
			string address,
			string dataType,
			string definitionUrl)
		{
			RequestService request = new RequestService
			{
				Address = address,
				DataType = dataType,
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
