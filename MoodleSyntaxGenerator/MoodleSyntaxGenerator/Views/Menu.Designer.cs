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
			components = new System.ComponentModel.Container();
			cmbSelect = new ComboBox();
			lblSelect = new Label();
			btnGenerate = new Button();
			txtBoxOutput = new TextBox();
			lblOutput = new Label();
			btnCopy = new Button();
			errorProvider1 = new ErrorProvider(components);
			((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
			SuspendLayout();
			// 
			// cmbSelect
			// 
			cmbSelect.FormattingEnabled = true;
			cmbSelect.Location = new Point(12, 38);
			cmbSelect.Name = "cmbSelect";
			cmbSelect.Size = new Size(265, 23);
			cmbSelect.TabIndex = 0;
			cmbSelect.SelectedIndexChanged += CmbSelect_SelectedIndexChanged;
			// 
			// lblSelect
			// 
			lblSelect.AutoSize = true;
			lblSelect.Location = new Point(12, 20);
			lblSelect.Name = "lblSelect";
			lblSelect.Size = new Size(124, 15);
			lblSelect.TabIndex = 1;
			lblSelect.Text = "Valitse kysymysmuoto";
			// 
			// btnGenerate
			// 
			btnGenerate.BackColor = SystemColors.ControlLightLight;
			btnGenerate.Location = new Point(12, 303);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new Size(75, 23);
			btnGenerate.TabIndex = 2;
			btnGenerate.Text = "Luo";
			btnGenerate.UseVisualStyleBackColor = false;
			btnGenerate.Click += BtnGenerate_Click;
			// 
			// txtBoxOutput
			// 
			txtBoxOutput.Location = new Point(12, 359);
			txtBoxOutput.Multiline = true;
			txtBoxOutput.Name = "txtBoxOutput";
			txtBoxOutput.ReadOnly = true;
			txtBoxOutput.Size = new Size(376, 23);
			txtBoxOutput.TabIndex = 3;
			// 
			// lblOutput
			// 
			lblOutput.AutoSize = true;
			lblOutput.Location = new Point(12, 341);
			lblOutput.Name = "lblOutput";
			lblOutput.Size = new Size(51, 15);
			lblOutput.TabIndex = 4;
			lblOutput.Text = "Ulostulo";
			// 
			// btnCopy
			// 
			btnCopy.Location = new Point(394, 359);
			btnCopy.Name = "btnCopy";
			btnCopy.Size = new Size(57, 23);
			btnCopy.TabIndex = 5;
			btnCopy.Text = "Copy";
			btnCopy.UseVisualStyleBackColor = true;
			btnCopy.Click += BtnCopy_Click;
			// 
			// errorProvider1
			// 
			errorProvider1.ContainerControl = this;
			// 
			// Menu
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(212, 207, 201);
			ClientSize = new Size(800, 450);
			Controls.Add(btnCopy);
			Controls.Add(lblOutput);
			Controls.Add(txtBoxOutput);
			Controls.Add(btnGenerate);
			Controls.Add(lblSelect);
			Controls.Add(cmbSelect);
			Name = "Menu";
			Text = "Main Menu";
			((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
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
		private ErrorProvider errorProvider1;
	}
}