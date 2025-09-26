namespace BIUK9000.UI.Forms
{
    partial class VideoImportForm
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panel2 = new System.Windows.Forms.Panel();
            importBTN = new System.Windows.Forms.Button();
            timelineSlider1 = new TimelineSlider();
            myPictureBox1 = new BIUK9000.UI.ExtendedControls.MyPictureBox();
            statusStrip = new System.Windows.Forms.StatusStrip();
            statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            panel1 = new System.Windows.Forms.Panel();
            memoryLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            changeFpsNUD = new System.Windows.Forms.NumericUpDown();
            changeFpsCB = new System.Windows.Forms.CheckBox();
            maxSideLengthNUD = new System.Windows.Forms.NumericUpDown();
            maxSideLengthCB = new System.Windows.Forms.CheckBox();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)myPictureBox1).BeginInit();
            statusStrip.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)changeFpsNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxSideLengthNUD).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Controls.Add(timelineSlider1, 0, 1);
            tableLayoutPanel1.Controls.Add(myPictureBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(statusStrip, 0, 2);
            tableLayoutPanel1.Controls.Add(panel1, 2, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new System.Drawing.Size(667, 413);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(importBTN);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(94, 277);
            panel2.TabIndex = 3;
            // 
            // importBTN
            // 
            importBTN.DialogResult = System.Windows.Forms.DialogResult.OK;
            importBTN.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
            importBTN.Location = new System.Drawing.Point(3, 9);
            importBTN.Name = "importBTN";
            importBTN.Size = new System.Drawing.Size(88, 48);
            importBTN.TabIndex = 4;
            importBTN.Text = "Import";
            importBTN.UseVisualStyleBackColor = true;
            // 
            // timelineSlider1
            // 
            timelineSlider1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(timelineSlider1, 3);
            timelineSlider1.FrameDelay = 10;
            timelineSlider1.Location = new System.Drawing.Point(3, 286);
            timelineSlider1.Maximum = 9;
            timelineSlider1.MouseButtonIsDown = false;
            timelineSlider1.Name = "timelineSlider1";
            timelineSlider1.SelectedFrameIndex = 0;
            timelineSlider1.Size = new System.Drawing.Size(661, 94);
            timelineSlider1.TabIndex = 0;
            // 
            // myPictureBox1
            // 
            myPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            myPictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            myPictureBox1.IsLMBDown = false;
            myPictureBox1.IsMMBDown = false;
            myPictureBox1.IsRMBDown = false;
            myPictureBox1.Location = new System.Drawing.Point(103, 3);
            myPictureBox1.Name = "myPictureBox1";
            myPictureBox1.ScaledDragDifference = new System.Drawing.Point(0, 0);
            myPictureBox1.Size = new System.Drawing.Size(411, 277);
            myPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            myPictureBox1.TabIndex = 1;
            myPictureBox1.TabStop = false;
            // 
            // statusStrip
            // 
            tableLayoutPanel1.SetColumnSpan(statusStrip, 3);
            statusStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusLabel });
            statusStrip.Location = new System.Drawing.Point(0, 383);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(667, 30);
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(118, 25);
            statusLabel.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            panel1.Controls.Add(memoryLabel);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(changeFpsNUD);
            panel1.Controls.Add(changeFpsCB);
            panel1.Controls.Add(maxSideLengthNUD);
            panel1.Controls.Add(maxSideLengthCB);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(520, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(144, 277);
            panel1.TabIndex = 5;
            // 
            // memoryLabel
            // 
            memoryLabel.AutoSize = true;
            memoryLabel.Location = new System.Drawing.Point(3, 188);
            memoryLabel.Name = "memoryLabel";
            memoryLabel.Size = new System.Drawing.Size(13, 15);
            memoryLabel.TabIndex = 5;
            memoryLabel.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 147);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 30);
            label1.TabIndex = 4;
            label1.Text = "Memory needed\r\nfor this import:";
            // 
            // changeFpsNUD
            // 
            changeFpsNUD.DecimalPlaces = 2;
            changeFpsNUD.Enabled = false;
            changeFpsNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            changeFpsNUD.Location = new System.Drawing.Point(3, 88);
            changeFpsNUD.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            changeFpsNUD.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            changeFpsNUD.Name = "changeFpsNUD";
            changeFpsNUD.Size = new System.Drawing.Size(120, 23);
            changeFpsNUD.TabIndex = 3;
            changeFpsNUD.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // changeFpsCB
            // 
            changeFpsCB.AutoSize = true;
            changeFpsCB.Location = new System.Drawing.Point(3, 63);
            changeFpsCB.Name = "changeFpsCB";
            changeFpsCB.Size = new System.Drawing.Size(121, 19);
            changeFpsCB.TabIndex = 2;
            changeFpsCB.Text = "Change framerate";
            changeFpsCB.UseVisualStyleBackColor = true;
            // 
            // maxSideLengthNUD
            // 
            maxSideLengthNUD.Enabled = false;
            maxSideLengthNUD.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            maxSideLengthNUD.Location = new System.Drawing.Point(3, 34);
            maxSideLengthNUD.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            maxSideLengthNUD.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            maxSideLengthNUD.Name = "maxSideLengthNUD";
            maxSideLengthNUD.Size = new System.Drawing.Size(120, 23);
            maxSideLengthNUD.TabIndex = 1;
            maxSideLengthNUD.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // maxSideLengthCB
            // 
            maxSideLengthCB.AutoSize = true;
            maxSideLengthCB.Location = new System.Drawing.Point(3, 9);
            maxSideLengthCB.Name = "maxSideLengthCB";
            maxSideLengthCB.Size = new System.Drawing.Size(116, 19);
            maxSideLengthCB.TabIndex = 0;
            maxSideLengthCB.Text = "Change max side";
            maxSideLengthCB.UseVisualStyleBackColor = true;
            // 
            // VideoImportForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(667, 413);
            Controls.Add(tableLayoutPanel1);
            Name = "VideoImportForm";
            Text = "VideoImportForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)myPictureBox1).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)changeFpsNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxSideLengthNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private TimelineSlider timelineSlider1;
        private System.Windows.Forms.Panel panel2;
        private ExtendedControls.MyPictureBox myPictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown changeFpsNUD;
        private System.Windows.Forms.CheckBox changeFpsCB;
        private System.Windows.Forms.NumericUpDown maxSideLengthNUD;
        private System.Windows.Forms.CheckBox maxSideLengthCB;
        private System.Windows.Forms.Button importBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label memoryLabel;
    }
}