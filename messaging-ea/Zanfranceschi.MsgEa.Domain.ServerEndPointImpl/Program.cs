namespace Zanfranceschi.MsgEa.Domain.ServerEndPointImpl
{
	using System;
	using Ninject;
	using Zanfranceschi.MsgEa.Domain.Impl.DAL;
	using Zanfranceschi.MsgEa.Domain.Impls.DAL.Impls.Memory;
	using Zanfranceschi.MsgEa.Domain.Impls.Services;

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
