namespace BIUK9000.UI.Forms
{
    partial class RecordForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            recordPanel = new System.Windows.Forms.Panel();
            SuspendLayout();
            // 
            // recordPanel
            // 
            recordPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            recordPanel.Location = new System.Drawing.Point(3, 3);
            recordPanel.Name = "recordPanel";
            recordPanel.Size = new System.Drawing.Size(297, 366);
            recordPanel.TabIndex = 2;
            // 
            // RecordForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(303, 372);
            Controls.Add(recordPanel);
            Name = "RecordForm";
            Padding = new System.Windows.Forms.Padding(3);
            Text = "Recording window";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel recordPanel;
    }
}