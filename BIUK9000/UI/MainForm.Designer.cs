
using BIUK9000.UI.ExtendedControls;

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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            mainTimelineSlider = new TimelineSlider();
            controlsPanel = new ControlsPanel();
            markLerpPanel = new System.Windows.Forms.Panel();
            frameDupeCountNUD = new System.Windows.Forms.NumericUpDown();
            dupeFrameButton = new System.Windows.Forms.Button();
            askDeleteCB = new System.Windows.Forms.CheckBox();
            lerpModeCBB = new System.Windows.Forms.ComboBox();
            deleteFramesButton = new System.Windows.Forms.Button();
            lerpButton = new System.Windows.Forms.Button();
            mainPictureBox = new MyPictureBox();
            mainLayersPanel = new LayersPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            layerParamsPanel = new System.Windows.Forms.Panel();
            hsbPanel = new CustomControls.HSBPanel();
            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            tableLayoutPanel1.SuspendLayout();
            markLerpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)frameDupeCountNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(mainTimelineSlider, 1, 2);
            tableLayoutPanel1.Controls.Add(controlsPanel, 0, 0);
            tableLayoutPanel1.Controls.Add(markLerpPanel, 2, 2);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 1);
            tableLayoutPanel1.Controls.Add(mainLayersPanel, 2, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(928, 598);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainTimelineSlider
            // 
            mainTimelineSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTimelineSlider.FrameDelay = 10;
            mainTimelineSlider.Location = new System.Drawing.Point(148, 501);
            mainTimelineSlider.Maximum = 9;
            mainTimelineSlider.MouseButtonIsDown = false;
            mainTimelineSlider.Name = "mainTimelineSlider";
            mainTimelineSlider.SelectedFrameIndex = 0;
            mainTimelineSlider.Size = new System.Drawing.Size(627, 94);
            mainTimelineSlider.TabIndex = 3;
            // 
            // controlsPanel
            // 
            controlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            controlsPanel.DraggingFileForExport = false;
            controlsPanel.IsLMBDown = false;
            controlsPanel.IsRMBDown = false;
            controlsPanel.Location = new System.Drawing.Point(3, 3);
            controlsPanel.Name = "controlsPanel";
            tableLayoutPanel1.SetRowSpan(controlsPanel, 3);
            controlsPanel.Size = new System.Drawing.Size(139, 592);
            controlsPanel.TabIndex = 5;
            // 
            // markLerpPanel
            // 
            markLerpPanel.Controls.Add(frameDupeCountNUD);
            markLerpPanel.Controls.Add(dupeFrameButton);
            markLerpPanel.Controls.Add(askDeleteCB);
            markLerpPanel.Controls.Add(lerpModeCBB);
            markLerpPanel.Controls.Add(deleteFramesButton);
            markLerpPanel.Controls.Add(lerpButton);
            markLerpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            markLerpPanel.Location = new System.Drawing.Point(781, 501);
            markLerpPanel.Name = "markLerpPanel";
            markLerpPanel.Size = new System.Drawing.Size(144, 94);
            markLerpPanel.TabIndex = 8;
            // 
            // frameDupeCountNUD
            // 
            frameDupeCountNUD.Location = new System.Drawing.Point(71, 61);
            frameDupeCountNUD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            frameDupeCountNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            frameDupeCountNUD.Name = "frameDupeCountNUD";
            frameDupeCountNUD.Size = new System.Drawing.Size(61, 23);
            frameDupeCountNUD.TabIndex = 8;
            frameDupeCountNUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dupeFrameButton
            // 
            dupeFrameButton.Location = new System.Drawing.Point(3, 61);
            dupeFrameButton.Name = "dupeFrameButton";
            dupeFrameButton.Size = new System.Drawing.Size(62, 23);
            dupeFrameButton.TabIndex = 7;
            dupeFrameButton.Text = "dupe fr.";
            dupeFrameButton.UseVisualStyleBackColor = true;
            // 
            // askDeleteCB
            // 
            askDeleteCB.AutoSize = true;
            askDeleteCB.Checked = true;
            askDeleteCB.CheckState = System.Windows.Forms.CheckState.Checked;
            askDeleteCB.Location = new System.Drawing.Point(71, 35);
            askDeleteCB.Name = "askDeleteCB";
            askDeleteCB.Size = new System.Drawing.Size(43, 19);
            askDeleteCB.TabIndex = 6;
            askDeleteCB.Text = "ask";
            askDeleteCB.UseVisualStyleBackColor = true;
            // 
            // lerpModeCBB
            // 
            lerpModeCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            lerpModeCBB.FormattingEnabled = true;
            lerpModeCBB.Items.AddRange(new object[] { "line", "trace" });
            lerpModeCBB.Location = new System.Drawing.Point(3, 3);
            lerpModeCBB.Name = "lerpModeCBB";
            lerpModeCBB.Size = new System.Drawing.Size(61, 23);
            lerpModeCBB.TabIndex = 5;
            // 
            // deleteFramesButton
            // 
            deleteFramesButton.Location = new System.Drawing.Point(3, 32);
            deleteFramesButton.Name = "deleteFramesButton";
            deleteFramesButton.Size = new System.Drawing.Size(62, 23);
            deleteFramesButton.TabIndex = 4;
            deleteFramesButton.Text = "del fr.";
            deleteFramesButton.UseVisualStyleBackColor = true;
            // 
            // lerpButton
            // 
            lerpButton.Location = new System.Drawing.Point(70, 3);
            lerpButton.Name = "lerpButton";
            lerpButton.Size = new System.Drawing.Size(62, 23);
            lerpButton.TabIndex = 1;
            lerpButton.Text = "lerp";
            lerpButton.UseVisualStyleBackColor = true;
            // 
            // mainPictureBox
            // 
            mainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            mainPictureBox.IsLMBDown = false;
            mainPictureBox.IsMMBDown = false;
            mainPictureBox.IsRMBDown = false;
            mainPictureBox.Location = new System.Drawing.Point(148, 161);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.ScaledDragDifference = new System.Drawing.Point(0, 0);
            mainPictureBox.Size = new System.Drawing.Size(627, 334);
            mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 9;
            mainPictureBox.TabStop = false;
            // 
            // mainLayersPanel
            // 
            mainLayersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayersPanel.Location = new System.Drawing.Point(781, 161);
            mainLayersPanel.Name = "mainLayersPanel";
            mainLayersPanel.SelectedLayerIndex = 0;
            mainLayersPanel.Size = new System.Drawing.Size(144, 334);
            mainLayersPanel.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            tableLayoutPanel2.Controls.Add(layerParamsPanel, 0, 0);
            tableLayoutPanel2.Controls.Add(hsbPanel, 1, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(148, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(777, 152);
            tableLayoutPanel2.TabIndex = 11;
            // 
            // layerParamsPanel
            // 
            layerParamsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            layerParamsPanel.Location = new System.Drawing.Point(3, 3);
            layerParamsPanel.Name = "layerParamsPanel";
            layerParamsPanel.Size = new System.Drawing.Size(575, 146);
            layerParamsPanel.TabIndex = 8;
            // 
            // hsbPanel
            // 
            hsbPanel.Brightness = 1F;
            hsbPanel.Location = new System.Drawing.Point(584, 3);
            hsbPanel.Name = "hsbPanel";
            hsbPanel.Saturation = 1F;
            hsbPanel.Size = new System.Drawing.Size(190, 143);
            hsbPanel.TabIndex = 10;
            hsbPanel.Transparency = 0F;
            // 
            // saveFileDialog
            // 
            saveFileDialog.Filter = "GIF files|*.gif";
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(928, 598);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            markLerpPanel.ResumeLayout(false);
            markLerpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)frameDupeCountNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LayersPanel mainLayersPanel;
        private TimelineSlider mainTimelineSlider;
        private ControlsPanel controlsPanel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel markLerpPanel;
        private System.Windows.Forms.Button lerpButton;
        private MyPictureBox mainPictureBox;
        private System.Windows.Forms.Button deleteFramesButton;
        private System.Windows.Forms.ComboBox lerpModeCBB;
        private System.Windows.Forms.CheckBox askDeleteCB;
        private System.Windows.Forms.NumericUpDown frameDupeCountNUD;
        private System.Windows.Forms.Button dupeFrameButton;
        private CustomControls.HSBPanel hsbPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel layerParamsPanel;
    }
}

