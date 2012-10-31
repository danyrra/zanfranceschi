using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIP.ServicesRegistry.Core.Entities;

namespace EIP.ServicesRegistry.Core.DAL
{
	public interface IServiceDAO
	{
		Service Insert(Service service);
		void Update(Service service);
		void Remove(string id);
		Service[] GetAll();
		RequestService[] GetAllRequest();
		EventService[] GetAllEvent();
		Service[] Search(string term);
		Service GetById(string id);
		EventService FindOneByProperty(string propertyName, object propertyValue);
	}
}