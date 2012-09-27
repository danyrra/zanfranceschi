using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUS;

namespace Band.MonitoramentoInfra
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Monitoramento Infra";
			
			Bus bus = new Bus();
			bus.OnMessageReceived += new MessageReceivedHandler(bus_OnMessageReceived);
			bus.StartConsuming("localhost", "opec");
			bus.StartConsuming("localhost", "projetosTI");
			bus.StartConsuming("localhost", "rh");
		}

		static void bus_OnMessageReceived(string message)
		{
			Console.WriteLine(message);
		}
	}
}
