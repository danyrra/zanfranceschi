using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;
using Band.Mensagens.Wf;
using System.IO;
using Topshelf;
using System.Timers;


namespace Band.Wf.Ti
{
	public class Bus_WfTi
	{
		public static void Main(string[] args)
		{
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
			bus = RabbitHutch.CreateBus("host=localhost");
			
		}

		public void Subscribe()
		{
			bus.Subscribe<ColaboradorContratado>("workflow.ti.colaborador-criado", HandleMessage);
		}

		public void HandleMessage(ColaboradorContratado obj)
		{
			using (StreamWriter writer = new StreamWriter(@"C:\temp\message.txt", true))
			{
				string[] lines = { obj.Nome, obj.Sobrenome, obj.Departamento, obj.InicioColaboracao.ToShortTimeString() };
				foreach (var line in lines)
				{
					writer.WriteLine(line);
				}
				writer.WriteLine("###############################");
			}
		}
	}
}
