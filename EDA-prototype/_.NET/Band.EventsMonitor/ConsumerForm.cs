using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyNetQ;
using System.Threading;

namespace Band.EventsMonitor
{
	public partial class ConsumerForm : Form
	{
		IBus bus = null;
		string subscriptionId = null;

		public ConsumerForm()
		{
			InitializeComponent();
			subscriptionId = Guid.NewGuid().ToString();
			this.Text = string.Format("Consumer ({0})", subscriptionId);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (btnToggle.Text.Equals("start"))
			{
				bus = RabbitHutch.CreateBus(string.Format("host={0}", txtHost.Text));
				bus.Subscribe<string>(subscriptionId, txtTopic.Text, HandleMessage);
				btnToggle.Text = "stop";
				imgStatus.Image = Band.EventsMonitor.Properties.Resources.connected;
			}
			else
			{
				btnToggle.Text = "start";
				imgStatus.Image = Band.EventsMonitor.Properties.Resources.disconnected;
				if (bus != null)
				{
					bus.Dispose();
				}
			}
		}

		private void HandleMessage(string message)
		{
			
			if (txtMessages.InvokeRequired)
			{
				txtMessages.Invoke(new MethodInvoker(delegate
				{
					txtMessages.Text += string.Format(
						"-------{3}{0} - {1}@{2}:{3}{4}{3}",
						DateTime.Now,
						txtTopic.Text,
						txtHost.Text,
						Environment.NewLine,
						message,
						Environment.NewLine);

					txtMessages.Select(txtMessages.Text.Length, 0);
					txtMessages.ScrollToCaret();
				}));
			}

			//for (int i = 0; i < 6; i++)
			//{
			//    imgStatus.Image = i % 2 == 0 ?
			//        Band.EventsMonitor.Properties.Resources.received :
			//        Band.EventsMonitor.Properties.Resources.connected;
			//    Thread.Sleep(50);
			//}
		}
	}
}