using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;

namespace Bandeirantes.Servicos.Testes.Sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			IBus bus = RabbitHutch.CreateBus();
			IPublishChannel channel = bus.OpenPublishChannel();

			channel.Publish<string>("asdas");
		}
	}
}
