using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;
using Band.Mensagens.Wf;
using System.IO;
using Topshelf;
using System.Timers;
using Band.Bus.Componentes;


namespace Band.Wf.Ti
{
	public class Bus_WfTi
	{
		public static void Main(string[] args)
		{
			Console.Title = "Projetos de TI";
			
			HostFactory.Run(x =>                                
			{
				x.Service<Bus_WfTi>(s =>                        
				{
					s.ConstructUsing(name => new Bus_WfTi());
					s.WhenStarted(tc => tc.Subscribe());
					s.WhenStopped(tc => Console.WriteLine("serviço terminado"));
				});
				x.RunAsLocalSystem();

				x.SetDescription("Consumidor da Mensagem ColaboradorContratado emitida pelo RH.");
				x.SetDisplayName("ProjetosTI.Workflow.Bus");
				x.SetServiceName("ProjetosTI.Workflow.Bus");
			});

		}

		IBus bus;

		public Bus_WfTi()
		{
			bus = RabbitHutch.CreateBus("host=localhost", r => r.Register<IEasyNetQLogger>(sp => new BandLogger()));
			
		}

		public void Subscribe()
		{
			bus.Subscribe<LoginColaboradorCriado>("workflow.ti.login-colaborador-criado", HandleMessage);
		}

		public void HandleMessage(LoginColaboradorCriado obj)
		{
			Console.WriteLine("Acesso aos sistemas liberado para o usuário {0} <{1}>", obj.Colaborador.Nome, obj.Login);
		}
	}
}
