namespace Generator
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
            btnEncode = new Button();
            txtBoxOutput = new TextBox();
            lblOutput = new Label();
            SuspendLayout();
            // 
            // cmbSelect
            // 
            cmbSelect.FormattingEnabled = true;
            cmbSelect.Location = new Point(12, 38);
            cmbSelect.Name = "cmbSelect";
            cmbSelect.Size = new Size(265, 23);
            cmbSelect.TabIndex = 0;
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
            // btnEncode
            // 
            btnEncode.BackColor = Color.White;
            btnEncode.Location = new Point(12, 303);
            btnEncode.Name = "btnEncode";
            btnEncode.Size = new Size(75, 23);
            btnEncode.TabIndex = 2;
            btnEncode.Text = "Luo";
            btnEncode.UseVisualStyleBackColor = false;
            // 
            // txtBoxOutput
            // 
            txtBoxOutput.Location = new Point(12, 359);
            txtBoxOutput.Name = "txtBoxOutput";
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
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(800, 450);
            Controls.Add(lblOutput);
            Controls.Add(txtBoxOutput);
            Controls.Add(btnEncode);
            Controls.Add(lblSelect);
            Controls.Add(cmbSelect);
            Name = "Menu";
            Text = "Main Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbSelect;
        private Label lblSelect;
        private Button btnEncode;
        private TextBox txtBoxOutput;
        private Label lblOutput;
    }
}