namespace AMO.GUI.Desktop
{
	partial class ID3Edit
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.txtArtist = new System.Windows.Forms.TextBox();
			this.txtAlbum = new System.Windows.Forms.TextBox();
			this.txtGenre = new System.Windows.Forms.TextBox();
			this.txtTrack = new System.Windows.Forms.TextBox();
			this.txtYear = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Artist";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 134);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Album";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(27, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Title";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 190);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(36, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Genre";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(426, 134);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Year";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(426, 76);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(45, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Track #";
			// 
			// txtTitle
			// 
			this.txtTitle.Location = new System.Drawing.Point(12, 31);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(531, 20);
			this.txtTitle.TabIndex = 6;
			// 
			// txtArtist
			// 
			this.txtArtist.Location = new System.Drawing.Point(12, 92);
			this.txtArtist.Name = "txtArtist";
			this.txtArtist.Size = new System.Drawing.Size(402, 20);
			this.txtArtist.TabIndex = 7;
			// 
			// txtAlbum
			// 
			this.txtAlbum.Location = new System.Drawing.Point(12, 150);
			this.txtAlbum.Name = "txtAlbum";
			this.txtAlbum.Size = new System.Drawing.Size(402, 20);
			this.txtAlbum.TabIndex = 8;
			// 
			// txtGenre
			// 
			this.txtGenre.Location = new System.Drawing.Point(12, 206);
			this.txtGenre.Name = "txtGenre";
			this.txtGenre.Size = new System.Drawing.Size(402, 20);
			this.txtGenre.TabIndex = 9;
			// 
			// txtTrack
			// 
			this.txtTrack.Location = new System.Drawing.Point(429, 92);
			this.txtTrack.Name = "txtTrack";
			this.txtTrack.Size = new System.Drawing.Size(114, 20);
			this.txtTrack.TabIndex = 10;
			// 
			// txtYear
			// 
			this.txtYear.Location = new System.Drawing.Point(429, 149);
			this.txtYear.Name = "txtYear";
			this.txtYear.Size = new System.Drawing.Size(114, 20);
			this.txtYear.TabIndex = 11;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(429, 203);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(114, 23);
			this.btnSave.TabIndex = 12;
			this.btnSave.Text = "ok";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// ID3Edit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 251);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtYear);
			this.Controls.Add(this.txtTrack);
			this.Controls.Add(this.txtGenre);
			this.Controls.Add(this.txtAlbum);
			this.Controls.Add(this.txtArtist);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ID3Edit";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "ID3Edit";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.TextBox txtArtist;
		private System.Windows.Forms.TextBox txtAlbum;
		private System.Windows.Forms.TextBox txtGenre;
		private System.Windows.Forms.TextBox txtTrack;
		private System.Windows.Forms.TextBox txtYear;
		private System.Windows.Forms.Button btnSave;
	}
}