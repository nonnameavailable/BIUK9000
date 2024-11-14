namespace BIUK9000.UI
{
    partial class LayersPanel
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
            layersFLP = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // layersFLP
            // 
            layersFLP.AllowDrop = true;
            layersFLP.AutoScroll = true;
            layersFLP.Dock = System.Windows.Forms.DockStyle.Fill;
            layersFLP.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            layersFLP.Location = new System.Drawing.Point(0, 0);
            layersFLP.Name = "layersFLP";
            layersFLP.Size = new System.Drawing.Size(150, 150);
            layersFLP.TabIndex = 0;
            layersFLP.WrapContents = false;
            // 
            // LayersPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layersFLP);
            DoubleBuffered = true;
            Name = "LayersPanel";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel layersFLP;
    }
}
