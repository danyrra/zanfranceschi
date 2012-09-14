using Tutorials.Core.Domain.Context;
using Tuturials.Core.Impls.Db4oFile.Infrastructure.Repository;
using System.Linq;
using System.Linq.Expressions;
using System;


namespace Tuturials.Core.Impls.Db4oFile.Domain.Context
{
	public class Db4oContextRepository
		: Db4oAbstractRepository<Tutorials.Core.Domain.Context.Context>, IContextRepository
	{
		
	}
}