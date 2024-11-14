namespace BIUK9000.UI.LayerParamControls
{
    partial class PlainGFLParamControl
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
            colorButton = new ColorButton();
            SuspendLayout();
            // 
            // colorButton
            // 
            colorButton.Color = System.Drawing.Color.Black;
            colorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            colorButton.Location = new System.Drawing.Point(0, 0);
            colorButton.Name = "colorButton";
            colorButton.Size = new System.Drawing.Size(150, 150);
            colorButton.TabIndex = 0;
            // 
            // PlainGFLParamControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(colorButton);
            Name = "PlainGFLParamControl";
            ResumeLayout(false);
        }

        #endregion

        private ColorButton colorButton;
    }
}
