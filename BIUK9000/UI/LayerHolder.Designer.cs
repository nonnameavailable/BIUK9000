namespace BIUK9000.UI
{
    partial class LayerHolder
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
            layerImagePB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)layerImagePB).BeginInit();
            SuspendLayout();
            // 
            // layerImagePB
            // 
            layerImagePB.Dock = System.Windows.Forms.DockStyle.Fill;
            layerImagePB.Location = new System.Drawing.Point(0, 0);
            layerImagePB.Name = "layerImagePB";
            layerImagePB.Size = new System.Drawing.Size(121, 56);
            layerImagePB.TabIndex = 0;
            layerImagePB.TabStop = false;
            // 
            // LayerHolder
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layerImagePB);
            Name = "LayerHolder";
            Size = new System.Drawing.Size(121, 56);
            ((System.ComponentModel.ISupportInitialize)layerImagePB).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox layerImagePB;
    }
}
