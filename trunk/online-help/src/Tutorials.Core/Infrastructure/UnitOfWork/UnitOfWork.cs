using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;
using Tutorials.Core.Infrastructure.Repository;
using System.Transactions;

namespace Tutorials.Core.Infrastructure.UnitOfWork
{
	public class UnitOfWork
		: IUnitOfWork
	{
		private List<Operation> operations;

		public UnitOfWork()
		{
			this.operations = new List<Operation>();
		}

		private enum TransactionType
		{
			Insert,
			Update,
			Delete
		}

		private class Operation
		{
			public IEntity Entity { get; set; }
			public DateTime ProcessDateTime { get; set; }
			public IUnitOfWorkRepository Repository { get; set; }
			public TransactionType Type { get; set; }
		}

		#region IUnitOfWork
		public void MarkAdded(IEntity entity, IUnitOfWorkRepository repository)
		{
			this.operations.Add(
				new Operation
				{
					Entity = entity,
					ProcessDateTime = DateTime.Now,
					Repository = repository,
					Type = TransactionType.Insert
				});
		}
		public void MarkChanged(IEntity entity, IUnitOfWorkRepository repository)
		{
			this.operations.Add(
				new Operation
				{
					Entity = entity,
					ProcessDateTime = DateTime.Now,
					Repository = repository,
					Type = TransactionType.Update
				});
		}
		public void MarkRemoved(IEntity entity, IUnitOfWorkRepository repository)
		{
			this.operations.Add(
				new Operation
				{
					Entity = entity,
					ProcessDateTime = DateTime.Now,
					Repository = repository,
					Type = TransactionType.Delete
				});
		}
		public void Commit()
		{
			using (var scope = new TransactionScope())
			{
				foreach (var operation in this.operations.OrderBy(o => o.ProcessDateTime))
				{
					switch (operation.Type)
					{
						case TransactionType.Insert:
							operation.Repository.PersistNewEntity(operation.Entity);
							break;
						case TransactionType.Delete:
							operation.Repository.PersistDeletedEntity(operation.Entity);
							break;
						case TransactionType.Update:
							operation.Repository.PersistUpdatedEntity(operation.Entity);
							break;
					}
				}
				scope.Complete();
			}
			this.operations.Clear();
		}
		#endregion
	}
}
