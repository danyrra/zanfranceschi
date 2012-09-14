using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Domain.System
{
    public class User
		: EntityBase
    {
		internal User() { }
		public string Login { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public Employer Employer { get; set; }
    }
}
