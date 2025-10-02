namespace BIUK9000.UI.CustomControls
{
    partial class GifSFDForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            createVideoCB = new System.Windows.Forms.CheckBox();
            currentFramerateLabel = new System.Windows.Forms.Label();
            newFramerateNUD = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            changeFramerateCB = new System.Windows.Forms.CheckBox();
            label5 = new System.Windows.Forms.Label();
            ImageExportJpegQualNUD = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            ImageExportFormatCBB = new System.Windows.Forms.ComboBox();
            createFramesCB = new System.Windows.Forms.CheckBox();
            saveBTN = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            gifskiRB = new System.Windows.Forms.RadioButton();
            animatedGifRB = new System.Windows.Forms.RadioButton();
            animatedGifOptionsGB = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            groupBox5 = new System.Windows.Forms.GroupBox();
            useGifSicleCB = new System.Windows.Forms.CheckBox();
            GifExportLossyNUD = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            GifExportColorsNUD = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            gifExportModeCBB = new System.Windows.Forms.ComboBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            createGifCB = new System.Windows.Forms.CheckBox();
            gifOptionsPanel = new System.Windows.Forms.Panel();
            groupBox2 = new System.Windows.Forms.GroupBox();
            framesOptionsPanel = new System.Windows.Forms.Panel();
            groupBox3 = new System.Windows.Forms.GroupBox();
            changeFramerateOptionsPanel = new System.Windows.Forms.Panel();
            groupBox4 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)newFramerateNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).BeginInit();
            groupBox1.SuspendLayout();
            animatedGifOptionsGB.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            gifOptionsPanel.SuspendLayout();
            groupBox2.SuspendLayout();
            framesOptionsPanel.SuspendLayout();
            groupBox3.SuspendLayout();
            changeFramerateOptionsPanel.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // createVideoCB
            // 
            createVideoCB.AutoSize = true;
            createVideoCB.Location = new System.Drawing.Point(3, 322);
            createVideoCB.Name = "createVideoCB";
            createVideoCB.Size = new System.Drawing.Size(92, 19);
            createVideoCB.TabIndex = 0;
            createVideoCB.Text = "Create video";
            createVideoCB.UseVisualStyleBackColor = true;
            // 
            // currentFramerateLabel
            // 
            currentFramerateLabel.AutoSize = true;
            currentFramerateLabel.Location = new System.Drawing.Point(70, 18);
            currentFramerateLabel.Name = "currentFramerateLabel";
            currentFramerateLabel.Size = new System.Drawing.Size(0, 15);
            currentFramerateLabel.TabIndex = 16;
            // 
            // newFramerateNUD
            // 
            newFramerateNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            newFramerateNUD.Location = new System.Drawing.Point(70, 42);
            newFramerateNUD.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            newFramerateNUD.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            newFramerateNUD.Name = "newFramerateNUD";
            newFramerateNUD.Size = new System.Drawing.Size(57, 23);
            newFramerateNUD.TabIndex = 15;
            newFramerateNUD.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(22, 44);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(37, 15);
            label6.TabIndex = 14;
            label6.Text = "New: ";
            // 
            // changeFramerateCB
            // 
            changeFramerateCB.AutoSize = true;
            changeFramerateCB.Location = new System.Drawing.Point(3, 347);
            changeFramerateCB.Name = "changeFramerateCB";
            changeFramerateCB.Size = new System.Drawing.Size(121, 19);
            changeFramerateCB.TabIndex = 13;
            changeFramerateCB.Text = "Change framerate";
            changeFramerateCB.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(9, 19);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(50, 15);
            label5.TabIndex = 0;
            label5.Text = "Current:";
            // 
            // ImageExportJpegQualNUD
            // 
            ImageExportJpegQualNUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            ImageExportJpegQualNUD.Location = new System.Drawing.Point(192, 23);
            ImageExportJpegQualNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ImageExportJpegQualNUD.Name = "ImageExportJpegQualNUD";
            ImageExportJpegQualNUD.Size = new System.Drawing.Size(41, 23);
            ImageExportJpegQualNUD.TabIndex = 5;
            ImageExportJpegQualNUD.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(132, 25);
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
            ImageExportFormatCBB.Size = new System.Drawing.Size(120, 23);
            ImageExportFormatCBB.TabIndex = 0;
            // 
            // createFramesCB
            // 
            createFramesCB.AutoSize = true;
            createFramesCB.Location = new System.Drawing.Point(3, 227);
            createFramesCB.Name = "createFramesCB";
            createFramesCB.Size = new System.Drawing.Size(99, 19);
            createFramesCB.TabIndex = 12;
            createFramesCB.Text = "Create frames";
            createFramesCB.UseVisualStyleBackColor = true;
            // 
            // saveBTN
            // 
            saveBTN.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            saveBTN.Location = new System.Drawing.Point(3, 465);
            saveBTN.Name = "saveBTN";
            saveBTN.Size = new System.Drawing.Size(292, 102);
            saveBTN.TabIndex = 18;
            saveBTN.Text = "Save";
            saveBTN.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(gifskiRB);
            groupBox1.Controls.Add(animatedGifRB);
            groupBox1.Location = new System.Drawing.Point(16, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(120, 159);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Export library";
            // 
            // gifskiRB
            // 
            gifskiRB.AutoSize = true;
            gifskiRB.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            gifskiRB.Checked = true;
            gifskiRB.Location = new System.Drawing.Point(6, 26);
            gifskiRB.Name = "gifskiRB";
            gifskiRB.Size = new System.Drawing.Size(90, 49);
            gifskiRB.TabIndex = 14;
            gifskiRB.TabStop = true;
            gifskiRB.Text = "Gifski\r\n- best colors\r\n- large files";
            gifskiRB.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            gifskiRB.UseVisualStyleBackColor = true;
            // 
            // animatedGifRB
            // 
            animatedGifRB.AutoSize = true;
            animatedGifRB.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            animatedGifRB.Location = new System.Drawing.Point(6, 81);
            animatedGifRB.Name = "animatedGifRB";
            animatedGifRB.Size = new System.Drawing.Size(99, 64);
            animatedGifRB.TabIndex = 15;
            animatedGifRB.Text = "AnimatedGif\r\n- worse colors\r\n- smaller files\r\nwith gifsicle";
            animatedGifRB.UseVisualStyleBackColor = true;
            // 
            // animatedGifOptionsGB
            // 
            animatedGifOptionsGB.Controls.Add(label4);
            animatedGifOptionsGB.Controls.Add(groupBox5);
            animatedGifOptionsGB.Controls.Add(gifExportModeCBB);
            animatedGifOptionsGB.Location = new System.Drawing.Point(142, 22);
            animatedGifOptionsGB.Name = "animatedGifOptionsGB";
            animatedGifOptionsGB.Size = new System.Drawing.Size(132, 159);
            animatedGifOptionsGB.TabIndex = 16;
            animatedGifOptionsGB.TabStop = false;
            animatedGifOptionsGB.Text = "AnimatedGif options";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 19);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(38, 15);
            label4.TabIndex = 10;
            label4.Text = "Mode";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(useGifSicleCB);
            groupBox5.Controls.Add(GifExportLossyNUD);
            groupBox5.Controls.Add(label2);
            groupBox5.Controls.Add(GifExportColorsNUD);
            groupBox5.Controls.Add(label1);
            groupBox5.Location = new System.Drawing.Point(6, 48);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(118, 102);
            groupBox5.TabIndex = 9;
            groupBox5.TabStop = false;
            groupBox5.Text = "Gifsicle";
            // 
            // useGifSicleCB
            // 
            useGifSicleCB.AutoSize = true;
            useGifSicleCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            useGifSicleCB.Location = new System.Drawing.Point(6, 21);
            useGifSicleCB.Name = "useGifSicleCB";
            useGifSicleCB.Size = new System.Drawing.Size(94, 21);
            useGifSicleCB.TabIndex = 5;
            useGifSicleCB.Text = "Use Gifsicle";
            useGifSicleCB.UseVisualStyleBackColor = true;
            // 
            // GifExportLossyNUD
            // 
            GifExportLossyNUD.Location = new System.Drawing.Point(55, 73);
            GifExportLossyNUD.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            GifExportLossyNUD.Name = "GifExportLossyNUD";
            GifExportLossyNUD.Size = new System.Drawing.Size(57, 23);
            GifExportLossyNUD.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(11, 75);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(36, 15);
            label2.TabIndex = 2;
            label2.Text = "Lossy";
            // 
            // GifExportColorsNUD
            // 
            GifExportColorsNUD.Location = new System.Drawing.Point(55, 48);
            GifExportColorsNUD.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            GifExportColorsNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GifExportColorsNUD.Name = "GifExportColorsNUD";
            GifExportColorsNUD.Size = new System.Drawing.Size(57, 23);
            GifExportColorsNUD.TabIndex = 1;
            GifExportColorsNUD.Value = new decimal(new int[] { 256, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 54);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 15);
            label1.TabIndex = 0;
            label1.Text = "Colors";
            // 
            // gifExportModeCBB
            // 
            gifExportModeCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            gifExportModeCBB.FormattingEnabled = true;
            gifExportModeCBB.Location = new System.Drawing.Point(55, 19);
            gifExportModeCBB.Name = "gifExportModeCBB";
            gifExportModeCBB.Size = new System.Drawing.Size(57, 23);
            gifExportModeCBB.TabIndex = 11;
            toolTip.SetToolTip(gifExportModeCBB, "Bit 8 - no dithering\r\ndefault - dithering");
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(createGifCB);
            flowLayoutPanel1.Controls.Add(gifOptionsPanel);
            flowLayoutPanel1.Controls.Add(createFramesCB);
            flowLayoutPanel1.Controls.Add(framesOptionsPanel);
            flowLayoutPanel1.Controls.Add(createVideoCB);
            flowLayoutPanel1.Controls.Add(changeFramerateCB);
            flowLayoutPanel1.Controls.Add(changeFramerateOptionsPanel);
            flowLayoutPanel1.Controls.Add(saveBTN);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(302, 573);
            flowLayoutPanel1.TabIndex = 22;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // createGifCB
            // 
            createGifCB.AutoSize = true;
            createGifCB.Location = new System.Drawing.Point(3, 3);
            createGifCB.Name = "createGifCB";
            createGifCB.Size = new System.Drawing.Size(80, 19);
            createGifCB.TabIndex = 0;
            createGifCB.Text = "Create GIF";
            createGifCB.UseVisualStyleBackColor = true;
            // 
            // gifOptionsPanel
            // 
            gifOptionsPanel.Controls.Add(groupBox2);
            gifOptionsPanel.Location = new System.Drawing.Point(3, 28);
            gifOptionsPanel.Name = "gifOptionsPanel";
            gifOptionsPanel.Size = new System.Drawing.Size(292, 193);
            gifOptionsPanel.TabIndex = 1;
            gifOptionsPanel.Visible = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox1);
            groupBox2.Controls.Add(animatedGifOptionsGB);
            groupBox2.Location = new System.Drawing.Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(280, 185);
            groupBox2.TabIndex = 19;
            groupBox2.TabStop = false;
            groupBox2.Text = "GIF Export Options";
            // 
            // framesOptionsPanel
            // 
            framesOptionsPanel.Controls.Add(groupBox3);
            framesOptionsPanel.Location = new System.Drawing.Point(3, 252);
            framesOptionsPanel.Name = "framesOptionsPanel";
            framesOptionsPanel.Size = new System.Drawing.Size(292, 64);
            framesOptionsPanel.TabIndex = 13;
            framesOptionsPanel.Visible = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ImageExportJpegQualNUD);
            groupBox3.Controls.Add(ImageExportFormatCBB);
            groupBox3.Controls.Add(label3);
            groupBox3.Location = new System.Drawing.Point(3, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(280, 58);
            groupBox3.TabIndex = 19;
            groupBox3.TabStop = false;
            groupBox3.Text = "Frames Export Options";
            // 
            // changeFramerateOptionsPanel
            // 
            changeFramerateOptionsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            changeFramerateOptionsPanel.Controls.Add(groupBox4);
            changeFramerateOptionsPanel.Location = new System.Drawing.Point(3, 372);
            changeFramerateOptionsPanel.Name = "changeFramerateOptionsPanel";
            changeFramerateOptionsPanel.Size = new System.Drawing.Size(292, 87);
            changeFramerateOptionsPanel.TabIndex = 14;
            changeFramerateOptionsPanel.Visible = false;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(currentFramerateLabel);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(newFramerateNUD);
            groupBox4.Location = new System.Drawing.Point(3, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(280, 79);
            groupBox4.TabIndex = 19;
            groupBox4.TabStop = false;
            groupBox4.Text = "Change FPS Options";
            // 
            // GifSFDForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            ClientSize = new System.Drawing.Size(302, 573);
            Controls.Add(flowLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "GifSFDForm";
            Text = "GifSFDForm";
            ((System.ComponentModel.ISupportInitialize)newFramerateNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            animatedGifOptionsGB.ResumeLayout(false);
            animatedGifOptionsGB.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            gifOptionsPanel.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            framesOptionsPanel.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            changeFramerateOptionsPanel.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.GroupBox animatedGifOptionsGB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox useGifSicleCB;
        private System.Windows.Forms.NumericUpDown GifExportLossyNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown GifExportColorsNUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox gifExportModeCBB;
        private System.Windows.Forms.CheckBox createFramesCB;
        private System.Windows.Forms.RadioButton animatedGifRB;
        private System.Windows.Forms.RadioButton gifskiRB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveBTN;
        private System.Windows.Forms.NumericUpDown ImageExportJpegQualNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ImageExportFormatCBB;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox changeFramerateCB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label currentFramerateLabel;
        private System.Windows.Forms.NumericUpDown newFramerateNUD;
        private System.Windows.Forms.CheckBox createVideoCB;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox createGifCB;
        private System.Windows.Forms.Panel gifOptionsPanel;
        private System.Windows.Forms.Panel framesOptionsPanel;
        private System.Windows.Forms.Panel changeFramerateOptionsPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}