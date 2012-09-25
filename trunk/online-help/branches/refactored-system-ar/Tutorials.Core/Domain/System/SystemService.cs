using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Ninject;

namespace Tutorials.Core.Domain.System
{
	internal class SystemService
	{
		private ISystemRepository repository;

		internal SystemService(ISystemRepository repository)
		{
			this.repository = repository;
		}

		internal _System[] GetAllSystems()
		{
			return repository.Find(s => true);
		}

		internal Topic CreateNewTopic(string contextKey, Topic topic)
		{
			repository.Add(obj);
			return obj;
		}

		internal _System CreateNewSystem(_System obj)
		{
			repository.Add(obj);
			return obj;
		}

		internal Context CreateNewContext(string systemKey, Context context)
		{
			var system = repository.FindOneByKey(systemKey);
			
			if (system.Contexts == null)
				system.Contexts = new List<Context>();

			system.Contexts.Add(context);
			repository.Update(system);
			return context;
		}
	}
}