using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.Context
{
    public class Context
		: EntityBase, IAggregateRoot
    {
		internal Context() : base() { }

		private IList<Topic> topics;
		private IList<Comment> comments;

		public string ContextId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
		public IList<Topic> Topics { get; set; }
		public IList<Comment> Comments { get; set; }

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
