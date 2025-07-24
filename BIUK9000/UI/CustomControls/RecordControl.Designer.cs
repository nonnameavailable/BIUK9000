namespace BIUK9000.UI.CustomControls
{
    partial class RecordControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startRecBtn = new System.Windows.Forms.Button();
            stopRecBtn = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            fpsNUD = new System.Windows.Forms.NumericUpDown();
            screenshotBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)fpsNUD).BeginInit();
            SuspendLayout();
            // 
            // startRecBtn
            // 
            startRecBtn.Location = new System.Drawing.Point(3, 3);
            startRecBtn.Name = "startRecBtn";
            startRecBtn.Size = new System.Drawing.Size(104, 40);
            startRecBtn.TabIndex = 0;
            startRecBtn.Text = "start";
            startRecBtn.UseVisualStyleBackColor = true;
            // 
            // stopRecBtn
            // 
            stopRecBtn.Location = new System.Drawing.Point(113, 3);
            stopRecBtn.Name = "stopRecBtn";
            stopRecBtn.Size = new System.Drawing.Size(104, 40);
            stopRecBtn.TabIndex = 1;
            stopRecBtn.Text = "stop";
            stopRecBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 51);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(23, 15);
            label1.TabIndex = 2;
            label1.Text = "fps";
            // 
            // fpsNUD
            // 
            fpsNUD.Location = new System.Drawing.Point(46, 49);
            fpsNUD.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            fpsNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            fpsNUD.Name = "fpsNUD";
            fpsNUD.Size = new System.Drawing.Size(120, 23);
            fpsNUD.TabIndex = 3;
            fpsNUD.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // screenshotBTN
            // 
            screenshotBTN.Location = new System.Drawing.Point(3, 82);
            screenshotBTN.Name = "screenshotBTN";
            screenshotBTN.Size = new System.Drawing.Size(104, 40);
            screenshotBTN.TabIndex = 4;
            screenshotBTN.Text = "Screenshot";
            screenshotBTN.UseVisualStyleBackColor = true;
            // 
            // RecordControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(screenshotBTN);
            Controls.Add(fpsNUD);
            Controls.Add(label1);
            Controls.Add(stopRecBtn);
            Controls.Add(startRecBtn);
            Name = "RecordControl";
            Size = new System.Drawing.Size(244, 125);
            ((System.ComponentModel.ISupportInitialize)fpsNUD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button startRecBtn;
        private System.Windows.Forms.Button stopRecBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown fpsNUD;
        private System.Windows.Forms.Button screenshotBTN;
    }
}
