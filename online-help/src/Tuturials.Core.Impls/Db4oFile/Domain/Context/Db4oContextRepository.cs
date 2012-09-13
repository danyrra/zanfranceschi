using Tutorials.Core.Domain.Context;
using Tuturials.Core.Impls.Db4oFile.Infrastructure.Repository;
using System.Linq;
using System.Linq.Expressions;


namespace Tuturials.Core.Impls.Db4oFile.Domain.Context
{
	public class Db4oContextRepository
		: Db4oAbstractRepository<Tutorials.Core.Domain.Context.Context>, IContextRepository
    {
		public void DeleteContextByKey(string key)
		{
			using (var db = ObjectContainerFactory.ObjectContainer)
			{
				var context = db.Query<Tutorials.Core.Domain.Context.Context>().AsQueryable().Where(c => c.Key.Equals(key)).First();
				db.Delete(context);
			}
		}
	}
}