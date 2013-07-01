using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zanfranceschi.MsgEa.Sandbox.AppA
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.Title = "RPC Client";
			Console.WindowHeight = 10;
			Console.WindowWidth = 45;

			ENQTest x = new ENQTest();

			
			RPCClient client = new RPCClient();

			while (true)
			{
				Console.WriteLine("Enter your request term ('exit' to quit):");
				string term = Console.ReadLine();

				if (term == "exit")
					break;

				var result = client.Call(term);
				Console.WriteLine("{0} - Reply: {1}", DateTime.Now, result);
			}

			client.Close();
		}
	}
}
