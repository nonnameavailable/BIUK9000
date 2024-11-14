namespace BIUK9000.UI.CustomControls
{
    partial class PaintControl
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
            paintColorButton = new ColorButton();
            transparencyTrackBar = new System.Windows.Forms.TrackBar();
            thicknessNUD = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thicknessNUD).BeginInit();
            SuspendLayout();
            // 
            // paintColorButton
            // 
            paintColorButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            paintColorButton.Color = System.Drawing.Color.Black;
            paintColorButton.Location = new System.Drawing.Point(3, 3);
            paintColorButton.Name = "paintColorButton";
            paintColorButton.Size = new System.Drawing.Size(37, 35);
            paintColorButton.TabIndex = 0;
            // 
            // transparencyTrackBar
            // 
            transparencyTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            transparencyTrackBar.Location = new System.Drawing.Point(3, 44);
            transparencyTrackBar.Maximum = 255;
            transparencyTrackBar.Name = "transparencyTrackBar";
            transparencyTrackBar.Size = new System.Drawing.Size(232, 45);
            transparencyTrackBar.TabIndex = 1;
            transparencyTrackBar.TickFrequency = 5;
            // 
            // thicknessNUD
            // 
            thicknessNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            thicknessNUD.Location = new System.Drawing.Point(46, 13);
            thicknessNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            thicknessNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            thicknessNUD.Name = "thicknessNUD";
            thicknessNUD.Size = new System.Drawing.Size(120, 25);
            thicknessNUD.TabIndex = 2;
            thicknessNUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // PaintControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(thicknessNUD);
            Controls.Add(transparencyTrackBar);
            Controls.Add(paintColorButton);
            Name = "PaintControl";
            Size = new System.Drawing.Size(238, 122);
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)thicknessNUD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ColorButton paintColorButton;
        private System.Windows.Forms.TrackBar transparencyTrackBar;
        private System.Windows.Forms.NumericUpDown thicknessNUD;
    }
}
