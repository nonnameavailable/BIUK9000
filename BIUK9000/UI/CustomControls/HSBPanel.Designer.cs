namespace BIUK9000.UI.CustomControls
{
    partial class HSBPanel
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
            components = new System.ComponentModel.Container();
            saturationTrackBar = new System.Windows.Forms.TrackBar();
            brightnessTrackBar = new System.Windows.Forms.TrackBar();
            transparencyTrackBar = new System.Windows.Forms.TrackBar();
            toolTip = new System.Windows.Forms.ToolTip(components);
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            saturationNUD = new System.Windows.Forms.NumericUpDown();
            brightnessNUD = new System.Windows.Forms.NumericUpDown();
            transparencyNUD = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)saturationTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)brightnessTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)saturationNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)brightnessNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)transparencyNUD).BeginInit();
            SuspendLayout();
            // 
            // saturationTrackBar
            // 
            saturationTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            saturationTrackBar.Location = new System.Drawing.Point(25, 6);
            saturationTrackBar.Maximum = 200;
            saturationTrackBar.Name = "saturationTrackBar";
            saturationTrackBar.Size = new System.Drawing.Size(126, 45);
            saturationTrackBar.TabIndex = 0;
            saturationTrackBar.TickFrequency = 10;
            toolTip.SetToolTip(saturationTrackBar, "Saturation");
            saturationTrackBar.Value = 100;
            // 
            // brightnessTrackBar
            // 
            brightnessTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            brightnessTrackBar.Location = new System.Drawing.Point(25, 57);
            brightnessTrackBar.Maximum = 200;
            brightnessTrackBar.Name = "brightnessTrackBar";
            brightnessTrackBar.Size = new System.Drawing.Size(126, 45);
            brightnessTrackBar.TabIndex = 1;
            brightnessTrackBar.TickFrequency = 10;
            toolTip.SetToolTip(brightnessTrackBar, "Brightness");
            brightnessTrackBar.Value = 100;
            // 
            // transparencyTrackBar
            // 
            transparencyTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            transparencyTrackBar.Location = new System.Drawing.Point(25, 108);
            transparencyTrackBar.Maximum = 200;
            transparencyTrackBar.Name = "transparencyTrackBar";
            transparencyTrackBar.Size = new System.Drawing.Size(126, 45);
            transparencyTrackBar.TabIndex = 2;
            transparencyTrackBar.TickFrequency = 10;
            toolTip.SetToolTip(transparencyTrackBar, "Transparency");
            transparencyTrackBar.Value = 200;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 6);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(22, 15);
            label1.TabIndex = 3;
            label1.Text = "sat";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 57);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(21, 15);
            label2.TabIndex = 4;
            label2.Text = "bri";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 108);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(21, 15);
            label3.TabIndex = 5;
            label3.Text = "tra";
            // 
            // saturationNUD
            // 
            saturationNUD.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            saturationNUD.DecimalPlaces = 2;
            saturationNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            saturationNUD.Location = new System.Drawing.Point(160, 6);
            saturationNUD.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            saturationNUD.Name = "saturationNUD";
            saturationNUD.Size = new System.Drawing.Size(49, 23);
            saturationNUD.TabIndex = 6;
            // 
            // brightnessNUD
            // 
            brightnessNUD.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            brightnessNUD.DecimalPlaces = 2;
            brightnessNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            brightnessNUD.Location = new System.Drawing.Point(160, 55);
            brightnessNUD.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            brightnessNUD.Name = "brightnessNUD";
            brightnessNUD.Size = new System.Drawing.Size(49, 23);
            brightnessNUD.TabIndex = 7;
            // 
            // transparencyNUD
            // 
            transparencyNUD.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            transparencyNUD.DecimalPlaces = 2;
            transparencyNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            transparencyNUD.Location = new System.Drawing.Point(160, 106);
            transparencyNUD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            transparencyNUD.Name = "transparencyNUD";
            transparencyNUD.Size = new System.Drawing.Size(49, 23);
            transparencyNUD.TabIndex = 8;
            // 
            // HSBPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(transparencyNUD);
            Controls.Add(brightnessNUD);
            Controls.Add(saturationNUD);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(transparencyTrackBar);
            Controls.Add(brightnessTrackBar);
            Controls.Add(saturationTrackBar);
            Name = "HSBPanel";
            Size = new System.Drawing.Size(213, 146);
            ((System.ComponentModel.ISupportInitialize)saturationTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)brightnessTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)saturationNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)brightnessNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)transparencyNUD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar saturationTrackBar;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.TrackBar transparencyTrackBar;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown saturationNUD;
        private System.Windows.Forms.NumericUpDown brightnessNUD;
        private System.Windows.Forms.NumericUpDown transparencyNUD;
    }
}
