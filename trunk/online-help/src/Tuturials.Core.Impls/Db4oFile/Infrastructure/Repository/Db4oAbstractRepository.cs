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
		: IRepository<T>, IUnitOfWorkRepository where T : IAggregateRoot
	{
		private IUnitOfWork unitOfWork;

		#region IUnitOfWork
		public void SetUnitOfWork(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public IUnitOfWork UnitOfWork
		{
			get { return this.unitOfWork; }
		}
		public void Add(T entity)
		{
			this.unitOfWork.MarkAdded(entity, this);
		}
		public void Remove(T entity)
		{
			this.unitOfWork.MarkRemoved(entity, this);
		}
		public void Update(T entity)
		{
			this.unitOfWork.MarkChanged(entity, this);
		}
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
		#endregion

		#region IUnitOfWorkRepository
		public void PersistNewEntity(IEntity entity)
		{
			PersistGambiEntity(entity);
		}
		public void PersistUpdatedEntity(IEntity entity)
		{
			PersistGambiEntity(entity);
		}
		private void PersistGambiEntity(IEntity entity)
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
		public void PersistDeletedEntity(IEntity entity)
		{
			using (var db = ObjectContainerFactory.ObjectContainer)
			{
				db.Delete(entity);
			}
		}
		#endregion
	}
}
