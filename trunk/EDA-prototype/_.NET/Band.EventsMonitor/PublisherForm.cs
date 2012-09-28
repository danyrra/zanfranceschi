using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyNetQ;

namespace Band.EventsMonitor
{
	public partial class PublisherForm : Form
	{
		public PublisherForm()
		{
			InitializeComponent();
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			using (var bus = RabbitHutch.CreateBus(string.Format("host={0}", txtHost.Text)))
			{
				var publishChannel = bus.OpenPublishChannel();
				publishChannel.Publish(txtTopic.Text, txtMessage.Text);
				txtMessage.Text = string.Empty;
				txtLog.Text += string.Format("{0} - message sent to {1}@{2}{3}", DateTime.Now, txtTopic.Text, txtHost.Text, Environment.NewLine);
			}
		}

		private void btnNewConsumer_Click(object sender, EventArgs e)
		{
			new ConsumerForm().Show();
		}
	}
}
