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
            ((System.ComponentModel.ISupportInitialize)saturationTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)brightnessTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).BeginInit();
            SuspendLayout();
            // 
            // saturationTrackBar
            // 
            saturationTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            saturationTrackBar.Location = new System.Drawing.Point(3, 3);
            saturationTrackBar.Maximum = 200;
            saturationTrackBar.Name = "saturationTrackBar";
            saturationTrackBar.Size = new System.Drawing.Size(144, 45);
            saturationTrackBar.TabIndex = 0;
            saturationTrackBar.TickFrequency = 10;
            toolTip.SetToolTip(saturationTrackBar, "Saturation");
            saturationTrackBar.Value = 100;
            // 
            // brightnessTrackBar
            // 
            brightnessTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            brightnessTrackBar.Location = new System.Drawing.Point(3, 42);
            brightnessTrackBar.Maximum = 200;
            brightnessTrackBar.Name = "brightnessTrackBar";
            brightnessTrackBar.Size = new System.Drawing.Size(144, 45);
            brightnessTrackBar.TabIndex = 1;
            brightnessTrackBar.TickFrequency = 10;
            toolTip.SetToolTip(brightnessTrackBar, "Brightness");
            brightnessTrackBar.Value = 100;
            // 
            // transparencyTrackBar
            // 
            transparencyTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            transparencyTrackBar.Location = new System.Drawing.Point(3, 82);
            transparencyTrackBar.Maximum = 200;
            transparencyTrackBar.Name = "transparencyTrackBar";
            transparencyTrackBar.Size = new System.Drawing.Size(144, 45);
            transparencyTrackBar.TabIndex = 2;
            transparencyTrackBar.TickFrequency = 10;
            toolTip.SetToolTip(transparencyTrackBar, "Transparency");
            transparencyTrackBar.Value = 200;
            // 
            // HSBPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(transparencyTrackBar);
            Controls.Add(brightnessTrackBar);
            Controls.Add(saturationTrackBar);
            Name = "HSBPanel";
            Size = new System.Drawing.Size(150, 123);
            ((System.ComponentModel.ISupportInitialize)saturationTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)brightnessTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar saturationTrackBar;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.TrackBar transparencyTrackBar;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
