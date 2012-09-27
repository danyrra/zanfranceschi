using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EasyNetQ;

namespace Band.RH
{
	class Rh
	{
		static void Main(string[] args)
		{
			Console.Title = "RH";

			string[] nomes = 
			{
				"Ana",
				"João",
				"Francisco",
				"Gilberto",
				"Fulano",
				"Ciclana"
			};

			string[] sobrenomes = 
			{
				"Silva",
				"Pinheiro",
				"Aguiar",
				"Valadares",
				"Maluf",
				"Swcherts"
			};

			var bus = RabbitHutch.CreateBus("host=localhost");
			var publishChannel = bus.OpenPublishChannel();
			
			int i = 0;

			while (bus.IsConnected)
			{
				i++;
				Random random = new Random();
				string nome = string.Format("{0} {1}", nomes[random.Next(0, 5)], sobrenomes[random.Next(0, 5)]);
				publishChannel.Publish("rh", string.Format("{0} - {1}", nome, i));
				Console.WriteLine("Novo colaborador Band: {0}", nome);
				Thread.Sleep(1500);
			}

			bus.Dispose();
		}
	}
}
