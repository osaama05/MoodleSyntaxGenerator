namespace MoodleSyntaxGenerator
{
	partial class Menu
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			cmbSelect = new ComboBox();
			lblSelect = new Label();
			btnGenerate = new Button();
			txtBoxOutput = new TextBox();
			lblOutput = new Label();
			btnCopy = new Button();
			menuStrip1 = new MenuStrip();
			toolStripMenuItem1 = new ToolStripMenuItem();
			toolStripTextBox1 = new ToolStripTextBox();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// cmbSelect
			// 
			cmbSelect.FormattingEnabled = true;
			cmbSelect.Location = new Point(12, 57);
			cmbSelect.Name = "cmbSelect";
			cmbSelect.Size = new Size(265, 23);
			cmbSelect.TabIndex = 0;
			cmbSelect.SelectedIndexChanged += CmbSelect_SelectedIndexChanged;
			// 
			// lblSelect
			// 
			lblSelect.AutoSize = true;
			lblSelect.Location = new Point(12, 39);
			lblSelect.Name = "lblSelect";
			lblSelect.Size = new Size(124, 15);
			lblSelect.TabIndex = 1;
			lblSelect.Text = "Valitse kysymysmuoto";
			// 
			// btnGenerate
			// 
			btnGenerate.BackColor = SystemColors.ControlLightLight;
			btnGenerate.Location = new Point(348, 86);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new Size(75, 23);
			btnGenerate.TabIndex = 2;
			btnGenerate.Text = "Luo";
			btnGenerate.UseVisualStyleBackColor = false;
			btnGenerate.Click += BtnGenerate_Click;
			// 
			// txtBoxOutput
			// 
			txtBoxOutput.Location = new Point(348, 57);
			txtBoxOutput.Multiline = true;
			txtBoxOutput.Name = "txtBoxOutput";
			txtBoxOutput.ReadOnly = true;
			txtBoxOutput.Size = new Size(376, 23);
			txtBoxOutput.TabIndex = 3;
			// 
			// lblOutput
			// 
			lblOutput.AutoSize = true;
			lblOutput.Location = new Point(348, 39);
			lblOutput.Name = "lblOutput";
			lblOutput.Size = new Size(51, 15);
			lblOutput.TabIndex = 4;
			lblOutput.Text = "Ulostulo";
			// 
			// btnCopy
			// 
			btnCopy.Location = new Point(730, 57);
			btnCopy.Name = "btnCopy";
			btnCopy.Size = new Size(57, 23);
			btnCopy.TabIndex = 5;
			btnCopy.Text = "Kopioi";
			btnCopy.UseVisualStyleBackColor = true;
			btnCopy.Click += BtnCopy_Click;
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(800, 24);
			menuStrip1.TabIndex = 6;
			menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripTextBox1 });
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new Size(40, 20);
			toolStripMenuItem1.Text = "Info";
			// 
			// toolStripTextBox1
			// 
			toolStripTextBox1.Name = "toolStripTextBox1";
			toolStripTextBox1.Size = new Size(100, 23);
			// 
			// Menu
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(212, 207, 201);
			ClientSize = new Size(800, 450);
			FormClosing += Menu_FormClosing;
			Controls.Add(btnCopy);
			Controls.Add(lblOutput);
			Controls.Add(txtBoxOutput);
			Controls.Add(btnGenerate);
			Controls.Add(lblSelect);
			Controls.Add(cmbSelect);
			Controls.Add(menuStrip1);
			Name = "Menu";
			Text = "Main Menu";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox cmbSelect;
		private Label lblSelect;
		private Button btnGenerate;
		private TextBox txtBoxOutput;
		private Label lblOutput;
		private Button btnCopy;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripTextBox toolStripTextBox1;
	}
}