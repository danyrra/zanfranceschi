using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.System
{
	public class Topic
		: EntityBase
    {
		internal Topic() { }
		public Context Context { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
    }
}
