using System;
using Bandeirantes.Servicos.Bus;
using Bandeirantes.Servicos.Corporativo.Comercial;

namespace Bandeirantes.Servicos.Demo.Subscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WindowHeight = 10;
			Console.WindowWidth = 50;
			Console.Title = "Subscriber";
			
			Console.WriteLine("Nome exclusivo da fila:");
			string subscriptionId = Console.ReadLine();
			
			using (IServicesBus bus = ServiceBusFactory.CreateServiceBus("localhost"))
			{
				bus.Subscribe<NegociacaoBloqueadaNotificacao>(subscriptionId, (notificacao) => {
					Console.WriteLine(notificacao.NegociacaoId);
				});
			}
		}
	}
}