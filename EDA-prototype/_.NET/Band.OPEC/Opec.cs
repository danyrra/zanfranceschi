using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EasyNetQ;

namespace Band.OPEC
{
	class Opec
	{
		static void Main(string[] args)
		{
			Console.Title = "OPEC";

			var bus = RabbitHutch.CreateBus("host=localhost");
			var publishChannel = bus.OpenPublishChannel();

			while (bus.IsConnected)
			{
				publishChannel.Publish("opec", string.Format("Novo contrato nº {0}", new Random().Next()));
				Thread.Sleep(1000);
			}
			bus.Dispose();
		}
	}
}
