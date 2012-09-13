using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.UnitOfWork;
using Ninject.Modules;
using Ninject;

namespace Tutorials.Core.Domain.Context
{
	internal class ContextService
	{
		private IContextRepository repository;

		internal ContextService(IContextRepository repository)
		{
			this.repository = repository;
			this.repository.SetUnitOfWork(new UnitOfWork());
		}

		#region Context
		internal Context CreateNewContext(Context context)
		{
			repository.Add(context);
			repository.UnitOfWork.Commit();
			return context;
		}
		internal Context GetContext(string key)
		{
			return repository.Find(c => c.EqualsByKey(key)).First();
		}
		internal Context[] GetAllContexts()
		{
			return repository.Find(obj => true).OrderBy(c => c.Order).ToArray();
		}
		internal Context GetFirstContext()
		{
			return GetAllContexts().First();
		}
		internal void OrderContexts(string[] ids)
		{
			for (int i = 0; i < ids.Length; i++)
			{
				var context = repository.Find(c => c.Key.Equals(ids[i])).First();
				context.Order = i;
				repository.Update(context);
			}

			repository.UnitOfWork.Commit();
		}
		internal void DeleteContext(string id)
		{
			repository.DeleteContextByKey(id);
		}
		internal void UpdateContextTitle(string contextKey, string title)
		{
			var context = repository.Find(c => c.Key.Equals(contextKey)).First();
			context.Title = title;
			repository.Update(context);
			repository.UnitOfWork.Commit();
		}
		internal void UpdateContextDescription(string contextKey, string description)
		{
			var context = repository.Find(c => c.Key.Equals(contextKey)).First();
			context.Description = description;
			repository.Update(context);
			repository.UnitOfWork.Commit();
		}
		#endregion


		#region Topic
		internal Topic CreateNewTopic(string contextKey, Topic topic)
		{
			Context context = repository.Find(c => c.EqualsByKey(contextKey)).First();

			if (context.Topics == null)
				context.Topics = new List<Topic>();

			context.Topics.Add(topic);
			repository.Update(context);
			repository.UnitOfWork.Commit();
			return topic;
		} 
		#endregion
	}
}