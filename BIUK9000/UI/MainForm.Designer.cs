﻿
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
            components = new System.ComponentModel.Container();
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
            hsbPanel = new BIUK9000.UI.CustomControls.HSBPanel();
            mainMenuStrip = new System.Windows.Forms.MenuStrip();
            framesMI = new System.Windows.Forms.ToolStripMenuItem();
            framesReverseMI = new System.Windows.Forms.ToolStripMenuItem();
            framesAddReversedMI = new System.Windows.Forms.ToolStripMenuItem();
            layersMI = new System.Windows.Forms.ToolStripMenuItem();
            layerAddMI = new System.Windows.Forms.ToolStripMenuItem();
            layerAddTextMI = new System.Windows.Forms.ToolStripMenuItem();
            layerAddShapeMI = new System.Windows.Forms.ToolStripMenuItem();
            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            toolTip = new System.Windows.Forms.ToolTip(components);
            layerFlattenMI = new System.Windows.Forms.ToolStripMenuItem();
            layerSnapToFrameMI = new System.Windows.Forms.ToolStripMenuItem();
            layerRestoreRatioMI = new System.Windows.Forms.ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            markLerpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)frameDupeCountNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            mainMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(mainTimelineSlider, 1, 3);
            tableLayoutPanel1.Controls.Add(controlsPanel, 0, 1);
            tableLayoutPanel1.Controls.Add(markLerpPanel, 2, 3);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 2);
            tableLayoutPanel1.Controls.Add(mainLayersPanel, 2, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 1);
            tableLayoutPanel1.Controls.Add(mainMenuStrip, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(918, 551);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainTimelineSlider
            // 
            mainTimelineSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTimelineSlider.FrameDelay = 10;
            mainTimelineSlider.Location = new System.Drawing.Point(148, 454);
            mainTimelineSlider.Maximum = 9;
            mainTimelineSlider.MouseButtonIsDown = false;
            mainTimelineSlider.Name = "mainTimelineSlider";
            mainTimelineSlider.SelectedFrameIndex = 0;
            mainTimelineSlider.Size = new System.Drawing.Size(617, 94);
            mainTimelineSlider.TabIndex = 3;
            // 
            // controlsPanel
            // 
            controlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            controlsPanel.DraggingFileForExport = false;
            controlsPanel.IsLMBDown = false;
            controlsPanel.IsRMBDown = false;
            controlsPanel.Location = new System.Drawing.Point(3, 33);
            controlsPanel.Name = "controlsPanel";
            tableLayoutPanel1.SetRowSpan(controlsPanel, 3);
            controlsPanel.SelectedMode = Mode.Move;
            controlsPanel.Size = new System.Drawing.Size(139, 515);
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
            markLerpPanel.Location = new System.Drawing.Point(771, 454);
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
            toolTip.SetToolTip(frameDupeCountNUD, "Number of frame duplicates");
            frameDupeCountNUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dupeFrameButton
            // 
            dupeFrameButton.Location = new System.Drawing.Point(3, 61);
            dupeFrameButton.Name = "dupeFrameButton";
            dupeFrameButton.Size = new System.Drawing.Size(62, 23);
            dupeFrameButton.TabIndex = 7;
            dupeFrameButton.Text = "dupe fr.";
            toolTip.SetToolTip(dupeFrameButton, "Duplicate currently selected frame");
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
            toolTip.SetToolTip(askDeleteCB, "Ask before frame deletion");
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
            toolTip.SetToolTip(lerpModeCBB, "lerp mode\r\nline - layer will travel across a straight line\r\ntrace - layer will travel across the dragged path");
            // 
            // deleteFramesButton
            // 
            deleteFramesButton.Location = new System.Drawing.Point(3, 32);
            deleteFramesButton.Name = "deleteFramesButton";
            deleteFramesButton.Size = new System.Drawing.Size(62, 23);
            deleteFramesButton.TabIndex = 4;
            deleteFramesButton.Text = "del fr.";
            toolTip.SetToolTip(deleteFramesButton, "Deletes the selected frames\r\n\r\nIf 2 frames are marked, the frames between them (including the marked frames) are deleted\r\nIf no frame is marked, only the selected frame is deleted");
            deleteFramesButton.UseVisualStyleBackColor = true;
            // 
            // lerpButton
            // 
            lerpButton.Location = new System.Drawing.Point(70, 3);
            lerpButton.Name = "lerpButton";
            lerpButton.Size = new System.Drawing.Size(62, 23);
            lerpButton.TabIndex = 1;
            lerpButton.Text = "lerp";
            toolTip.SetToolTip(lerpButton, "Interpolate the selected layer\r\n\r\n2 frames must be marked for this to work");
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
            mainPictureBox.Location = new System.Drawing.Point(148, 191);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.ScaledDragDifference = new System.Drawing.Point(0, 0);
            mainPictureBox.Size = new System.Drawing.Size(617, 257);
            mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 9;
            mainPictureBox.TabStop = false;
            // 
            // mainLayersPanel
            // 
            mainLayersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayersPanel.Location = new System.Drawing.Point(771, 191);
            mainLayersPanel.Name = "mainLayersPanel";
            mainLayersPanel.SelectedLayerIndex = 0;
            mainLayersPanel.Size = new System.Drawing.Size(144, 257);
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
            tableLayoutPanel2.Location = new System.Drawing.Point(148, 33);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(767, 152);
            tableLayoutPanel2.TabIndex = 11;
            // 
            // layerParamsPanel
            // 
            layerParamsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            layerParamsPanel.Location = new System.Drawing.Point(3, 3);
            layerParamsPanel.Name = "layerParamsPanel";
            layerParamsPanel.Size = new System.Drawing.Size(565, 146);
            layerParamsPanel.TabIndex = 8;
            // 
            // hsbPanel
            // 
            hsbPanel.Brightness = 1F;
            hsbPanel.Hue = 0F;
            hsbPanel.Location = new System.Drawing.Point(574, 3);
            hsbPanel.Name = "hsbPanel";
            hsbPanel.Saturation = 1F;
            hsbPanel.Size = new System.Drawing.Size(190, 143);
            hsbPanel.TabIndex = 10;
            hsbPanel.Transparency = 0F;
            // 
            // mainMenuStrip
            // 
            tableLayoutPanel1.SetColumnSpan(mainMenuStrip, 3);
            mainMenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { framesMI, layersMI });
            mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Size = new System.Drawing.Size(918, 30);
            mainMenuStrip.TabIndex = 12;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // framesMI
            // 
            framesMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { framesReverseMI, framesAddReversedMI });
            framesMI.Name = "framesMI";
            framesMI.Size = new System.Drawing.Size(57, 26);
            framesMI.Text = "Frames";
            // 
            // framesReverseMI
            // 
            framesReverseMI.Name = "framesReverseMI";
            framesReverseMI.Size = new System.Drawing.Size(146, 22);
            framesReverseMI.Text = "Reverse";
            // 
            // framesAddReversedMI
            // 
            framesAddReversedMI.Name = "framesAddReversedMI";
            framesAddReversedMI.Size = new System.Drawing.Size(146, 22);
            framesAddReversedMI.Text = "Add Reversed";
            // 
            // layersMI
            // 
            layersMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { layerAddMI, layerFlattenMI, layerSnapToFrameMI, layerRestoreRatioMI });
            layersMI.Name = "layersMI";
            layersMI.Size = new System.Drawing.Size(52, 26);
            layersMI.Text = "Layers";
            // 
            // layerAddMI
            // 
            layerAddMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { layerAddTextMI, layerAddShapeMI });
            layerAddMI.Name = "layerAddMI";
            layerAddMI.Size = new System.Drawing.Size(180, 22);
            layerAddMI.Text = "Add";
            // 
            // layerAddTextMI
            // 
            layerAddTextMI.Name = "layerAddTextMI";
            layerAddTextMI.Size = new System.Drawing.Size(180, 22);
            layerAddTextMI.Text = "Text";
            // 
            // layerAddShapeMI
            // 
            layerAddShapeMI.Name = "layerAddShapeMI";
            layerAddShapeMI.Size = new System.Drawing.Size(180, 22);
            layerAddShapeMI.Text = "Shape";
            // 
            // saveFileDialog
            // 
            saveFileDialog.Filter = "GIF files|*.gif";
            // 
            // layerFlattenMI
            // 
            layerFlattenMI.Name = "layerFlattenMI";
            layerFlattenMI.Size = new System.Drawing.Size(180, 22);
            layerFlattenMI.Text = "Flatten";
            // 
            // layerSnapToFrameMI
            // 
            layerSnapToFrameMI.Name = "layerSnapToFrameMI";
            layerSnapToFrameMI.Size = new System.Drawing.Size(180, 22);
            layerSnapToFrameMI.Text = "Snap to frame";
            // 
            // layerRestoreRatioMI
            // 
            layerRestoreRatioMI.Name = "layerRestoreRatioMI";
            layerRestoreRatioMI.Size = new System.Drawing.Size(180, 22);
            layerRestoreRatioMI.Text = "Restore ratio";
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(918, 551);
            Controls.Add(tableLayoutPanel1);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            ForeColor = System.Drawing.SystemColors.Desktop;
            MainMenuStrip = mainMenuStrip;
            Name = "MainForm";
            Text = "BIUK";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            markLerpPanel.ResumeLayout(false);
            markLerpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)frameDupeCountNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
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
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem framesMI;
        private System.Windows.Forms.ToolStripMenuItem layersMI;
        private System.Windows.Forms.ToolStripMenuItem layerAddMI;
        private System.Windows.Forms.ToolStripMenuItem framesReverseMI;
        private System.Windows.Forms.ToolStripMenuItem framesAddReversedMI;
        private System.Windows.Forms.ToolStripMenuItem layerAddTextMI;
        private System.Windows.Forms.ToolStripMenuItem layerAddShapeMI;
        private System.Windows.Forms.ToolStripMenuItem layerFlattenMI;
        private System.Windows.Forms.ToolStripMenuItem layerSnapToFrameMI;
        private System.Windows.Forms.ToolStripMenuItem layerRestoreRatioMI;
    }
}

