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
			
			Band();
		}

		static void RPC()
		{
			RPCClient rpcClient = new RPCClient();

			Console.WriteLine(" [x] Requesting fib(30)");
			string response = rpcClient.Call("30");
			Console.WriteLine(" [.] Got '{0}'", response);

			rpcClient.Close();
		}

		static void Band()
		{
			while (true)
			{
				try
				{
					Console.WriteLine("Entre com o número de requisições ou digite 'sair' para sair.");
					int number = Convert.ToInt32(Console.ReadLine());
					using (BandBus bus = new BandBus("localhost"))
					{
						using (PublishChannel pc = bus.OpenPublishChannel())
						{
							Console.WriteLine("Publicando {0} mensagens...", number);
							for (int i = 0; i < number; i++)
							{
								pc.Request<ListarProgramasRequisicao, ListarProgramasResposta>(new ListarProgramasRequisicao(), resposta => {
									Console.WriteLine("Responsta recebida.");
								}, "ff");
							}
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

	class RPCClient
	{
		private IConnection connection;
		private IModel channel;
		private string replyQueueName;
		private QueueingBasicConsumer consumer;

		public RPCClient()
		{
			ConnectionFactory factory = new ConnectionFactory();
			factory.HostName = "localhost";
			connection = factory.CreateConnection();
			channel = connection.CreateModel();
			replyQueueName = channel.QueueDeclare();
			consumer = new QueueingBasicConsumer(channel);
			channel.BasicConsume(replyQueueName, false, consumer);
		}

		public string Call(string message)
		{
			string corrId = Guid.NewGuid().ToString();
			IBasicProperties props = channel.CreateBasicProperties();
			props.ReplyTo = replyQueueName;
			props.CorrelationId = corrId;

			byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
			channel.BasicPublish("", "rpc_queue", props, messageBytes);

			while (true)
			{
				BasicDeliverEventArgs ea =
					(BasicDeliverEventArgs)consumer.Queue.Dequeue();
				if (ea.BasicProperties.CorrelationId == corrId)
				{
					return System.Text.Encoding.UTF8.GetString(ea.Body);
				}
			}
		}

		public void Close()
		{
			connection.Close();
		}
	}

}
