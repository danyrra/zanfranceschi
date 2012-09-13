using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.Context
{
	public class Topic
		: EntityBase
    {
		internal Topic() { }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
    }
}
