﻿
namespace BIUK9000.UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            mainTimelinePanel = new TimelinePanel();
            mainPictureBox = new System.Windows.Forms.PictureBox();
            mainLayersPanel = new LayersPanel();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(mainTimelinePanel, 1, 2);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 1);
            tableLayoutPanel1.Controls.Add(mainLayersPanel, 2, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Size = new System.Drawing.Size(906, 605);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainTimelinePanel
            // 
            mainTimelinePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTimelinePanel.Location = new System.Drawing.Point(153, 458);
            mainTimelinePanel.Name = "mainTimelinePanel";
            mainTimelinePanel.Size = new System.Drawing.Size(600, 144);
            mainTimelinePanel.TabIndex = 0;
            // 
            // mainPictureBox
            // 
            mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPictureBox.Location = new System.Drawing.Point(153, 153);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new System.Drawing.Size(600, 299);
            mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 1;
            mainPictureBox.TabStop = false;
            // 
            // mainLayersPanel
            // 
            mainLayersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayersPanel.Location = new System.Drawing.Point(759, 153);
            mainLayersPanel.Name = "mainLayersPanel";
            mainLayersPanel.Size = new System.Drawing.Size(144, 299);
            mainLayersPanel.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(906, 605);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private TimelinePanel mainTimelinePanel;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private LayersPanel mainLayersPanel;
    }
}

