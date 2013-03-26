﻿namespace Zanfranceschi.MsgEa.Domain.ServerEndPointImpl
{
	using System;
	using Ninject;
	using Zanfranceschi.MsgEa.Domain.Impl.DAL;
	using Zanfranceschi.MsgEa.Domain.Impls.DAL.Impls.Memory;
	using Zanfranceschi.MsgEa.Domain.Impls.Services;
	using Zanfranceschi.MsgEa.Domain.Services;
	using Zanfranceschi.MsgEa.Domain.Impl.Services;
	using System.Threading;

	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Server";
			Console.WindowHeight = 10;
			Console.WindowWidth = 80;

			Thread serverT = new Thread(StartServer);
			Thread utilServerT = new Thread(StartUtilServer);

			serverT.Start();
			utilServerT.Start();

			Console.Read();
		}

		static void StartServer()
		{
			using (IKernel kernel = new StandardKernel())
			{
				kernel.Bind<ICustomerDAO>().To<MemoryCustomerDAO>();
				ICustomerServices services = kernel.Get<CustomerServices>();

				Server server = new Server(services);
				server.Start();
			}
		}

		static void StartUtilServer()
		{
			UtilServices services = new UtilServices();
			UtilServer server = new UtilServer(services);
			server.Start();
		}
	}
}
