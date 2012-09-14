using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.System
{
    public class Context
		: EntityBase
    {
		internal Context() : base() { }

		private IList<Topic> topics;
		private IList<Comment> comments;

		public string SystemKey { get; set; }

		public string ContextId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
		public IList<Topic> Topics { get; set; }
		public IList<Comment> Comments { get; set; }

		public Topic FindTopicByKey(string key)
		{
			return (from t in Topics
					where t.Key.Equals(key)
					select t).First();
		}
		public void UpdateTopicTitle(string topicKey, string title)
		{
			Topic topic = FindTopicByKey(topicKey);
			topic.Title = title;
		}
		public void UpdateTopicDescription(string topicKey, string description)
		{
			Topic topic = FindTopicByKey(topicKey);
			topic.Description = description;
		}
		public void AddTopic(Topic topic)
		{
			if (topics == null)
				topics = new List<Topic>();

			topics.Add(topic);
		}

		public void AddComment(Comment comment)
		{
			if (comments == null)
				comments = new List<Comment>();

			comments.Add(comment);
		}
	}
}
