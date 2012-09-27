using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUS;

namespace Band.ProjetosTI
{
	class ProjetosTI
	{
		static Bus bus = new Bus();
		
		static void Main(string[] args)
		{
			Console.Title = "Projetos TI";

			bus.OnMessageReceived += new MessageReceivedHandler(bus_OnMessageReceived);
			bus.StartConsuming("localhost", "rh");
		}

		static void bus_OnMessageReceived(string message)
		{
			Console.WriteLine("Notificação de novo colaborador recebida.");
			Console.WriteLine("Criado login para área de projetos de TI para {0}", message);
			bus.Publish("localhost", "projetosTI", message.Replace(" ", ".").ToLower());
		}
	}
}
