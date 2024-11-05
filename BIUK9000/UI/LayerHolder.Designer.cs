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
            mainPictureBox.Image?.Dispose();
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            mainPictureBox = new System.Windows.Forms.PictureBox();
            panel1 = new System.Windows.Forms.Panel();
            deleteButton = new System.Windows.Forms.Button();
            visibleButton = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(121, 62);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainPictureBox
            // 
            mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPictureBox.Location = new System.Drawing.Point(53, 3);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new System.Drawing.Size(65, 56);
            mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 0;
            mainPictureBox.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(deleteButton);
            panel1.Controls.Add(visibleButton);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(50, 62);
            panel1.TabIndex = 1;
            // 
            // deleteButton
            // 
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            deleteButton.Location = new System.Drawing.Point(12, 37);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new System.Drawing.Size(23, 22);
            deleteButton.TabIndex = 1;
            deleteButton.UseVisualStyleBackColor = true;
            // 
            // visibleButton
            // 
            visibleButton.FlatAppearance.BorderSize = 0;
            visibleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            visibleButton.Location = new System.Drawing.Point(3, 3);
            visibleButton.Name = "visibleButton";
            visibleButton.Size = new System.Drawing.Size(41, 28);
            visibleButton.TabIndex = 0;
            visibleButton.UseVisualStyleBackColor = true;
            // 
            // LayerHolder
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "LayerHolder";
            Size = new System.Drawing.Size(121, 62);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button visibleButton;
        private System.Windows.Forms.Button deleteButton;
    }
}
