namespace EIP.ServicesRegistry.Admin
{
	partial class EventRegistryForm
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
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtTechincalDetails = new System.Windows.Forms.TextBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtVersion = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ddlTypes = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ddlProtocol = new System.Windows.Forms.ComboBox();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.gridEvents = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.txtTerm = new System.Windows.Forms.TextBox();
			this.btnNew = new System.Windows.Forms.Button();
			this.groupEventRegistry = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnDel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridEvents)).BeginInit();
			this.groupEventRegistry.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(69, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(250, 20);
			this.txtName.TabIndex = 1;
			// 
			// txtTechincalDetails
			// 
			this.txtTechincalDetails.Location = new System.Drawing.Point(483, 30);
			this.txtTechincalDetails.Multiline = true;
			this.txtTechincalDetails.Name = "txtTechincalDetails";
			this.txtTechincalDetails.Size = new System.Drawing.Size(250, 115);
			this.txtTechincalDetails.TabIndex = 2;
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(69, 30);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(250, 115);
			this.txtDescription.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 27);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Description";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(28, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Name";
			// 
			// txtVersion
			// 
			this.txtVersion.Location = new System.Drawing.Point(483, 178);
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.Size = new System.Drawing.Size(250, 20);
			this.txtVersion.TabIndex = 11;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(332, 175);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(145, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "Canonical Data Type Version";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(370, 148);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(107, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Canonical Data Type";
			// 
			// ddlTypes
			// 
			this.ddlTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlTypes.FormattingEnabled = true;
			this.ddlTypes.Location = new System.Drawing.Point(483, 151);
			this.ddlTypes.Name = "ddlTypes";
			this.ddlTypes.Size = new System.Drawing.Size(250, 21);
			this.ddlTypes.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(388, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Techincal Details";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 148);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Address";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(431, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Protocol";
			// 
			// ddlProtocol
			// 
			this.ddlProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlProtocol.FormattingEnabled = true;
			this.ddlProtocol.Items.AddRange(new object[] {
            "msmq",
            "rabbitmq"});
			this.ddlProtocol.Location = new System.Drawing.Point(483, 3);
			this.ddlProtocol.Name = "ddlProtocol";
			this.ddlProtocol.Size = new System.Drawing.Size(250, 21);
			this.ddlProtocol.TabIndex = 4;
			// 
			// txtAddress
			// 
			this.txtAddress.Location = new System.Drawing.Point(69, 151);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(250, 20);
			this.txtAddress.TabIndex = 3;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(605, 235);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(98, 27);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// gridEvents
			// 
			this.gridEvents.AllowUserToAddRows = false;
			this.gridEvents.AllowUserToOrderColumns = true;
			this.gridEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridEvents.Location = new System.Drawing.Point(12, 45);
			this.gridEvents.Name = "gridEvents";
			this.gridEvents.ReadOnly = true;
			this.gridEvents.Size = new System.Drawing.Size(739, 258);
			this.gridEvents.TabIndex = 7;
			this.gridEvents.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridEvents_CellClick);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(374, 15);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(81, 27);
			this.button1.TabIndex = 8;
			this.button1.Text = "Search";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtTerm
			// 
			this.txtTerm.Location = new System.Drawing.Point(12, 19);
			this.txtTerm.Name = "txtTerm";
			this.txtTerm.Size = new System.Drawing.Size(356, 20);
			this.txtTerm.TabIndex = 9;
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(501, 235);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(98, 27);
			this.btnNew.TabIndex = 10;
			this.btnNew.Text = "New";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// groupEventRegistry
			// 
			this.groupEventRegistry.Controls.Add(this.btnDel);
			this.groupEventRegistry.Controls.Add(this.tableLayoutPanel1);
			this.groupEventRegistry.Controls.Add(this.btnNew);
			this.groupEventRegistry.Controls.Add(this.btnSave);
			this.groupEventRegistry.Location = new System.Drawing.Point(12, 12);
			this.groupEventRegistry.Name = "groupEventRegistry";
			this.groupEventRegistry.Size = new System.Drawing.Size(764, 270);
			this.groupEventRegistry.TabIndex = 11;
			this.groupEventRegistry.TabStop = false;
			this.groupEventRegistry.Text = "Event Registry";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 266F));
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtDescription, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtAddress, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtTechincalDetails, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtVersion, 3, 5);
			this.tableLayoutPanel1.Controls.Add(this.label7, 2, 5);
			this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.ddlProtocol, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.ddlTypes, 3, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(746, 210);
			this.tableLayoutPanel1.TabIndex = 12;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtTerm);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.gridEvents);
			this.groupBox1.Location = new System.Drawing.Point(12, 288);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(764, 309);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Search";
			// 
			// btnDel
			// 
			this.btnDel.Location = new System.Drawing.Point(709, 235);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(42, 27);
			this.btnDel.TabIndex = 13;
			this.btnDel.Text = "X";
			this.btnDel.UseVisualStyleBackColor = true;
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// EventRegistryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(787, 604);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupEventRegistry);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "EventRegistryForm";
			this.Text = "Event Registry";
			((System.ComponentModel.ISupportInitialize)(this.gridEvents)).EndInit();
			this.groupEventRegistry.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TextBox txtTechincalDetails;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.ComboBox ddlProtocol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox ddlTypes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtVersion;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.DataGridView gridEvents;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtTerm;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.GroupBox groupEventRegistry;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnDel;
	}
}