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
		/// <summary>
		/// TODO: Has to go away...
		/// </summary>
		/// <returns></returns>
		internal Context[] GetAllContexts()
		{
			return repository.Find(obj => true).OrderBy(c => c.Order).ToArray();
		}
		
		internal Context[] GetContextsBySystemKey(string systemKey)
		{
			return repository.Find(c => c.SystemKey.Equals(systemKey)).OrderBy(c => c.Order).ToArray();
		}
		internal Context CreateNewContext(Context context)
		{
			repository.Add(context);
			repository.UnitOfWork.Commit();
			return context;
		}
		internal Context GetContextByKey(string key)
		{
			return repository.FindOneByKey(key);
		}
		internal Context GetFirstContext()
		{
			return GetAllContexts().First();
		}
		internal void OrderContexts(string[] ids)
		{
			for (int i = 0; i < ids.Length; i++)
			{
				var context = repository.FindOneByKey(ids[i]);
				context.Order = i;
				repository.Update(context);
			}
			repository.UnitOfWork.Commit();
		}
		internal void DeleteContext(string id)
		{
			var context = repository.FindOneByKey(id);
			repository.Remove(context);
			repository.UnitOfWork.Commit();
		}
		internal void UpdateContextTitle(Context context, string title)
		{
			context.Title = title;
			repository.Update(context);
			repository.UnitOfWork.Commit();
		}
		internal void UpdateContextDescription(Context context, string description)
		{
			context.Description = description;
			repository.Update(context);
			repository.UnitOfWork.Commit();
		}
		#endregion

		#region Topic
		internal Topic CreateNewTopic(Context context, Topic topic)
		{
			if (context.Topics == null)
				context.Topics = new List<Topic>();

			context.Topics.Add(topic);
			repository.Update(context);
			repository.UnitOfWork.Commit();
			return topic;
		} 
		internal void OrderTopics(Context context, string[] ids)
		{
			for (int i = 0; i < ids.Length; i++)
			{
				var topic = context.FindTopicByKey(ids[i]);
				topic.Order = i;
			}
			var topics = context.Topics.OrderBy(t => t.Order).ToList();
			context.Topics = topics;
			repository.Update(context);
			repository.UnitOfWork.Commit();
		}
		internal void UpdateTopicTitle(Context context, string topicKey, string title)
		{
			var topic = context.FindTopicByKey(topicKey);
			topic.Title = title;
			repository.Update(context);
			repository.UnitOfWork.Commit();	
		}
		internal void UpdateTopicDescription(Context context, string topicKey, string description)
		{
			var topic = context.FindTopicByKey(topicKey);
			topic.Description = description;
			repository.Update(context);
			repository.UnitOfWork.Commit();
		}
		#endregion
	}
}