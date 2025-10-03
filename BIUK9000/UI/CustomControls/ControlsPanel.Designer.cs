namespace BIUK9000.UI
{
    partial class ControlsPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlsPanel));
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            saveButton = new System.Windows.Forms.Button();
            controlsTC = new System.Windows.Forms.TabControl();
            optionsTP = new System.Windows.Forms.TabPage();
            groupBox2 = new System.Windows.Forms.GroupBox();
            ImageExportJpegQualNUD = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            ImageExportFormatCBB = new System.Windows.Forms.ComboBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            toolRecordRB = new System.Windows.Forms.RadioButton();
            toolPaintRB = new System.Windows.Forms.RadioButton();
            toolMoveRB = new System.Windows.Forms.RadioButton();
            mpbAAModeCBB = new System.Windows.Forms.ComboBox();
            applyParamsCBB = new System.Windows.Forms.ComboBox();
            positionSnapCB = new System.Windows.Forms.CheckBox();
            drawHelpCB = new System.Windows.Forms.CheckBox();
            rotationSnapCB = new System.Windows.Forms.CheckBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            tableLayoutPanel1.SuspendLayout();
            controlsTC.SuspendLayout();
            optionsTP.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).BeginInit();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(saveButton, 0, 0);
            tableLayoutPanel1.Controls.Add(controlsTC, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(143, 523);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // saveButton
            // 
            saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            saveButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            saveButton.Location = new System.Drawing.Point(3, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(137, 134);
            saveButton.TabIndex = 8;
            saveButton.Text = "Save";
            toolTip.SetToolTip(saveButton, "Left click - save dialog will open, allowing you to save the gif\r\n\r\nLeft mouse down and drag to desktop / explorer - export current frame as jpeg / png\r\nRight mouse down + drag - export current layer");
            saveButton.UseVisualStyleBackColor = true;
            // 
            // controlsTC
            // 
            controlsTC.Controls.Add(optionsTP);
            controlsTC.Dock = System.Windows.Forms.DockStyle.Fill;
            controlsTC.Location = new System.Drawing.Point(3, 143);
            controlsTC.Name = "controlsTC";
            controlsTC.Padding = new System.Drawing.Point(3, 3);
            controlsTC.SelectedIndex = 0;
            controlsTC.Size = new System.Drawing.Size(137, 377);
            controlsTC.TabIndex = 7;
            // 
            // optionsTP
            // 
            optionsTP.AutoScroll = true;
            optionsTP.Controls.Add(groupBox2);
            optionsTP.Controls.Add(groupBox4);
            optionsTP.Controls.Add(mpbAAModeCBB);
            optionsTP.Controls.Add(applyParamsCBB);
            optionsTP.Controls.Add(positionSnapCB);
            optionsTP.Controls.Add(drawHelpCB);
            optionsTP.Controls.Add(rotationSnapCB);
            optionsTP.Location = new System.Drawing.Point(4, 24);
            optionsTP.Name = "optionsTP";
            optionsTP.Padding = new System.Windows.Forms.Padding(3);
            optionsTP.Size = new System.Drawing.Size(129, 349);
            optionsTP.TabIndex = 1;
            optionsTP.Text = "Options";
            optionsTP.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ImageExportJpegQualNUD);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(ImageExportFormatCBB);
            groupBox2.Location = new System.Drawing.Point(6, 253);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(99, 86);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Image export";
            // 
            // ImageExportJpegQualNUD
            // 
            ImageExportJpegQualNUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            ImageExportJpegQualNUD.Location = new System.Drawing.Point(56, 51);
            ImageExportJpegQualNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ImageExportJpegQualNUD.Name = "ImageExportJpegQualNUD";
            ImageExportJpegQualNUD.Size = new System.Drawing.Size(41, 23);
            ImageExportJpegQualNUD.TabIndex = 5;
            toolTip.SetToolTip(ImageExportJpegQualNUD, "If .jpeg is selected above, this affects the quality of the export\r\n100 is highest quality\r\nLower means lower quality and smaller file size");
            ImageExportJpegQualNUD.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 53);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(54, 15);
            label3.TabIndex = 4;
            label3.Text = "Jpg qual.";
            // 
            // ImageExportFormatCBB
            // 
            ImageExportFormatCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ImageExportFormatCBB.FormattingEnabled = true;
            ImageExportFormatCBB.IntegralHeight = false;
            ImageExportFormatCBB.Items.AddRange(new object[] { ".jpeg", ".png", ".gif" });
            ImageExportFormatCBB.Location = new System.Drawing.Point(6, 22);
            ImageExportFormatCBB.Name = "ImageExportFormatCBB";
            ImageExportFormatCBB.Size = new System.Drawing.Size(87, 23);
            ImageExportFormatCBB.TabIndex = 0;
            toolTip.SetToolTip(ImageExportFormatCBB, "When you export a single frame or layer,\r\nthe resulting file will be in this format.");
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(toolRecordRB);
            groupBox4.Controls.Add(toolPaintRB);
            groupBox4.Controls.Add(toolMoveRB);
            groupBox4.Location = new System.Drawing.Point(6, 147);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(99, 100);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "Mode";
            // 
            // toolRecordRB
            // 
            toolRecordRB.AutoSize = true;
            toolRecordRB.Location = new System.Drawing.Point(6, 72);
            toolRecordRB.Name = "toolRecordRB";
            toolRecordRB.Size = new System.Drawing.Size(62, 19);
            toolRecordRB.TabIndex = 2;
            toolRecordRB.Text = "Record";
            toolRecordRB.UseVisualStyleBackColor = true;
            // 
            // toolPaintRB
            // 
            toolPaintRB.AutoSize = true;
            toolPaintRB.Location = new System.Drawing.Point(6, 47);
            toolPaintRB.Name = "toolPaintRB";
            toolPaintRB.Size = new System.Drawing.Size(52, 19);
            toolPaintRB.TabIndex = 1;
            toolPaintRB.Text = "Paint";
            toolPaintRB.UseVisualStyleBackColor = true;
            // 
            // toolMoveRB
            // 
            toolMoveRB.AutoSize = true;
            toolMoveRB.Checked = true;
            toolMoveRB.Location = new System.Drawing.Point(6, 22);
            toolMoveRB.Name = "toolMoveRB";
            toolMoveRB.Size = new System.Drawing.Size(55, 19);
            toolMoveRB.TabIndex = 0;
            toolMoveRB.TabStop = true;
            toolMoveRB.Text = "Move";
            toolMoveRB.UseVisualStyleBackColor = true;
            // 
            // mpbAAModeCBB
            // 
            mpbAAModeCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            mpbAAModeCBB.FormattingEnabled = true;
            mpbAAModeCBB.Location = new System.Drawing.Point(6, 118);
            mpbAAModeCBB.Name = "mpbAAModeCBB";
            mpbAAModeCBB.Size = new System.Drawing.Size(99, 23);
            mpbAAModeCBB.TabIndex = 4;
            toolTip.SetToolTip(mpbAAModeCBB, "Sets the interpolation mode of the displayed and exported images.\r\nChoose nearest neighbor for no antialiasing.\r\n");
            // 
            // applyParamsCBB
            // 
            applyParamsCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            applyParamsCBB.DropDownWidth = 150;
            applyParamsCBB.FormattingEnabled = true;
            applyParamsCBB.Location = new System.Drawing.Point(6, 87);
            applyParamsCBB.Name = "applyParamsCBB";
            applyParamsCBB.Size = new System.Drawing.Size(99, 23);
            applyParamsCBB.TabIndex = 3;
            toolTip.SetToolTip(applyParamsCBB, resources.GetString("applyParamsCBB.ToolTip"));
            // 
            // positionSnapCB
            // 
            positionSnapCB.AutoSize = true;
            positionSnapCB.Location = new System.Drawing.Point(6, 33);
            positionSnapCB.Name = "positionSnapCB";
            positionSnapCB.Size = new System.Drawing.Size(97, 19);
            positionSnapCB.TabIndex = 2;
            positionSnapCB.Text = "Position snap";
            toolTip.SetToolTip(positionSnapCB, "When enabled, the layer position will snap to the top left corner of the frame");
            positionSnapCB.UseVisualStyleBackColor = true;
            // 
            // drawHelpCB
            // 
            drawHelpCB.AutoSize = true;
            drawHelpCB.Checked = true;
            drawHelpCB.CheckState = System.Windows.Forms.CheckState.Checked;
            drawHelpCB.Location = new System.Drawing.Point(6, 60);
            drawHelpCB.Name = "drawHelpCB";
            drawHelpCB.Size = new System.Drawing.Size(78, 19);
            drawHelpCB.TabIndex = 1;
            drawHelpCB.Text = "draw help";
            toolTip.SetToolTip(drawHelpCB, "When enabled, red border will be drawn around all layers and the frame itself");
            drawHelpCB.UseVisualStyleBackColor = true;
            // 
            // rotationSnapCB
            // 
            rotationSnapCB.AutoSize = true;
            rotationSnapCB.Location = new System.Drawing.Point(6, 6);
            rotationSnapCB.Name = "rotationSnapCB";
            rotationSnapCB.Size = new System.Drawing.Size(99, 19);
            rotationSnapCB.TabIndex = 0;
            rotationSnapCB.Text = "Rotation snap";
            toolTip.SetToolTip(rotationSnapCB, "If enabled, the layer rotation will snap to the nearest 90°");
            rotationSnapCB.UseVisualStyleBackColor = true;
            // 
            // ControlsPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ControlsPanel";
            Size = new System.Drawing.Size(143, 523);
            tableLayoutPanel1.ResumeLayout(false);
            controlsTC.ResumeLayout(false);
            optionsTP.ResumeLayout(false);
            optionsTP.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl controlsTC;
        private System.Windows.Forms.TabPage optionsTP;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox rotationSnapCB;
        private System.Windows.Forms.CheckBox drawHelpCB;
        private System.Windows.Forms.CheckBox positionSnapCB;
        private System.Windows.Forms.ComboBox applyParamsCBB;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox mpbAAModeCBB;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage settingsTC;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton toolPaintRB;
        private System.Windows.Forms.RadioButton toolMoveRB;
        private System.Windows.Forms.RadioButton toolRecordRB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown ImageExportJpegQualNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ImageExportFormatCBB;
    }
}
