using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyNetQ;
using Band.Mensagens.Wf;
using System.Runtime.InteropServices;

namespace Band.Comum.NotificacaoDesktop
{
	public partial class Notificacoes
		: Form
	{

		#region Moveable window
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();
		void Notificacoes_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		} 
		#endregion

		bool clickedExitMenu = false;

		IBus bus;

		public Notificacoes()
		{
			InitializeComponent();
			bus = RabbitHutch.CreateBus("host=localhost");
			this.MouseDown += new MouseEventHandler(Notificacoes_MouseDown);
			bus.Subscribe<ColaboradorContratado>("xxx", HandleMessage);
		}

		private delegate void HandleMessageCallback(ColaboradorContratado obj);

		protected void HandleMessage(ColaboradorContratado obj)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new HandleMessageCallback(HandleMessage), obj);
			}
			else
			{
				this.notifyIcon.BalloonTipTitle = "Novo Colaborador Contratado";
				this.notifyIcon.BalloonTipText = obj.Nome;
				this.notifyIcon.Icon = new Icon("Messages.ico");
				this.notifyIcon.Visible = true;
				this.notifyIcon.ShowBalloonTip(3);
				this.notifyIcon.BalloonTipClicked += new EventHandler(notifyIcon_BalloonTipClicked);
			}
		}

		void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
		{
			this.Show();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = !clickedExitMenu;
			this.Hide();
		}

		private void toolStripSair_Click(object sender, EventArgs e)
		{
			clickedExitMenu = true;
			Close();
		}

		private void toolStripConfigs_Click(object sender, EventArgs e)
		{
			this.Show();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
			{
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			}
			else
			{
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			}
		}
	}
}
