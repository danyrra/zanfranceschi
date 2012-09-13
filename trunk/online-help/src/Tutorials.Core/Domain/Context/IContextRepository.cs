using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Domain;
using Tutorials.Core.Infrastructure.Repository;


namespace Tutorials.Core.Domain.Context
{
	public interface IContextRepository
        : IRepository<Context> 
    {
		
    }
}
