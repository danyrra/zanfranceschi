using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;
using Tutorials.Core.Infrastructure.Repository;

namespace Tutorials.Core.Infrastructure.UnitOfWork
{
	public interface IUnitOfWork
	{
		void MarkAdded(IEntity entity, IUnitOfWorkRepository repository);
		void MarkChanged(IEntity entity, IUnitOfWorkRepository repository);
		void MarkRemoved(IEntity entity, IUnitOfWorkRepository repository);
		void Commit();
	}
}
