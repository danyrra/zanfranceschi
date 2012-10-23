using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Net.NetworkInformation;
using System.Threading;

namespace EIP.Sandbox
{
	internal class Receiver
	{
		const string Queue = @"INFO1301040072\TestQueue";

		TimeSpan timeout = TimeSpan.FromSeconds(2);

		internal Receiver()
		{
			Thread thread = new Thread(new ThreadStart(Start));
			thread.Start();
			Console.Read();
		}

		internal void Start()
		{
			if (MessageQueue.Exists(Queue))
			{
				MessageQueue mq = new MessageQueue(Queue, QueueAccessMode.Receive);
				mq.ReceiveCompleted += new ReceiveCompletedEventHandler(mq_ReceiveCompleted);
				mq.BeginReceive(timeout);
			}
		}

		void mq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
		{
			MessageQueue mq = (MessageQueue)sender;
			try
			{
				Message msg = mq.EndReceive(e.AsyncResult);
				var formatter = new BinaryMessageFormatter();
				var m = formatter.Read(msg);

				Console.WriteLine("mensagem recebida: {0}", m);
				mq.BeginReceive(timeout);
			}
			catch (MessageQueueException)
			{
				mq.Close();
				Start();
			}
		}
	}
}
