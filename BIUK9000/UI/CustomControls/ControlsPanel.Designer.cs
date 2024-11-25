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
            groupBox4 = new System.Windows.Forms.GroupBox();
            toolPaintRB = new System.Windows.Forms.RadioButton();
            toolMoveRB = new System.Windows.Forms.RadioButton();
            mpbAAModeCBB = new System.Windows.Forms.ComboBox();
            applyParamsCBB = new System.Windows.Forms.ComboBox();
            positionSnapCB = new System.Windows.Forms.CheckBox();
            drawHelpCB = new System.Windows.Forms.CheckBox();
            rotationSnapCB = new System.Windows.Forms.CheckBox();
            exportTP = new System.Windows.Forms.TabPage();
            createFramesCB = new System.Windows.Forms.CheckBox();
            gifExportModeCBB = new System.Windows.Forms.ComboBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            useDitheringCB = new System.Windows.Forms.CheckBox();
            label4 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            ImageExportJpegQualNUD = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            ImageExportFormatCBB = new System.Windows.Forms.ComboBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            useGifSicleCB = new System.Windows.Forms.CheckBox();
            GifExportLossyNUD = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            GifExportColorsNUD = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            toolTip = new System.Windows.Forms.ToolTip(components);
            tableLayoutPanel1.SuspendLayout();
            controlsTC.SuspendLayout();
            optionsTP.SuspendLayout();
            groupBox4.SuspendLayout();
            exportTP.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).BeginInit();
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
            tableLayoutPanel1.Size = new System.Drawing.Size(139, 673);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // saveButton
            // 
            saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            saveButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            saveButton.Location = new System.Drawing.Point(3, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(133, 134);
            saveButton.TabIndex = 8;
            saveButton.Text = "Save";
            toolTip.SetToolTip(saveButton, "Left click - save dialog will open, allowing you to save the gif\r\n\r\nLeft mouse down and drag to desktop / explorer - export current frame as jpeg / png\r\nRight mouse down + drag - export current layer");
            saveButton.UseVisualStyleBackColor = true;
            // 
            // controlsTC
            // 
            controlsTC.Controls.Add(optionsTP);
            controlsTC.Controls.Add(exportTP);
            controlsTC.Dock = System.Windows.Forms.DockStyle.Fill;
            controlsTC.Location = new System.Drawing.Point(3, 143);
            controlsTC.Name = "controlsTC";
            controlsTC.Padding = new System.Drawing.Point(3, 3);
            controlsTC.SelectedIndex = 0;
            controlsTC.Size = new System.Drawing.Size(133, 527);
            controlsTC.TabIndex = 7;
            // 
            // optionsTP
            // 
            optionsTP.Controls.Add(groupBox4);
            optionsTP.Controls.Add(mpbAAModeCBB);
            optionsTP.Controls.Add(applyParamsCBB);
            optionsTP.Controls.Add(positionSnapCB);
            optionsTP.Controls.Add(drawHelpCB);
            optionsTP.Controls.Add(rotationSnapCB);
            optionsTP.Location = new System.Drawing.Point(4, 24);
            optionsTP.Name = "optionsTP";
            optionsTP.Padding = new System.Windows.Forms.Padding(3);
            optionsTP.Size = new System.Drawing.Size(125, 499);
            optionsTP.TabIndex = 1;
            optionsTP.Text = "options";
            optionsTP.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(toolPaintRB);
            groupBox4.Controls.Add(toolMoveRB);
            groupBox4.Location = new System.Drawing.Point(6, 147);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(113, 75);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "Mode";
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
            mpbAAModeCBB.Size = new System.Drawing.Size(113, 23);
            mpbAAModeCBB.TabIndex = 4;
            toolTip.SetToolTip(mpbAAModeCBB, "Sets the interpolation mode of the displayed and exported images.\r\nChoose nearest neighbor for no antialiasing.\r\n");
            // 
            // applyParamsCBB
            // 
            applyParamsCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            applyParamsCBB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            applyParamsCBB.FormattingEnabled = true;
            applyParamsCBB.Location = new System.Drawing.Point(6, 87);
            applyParamsCBB.Name = "applyParamsCBB";
            applyParamsCBB.Size = new System.Drawing.Size(113, 25);
            applyParamsCBB.TabIndex = 3;
            toolTip.SetToolTip(applyParamsCBB, resources.GetString("applyParamsCBB.ToolTip"));
            // 
            // positionSnapCB
            // 
            positionSnapCB.AutoSize = true;
            positionSnapCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            positionSnapCB.Location = new System.Drawing.Point(6, 33);
            positionSnapCB.Name = "positionSnapCB";
            positionSnapCB.Size = new System.Drawing.Size(106, 21);
            positionSnapCB.TabIndex = 2;
            positionSnapCB.Text = "position snap";
            toolTip.SetToolTip(positionSnapCB, "When enabled, the layer position will snap to the top left corner of the frame");
            positionSnapCB.UseVisualStyleBackColor = true;
            // 
            // drawHelpCB
            // 
            drawHelpCB.AutoSize = true;
            drawHelpCB.Checked = true;
            drawHelpCB.CheckState = System.Windows.Forms.CheckState.Checked;
            drawHelpCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            drawHelpCB.Location = new System.Drawing.Point(6, 60);
            drawHelpCB.Name = "drawHelpCB";
            drawHelpCB.Size = new System.Drawing.Size(85, 21);
            drawHelpCB.TabIndex = 1;
            drawHelpCB.Text = "draw help";
            toolTip.SetToolTip(drawHelpCB, "When enabled, red border will be drawn around all layers and the frame itself");
            drawHelpCB.UseVisualStyleBackColor = true;
            // 
            // rotationSnapCB
            // 
            rotationSnapCB.AutoSize = true;
            rotationSnapCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            rotationSnapCB.Location = new System.Drawing.Point(6, 6);
            rotationSnapCB.Name = "rotationSnapCB";
            rotationSnapCB.Size = new System.Drawing.Size(105, 21);
            rotationSnapCB.TabIndex = 0;
            rotationSnapCB.Text = "rotation snap";
            toolTip.SetToolTip(rotationSnapCB, "If enabled, the layer rotation will snap to the nearest 90°");
            rotationSnapCB.UseVisualStyleBackColor = true;
            // 
            // exportTP
            // 
            exportTP.AutoScroll = true;
            exportTP.Controls.Add(createFramesCB);
            exportTP.Controls.Add(gifExportModeCBB);
            exportTP.Controls.Add(groupBox3);
            exportTP.Controls.Add(label4);
            exportTP.Controls.Add(groupBox2);
            exportTP.Controls.Add(groupBox1);
            exportTP.Location = new System.Drawing.Point(4, 24);
            exportTP.Name = "exportTP";
            exportTP.Size = new System.Drawing.Size(125, 499);
            exportTP.TabIndex = 0;
            exportTP.Text = "export";
            exportTP.UseVisualStyleBackColor = true;
            // 
            // createFramesCB
            // 
            createFramesCB.AutoSize = true;
            createFramesCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            createFramesCB.Location = new System.Drawing.Point(9, 38);
            createFramesCB.Name = "createFramesCB";
            createFramesCB.Size = new System.Drawing.Size(107, 21);
            createFramesCB.TabIndex = 8;
            createFramesCB.Text = "create frames";
            toolTip.SetToolTip(createFramesCB, "Your work will be exported not only as a single .gif file,\r\nbut also as a yourGifName_Frames folder, containing the frames,\r\nexported as images in the format specified below.");
            createFramesCB.UseVisualStyleBackColor = true;
            // 
            // gifExportModeCBB
            // 
            gifExportModeCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            gifExportModeCBB.FormattingEnabled = true;
            gifExportModeCBB.Location = new System.Drawing.Point(58, 9);
            gifExportModeCBB.Name = "gifExportModeCBB";
            gifExportModeCBB.Size = new System.Drawing.Size(57, 23);
            gifExportModeCBB.TabIndex = 7;
            toolTip.SetToolTip(gifExportModeCBB, "Affects the quality of the gif.\r\nBit8 - highest quality\r\nBit4 - less colors, lower file size\r\nDefault - dithering\r\nGrayscale - obvious");
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(useDitheringCB);
            groupBox3.Location = new System.Drawing.Point(4, 277);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(118, 54);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "My dithering";
            // 
            // useDitheringCB
            // 
            useDitheringCB.AutoSize = true;
            useDitheringCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            useDitheringCB.Location = new System.Drawing.Point(6, 22);
            useDitheringCB.Name = "useDitheringCB";
            useDitheringCB.Size = new System.Drawing.Size(103, 21);
            useDitheringCB.TabIndex = 4;
            useDitheringCB.Text = "use dithering";
            toolTip.SetToolTip(useDitheringCB, "Enables dithering for both the Gif and single image.\r\nThis is my own, idiotic implementation of Floyd-Steinberg dithering and it is very slow.");
            useDitheringCB.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label4.Location = new System.Drawing.Point(9, 9);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(42, 17);
            label4.TabIndex = 6;
            label4.Text = "mode";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ImageExportJpegQualNUD);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(ImageExportFormatCBB);
            groupBox2.Location = new System.Drawing.Point(4, 185);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(118, 86);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Single image";
            // 
            // ImageExportJpegQualNUD
            // 
            ImageExportJpegQualNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            ImageExportJpegQualNUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            ImageExportJpegQualNUD.Location = new System.Drawing.Point(71, 51);
            ImageExportJpegQualNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ImageExportJpegQualNUD.Name = "ImageExportJpegQualNUD";
            ImageExportJpegQualNUD.Size = new System.Drawing.Size(41, 25);
            ImageExportJpegQualNUD.TabIndex = 5;
            toolTip.SetToolTip(ImageExportJpegQualNUD, "If .jpeg is selected above, this affects the quality of the export\r\n100 is highest quality\r\nLower means lower quality and smaller file size");
            ImageExportJpegQualNUD.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label3.Location = new System.Drawing.Point(6, 53);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 17);
            label3.TabIndex = 4;
            label3.Text = "jpg qual.";
            // 
            // ImageExportFormatCBB
            // 
            ImageExportFormatCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ImageExportFormatCBB.FormattingEnabled = true;
            ImageExportFormatCBB.IntegralHeight = false;
            ImageExportFormatCBB.Items.AddRange(new object[] { ".jpeg", ".png", ".gif" });
            ImageExportFormatCBB.Location = new System.Drawing.Point(6, 22);
            ImageExportFormatCBB.Name = "ImageExportFormatCBB";
            ImageExportFormatCBB.Size = new System.Drawing.Size(106, 23);
            ImageExportFormatCBB.TabIndex = 0;
            toolTip.SetToolTip(ImageExportFormatCBB, "Format of a single frame export");
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(useGifSicleCB);
            groupBox1.Controls.Add(GifExportLossyNUD);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(GifExportColorsNUD);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(4, 65);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(118, 114);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Gifsicle";
            // 
            // useGifSicleCB
            // 
            useGifSicleCB.AutoSize = true;
            useGifSicleCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            useGifSicleCB.Location = new System.Drawing.Point(6, 21);
            useGifSicleCB.Name = "useGifSicleCB";
            useGifSicleCB.Size = new System.Drawing.Size(92, 21);
            useGifSicleCB.TabIndex = 5;
            useGifSicleCB.Text = "use Gifsicle";
            toolTip.SetToolTip(useGifSicleCB, "When checked, gif will be compressed with Gifsicle.\r\nThis can sometimes result in the output actually being larger than without using it.");
            useGifSicleCB.UseVisualStyleBackColor = true;
            // 
            // GifExportLossyNUD
            // 
            GifExportLossyNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            GifExportLossyNUD.Location = new System.Drawing.Point(55, 79);
            GifExportLossyNUD.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            GifExportLossyNUD.Name = "GifExportLossyNUD";
            GifExportLossyNUD.Size = new System.Drawing.Size(57, 25);
            GifExportLossyNUD.TabIndex = 3;
            toolTip.SetToolTip(GifExportLossyNUD, "Lossiness of the compression\r\nHigher number means lower quality and smaller file size");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label2.Location = new System.Drawing.Point(6, 85);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(37, 17);
            label2.TabIndex = 2;
            label2.Text = "lossy";
            // 
            // GifExportColorsNUD
            // 
            GifExportColorsNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            GifExportColorsNUD.Location = new System.Drawing.Point(55, 48);
            GifExportColorsNUD.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            GifExportColorsNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GifExportColorsNUD.Name = "GifExportColorsNUD";
            GifExportColorsNUD.Size = new System.Drawing.Size(57, 25);
            GifExportColorsNUD.TabIndex = 1;
            toolTip.SetToolTip(GifExportColorsNUD, "Number of colors in the compressed gif");
            GifExportColorsNUD.Value = new decimal(new int[] { 256, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label1.Location = new System.Drawing.Point(6, 54);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 17);
            label1.TabIndex = 0;
            label1.Text = "colors";
            // 
            // ControlsPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ControlsPanel";
            Size = new System.Drawing.Size(139, 673);
            tableLayoutPanel1.ResumeLayout(false);
            controlsTC.ResumeLayout(false);
            optionsTP.ResumeLayout(false);
            optionsTP.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            exportTP.ResumeLayout(false);
            exportTP.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl controlsTC;
        private System.Windows.Forms.TabPage exportTP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ImageExportFormatCBB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown GifExportLossyNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown GifExportColorsNUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage optionsTP;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.NumericUpDown ImageExportJpegQualNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox rotationSnapCB;
        private System.Windows.Forms.CheckBox drawHelpCB;
        private System.Windows.Forms.CheckBox positionSnapCB;
        private System.Windows.Forms.ComboBox applyParamsCBB;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox useGifSicleCB;
        private System.Windows.Forms.CheckBox useDitheringCB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox mpbAAModeCBB;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage settingsTC;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton toolPaintRB;
        private System.Windows.Forms.RadioButton toolMoveRB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox gifExportModeCBB;
        private System.Windows.Forms.CheckBox createFramesCB;
    }
}
