namespace Band.EventsMonitor
{
	partial class PublisherForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublisherForm));
			this.txtTopic = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtHost = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSend = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnNewConsumer = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtTopic
			// 
			this.txtTopic.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.txtTopic.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			this.txtTopic.Location = new System.Drawing.Point(96, 38);
			this.txtTopic.Name = "txtTopic";
			this.txtTopic.Size = new System.Drawing.Size(287, 20);
			this.txtTopic.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(51, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Topic";
			// 
			// txtMessage
			// 
			this.txtMessage.Location = new System.Drawing.Point(96, 64);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(287, 97);
			this.txtMessage.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(40, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Message";
			// 
			// txtHost
			// 
			this.txtHost.Location = new System.Drawing.Point(96, 12);
			this.txtHost.Name = "txtHost";
			this.txtHost.Size = new System.Drawing.Size(287, 20);
			this.txtHost.TabIndex = 1;
			this.txtHost.Text = "localhost";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(61, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Host";
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(308, 167);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 4;
			this.btnSend.Text = "send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(12, 219);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(371, 159);
			this.txtLog.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 203);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Log";
			// 
			// btnNewConsumer
			// 
			this.btnNewConsumer.Location = new System.Drawing.Point(280, 384);
			this.btnNewConsumer.Name = "btnNewConsumer";
			this.btnNewConsumer.Size = new System.Drawing.Size(103, 23);
			this.btnNewConsumer.TabIndex = 6;
			this.btnNewConsumer.Text = "new consumer";
			this.btnNewConsumer.UseVisualStyleBackColor = true;
			this.btnNewConsumer.Click += new System.EventHandler(this.btnNewConsumer_Click);
			// 
			// PublisherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 413);
			this.Controls.Add(this.btnNewConsumer);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtHost);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtTopic);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "PublisherForm";
			this.Text = "Publisher";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtTopic;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtHost;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnNewConsumer;
	}
}

