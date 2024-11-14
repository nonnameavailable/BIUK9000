
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
            mainLayersPanel = new LayersPanel();
            mainTimelineSlider = new TimelineSlider();
            controlsPanel = new ControlsPanel();
            layerParamsPanel = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            deleteFramesButton = new System.Windows.Forms.Button();
            unmarkButton = new System.Windows.Forms.Button();
            markButton = new System.Windows.Forms.Button();
            lerpExecuteButton = new System.Windows.Forms.Button();
            lerpStartButton = new System.Windows.Forms.Button();
            mainPictureBox = new MyPictureBox();
            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(mainLayersPanel, 2, 1);
            tableLayoutPanel1.Controls.Add(mainTimelineSlider, 1, 2);
            tableLayoutPanel1.Controls.Add(controlsPanel, 0, 0);
            tableLayoutPanel1.Controls.Add(layerParamsPanel, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 2, 2);
            tableLayoutPanel1.Controls.Add(mainPictureBox, 1, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(872, 561);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // mainLayersPanel
            // 
            mainLayersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayersPanel.Location = new System.Drawing.Point(725, 158);
            mainLayersPanel.Name = "mainLayersPanel";
            mainLayersPanel.SelectedLayerIndex = 0;
            mainLayersPanel.Size = new System.Drawing.Size(144, 300);
            mainLayersPanel.TabIndex = 2;
            // 
            // mainTimelineSlider
            // 
            mainTimelineSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            mainTimelineSlider.FrameDelay = 10;
            mainTimelineSlider.Location = new System.Drawing.Point(148, 464);
            mainTimelineSlider.Maximum = 9;
            mainTimelineSlider.MouseButtonIsDown = false;
            mainTimelineSlider.Name = "mainTimelineSlider";
            mainTimelineSlider.SelectedFrameIndex = 0;
            mainTimelineSlider.Size = new System.Drawing.Size(571, 94);
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
            controlsPanel.Size = new System.Drawing.Size(139, 555);
            controlsPanel.TabIndex = 5;
            // 
            // layerParamsPanel
            // 
            layerParamsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            layerParamsPanel.Location = new System.Drawing.Point(148, 3);
            layerParamsPanel.Name = "layerParamsPanel";
            layerParamsPanel.Size = new System.Drawing.Size(571, 149);
            layerParamsPanel.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.Controls.Add(deleteFramesButton);
            panel1.Controls.Add(unmarkButton);
            panel1.Controls.Add(markButton);
            panel1.Controls.Add(lerpExecuteButton);
            panel1.Controls.Add(lerpStartButton);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(725, 464);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(144, 94);
            panel1.TabIndex = 8;
            // 
            // deleteFramesButton
            // 
            deleteFramesButton.Location = new System.Drawing.Point(71, 62);
            deleteFramesButton.Name = "deleteFramesButton";
            deleteFramesButton.Size = new System.Drawing.Size(62, 23);
            deleteFramesButton.TabIndex = 4;
            deleteFramesButton.Text = "del fr.";
            deleteFramesButton.UseVisualStyleBackColor = true;
            // 
            // unmarkButton
            // 
            unmarkButton.Location = new System.Drawing.Point(71, 32);
            unmarkButton.Name = "unmarkButton";
            unmarkButton.Size = new System.Drawing.Size(62, 23);
            unmarkButton.TabIndex = 3;
            unmarkButton.Text = "unmark";
            unmarkButton.UseVisualStyleBackColor = true;
            // 
            // markButton
            // 
            markButton.Location = new System.Drawing.Point(71, 3);
            markButton.Name = "markButton";
            markButton.Size = new System.Drawing.Size(62, 23);
            markButton.TabIndex = 2;
            markButton.Text = "mark";
            markButton.UseVisualStyleBackColor = true;
            // 
            // lerpExecuteButton
            // 
            lerpExecuteButton.Location = new System.Drawing.Point(3, 32);
            lerpExecuteButton.Name = "lerpExecuteButton";
            lerpExecuteButton.Size = new System.Drawing.Size(62, 23);
            lerpExecuteButton.TabIndex = 1;
            lerpExecuteButton.Text = "lerp exe";
            lerpExecuteButton.UseVisualStyleBackColor = true;
            // 
            // lerpStartButton
            // 
            lerpStartButton.Location = new System.Drawing.Point(3, 3);
            lerpStartButton.Name = "lerpStartButton";
            lerpStartButton.Size = new System.Drawing.Size(62, 23);
            lerpStartButton.TabIndex = 0;
            lerpStartButton.Text = "lerp start";
            lerpStartButton.UseVisualStyleBackColor = true;
            // 
            // mainPictureBox
            // 
            mainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            mainPictureBox.IsLMBDown = false;
            mainPictureBox.IsMMBDown = false;
            mainPictureBox.IsRMBDown = false;
            mainPictureBox.Location = new System.Drawing.Point(148, 158);
            mainPictureBox.Name = "mainPictureBox";
            mainPictureBox.ScaledDragDifference = new System.Drawing.Point(0, 0);
            mainPictureBox.Size = new System.Drawing.Size(571, 300);
            mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            mainPictureBox.TabIndex = 9;
            mainPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(872, 561);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LayersPanel mainLayersPanel;
        private TimelineSlider mainTimelineSlider;
        private ControlsPanel controlsPanel;
        private System.Windows.Forms.Panel layerParamsPanel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button lerpExecuteButton;
        private System.Windows.Forms.Button lerpStartButton;
        private MyPictureBox mainPictureBox;
        private System.Windows.Forms.Button unmarkButton;
        private System.Windows.Forms.Button markButton;
        private System.Windows.Forms.Button deleteFramesButton;
    }
}

