using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zanfranceschi.MsgEa.Sandbox.AppB
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "RPC Server";
			Console.WindowHeight = 10;
			Console.WindowWidth = 40;


			WebCEP cep = new WebCEP("06710590");

			
			RPCServer server = new RPCServer();
			server.Start();
		}
	}
}