﻿namespace BIUK9000.UI
{
    partial class TimelineSlider
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            timeLineTrackBar = new System.Windows.Forms.TrackBar();
            framePreviewPictureBox = new System.Windows.Forms.PictureBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)timeLineTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)framePreviewPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(timeLineTrackBar, 1, 0);
            tableLayoutPanel1.Controls.Add(framePreviewPictureBox, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(482, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // timeLineTrackBar
            // 
            timeLineTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            timeLineTrackBar.Location = new System.Drawing.Point(103, 52);
            timeLineTrackBar.Maximum = 100;
            timeLineTrackBar.Name = "timeLineTrackBar";
            timeLineTrackBar.Size = new System.Drawing.Size(376, 45);
            timeLineTrackBar.TabIndex = 0;
            // 
            // framePreviewPictureBox
            // 
            framePreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            framePreviewPictureBox.Location = new System.Drawing.Point(3, 3);
            framePreviewPictureBox.Name = "framePreviewPictureBox";
            framePreviewPictureBox.Size = new System.Drawing.Size(94, 94);
            framePreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            framePreviewPictureBox.TabIndex = 1;
            framePreviewPictureBox.TabStop = false;
            // 
            // TimelineSlider
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "TimelineSlider";
            Size = new System.Drawing.Size(482, 100);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)timeLineTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)framePreviewPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar timeLineTrackBar;
        private System.Windows.Forms.PictureBox framePreviewPictureBox;
    }
}
