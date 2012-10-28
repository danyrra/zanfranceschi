using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.ServicesRegistry.Core.DAL
{
	internal interface IServiceDAO
	{
		Service Insert(Service service);
		void Update(Service service);
		void Remove(string id);
		Service[] GetAll();
		RequestService[] GetAllRequest();
		EventService[] GetAllEvent();
		Service[] Search(string term);
		Service GetById(string id);
	}
}