using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace EIP.Sandbox
{
	internal class Sender
	{
		const string Queue = @"INFO1301040072\TestQueue";

		internal void Start()
		{
			MessageQueue mq = null;

			if (MessageQueue.Exists(Queue))
			{
				mq = new MessageQueue(Queue);
			}
			else
			{
				mq = MessageQueue.Create(Queue);
			}

			while (true)
			{
				Console.WriteLine("Mensagem (0 para sair):");
				string body = Console.ReadLine();

				// exit the console
				if (body.Equals("0"))
				{
					mq.Close();
					break;
				}
				// send 10K messages for testing
				else if (body.Equals("stress"))
				{
					for (int i = 0; i < 10000; i++)
					{
						Message m = new Message(i.ToString(), new BinaryMessageFormatter());
						mq.Send(m);
					}
				}
				// just sends the message
				else
				{
					Message m = new Message(body, new BinaryMessageFormatter());
					mq.Send(m);
				}
			}
			mq.Close();
		}
	}
}
