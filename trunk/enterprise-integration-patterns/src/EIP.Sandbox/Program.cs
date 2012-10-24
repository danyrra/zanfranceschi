using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.ServiceModel;
using EIP.Sandbox.TestServiceRef;

namespace EIP.Sandbox
{
	class Program
	{

		static void Main(string[] args)
		{
			Console.Title = "Sender";

			BasicHttpBinding binding = new BasicHttpBinding();
			EndpointAddress endpoint = new EndpointAddress("http://localhost:22043/TestService.svc");

			ITestService service = new TestServiceClient(binding, endpoint);
			string result = service.GetData(10);
			
			Console.WriteLine(result);

			Console.Read();

			var sender = new Sender();
			sender.Start();
		}
	}
}
