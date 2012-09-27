using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;

namespace Band.MonitoramentoInfra
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Monitoramento Infra";

			var bus = RabbitHutch.CreateBus("host=localhost");
			bus.Subscribe<string>("monitor-id", "rh", bus_OnMessageReceived);
			bus.Subscribe<string>("monitor-id", "projetosTI", bus_OnMessageReceived);
			bus.Subscribe<string>("monitor-id", "opec", bus_OnMessageReceived);
		}

		static void bus_OnMessageReceived(string message)
		{
			Console.WriteLine(message);
		}
	}
}
