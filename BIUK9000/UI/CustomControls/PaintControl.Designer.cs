namespace BIUK9000.UI.CustomControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintControl));
            paintColorButton = new ColorButton();
            transparencyTrackBar = new System.Windows.Forms.TrackBar();
            thicknessNUD = new System.Windows.Forms.NumericUpDown();
            transparencyDisplayLabel = new System.Windows.Forms.Label();
            toolTip = new System.Windows.Forms.ToolTip(components);
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            toolsGroupBox = new System.Windows.Forms.GroupBox();
            lassoRB = new System.Windows.Forms.RadioButton();
            deleteColorRB = new System.Windows.Forms.RadioButton();
            drawLineRB = new System.Windows.Forms.RadioButton();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lassoConstrainCB = new System.Windows.Forms.CheckBox();
            lassoIncludeComplementCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thicknessNUD).BeginInit();
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
            paintColorButton.Size = new System.Drawing.Size(42, 35);
            paintColorButton.TabIndex = 0;
            toolTip.SetToolTip(paintColorButton, "Fill color for painting");
            // 
            // transparencyTrackBar
            // 
            transparencyTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            transparencyTrackBar.Location = new System.Drawing.Point(3, 3);
            transparencyTrackBar.Maximum = 255;
            transparencyTrackBar.Name = "transparencyTrackBar";
            transparencyTrackBar.Size = new System.Drawing.Size(133, 45);
            transparencyTrackBar.TabIndex = 1;
            transparencyTrackBar.TickFrequency = 15;
            toolTip.SetToolTip(transparencyTrackBar, resources.GetString("transparencyTrackBar.ToolTip"));
            transparencyTrackBar.Value = 255;
            // 
            // thicknessNUD
            // 
            thicknessNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            thicknessNUD.Location = new System.Drawing.Point(51, 3);
            thicknessNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            thicknessNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            thicknessNUD.Name = "thicknessNUD";
            thicknessNUD.Size = new System.Drawing.Size(120, 25);
            thicknessNUD.TabIndex = 2;
            toolTip.SetToolTip(thicknessNUD, "Line thickness");
            thicknessNUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // transparencyDisplayLabel
            // 
            transparencyDisplayLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            transparencyDisplayLabel.AutoSize = true;
            transparencyDisplayLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            transparencyDisplayLabel.Location = new System.Drawing.Point(139, 14);
            transparencyDisplayLabel.Name = "transparencyDisplayLabel";
            transparencyDisplayLabel.Size = new System.Drawing.Size(20, 17);
            transparencyDisplayLabel.TabIndex = 3;
            transparencyDisplayLabel.Text = "fff";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            tableLayoutPanel1.Controls.Add(paintColorButton, 0, 0);
            tableLayoutPanel1.Controls.Add(thicknessNUD, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(toolsGroupBox, 2, 0);
            tableLayoutPanel1.Controls.Add(groupBox1, 3, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            tableLayoutPanel1.Size = new System.Drawing.Size(431, 108);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            tableLayoutPanel1.SetColumnSpan(panel1, 2);
            panel1.Controls.Add(transparencyDisplayLabel);
            panel1.Controls.Add(transparencyTrackBar);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 51);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(174, 54);
            panel1.TabIndex = 4;
            // 
            // toolsGroupBox
            // 
            toolsGroupBox.Controls.Add(lassoRB);
            toolsGroupBox.Controls.Add(deleteColorRB);
            toolsGroupBox.Controls.Add(drawLineRB);
            toolsGroupBox.Location = new System.Drawing.Point(183, 3);
            toolsGroupBox.Name = "toolsGroupBox";
            tableLayoutPanel1.SetRowSpan(toolsGroupBox, 2);
            toolsGroupBox.Size = new System.Drawing.Size(94, 100);
            toolsGroupBox.TabIndex = 5;
            toolsGroupBox.TabStop = false;
            toolsGroupBox.Text = "tools";
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
            // deleteColorRB
            // 
            deleteColorRB.AutoSize = true;
            deleteColorRB.Location = new System.Drawing.Point(6, 47);
            deleteColorRB.Name = "deleteColorRB";
            deleteColorRB.Size = new System.Drawing.Size(87, 19);
            deleteColorRB.TabIndex = 1;
            deleteColorRB.Text = "delete color";
            deleteColorRB.UseVisualStyleBackColor = true;
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
            groupBox1.Controls.Add(lassoIncludeComplementCB);
            groupBox1.Controls.Add(lassoConstrainCB);
            groupBox1.Location = new System.Drawing.Point(283, 3);
            groupBox1.Name = "groupBox1";
            tableLayoutPanel1.SetRowSpan(groupBox1, 2);
            groupBox1.Size = new System.Drawing.Size(145, 100);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "lasso options";
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
            // PaintControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "PaintControl";
            Size = new System.Drawing.Size(431, 108);
            ((System.ComponentModel.ISupportInitialize)transparencyTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)thicknessNUD).EndInit();
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
        private System.Windows.Forms.RadioButton deleteColorRB;
        private System.Windows.Forms.RadioButton drawLineRB;
        private System.Windows.Forms.RadioButton lassoRB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox lassoIncludeComplementCB;
        private System.Windows.Forms.CheckBox lassoConstrainCB;
    }
}
