namespace BIUK9000.UI
{
    partial class ColorButton
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
            button = new System.Windows.Forms.Button();
            colorDialog = new System.Windows.Forms.ColorDialog();
            SuspendLayout();
            // 
            // button
            // 
            button.Dock = System.Windows.Forms.DockStyle.Fill;
            button.Location = new System.Drawing.Point(0, 0);
            button.Name = "button";
            button.Size = new System.Drawing.Size(35, 35);
            button.TabIndex = 0;
            button.UseVisualStyleBackColor = true;
            // 
            // ColorButton
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(button);
            Name = "ColorButton";
            Size = new System.Drawing.Size(35, 35);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button button;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}
