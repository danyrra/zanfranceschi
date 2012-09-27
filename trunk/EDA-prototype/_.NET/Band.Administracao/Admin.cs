using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUS;

namespace Band.Administracao
{
	class Admin
	{
		static void Main(string[] args)
		{
			Console.Title = "Administração";
			
			Bus bus = new Bus();
			bus.OnMessageReceived += new MessageReceivedHandler(bus_OnMessageReceived);
			bus.StartConsuming("localhost", "rh");
		}

		static void bus_OnMessageReceived(string message)
		{
			Console.WriteLine("Enviada solicitação para confecção de crachá para {0}", message);
		}
	}
}
