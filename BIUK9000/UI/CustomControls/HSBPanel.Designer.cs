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
            saturationTrackBar = new System.Windows.Forms.TrackBar();
            brightnessTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)saturationTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)brightnessTrackBar).BeginInit();
            SuspendLayout();
            // 
            // saturationTrackBar
            // 
            saturationTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            saturationTrackBar.Location = new System.Drawing.Point(3, 4);
            saturationTrackBar.Maximum = 200;
            saturationTrackBar.Name = "saturationTrackBar";
            saturationTrackBar.Size = new System.Drawing.Size(144, 30);
            saturationTrackBar.TabIndex = 0;
            saturationTrackBar.TickFrequency = 10;
            saturationTrackBar.Value = 100;
            // 
            // brightnessTrackBar
            // 
            brightnessTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            brightnessTrackBar.Location = new System.Drawing.Point(3, 40);
            brightnessTrackBar.Maximum = 200;
            brightnessTrackBar.Name = "brightnessTrackBar";
            brightnessTrackBar.Size = new System.Drawing.Size(144, 30);
            brightnessTrackBar.TabIndex = 1;
            brightnessTrackBar.TickFrequency = 10;
            brightnessTrackBar.Value = 100;
            // 
            // HueSatPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(brightnessTrackBar);
            Controls.Add(saturationTrackBar);
            Name = "HueSatPanel";
            Size = new System.Drawing.Size(150, 95);
            ((System.ComponentModel.ISupportInitialize)saturationTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)brightnessTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar saturationTrackBar;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
    }
}
