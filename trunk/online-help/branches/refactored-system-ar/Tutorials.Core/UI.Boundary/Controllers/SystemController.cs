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

		public static Context GetContext(string key)
		{
			return service.GetContextByKey(key);
		}

		#region Context
		public static Context CreateNewContext(string systemKey, string contextId, string title, string description, int order)
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
			return service.GetContextByKey(key);
		}
		public static Context[] GetAllContexts()
		{
			return service.GetAllContexts();
		}
		public static Context GetFirstContext()
		{
			return service.GetFirstContext();
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
			var context = service.GetContextByKey(contextKey);
			service.UpdateContextTitle(context, title);
		}
		public static void UpdateContextDescription(string contextKey, string description)
		{
			var context = service.GetContextByKey(contextKey);
			service.UpdateContextDescription(context, description);
		} 
		#endregion

		#region Topic
		public static void OrderTopics(string contextKey, string[] ids)
		{
			var context = service.GetContextByKey(contextKey);
			service.OrderTopics(context, ids);
		}
		public static Topic CreateNewTopic(string contextKey, string title, string description, int order)
		{
			var topic = new Topic
			{
				Title = title,
				Description = description,
				Order = order
			};
			var context = service.GetContextByKey(contextKey);

			return service.CreateNewTopic(context, topic);
		} 
		#endregion
	}
}
