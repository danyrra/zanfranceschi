namespace Zanfranceschi.MsgEa.Services
{
	using System;
	using Ninject;
	using Zanfranceschi.MsgEa.Domain;
	using Zanfranceschi.MsgEa.Domain.DAL;
	using Zanfranceschi.MsgEa.Domain.DAL.Impls.Memory;

	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Server";
			Console.WindowHeight = 10;
			Console.WindowWidth = 80;
			
			using (IKernel kernel = new StandardKernel())
			{
				kernel.Bind<ICustomerDAO>().To<MemoryCustomerDAO>();
				CustomerServices services = kernel.Get<CustomerServices>();

				Server server = new Server(services);
				server.Start();
			}
		}
	}
}