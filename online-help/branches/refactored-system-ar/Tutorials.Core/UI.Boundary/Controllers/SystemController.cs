using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Domain.System;
using Tutorials.Core.Infrastructure.Factories;

namespace Tutorials.Core.UI.Boundary.Controllers
{
	public static class SystemController
	{
		static SystemService service;

		static SystemController()
		{
			service = ServicesFactory.ContextService;
		}

		public static _System[] GetAllSystems()
		{
			return service.GetAllSystems();
		}

		public static _System CreateSystem(string name, string description)
		{
			var system = new _System { Name = name, Description = description };
			return service.CreateSystem(system);
		}
	}
}