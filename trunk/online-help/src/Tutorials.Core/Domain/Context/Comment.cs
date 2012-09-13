using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.Context
{
    public class Comment
		: EntityBase
    {
		internal Comment() { }
		public string Post { get; set; }
		public string User { get; set; }
    }
}