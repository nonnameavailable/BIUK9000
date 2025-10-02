namespace BIUK9000.UI.Forms
{
    partial class PromptForm
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
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            nud1Label = new System.Windows.Forms.Label();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            okButton = new System.Windows.Forms.Button();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(nud1Label);
            flowLayoutPanel1.Controls.Add(numericUpDown1);
            flowLayoutPanel1.Controls.Add(okButton);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(254, 255);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // nud1Label
            // 
            nud1Label.AutoSize = true;
            nud1Label.Location = new System.Drawing.Point(5, 5);
            nud1Label.Margin = new System.Windows.Forms.Padding(5);
            nud1Label.Name = "nud1Label";
            nud1Label.Size = new System.Drawing.Size(38, 15);
            nud1Label.TabIndex = 0;
            nud1Label.Text = "label1";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(5, 30);
            numericUpDown1.Margin = new System.Windows.Forms.Padding(5);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(112, 23);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // okButton
            // 
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Location = new System.Drawing.Point(5, 63);
            okButton.Margin = new System.Windows.Forms.Padding(5);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(112, 54);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // PromptForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            ClientSize = new System.Drawing.Size(254, 255);
            Controls.Add(flowLayoutPanel1);
            Name = "PromptForm";
            Text = "PromptForm";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label nud1Label;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}