using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace EIP.Sandbox
{
	class Program
	{
		const string Queue = @"INFO1301040072\TestQueue";

		static void Main(string[] args)
		{
			Console.Title = "Receiver";

			Console.WriteLine("Ouvindo mensagens...");

			var receiver = new Receiver();
			receiver.Start();
		}
	}
}
