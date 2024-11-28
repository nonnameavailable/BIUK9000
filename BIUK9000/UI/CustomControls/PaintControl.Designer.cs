﻿namespace BIUK9000.UI.CustomControls
{
    partial class PaintControl
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
            components = new System.ComponentModel.Container();
            paintColorButton = new ColorButton();
            transparencyTrackBar = new System.Windows.Forms.TrackBar();
            thicknessNUD = new System.Windows.Forms.NumericUpDown();
            transparencyDisplayLabel = new System.Windows.Forms.Label();
            toolTip = new System.Windows.Forms.ToolTip(components);
            toleranceTrackBar = new System.Windows.Forms.TrackBar();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            toleranceDisplayLabel = new System.Windows.Forms.Label();
            toolsGroupBox = new System.Windows.Forms.GroupBox();
            fillColorRB = new System.Windows.Forms.RadioButton();
            lassoRB = new System.Windows.Forms.RadioButton();
            replaceColorRB = new System.Windows.Forms.RadioButton();
            drawLineRB = new System.Windows.Forms.RadioButton();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lassoIncludeComplementCB = new System.Windows.Forms.CheckBox();
            lassoConstrainCB = new System.Windows.Forms.CheckBox();
            animateCutoutCB = new System.Windows.Forms.CheckBox();
            animateComplementCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thicknessNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toleranceTrackBar).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            toolsGroupBox.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // paintColorButton
            // 
            paintColorButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            paintColorButton.Color = System.Drawing.Color.Black;
            paintColorButton.Location = new System.Drawing.Point(3, 3);
            paintColorButton.Name = "paintColorButton";
            paintColorButton.Size = new System.Drawing.Size(46, 29);
            paintColorButton.TabIndex = 0;
            toolTip.SetToolTip(paintColorButton, "Fill color for painting");
            // 
            // transparencyTrackBar
            // 
            transparencyTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            transparencyTrackBar.Location = new System.Drawing.Point(8, 12);
            transparencyTrackBar.Maximum = 255;
            transparencyTrackBar.Name = "transparencyTrackBar";
            transparencyTrackBar.Size = new System.Drawing.Size(140, 45);
            transparencyTrackBar.TabIndex = 1;
            transparencyTrackBar.TickFrequency = 15;
            toolTip.SetToolTip(transparencyTrackBar, "Opacity of fill color\r\n\r\ndrawing:\r\n0 - Completely transparent\r\n255 - Completely opaque");
            transparencyTrackBar.Value = 255;
            // 
            // thicknessNUD
            // 
            thicknessNUD.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            thicknessNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            thicknessNUD.Location = new System.Drawing.Point(55, 3);
            thicknessNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            thicknessNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            thicknessNUD.Name = "thicknessNUD";
            thicknessNUD.Size = new System.Drawing.Size(134, 25);
            thicknessNUD.TabIndex = 2;
            toolTip.SetToolTip(thicknessNUD, "Line thickness");
            thicknessNUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // transparencyDisplayLabel
            // 
            transparencyDisplayLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            transparencyDisplayLabel.AutoSize = true;
            transparencyDisplayLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            transparencyDisplayLabel.Location = new System.Drawing.Point(151, 14);
            transparencyDisplayLabel.Name = "transparencyDisplayLabel";
            transparencyDisplayLabel.Size = new System.Drawing.Size(20, 17);
            transparencyDisplayLabel.TabIndex = 3;
            transparencyDisplayLabel.Text = "fff";
            // 
            // toleranceTrackBar
            // 
            toleranceTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            toleranceTrackBar.Location = new System.Drawing.Point(8, 63);
            toleranceTrackBar.Maximum = 255;
            toleranceTrackBar.Name = "toleranceTrackBar";
            toleranceTrackBar.Size = new System.Drawing.Size(140, 45);
            toleranceTrackBar.TabIndex = 4;
            toleranceTrackBar.TickFrequency = 15;
            toolTip.SetToolTip(toleranceTrackBar, "Tolerance for replace color and fill\r\n\r\n0 - no tolerance, only exactly the same color will be replaced / filled\r\n255 - everything will be replaced / filled");
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(paintColorButton, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(toolsGroupBox, 2, 0);
            tableLayoutPanel1.Controls.Add(groupBox1, 3, 0);
            tableLayoutPanel1.Controls.Add(thicknessNUD, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            tableLayoutPanel1.Size = new System.Drawing.Size(448, 152);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            tableLayoutPanel1.SetColumnSpan(panel1, 2);
            panel1.Controls.Add(toleranceDisplayLabel);
            panel1.Controls.Add(toleranceTrackBar);
            panel1.Controls.Add(transparencyDisplayLabel);
            panel1.Controls.Add(transparencyTrackBar);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 38);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(186, 111);
            panel1.TabIndex = 4;
            // 
            // toleranceDisplayLabel
            // 
            toleranceDisplayLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            toleranceDisplayLabel.AutoSize = true;
            toleranceDisplayLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            toleranceDisplayLabel.Location = new System.Drawing.Point(151, 66);
            toleranceDisplayLabel.Name = "toleranceDisplayLabel";
            toleranceDisplayLabel.Size = new System.Drawing.Size(20, 17);
            toleranceDisplayLabel.TabIndex = 5;
            toleranceDisplayLabel.Text = "fff";
            // 
            // toolsGroupBox
            // 
            toolsGroupBox.Controls.Add(fillColorRB);
            toolsGroupBox.Controls.Add(lassoRB);
            toolsGroupBox.Controls.Add(replaceColorRB);
            toolsGroupBox.Controls.Add(drawLineRB);
            toolsGroupBox.Location = new System.Drawing.Point(195, 3);
            toolsGroupBox.Name = "toolsGroupBox";
            tableLayoutPanel1.SetRowSpan(toolsGroupBox, 2);
            toolsGroupBox.Size = new System.Drawing.Size(99, 143);
            toolsGroupBox.TabIndex = 5;
            toolsGroupBox.TabStop = false;
            toolsGroupBox.Text = "tools";
            // 
            // fillColorRB
            // 
            fillColorRB.AutoSize = true;
            fillColorRB.Location = new System.Drawing.Point(6, 100);
            fillColorRB.Name = "fillColorRB";
            fillColorRB.Size = new System.Drawing.Size(68, 19);
            fillColorRB.TabIndex = 3;
            fillColorRB.TabStop = true;
            fillColorRB.Text = "fill color";
            fillColorRB.UseVisualStyleBackColor = true;
            // 
            // lassoRB
            // 
            lassoRB.AutoSize = true;
            lassoRB.Location = new System.Drawing.Point(6, 72);
            lassoRB.Name = "lassoRB";
            lassoRB.Size = new System.Drawing.Size(51, 19);
            lassoRB.TabIndex = 2;
            lassoRB.TabStop = true;
            lassoRB.Text = "lasso";
            lassoRB.UseVisualStyleBackColor = true;
            // 
            // replaceColorRB
            // 
            replaceColorRB.AutoSize = true;
            replaceColorRB.Location = new System.Drawing.Point(6, 47);
            replaceColorRB.Name = "replaceColorRB";
            replaceColorRB.Size = new System.Drawing.Size(93, 19);
            replaceColorRB.TabIndex = 1;
            replaceColorRB.Text = "replace color";
            replaceColorRB.UseVisualStyleBackColor = true;
            // 
            // drawLineRB
            // 
            drawLineRB.AutoSize = true;
            drawLineRB.Checked = true;
            drawLineRB.Location = new System.Drawing.Point(6, 22);
            drawLineRB.Name = "drawLineRB";
            drawLineRB.Size = new System.Drawing.Size(73, 19);
            drawLineRB.TabIndex = 0;
            drawLineRB.TabStop = true;
            drawLineRB.Text = "draw line";
            drawLineRB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(animateComplementCB);
            groupBox1.Controls.Add(animateCutoutCB);
            groupBox1.Controls.Add(lassoIncludeComplementCB);
            groupBox1.Controls.Add(lassoConstrainCB);
            groupBox1.Location = new System.Drawing.Point(300, 3);
            groupBox1.Name = "groupBox1";
            tableLayoutPanel1.SetRowSpan(groupBox1, 2);
            groupBox1.Size = new System.Drawing.Size(145, 143);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "lasso options";
            // 
            // lassoIncludeComplementCB
            // 
            lassoIncludeComplementCB.AutoSize = true;
            lassoIncludeComplementCB.Location = new System.Drawing.Point(6, 47);
            lassoIncludeComplementCB.Name = "lassoIncludeComplementCB";
            lassoIncludeComplementCB.Size = new System.Drawing.Size(136, 19);
            lassoIncludeComplementCB.TabIndex = 1;
            lassoIncludeComplementCB.Text = "include complement";
            lassoIncludeComplementCB.UseVisualStyleBackColor = true;
            // 
            // lassoConstrainCB
            // 
            lassoConstrainCB.AutoSize = true;
            lassoConstrainCB.Location = new System.Drawing.Point(6, 23);
            lassoConstrainCB.Name = "lassoConstrainCB";
            lassoConstrainCB.Size = new System.Drawing.Size(75, 19);
            lassoConstrainCB.TabIndex = 0;
            lassoConstrainCB.Text = "constrain";
            lassoConstrainCB.UseVisualStyleBackColor = true;
            // 
            // animateCutoutCB
            // 
            animateCutoutCB.AutoSize = true;
            animateCutoutCB.Location = new System.Drawing.Point(6, 72);
            animateCutoutCB.Name = "animateCutoutCB";
            animateCutoutCB.Size = new System.Drawing.Size(107, 19);
            animateCutoutCB.TabIndex = 2;
            animateCutoutCB.Text = "animate cutout";
            animateCutoutCB.UseVisualStyleBackColor = true;
            // 
            // animateComplementCB
            // 
            animateComplementCB.AutoSize = true;
            animateComplementCB.Location = new System.Drawing.Point(6, 97);
            animateComplementCB.Name = "animateComplementCB";
            animateComplementCB.Size = new System.Drawing.Size(140, 19);
            animateComplementCB.TabIndex = 3;
            animateComplementCB.Text = "animate complement";
            animateComplementCB.UseVisualStyleBackColor = true;
            // 
            // PaintControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "PaintControl";
            Size = new System.Drawing.Size(448, 152);
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)thicknessNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)toleranceTrackBar).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            toolsGroupBox.ResumeLayout(false);
            toolsGroupBox.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ColorButton paintColorButton;
        private System.Windows.Forms.TrackBar transparencyTrackBar;
        private System.Windows.Forms.NumericUpDown thicknessNUD;
        private System.Windows.Forms.Label transparencyDisplayLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox toolsGroupBox;
        private System.Windows.Forms.RadioButton replaceColorRB;
        private System.Windows.Forms.RadioButton drawLineRB;
        private System.Windows.Forms.RadioButton lassoRB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox lassoIncludeComplementCB;
        private System.Windows.Forms.CheckBox lassoConstrainCB;
        private System.Windows.Forms.Label toleranceDisplayLabel;
        private System.Windows.Forms.TrackBar toleranceTrackBar;
        private System.Windows.Forms.RadioButton fillColorRB;
        private System.Windows.Forms.CheckBox animateComplementCB;
        private System.Windows.Forms.CheckBox animateCutoutCB;
    }
}
