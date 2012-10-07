using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;
using Band.Mensagens.Wf;
using Band.Bus.Componentes;

namespace Band.Wf.Infra
{
	class Bus_WfInfra
	{
		public static void Main(string[] args)
		{
			Console.Title = "Infraestrutura";
			var bus = new Bus_WfInfra();
			bus.Subscribe();
		}

		IBus bus;
		IPublishChannel channel;


		public Bus_WfInfra()
		{
			bus = RabbitHutch.CreateBus("host=localhost", r => r.Register<IEasyNetQLogger>(sp => new BandLogger()));
			channel = bus.OpenPublishChannel();
			
		}

		public void Subscribe()
		{
			bus.Subscribe<ColaboradorContratado>("workflow.infra.colaborador-criado", HandleMessage);
		}

		public void HandleMessage(ColaboradorContratado obj)
		{
			Console.WriteLine("Login de rede criado para o usuário {0}", obj.Nome);
			channel.Publish<LoginColaboradorCriado>(
				new LoginColaboradorCriado 
				{ 
					Colaborador = obj, 
					Login = obj.Nome.Replace(" ", ".").ToLower()
				});
		}
	}
}
