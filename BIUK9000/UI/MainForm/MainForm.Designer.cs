
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
            controlsPanel = new ControlsPanel();
            mainPictureBox = new System.Windows.Forms.PictureBox();
            textLayerParamsGB = new System.Windows.Forms.GroupBox();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            fontColorButton = new ColorButton();
            borderColorButton = new ColorButton();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            TextLayerBorderWidthNUD = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            textLayerFontCBB = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            textLayerTextTB = new System.Windows.Forms.TextBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            textLayerParamsGB.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TextLayerBorderWidthNUD).BeginInit();
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
            tableLayoutPanel1.Controls.Add(controlsPanel, 0, 0);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 1);
            tableLayoutPanel1.Controls.Add(textLayerParamsGB, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(906, 561);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainLayersPanel
            // 
            mainLayersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayersPanel.Location = new System.Drawing.Point(759, 158);
            mainLayersPanel.Name = "mainLayersPanel";
            mainLayersPanel.SelectedLayerIndex = 0;
            mainLayersPanel.Size = new System.Drawing.Size(144, 300);
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
            // controlsPanel
            // 
            controlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            controlsPanel.DraggingFileForExport = false;
            controlsPanel.Location = new System.Drawing.Point(3, 3);
            controlsPanel.Name = "controlsPanel";
            tableLayoutPanel1.SetRowSpan(controlsPanel, 2);
            controlsPanel.Size = new System.Drawing.Size(151, 455);
            controlsPanel.TabIndex = 5;
            // 
            // mainPictureBox
            // 
            mainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPictureBox.Location = new System.Drawing.Point(160, 158);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.Size = new System.Drawing.Size(593, 300);
            mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 6;
            mainPictureBox.TabStop = false;
            // 
            // textLayerParamsGB
            // 
            textLayerParamsGB.Controls.Add(tableLayoutPanel2);
            textLayerParamsGB.Dock = System.Windows.Forms.DockStyle.Fill;
            textLayerParamsGB.Location = new System.Drawing.Point(160, 3);
            textLayerParamsGB.Name = "textLayerParamsGB";
            textLayerParamsGB.Size = new System.Drawing.Size(593, 149);
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
            tableLayoutPanel2.Controls.Add(textLayerTextTB, 1, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new System.Drawing.Size(587, 127);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(fontColorButton);
            panel1.Controls.Add(borderColorButton);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(TextLayerBorderWidthNUD);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textLayerFontCBB);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(194, 121);
            panel1.TabIndex = 0;
            // 
            // fontColorButton
            // 
            fontColorButton.Color = System.Drawing.Color.White;
            fontColorButton.Location = new System.Drawing.Point(19, 83);
            fontColorButton.Name = "fontColorButton";
            fontColorButton.Size = new System.Drawing.Size(35, 35);
            fontColorButton.TabIndex = 7;
            // 
            // borderColorButton
            // 
            borderColorButton.Color = System.Drawing.Color.Black;
            borderColorButton.Location = new System.Drawing.Point(116, 84);
            borderColorButton.Name = "borderColorButton";
            borderColorButton.Size = new System.Drawing.Size(35, 35);
            borderColorButton.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label4.Location = new System.Drawing.Point(98, 64);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(83, 17);
            label4.TabIndex = 5;
            label4.Text = "border color";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label3.Location = new System.Drawing.Point(5, 64);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(65, 17);
            label3.TabIndex = 4;
            label3.Text = "font color";
            // 
            // TextLayerBorderWidthNUD
            // 
            TextLayerBorderWidthNUD.Location = new System.Drawing.Point(98, 34);
            TextLayerBorderWidthNUD.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            TextLayerBorderWidthNUD.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            TextLayerBorderWidthNUD.Name = "TextLayerBorderWidthNUD";
            TextLayerBorderWidthNUD.Size = new System.Drawing.Size(82, 23);
            TextLayerBorderWidthNUD.TabIndex = 3;
            TextLayerBorderWidthNUD.Value = new decimal(new int[] { 3, 0, 0, 0 });
            TextLayerBorderWidthNUD.ValueChanged += TextLayerBorderWidthNUD_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label2.Location = new System.Drawing.Point(5, 34);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(84, 17);
            label2.TabIndex = 2;
            label2.Text = "border width";
            // 
            // textLayerFontCBB
            // 
            textLayerFontCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            textLayerFontCBB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            textLayerFontCBB.FormattingEnabled = true;
            textLayerFontCBB.Location = new System.Drawing.Point(47, 3);
            textLayerFontCBB.Name = "textLayerFontCBB";
            textLayerFontCBB.Size = new System.Drawing.Size(121, 25);
            textLayerFontCBB.TabIndex = 1;
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
            // textLayerTextTB
            // 
            textLayerTextTB.Dock = System.Windows.Forms.DockStyle.Fill;
            textLayerTextTB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            textLayerTextTB.Location = new System.Drawing.Point(203, 3);
            textLayerTextTB.Multiline = true;
            textLayerTextTB.Name = "textLayerTextTB";
            textLayerTextTB.Size = new System.Drawing.Size(381, 121);
            textLayerTextTB.TabIndex = 1;
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
            ((System.ComponentModel.ISupportInitialize)TextLayerBorderWidthNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LayersPanel mainLayersPanel;
        private TimelineSlider mainTimelineSlider;
        private ControlsPanel controlsPanel;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.GroupBox textLayerParamsGB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox textLayerFontCBB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textLayerTextTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown TextLayerBorderWidthNUD;
        private System.Windows.Forms.Label label2;
        private ColorButton fontColorButton;
        private ColorButton borderColorButton;
    }
}

