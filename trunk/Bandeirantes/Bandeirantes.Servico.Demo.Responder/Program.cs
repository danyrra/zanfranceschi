using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bandeirantes.Servicos.Bus;
using Bandeirantes.Servicos.Tv.Programacao;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bandeirantes.Servico.Demo.Responder
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WindowHeight = 10;
			Console.WindowWidth = 50;
			Console.Title = "Responder";

			Console.WriteLine("Aceitando requisições...");
			using (IServicesBus bus = ServiceBusFactory.CreateServiceBus("localhost"))
			{
				var requisicao = new ListarProgramasRequisicao();
				bus.Respond<ListarProgramasRequisicao, ListarProgramasResposta>(req =>
				{
					Console.WriteLine("Requisição recebida.");
					ListarProgramasRespostaEntidade lista = new ListarProgramasRespostaEntidade();
					ListarProgramasResposta resposta = new ListarProgramasResposta(lista);
					resposta.EnviadaEm = DateTime.Now;
					return resposta;
				});
			}
		}
	}
}