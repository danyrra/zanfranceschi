using Tutorials.Core.Domain.System;
using Tuturials.Core.Impls.Db4oFile.Infrastructure.Repository;
using System.Linq;
using System.Linq.Expressions;
using System;


namespace Tuturials.Core.Impls.Db4oFile.Domain.System
{
	public class Db4oSystemRepository
		: Db4oAbstractRepository<Tutorials.Core.Domain.System.System>, ISystemRepository
	{
		
	}
}