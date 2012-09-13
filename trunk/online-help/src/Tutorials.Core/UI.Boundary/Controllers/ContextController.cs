using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Domain.Context;
using Tutorials.Core.Infrastructure.Factories;

namespace Tutorials.Core.UI.Boundary.Controllers
{
	public static class ContextController
	{
		static ContextService service;

		static ContextController()
		{
			service = ServicesFactory.ContextService;
		}
		
		public static Context CreateNewContext(string contextId, string title, string description, int order)
		{
			Context context = new Context 
			{ 
				ContextId = contextId, 
				Title = title,
				Description = description,
				Order = order
			};
			return service.CreateNewContext(context);
		}

		public static Context GetContext(string key)
		{
			return service.GetContext(key);
		}

		public static Context[] GetAllContexts()
		{
			return service.GetAllContexts();
		}

		public static Context GetFirstContext()
		{
			return service.GetFirstContext();
		}

		public static Topic CreateNewTopic(string contextKey, string title, string description, int order)
		{
			Topic topic = new Topic 
			{
				Title = title,
				Description = description,
				Order = order
			};

			return service.CreateNewTopic(contextKey, topic);
		}

		public static void OrderContexts(string[] ids)
		{
			service.OrderContexts(ids);
		}

		public static void DeleteContext(string id)
		{
			service.DeleteContext(id);
		}

		public static void UpdateContextTitle(string contextKey, string title)
		{
			service.UpdateContextTitle(contextKey, title);
		}
		
		public static void UpdateContextDescription(string contextKey, string description)
		{
			service.UpdateContextDescription(contextKey, description);
		}
	}
}
