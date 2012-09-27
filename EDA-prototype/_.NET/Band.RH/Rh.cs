using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BUS;
using System.Threading;

namespace Band.RH
{
	class Rh
	{
		static void Main(string[] args)
		{
			Console.Title = "RH";
			
			Bus bus = new Bus();

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
			
			while (true)
			{
				Random random = new Random();
				string nome = string.Format("{0} {1}", nomes[random.Next(0, 5)], sobrenomes[random.Next(0, 5)]);
				bus.Publish("localhost", "rh", nome);
				Console.WriteLine("Novo colaborador Band: {0}", nome);
				Thread.Sleep(1500);
			}
		}
	}
}
