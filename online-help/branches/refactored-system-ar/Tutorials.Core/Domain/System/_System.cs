using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.System
{
    public class _System
		: EntityBase, IAggregateRoot
    {
		internal _System() { }
		public string Name { get; set; }
		public string Description { get; set; }
		public IList<Context> Contexts { get; set; }
    }
}
