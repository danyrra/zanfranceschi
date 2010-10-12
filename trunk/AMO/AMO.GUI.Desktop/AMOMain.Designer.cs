namespace AMO.GUI.Desktop
{
	partial class AMO
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
			this.components = new System.ComponentModel.Container();
			this.cmsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.grid = new System.Windows.Forms.DataGridView();
			this.FileInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Genre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Track = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtTrack = new System.Windows.Forms.MaskedTextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.ckbYear = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtYear = new System.Windows.Forms.MaskedTextBox();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.ckbTitle = new System.Windows.Forms.CheckBox();
			this.ddlAlbum = new System.Windows.Forms.ComboBox();
			this.ckbAlbum = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ddlArtist = new System.Windows.Forms.ComboBox();
			this.ckbArtist = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ckbGenre = new System.Windows.Forms.CheckBox();
			this.ddlGenre = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.fbdMediaDirectory = new System.Windows.Forms.FolderBrowserDialog();
			this.btnPickDir = new System.Windows.Forms.Button();
			this.lblDirectory = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.cmsGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsGrid
			// 
			this.cmsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
			this.cmsGrid.Name = "cmsGrid";
			this.cmsGrid.Size = new System.Drawing.Size(193, 76);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.editToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.editToolStripMenuItem.Text = "View Metadata";
			this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// grid
			// 
			this.grid.AllowUserToAddRows = false;
			this.grid.AllowUserToDeleteRows = false;
			this.grid.AllowUserToOrderColumns = true;
			this.grid.AllowUserToResizeRows = false;
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileInfo,
            this.Genre,
            this.Artist,
            this.Album,
            this.Year,
            this.Title,
            this.Track});
			this.grid.ContextMenuStrip = this.cmsGrid;
			this.grid.Location = new System.Drawing.Point(3, 3);
			this.grid.Name = "grid";
			this.grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.grid.Size = new System.Drawing.Size(1025, 244);
			this.grid.TabIndex = 0;
			this.grid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
			// 
			// FileInfo
			// 
			this.FileInfo.DataPropertyName = "FileInfo";
			this.FileInfo.HeaderText = "File";
			this.FileInfo.Name = "FileInfo";
			this.FileInfo.ReadOnly = true;
			// 
			// Genre
			// 
			this.Genre.DataPropertyName = "Genre";
			this.Genre.HeaderText = "Genre";
			this.Genre.Name = "Genre";
			// 
			// Artist
			// 
			this.Artist.DataPropertyName = "Artist";
			this.Artist.HeaderText = "Artist";
			this.Artist.Name = "Artist";
			// 
			// Album
			// 
			this.Album.DataPropertyName = "Album";
			this.Album.HeaderText = "Album";
			this.Album.Name = "Album";
			// 
			// Year
			// 
			this.Year.HeaderText = "Year";
			this.Year.Name = "Year";
			// 
			// Title
			// 
			this.Title.DataPropertyName = "Title";
			this.Title.HeaderText = "Title";
			this.Title.Name = "Title";
			// 
			// Track
			// 
			this.Track.DataPropertyName = "Track";
			this.Track.HeaderText = "Track #";
			this.Track.Name = "Track";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(3, 42);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.grid);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnSave);
			this.splitContainer1.Panel2.Controls.Add(this.txtTrack);
			this.splitContainer1.Panel2.Controls.Add(this.label6);
			this.splitContainer1.Panel2.Controls.Add(this.ckbYear);
			this.splitContainer1.Panel2.Controls.Add(this.label5);
			this.splitContainer1.Panel2.Controls.Add(this.txtYear);
			this.splitContainer1.Panel2.Controls.Add(this.txtTitle);
			this.splitContainer1.Panel2.Controls.Add(this.label4);
			this.splitContainer1.Panel2.Controls.Add(this.ckbTitle);
			this.splitContainer1.Panel2.Controls.Add(this.ddlAlbum);
			this.splitContainer1.Panel2.Controls.Add(this.ckbAlbum);
			this.splitContainer1.Panel2.Controls.Add(this.label3);
			this.splitContainer1.Panel2.Controls.Add(this.ddlArtist);
			this.splitContainer1.Panel2.Controls.Add(this.ckbArtist);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.ckbGenre);
			this.splitContainer1.Panel2.Controls.Add(this.ddlGenre);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Size = new System.Drawing.Size(1031, 412);
			this.splitContainer1.SplitterDistance = 250;
			this.splitContainer1.TabIndex = 1;
			// 
			// txtTrack
			// 
			this.txtTrack.Location = new System.Drawing.Point(578, 111);
			this.txtTrack.Mask = "999";
			this.txtTrack.Name = "txtTrack";
			this.txtTrack.Size = new System.Drawing.Size(100, 20);
			this.txtTrack.TabIndex = 17;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(575, 94);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "Track Number";
			// 
			// ckbYear
			// 
			this.ckbYear.AutoSize = true;
			this.ckbYear.Location = new System.Drawing.Point(353, 91);
			this.ckbYear.Name = "ckbYear";
			this.ckbYear.Size = new System.Drawing.Size(15, 14);
			this.ckbYear.TabIndex = 15;
			this.ckbYear.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(374, 91);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Year";
			// 
			// txtYear
			// 
			this.txtYear.Location = new System.Drawing.Point(353, 111);
			this.txtYear.Mask = "0000";
			this.txtYear.Name = "txtYear";
			this.txtYear.Size = new System.Drawing.Size(69, 20);
			this.txtYear.TabIndex = 13;
			// 
			// txtTitle
			// 
			this.txtTitle.Location = new System.Drawing.Point(19, 112);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(281, 20);
			this.txtTitle.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(37, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(27, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Title";
			// 
			// ckbTitle
			// 
			this.ckbTitle.AutoSize = true;
			this.ckbTitle.Location = new System.Drawing.Point(19, 96);
			this.ckbTitle.Name = "ckbTitle";
			this.ckbTitle.Size = new System.Drawing.Size(26, 17);
			this.ckbTitle.TabIndex = 10;
			this.ckbTitle.Text = "\r\n";
			this.ckbTitle.UseVisualStyleBackColor = true;
			// 
			// ddlAlbum
			// 
			this.ddlAlbum.FormattingEnabled = true;
			this.ddlAlbum.Location = new System.Drawing.Point(586, 30);
			this.ddlAlbum.Name = "ddlAlbum";
			this.ddlAlbum.Size = new System.Drawing.Size(257, 21);
			this.ddlAlbum.TabIndex = 9;
			// 
			// ckbAlbum
			// 
			this.ckbAlbum.AutoSize = true;
			this.ckbAlbum.Location = new System.Drawing.Point(586, 15);
			this.ckbAlbum.Name = "ckbAlbum";
			this.ckbAlbum.Size = new System.Drawing.Size(15, 14);
			this.ckbAlbum.TabIndex = 8;
			this.ckbAlbum.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(607, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Album";
			// 
			// ddlArtist
			// 
			this.ddlArtist.FormattingEnabled = true;
			this.ddlArtist.Location = new System.Drawing.Point(304, 30);
			this.ddlArtist.Name = "ddlArtist";
			this.ddlArtist.Size = new System.Drawing.Size(257, 21);
			this.ddlArtist.TabIndex = 6;
			// 
			// ckbArtist
			// 
			this.ckbArtist.AutoSize = true;
			this.ckbArtist.Location = new System.Drawing.Point(304, 14);
			this.ckbArtist.Name = "ckbArtist";
			this.ckbArtist.Size = new System.Drawing.Size(15, 14);
			this.ckbArtist.TabIndex = 5;
			this.ckbArtist.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(37, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Genre";
			// 
			// ckbGenre
			// 
			this.ckbGenre.AutoSize = true;
			this.ckbGenre.Location = new System.Drawing.Point(19, 14);
			this.ckbGenre.Name = "ckbGenre";
			this.ckbGenre.Size = new System.Drawing.Size(15, 14);
			this.ckbGenre.TabIndex = 1;
			this.ckbGenre.UseVisualStyleBackColor = true;
			// 
			// ddlGenre
			// 
			this.ddlGenre.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ddlGenre.FormattingEnabled = true;
			this.ddlGenre.Location = new System.Drawing.Point(19, 30);
			this.ddlGenre.Name = "ddlGenre";
			this.ddlGenre.Size = new System.Drawing.Size(256, 21);
			this.ddlGenre.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(322, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Artist";
			// 
			// btnPickDir
			// 
			this.btnPickDir.Location = new System.Drawing.Point(3, 12);
			this.btnPickDir.Name = "btnPickDir";
			this.btnPickDir.Size = new System.Drawing.Size(109, 23);
			this.btnPickDir.TabIndex = 2;
			this.btnPickDir.Text = "Choose Directory";
			this.btnPickDir.UseVisualStyleBackColor = true;
			this.btnPickDir.Click += new System.EventHandler(this.btnPickDir_Click);
			// 
			// lblDirectory
			// 
			this.lblDirectory.AutoSize = true;
			this.lblDirectory.Location = new System.Drawing.Point(118, 17);
			this.lblDirectory.Name = "lblDirectory";
			this.lblDirectory.Size = new System.Drawing.Size(0, 13);
			this.lblDirectory.TabIndex = 3;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(720, 109);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 18;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// AMO
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1046, 466);
			this.Controls.Add(this.lblDirectory);
			this.Controls.Add(this.btnPickDir);
			this.Controls.Add(this.splitContainer1);
			this.Name = "AMO";
			this.Text = "AMO";
			this.cmsGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmsGrid;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.DataGridView grid;
		private System.Windows.Forms.DataGridViewTextBoxColumn FileInfo;
		private System.Windows.Forms.DataGridViewTextBoxColumn Genre;
		private System.Windows.Forms.DataGridViewTextBoxColumn Artist;
		private System.Windows.Forms.DataGridViewTextBoxColumn Album;
		private System.Windows.Forms.DataGridViewTextBoxColumn Year;
		private System.Windows.Forms.DataGridViewTextBoxColumn Title;
		private System.Windows.Forms.DataGridViewTextBoxColumn Track;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.CheckBox ckbGenre;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox ddlGenre;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox ddlArtist;
		private System.Windows.Forms.CheckBox ckbArtist;
		private System.Windows.Forms.ComboBox ddlAlbum;
		private System.Windows.Forms.CheckBox ckbAlbum;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox ckbTitle;
		private System.Windows.Forms.MaskedTextBox txtYear;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox ckbYear;
		private System.Windows.Forms.MaskedTextBox txtTrack;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.FolderBrowserDialog fbdMediaDirectory;
		private System.Windows.Forms.Button btnPickDir;
		private System.Windows.Forms.Label lblDirectory;
		private System.Windows.Forms.Button btnSave;

	}
}

