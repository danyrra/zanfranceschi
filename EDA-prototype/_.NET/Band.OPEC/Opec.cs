using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUS;
using System.Threading;

namespace Band.OPEC
{
	class Opec
	{
		static void Main(string[] args)
		{
			Console.Title = "OPEC";

			Bus bus = new Bus();
			bus.OnMessageReceived += new MessageReceivedHandler(bus_OnMessageReceived);
			bus.OnBusNotificationOccured += new BusNotificationHandler(bus_OnBusNotificationOccured);

			while (true)
			{
				bus.Publish("localhost", "opec", string.Format("Novo contrato nº {0}", new Random().Next()));
				Thread.Sleep(1000);
			}
		}

		static void bus_OnBusNotificationOccured(string message)
		{
			Console.WriteLine(message);
		}

		static void bus_OnMessageReceived(string message)
		{
			Console.WriteLine(message);
		}
	}
}
