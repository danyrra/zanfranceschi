using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.ServiceModel;
using EIP.Sandbox.TestServiceRef;
using System.ServiceModel.MsmqIntegration;
using EIP.Sandbox.ServiceReference1;

namespace EIP.Sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Sender";


			IServiceRegistry s = new ServiceRegistryClient();

			Console.WriteLine("busca");
			var q = Console.ReadLine();
			var ss = s.Search(q);

			foreach (var item in ss)
			{
				Console.WriteLine(item.Name);
			}

			Console.Read();
			 

			string address = "http://localhost:22043/TestService.svc/WsPlain";
			//string address = "http://localhost:22043/TestService.svc";
			
			EndpointAddress endpoint = new EndpointAddress(address);

			BasicHttpBinding basicBinding = new BasicHttpBinding();
			WSHttpBinding wsBinding = new WSHttpBinding(SecurityMode.None);

			ITestService channel = ChannelFactory<ITestService>.CreateChannel(wsBinding, endpoint);
			
			string result = channel.GetData(10);
			
			Console.WriteLine(result);

			Console.Read();

			var sender = new Sender();
			sender.Start();
		}
	}
}
