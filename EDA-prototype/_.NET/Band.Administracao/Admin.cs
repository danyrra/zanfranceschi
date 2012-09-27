using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;

namespace Band.Administracao
{
	class Admin
	{
		static void Main(string[] args)
		{
			Console.Title = "Administração";

			var bus = RabbitHutch.CreateBus("host=localhost");
			bus.Subscribe<string>("admin-id", "rh", bus_OnMessageReceived);
		}

		static void bus_OnMessageReceived(string message)
		{
			Console.WriteLine("Enviada solicitação para confecção de crachá para {0}", message);
		}
	}
}
