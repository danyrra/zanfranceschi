using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.TA;

namespace Tuturials.Core.Impls.Db4oFile.Infrastructure.Repository
{
    internal static class ObjectContainerFactory
    {
        internal static IObjectContainer ObjectContainer
        {
            get 
			{
				return Db4oFactory.OpenClient("localhost", 7878, "db4o", "123");
			}
        }
    }
}
