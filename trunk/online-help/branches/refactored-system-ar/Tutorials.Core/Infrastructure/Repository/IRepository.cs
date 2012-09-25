using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Tutorials.Core.Infrastructure.DomainBase;

namespace Tutorials.Core.Infrastructure.Repository
{
	public interface IRepository<T> 
		where T : IAggregateRoot
	{
		void Add(T entity);
		void Remove(T entity);
		void Update(T entity);
		T[] Find(Expression<Func<T, bool>> expression);
		T FindOneByKey(string key);
	}
}
