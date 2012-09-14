using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.System
{
    public class Employer
		: EntityBase
    {
		internal Employer() { }
		public string Name { get; set; }
    }
}