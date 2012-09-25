using System;
using System.Linq;
using System.Linq.Expressions;
using Tutorials.Core.Infrastructure.DomainBase;
using Tutorials.Core.Infrastructure.Repository;
using Tutorials.Core.Infrastructure.UnitOfWork;
using System.Threading;

namespace Tuturials.Core.Impls.Db4oFile.Infrastructure.Repository
{
	public abstract class Db4oAbstractRepository<T>
		: IRepository<T> where T : IAggregateRoot
	{
		public T[] Find(Expression<Func<T, bool>> expression)
		{
			using (var db = ObjectContainerFactory.ObjectContainer)
			{
				return db.Query<T>().AsQueryable().Where(expression).ToArray();
			}
		}
		public T FindOneByKey(string key)
		{
			using (var db = ObjectContainerFactory.ObjectContainer)
			{
				return db.Query<T>().AsQueryable().Where(obj => obj.Key.Equals(key)).First();
			}
		}
		public void Add(IEntity entity)
		{
			GambiSave(entity);
		}
		public void Update(IEntity entity)
		{
			GambiSave(entity);
		}
		public void Remove(IEntity entity)
		{
			using (var db = ObjectContainerFactory.ObjectContainer)
			{
				db.Delete(entity);
			}
		}
		
		private void GambiSave(IEntity entity)
		{
			using (var db = ObjectContainerFactory.ObjectContainer)
			{
				var persistentObjects = db.Query<IEntity>(delegate(IEntity e) { return e.Key.Equals(entity.Key); });
				foreach (var @object in persistentObjects)
				{
					db.Delete(@object);
				}
				db.Store(entity);
			}
		}
	}
}
