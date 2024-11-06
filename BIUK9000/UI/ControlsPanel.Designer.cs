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
            SaveButton = new System.Windows.Forms.Button();
            exportTC = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            groupBox3 = new System.Windows.Forms.GroupBox();
            useDitheringCB = new System.Windows.Forms.CheckBox();
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
            settingsTC = new System.Windows.Forms.TabPage();
            applyParamsCBB = new System.Windows.Forms.ComboBox();
            positionSnapCB = new System.Windows.Forms.CheckBox();
            drawHelpCB = new System.Windows.Forms.CheckBox();
            rotationSnapCB = new System.Windows.Forms.CheckBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            tableLayoutPanel1.SuspendLayout();
            exportTC.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).BeginInit();
            settingsTC.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(SaveButton, 0, 0);
            tableLayoutPanel1.Controls.Add(exportTC, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(139, 673);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // SaveButton
            // 
            SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            SaveButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            SaveButton.Location = new System.Drawing.Point(3, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new System.Drawing.Size(133, 134);
            SaveButton.TabIndex = 8;
            SaveButton.Text = "Save";
            toolTip.SetToolTip(SaveButton, "Left click - save dialog will open, allowing you to save the gif\r\n\r\nLeft mouse down and drag to desktop / explorer - export current frame as jpeg / png");
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // exportTC
            // 
            exportTC.Controls.Add(tabPage1);
            exportTC.Controls.Add(settingsTC);
            exportTC.Dock = System.Windows.Forms.DockStyle.Fill;
            exportTC.Location = new System.Drawing.Point(3, 143);
            exportTC.Name = "exportTC";
            exportTC.Padding = new System.Drawing.Point(3, 3);
            exportTC.SelectedIndex = 0;
            exportTC.Size = new System.Drawing.Size(133, 527);
            exportTC.TabIndex = 7;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new System.Drawing.Size(125, 499);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "export";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(useDitheringCB);
            groupBox3.Location = new System.Drawing.Point(3, 213);
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
            // groupBox2
            // 
            groupBox2.Controls.Add(ImageExportJpegQualNUD);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(ImageExportFormatCBB);
            groupBox2.Location = new System.Drawing.Point(3, 121);
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
            ImageExportFormatCBB.Items.AddRange(new object[] { ".jpeg", ".png" });
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
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(118, 112);
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
            toolTip.SetToolTip(useGifSicleCB, "When checked, gif will be compressed with Gifsicle");
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
            // settingsTC
            // 
            settingsTC.Controls.Add(applyParamsCBB);
            settingsTC.Controls.Add(positionSnapCB);
            settingsTC.Controls.Add(drawHelpCB);
            settingsTC.Controls.Add(rotationSnapCB);
            settingsTC.Location = new System.Drawing.Point(4, 24);
            settingsTC.Name = "settingsTC";
            settingsTC.Padding = new System.Windows.Forms.Padding(3);
            settingsTC.Size = new System.Drawing.Size(136, 499);
            settingsTC.TabIndex = 1;
            settingsTC.Text = "settings";
            settingsTC.UseVisualStyleBackColor = true;
            // 
            // applyParamsCBB
            // 
            applyParamsCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            applyParamsCBB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            applyParamsCBB.FormattingEnabled = true;
            applyParamsCBB.Location = new System.Drawing.Point(6, 87);
            applyParamsCBB.Name = "applyParamsCBB";
            applyParamsCBB.Size = new System.Drawing.Size(124, 25);
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
            toolTip.SetToolTip(positionSnapCB, "When enabled, the layer position will snap to the top left corner");
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
            // ControlsPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ControlsPanel";
            Size = new System.Drawing.Size(139, 673);
            tableLayoutPanel1.ResumeLayout(false);
            exportTC.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).EndInit();
            settingsTC.ResumeLayout(false);
            settingsTC.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl exportTC;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ImageExportFormatCBB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown GifExportLossyNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown GifExportColorsNUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage settingsTC;
        private System.Windows.Forms.Button SaveButton;
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
    }
}
