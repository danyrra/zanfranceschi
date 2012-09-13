using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorials.Core.Domain.Context;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Configuration;

namespace Tutorials.Core.Infrastructure.Factories
{
	internal static class ServicesFactory
	{
		private static IKernel kernel;
		
		static ServicesFactory()
		{
			string implAssemblyPath = ConfigurationManager.AppSettings["impl-assembly_full_path"];
			
			kernel = new StandardKernel(
				new NinjectSettings 
				{ 
					InjectNonPublic = true 
				});
			
			kernel.Bind(
				config => config.From(implAssemblyPath)
					.SelectAllClasses()
					.BindAllInterfaces()
					.Configure(b => b.InSingletonScope())
				);
		}
		public static ContextService ContextService
		{
			get { return kernel.Get<ContextService>(); }
		}
	}
}
