using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.System
{
    public class System
		: EntityBase, IAggregateRoot
    {
		internal System() { }
		public string Name { get; set; }
		public string Description { get; set; }
    }
}
