using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Configuration;
using EIP.CanonicalDomain.Events;

namespace EIP.Sandbox
{
	internal class Sender
	{
		private string Queue = @"INFO1301040072\TestQueue";

		internal Sender()
		{
			Queue = ConfigurationManager.AppSettings["queue"];
		}

		internal void Start()
		{
			MessageQueue mq = null;

			if (MessageQueue.Exists(Queue))
			{
				mq = new MessageQueue(Queue);
			}
			else
			{
				mq = MessageQueue.Create(Queue, true);
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
						mq.Send(FormMessage(i.ToString()), MessageQueueTransactionType.Single);
					}
				}
				// just sends the message
				else
				{
					mq.Send(FormMessage(body), MessageQueueTransactionType.Single);
				}
			}
			mq.Close();
		}

		private Message FormMessage(string text)
		{
			TestOccurred obj = new TestOccurred();
			obj.Text = text;

			Message m = new Message(obj, new BinaryMessageFormatter());
			m.Recoverable = true; // write message to disk - safer
			//m.Label = "EIP.Sandbox";
			m.AcknowledgeType = AcknowledgeTypes.NegativeReceive | AcknowledgeTypes.NotAcknowledgeReceive;
			//m.UseDeadLetterQueue = true;
			m.UseJournalQueue = true;
			m.AdministrationQueue = new MessageQueue(@".\private$\Ack");
			m.
			return m;
		}
	}
}
