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
            timeLineTrackBar = new System.Windows.Forms.TrackBar();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            frameDelayNUD = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)timeLineTrackBar).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)frameDelayNUD).BeginInit();
            SuspendLayout();
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(timeLineTrackBar, 1, 0);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(482, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(frameDelayNUD);
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(94, 94);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "delay";
            // 
            // frameDelayNUD
            // 
            frameDelayNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            frameDelayNUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            frameDelayNUD.Location = new System.Drawing.Point(6, 22);
            frameDelayNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            frameDelayNUD.Name = "frameDelayNUD";
            frameDelayNUD.Size = new System.Drawing.Size(82, 25);
            frameDelayNUD.TabIndex = 0;
            frameDelayNUD.Value = new decimal(new int[] { 10, 0, 0, 0 });
            frameDelayNUD.ValueChanged += frameDelayNUD_ValueChanged;
            // 
            // TimelineSlider
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "TimelineSlider";
            Size = new System.Drawing.Size(482, 100);
            ((System.ComponentModel.ISupportInitialize)timeLineTrackBar).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)frameDelayNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TrackBar timeLineTrackBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown frameDelayNUD;
    }
}
