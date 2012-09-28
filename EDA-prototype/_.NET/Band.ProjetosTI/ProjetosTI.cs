using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;

namespace Band.ProjetosTI
{
	class ProjetosTI
	{
		static IBus bus = RabbitHutch.CreateBus("host=localhost");
		
		static void Main(string[] args)
		{
			Console.Title = "Projetos TI";

			bus.Subscribe<string>("projetosTI-id", "rh", bus_OnMessageReceived); 
		}

		static void bus_OnMessageReceived(string message)
		{
			Console.WriteLine("Notificação de novo colaborador recebida.");
			Console.WriteLine("Criado login para área de projetos de TI para {0}", message);

			var publishChannel = bus.OpenPublishChannel();
			publishChannel.Publish("projetosTI", message.Replace(" ", ".").ToLower());
		}
	}
}
