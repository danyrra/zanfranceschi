using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Infrastructure.Repository
{
	public interface IUnitOfWorkRepository
	{
		void PersistNewEntity(IEntity entity);
		void PersistUpdatedEntity(IEntity entity);
		void PersistDeletedEntity(IEntity entity);
	}
}