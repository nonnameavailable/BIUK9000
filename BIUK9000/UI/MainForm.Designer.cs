
namespace BIUK9000.UI
{
    partial class MainForm
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
            mainLayersPanel = new LayersPanel();
            mainTimelineSlider = new TimelineSlider();
            mainPictureBox = new MyPictureBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(mainLayersPanel, 2, 1);
            tableLayoutPanel1.Controls.Add(mainTimelineSlider, 1, 2);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(906, 561);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainLayersPanel
            // 
            mainLayersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayersPanel.Location = new System.Drawing.Point(759, 153);
            mainLayersPanel.Name = "mainLayersPanel";
            mainLayersPanel.Size = new System.Drawing.Size(144, 305);
            mainLayersPanel.TabIndex = 2;
            // 
            // mainTimelineSlider
            // 
            mainTimelineSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTimelineSlider.Giffer = null;
            mainTimelineSlider.Location = new System.Drawing.Point(153, 464);
            mainTimelineSlider.Name = "mainTimelineSlider";
            mainTimelineSlider.Size = new System.Drawing.Size(600, 94);
            mainTimelineSlider.TabIndex = 3;
            // 
            // mainPictureBox
            // 
            mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPictureBox.Image = null;
            mainPictureBox.Location = new System.Drawing.Point(153, 153);
            mainPictureBox.MainGiffer = null;
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new System.Drawing.Size(600, 305);
            mainPictureBox.TabIndex = 4;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(906, 561);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LayersPanel mainLayersPanel;
        private TimelineSlider mainTimelineSlider;
        private MyPictureBox mainPictureBox;
    }
}

