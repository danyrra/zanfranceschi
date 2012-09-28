namespace Band.EventsMonitor
{
	partial class ConsumerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsumerForm));
			this.txtMessages = new System.Windows.Forms.TextBox();
			this.txtHost = new System.Windows.Forms.TextBox();
			this.txtTopic = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnToggle = new System.Windows.Forms.Button();
			this.imgStatus = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.imgStatus)).BeginInit();
			this.SuspendLayout();
			// 
			// txtMessages
			// 
			this.txtMessages.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMessages.Location = new System.Drawing.Point(12, 98);
			this.txtMessages.Multiline = true;
			this.txtMessages.Name = "txtMessages";
			this.txtMessages.ReadOnly = true;
			this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMessages.Size = new System.Drawing.Size(403, 152);
			this.txtMessages.TabIndex = 0;
			// 
			// txtHost
			// 
			this.txtHost.Location = new System.Drawing.Point(90, 12);
			this.txtHost.Name = "txtHost";
			this.txtHost.Size = new System.Drawing.Size(182, 20);
			this.txtHost.TabIndex = 1;
			this.txtHost.Text = "localhost";
			// 
			// txtTopic
			// 
			this.txtTopic.Location = new System.Drawing.Point(90, 38);
			this.txtTopic.Name = "txtTopic";
			this.txtTopic.Size = new System.Drawing.Size(182, 20);
			this.txtTopic.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Host";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Topic";
			// 
			// btnToggle
			// 
			this.btnToggle.Location = new System.Drawing.Point(196, 65);
			this.btnToggle.Name = "btnToggle";
			this.btnToggle.Size = new System.Drawing.Size(75, 23);
			this.btnToggle.TabIndex = 5;
			this.btnToggle.Text = "start";
			this.btnToggle.UseVisualStyleBackColor = true;
			this.btnToggle.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// imgStatus
			// 
			this.imgStatus.Image = global::Band.EventsMonitor.Properties.Resources.disconnected;
			this.imgStatus.Location = new System.Drawing.Point(318, 18);
			this.imgStatus.Name = "imgStatus";
			this.imgStatus.Size = new System.Drawing.Size(61, 70);
			this.imgStatus.TabIndex = 6;
			this.imgStatus.TabStop = false;
			// 
			// ConsumerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(427, 262);
			this.Controls.Add(this.imgStatus);
			this.Controls.Add(this.btnToggle);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtTopic);
			this.Controls.Add(this.txtHost);
			this.Controls.Add(this.txtMessages);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConsumerForm";
			this.Text = "ConsumerForm";
			((System.ComponentModel.ISupportInitialize)(this.imgStatus)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtMessages;
		private System.Windows.Forms.TextBox txtHost;
		private System.Windows.Forms.TextBox txtTopic;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnToggle;
		private System.Windows.Forms.PictureBox imgStatus;
	}
}