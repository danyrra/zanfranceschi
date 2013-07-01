using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Bandeirantes.Servicos.Bus;
using Bandeirantes.Servicos.Comum;
using Bandeirantes.Servicos.Corporativo.Comercial;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bandeirantes.Servicos.Demo.Publisher
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				try
				{
					Console.WriteLine("Entre com o número de mensagens para publicar ou digite sair para sair.");
					int number = Convert.ToInt32(Console.ReadLine());
					using (BandBus bus = new BandBus("localhost"))
					{
						using (PublishChannel pc = bus.OpenPublishChannel())
						{
							Console.WriteLine("Publicando {0} mensagens...", number);
							DateTime start = DateTime.Now;
							for (int i = 0; i < number; i++)
							{
								int id = new Random(i).Next();
								NegociacaoBloqueadaNotificacao notificacao = new NegociacaoBloqueadaNotificacao(id);
								pc.Publish<NegociacaoBloqueadaNotificacao>(notificacao);
							}
							DateTime end = DateTime.Now;
							TimeSpan time = end - start;
							Console.WriteLine("{0} mensagens publicadas em {1} segundos", number, time.TotalSeconds, time.Milliseconds);
						}
					}
				}
				catch
				{
					break;
				}
			}
		}
	}

	
}