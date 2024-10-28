
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
            controlsPanel1 = new ControlsPanel();
            mainPictureBox = new System.Windows.Forms.PictureBox();
            textLayerParamsGB = new System.Windows.Forms.GroupBox();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            TextLayerFontCBB = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            TextLayerTextTB = new System.Windows.Forms.TextBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            textLayerParamsGB.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(mainLayersPanel, 2, 1);
            tableLayoutPanel1.Controls.Add(mainTimelineSlider, 1, 2);
            tableLayoutPanel1.Controls.Add(controlsPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 1);
            tableLayoutPanel1.Controls.Add(textLayerParamsGB, 1, 0);
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
            mainLayersPanel.SelectedLayerIndex = 0;
            mainLayersPanel.Size = new System.Drawing.Size(144, 305);
            mainLayersPanel.TabIndex = 2;
            // 
            // mainTimelineSlider
            // 
            mainTimelineSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTimelineSlider.Giffer = null;
            mainTimelineSlider.Location = new System.Drawing.Point(160, 464);
            mainTimelineSlider.Name = "mainTimelineSlider";
            mainTimelineSlider.Size = new System.Drawing.Size(593, 94);
            mainTimelineSlider.TabIndex = 3;
            // 
            // controlsPanel1
            // 
            controlsPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            controlsPanel1.DraggingFileForExport = false;
            controlsPanel1.Location = new System.Drawing.Point(3, 3);
            controlsPanel1.Name = "controlsPanel1";
            tableLayoutPanel1.SetRowSpan(controlsPanel1, 2);
            controlsPanel1.Size = new System.Drawing.Size(151, 455);
            controlsPanel1.TabIndex = 5;
            // 
            // mainPictureBox
            // 
            mainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPictureBox.Location = new System.Drawing.Point(160, 153);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new System.Drawing.Size(593, 305);
            mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 6;
            mainPictureBox.TabStop = false;
            // 
            // textLayerParamsGB
            // 
            textLayerParamsGB.Controls.Add(tableLayoutPanel2);
            textLayerParamsGB.Location = new System.Drawing.Point(160, 3);
            textLayerParamsGB.Name = "textLayerParamsGB";
            textLayerParamsGB.Size = new System.Drawing.Size(593, 144);
            textLayerParamsGB.TabIndex = 7;
            textLayerParamsGB.TabStop = false;
            textLayerParamsGB.Text = "text layer params";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(panel1, 0, 0);
            tableLayoutPanel2.Controls.Add(TextLayerTextTB, 1, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new System.Drawing.Size(587, 122);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(TextLayerFontCBB);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(194, 116);
            panel1.TabIndex = 0;
            // 
            // TextLayerFontCBB
            // 
            TextLayerFontCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            TextLayerFontCBB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            TextLayerFontCBB.FormattingEnabled = true;
            TextLayerFontCBB.Location = new System.Drawing.Point(47, 3);
            TextLayerFontCBB.Name = "TextLayerFontCBB";
            TextLayerFontCBB.Size = new System.Drawing.Size(121, 25);
            TextLayerFontCBB.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label1.Location = new System.Drawing.Point(3, 6);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 17);
            label1.TabIndex = 0;
            label1.Text = "font";
            // 
            // TextLayerTextTB
            // 
            TextLayerTextTB.Dock = System.Windows.Forms.DockStyle.Fill;
            TextLayerTextTB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            TextLayerTextTB.Location = new System.Drawing.Point(203, 3);
            TextLayerTextTB.Multiline = true;
            TextLayerTextTB.Name = "TextLayerTextTB";
            TextLayerTextTB.Size = new System.Drawing.Size(381, 116);
            TextLayerTextTB.TabIndex = 1;
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
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            textLayerParamsGB.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LayersPanel mainLayersPanel;
        private TimelineSlider mainTimelineSlider;
        private ControlsPanel controlsPanel1;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.GroupBox textLayerParamsGB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox TextLayerFontCBB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextLayerTextTB;
    }
}

