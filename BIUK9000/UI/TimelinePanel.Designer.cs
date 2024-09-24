namespace BIUK9000.UI
{
    partial class TimelinePanel
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
            timelineFLP = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // timelineFLP
            // 
            timelineFLP.AutoScroll = true;
            timelineFLP.Dock = System.Windows.Forms.DockStyle.Fill;
            timelineFLP.Location = new System.Drawing.Point(0, 0);
            timelineFLP.Name = "timelineFLP";
            timelineFLP.Size = new System.Drawing.Size(413, 150);
            timelineFLP.TabIndex = 0;
            timelineFLP.WrapContents = false;
            // 
            // TimelinePanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(timelineFLP);
            Name = "TimelinePanel";
            Size = new System.Drawing.Size(413, 150);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel timelineFLP;
    }
}
