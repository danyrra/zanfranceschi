using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Messaging;

namespace EIP.PubSub
{
	public class Splitter
	{
		private string Queue = null;

		TimeSpan timeout = TimeSpan.FromSeconds(2);

		internal Splitter()
		{
			Queue = ConfigurationManager.AppSettings["queue"];
			Thread thread = new Thread(new ThreadStart(Start));
			thread.Start();
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
				// send to all subscribers...
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
