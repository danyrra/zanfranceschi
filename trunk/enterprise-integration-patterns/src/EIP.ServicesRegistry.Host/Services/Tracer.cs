using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.ServicesRegistry.Host.Services
{
	internal static class Tracer
	{
		internal static void Trace(string message, params string[] args)
		{
			Console.WriteLine(message, args);
		}
	}
}
