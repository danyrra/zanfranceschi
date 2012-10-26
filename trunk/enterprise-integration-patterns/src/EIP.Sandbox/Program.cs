using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.ServiceModel;
using EIP.Sandbox.ServiceReference1;


namespace EIP.Sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Sender";

			IServiceRegistry service = new ServiceRegistryClient();
			var list = service.GetAll();

			foreach (var item in list)
			{
				Console.WriteLine(item.Name);
			}

			Console.Read();

			var sender = new Sender();
			sender.Start();
		}
	}
}
