using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bandeirantes.Servicos.Bus;
using Bandeirantes.Servicos.Corporativo.Comercial;
using Bandeirantes.Servicos.Tv.Programacao;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bandeirantes.Servico.Demo.Requestor
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WindowHeight = 10;
			Console.WindowWidth = 50;
			Console.Title = "Requestor";

			while (true)
			{
				try
				{
					Console.WriteLine("Entre com o número de requisições ou digite 'sair' para sair.");
					int number = Convert.ToInt32(Console.ReadLine());
					using (IServicesBus bus = ServiceBusFactory.CreateServiceBus("localhost"))
					{
						using (IPublishChannel pc = bus.OpenPublishChannel())
						{
							Console.WriteLine("Publicando {0} mensagens...", number);

							IList<ListarProgramasRequisicao> requisicoes = new List<ListarProgramasRequisicao>();

							for (int i = 0; i < number; i++)
							{
								requisicoes.Add(new ListarProgramasRequisicao());
							}

							pc.BatchRequest<ListarProgramasRequisicao, ListarProgramasResposta>(requisicoes, resposta =>
							{
								Console.WriteLine("Resposta recebida. {0}", resposta.EnviadaEm);
							});
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
