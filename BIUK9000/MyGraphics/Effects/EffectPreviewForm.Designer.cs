namespace BIUK9000.MyGraphics.Effects
{
    partial class EffectPreviewForm
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            effectPictureBox = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            originalPictureBox = new System.Windows.Forms.PictureBox();
            reportLabel = new System.Windows.Forms.Label();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)effectPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(effectPictureBox, 2, 0);
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(originalPictureBox, 0, 0);
            tableLayoutPanel1.Controls.Add(reportLabel, 0, 1);
            tableLayoutPanel1.Controls.Add(okButton, 0, 2);
            tableLayoutPanel1.Controls.Add(cancelButton, 2, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(658, 359);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // effectPictureBox
            // 
            effectPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            effectPictureBox.Location = new System.Drawing.Point(354, 3);
            effectPictureBox.Name = "effectPictureBox";
            effectPictureBox.Size = new System.Drawing.Size(301, 253);
            effectPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            effectPictureBox.TabIndex = 3;
            effectPictureBox.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
            label1.Location = new System.Drawing.Point(310, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(37, 259);
            label1.TabIndex = 1;
            label1.Text = "->";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // originalPictureBox
            // 
            originalPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            originalPictureBox.Location = new System.Drawing.Point(3, 3);
            originalPictureBox.Name = "originalPictureBox";
            originalPictureBox.Size = new System.Drawing.Size(300, 253);
            originalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            originalPictureBox.TabIndex = 2;
            originalPictureBox.TabStop = false;
            // 
            // reportLabel
            // 
            reportLabel.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(reportLabel, 3);
            reportLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            reportLabel.Location = new System.Drawing.Point(3, 259);
            reportLabel.Name = "reportLabel";
            reportLabel.Size = new System.Drawing.Size(652, 50);
            reportLabel.TabIndex = 4;
            reportLabel.Text = "label2";
            reportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // okButton
            // 
            okButton.Dock = System.Windows.Forms.DockStyle.Fill;
            okButton.Location = new System.Drawing.Point(3, 312);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(300, 44);
            okButton.TabIndex = 5;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            cancelButton.Location = new System.Drawing.Point(354, 312);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(301, 44);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // EffectPreviewForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(658, 359);
            Controls.Add(tableLayoutPanel1);
            Name = "EffectPreviewForm";
            Text = "EffectPreviewForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)effectPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox effectPictureBox;
        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.Label reportLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}