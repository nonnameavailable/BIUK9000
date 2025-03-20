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
            panel1 = new System.Windows.Forms.Panel();
            framerateChangeGB = new System.Windows.Forms.GroupBox();
            currentFramerateLabel = new System.Windows.Forms.Label();
            newFramerateNUD = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            changeFramerateCB = new System.Windows.Forms.CheckBox();
            label5 = new System.Windows.Forms.Label();
            framesAsFilesGB = new System.Windows.Forms.GroupBox();
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
            panel1.SuspendLayout();
            framerateChangeGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)newFramerateNUD).BeginInit();
            framesAsFilesGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).BeginInit();
            groupBox1.SuspendLayout();
            animatedGifOptionsGB.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(framerateChangeGB);
            panel1.Controls.Add(framesAsFilesGB);
            panel1.Controls.Add(saveBTN);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(animatedGifOptionsGB);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(296, 416);
            panel1.TabIndex = 0;
            // 
            // framerateChangeGB
            // 
            framerateChangeGB.Controls.Add(currentFramerateLabel);
            framerateChangeGB.Controls.Add(newFramerateNUD);
            framerateChangeGB.Controls.Add(label6);
            framerateChangeGB.Controls.Add(changeFramerateCB);
            framerateChangeGB.Controls.Add(label5);
            framerateChangeGB.Location = new System.Drawing.Point(150, 185);
            framerateChangeGB.Name = "framerateChangeGB";
            framerateChangeGB.Size = new System.Drawing.Size(134, 110);
            framerateChangeGB.TabIndex = 20;
            framerateChangeGB.TabStop = false;
            framerateChangeGB.Text = "Change framerate";
            // 
            // currentFramerateLabel
            // 
            currentFramerateLabel.AutoSize = true;
            currentFramerateLabel.Location = new System.Drawing.Point(67, 40);
            currentFramerateLabel.Name = "currentFramerateLabel";
            currentFramerateLabel.Size = new System.Drawing.Size(0, 15);
            currentFramerateLabel.TabIndex = 16;
            // 
            // newFramerateNUD
            // 
            newFramerateNUD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            newFramerateNUD.Location = new System.Drawing.Point(67, 64);
            newFramerateNUD.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            newFramerateNUD.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            newFramerateNUD.Name = "newFramerateNUD";
            newFramerateNUD.Size = new System.Drawing.Size(57, 23);
            newFramerateNUD.TabIndex = 15;
            newFramerateNUD.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(19, 66);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(37, 15);
            label6.TabIndex = 14;
            label6.Text = "New: ";
            // 
            // changeFramerateCB
            // 
            changeFramerateCB.AutoSize = true;
            changeFramerateCB.Location = new System.Drawing.Point(6, 19);
            changeFramerateCB.Name = "changeFramerateCB";
            changeFramerateCB.Size = new System.Drawing.Size(121, 19);
            changeFramerateCB.TabIndex = 13;
            changeFramerateCB.Text = "Change framerate";
            changeFramerateCB.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 41);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(50, 15);
            label5.TabIndex = 0;
            label5.Text = "Current:";
            // 
            // framesAsFilesGB
            // 
            framesAsFilesGB.Controls.Add(ImageExportJpegQualNUD);
            framesAsFilesGB.Controls.Add(label3);
            framesAsFilesGB.Controls.Add(ImageExportFormatCBB);
            framesAsFilesGB.Controls.Add(createFramesCB);
            framesAsFilesGB.Location = new System.Drawing.Point(12, 185);
            framesAsFilesGB.Name = "framesAsFilesGB";
            framesAsFilesGB.Size = new System.Drawing.Size(132, 110);
            framesAsFilesGB.TabIndex = 19;
            framesAsFilesGB.TabStop = false;
            framesAsFilesGB.Text = "Frames as files";
            // 
            // ImageExportJpegQualNUD
            // 
            ImageExportJpegQualNUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            ImageExportJpegQualNUD.Location = new System.Drawing.Point(85, 78);
            ImageExportJpegQualNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ImageExportJpegQualNUD.Name = "ImageExportJpegQualNUD";
            ImageExportJpegQualNUD.Size = new System.Drawing.Size(41, 23);
            ImageExportJpegQualNUD.TabIndex = 5;
            ImageExportJpegQualNUD.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 80);
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
            ImageExportFormatCBB.Location = new System.Drawing.Point(6, 49);
            ImageExportFormatCBB.Name = "ImageExportFormatCBB";
            ImageExportFormatCBB.Size = new System.Drawing.Size(120, 23);
            ImageExportFormatCBB.TabIndex = 0;
            // 
            // createFramesCB
            // 
            createFramesCB.AutoSize = true;
            createFramesCB.Location = new System.Drawing.Point(6, 22);
            createFramesCB.Name = "createFramesCB";
            createFramesCB.Size = new System.Drawing.Size(99, 19);
            createFramesCB.TabIndex = 12;
            createFramesCB.Text = "Create frames";
            createFramesCB.UseVisualStyleBackColor = true;
            // 
            // saveBTN
            // 
            saveBTN.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            saveBTN.Location = new System.Drawing.Point(12, 301);
            saveBTN.Name = "saveBTN";
            saveBTN.Size = new System.Drawing.Size(272, 102);
            saveBTN.TabIndex = 18;
            saveBTN.Text = "Save";
            saveBTN.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(gifskiRB);
            groupBox1.Controls.Add(animatedGifRB);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(132, 167);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Export library";
            // 
            // gifskiRB
            // 
            gifskiRB.AutoSize = true;
            gifskiRB.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            gifskiRB.Checked = true;
            gifskiRB.Location = new System.Drawing.Point(6, 17);
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
            animatedGifRB.Location = new System.Drawing.Point(6, 82);
            animatedGifRB.Name = "animatedGifRB";
            animatedGifRB.Size = new System.Drawing.Size(99, 64);
            animatedGifRB.TabIndex = 15;
            animatedGifRB.TabStop = true;
            animatedGifRB.Text = "AnimatedGif\r\n- worse colors\r\n- smaller files\r\nwith gifsicle";
            animatedGifRB.UseVisualStyleBackColor = true;
            // 
            // animatedGifOptionsGB
            // 
            animatedGifOptionsGB.Controls.Add(label4);
            animatedGifOptionsGB.Controls.Add(groupBox5);
            animatedGifOptionsGB.Controls.Add(gifExportModeCBB);
            animatedGifOptionsGB.Location = new System.Drawing.Point(150, 12);
            animatedGifOptionsGB.Name = "animatedGifOptionsGB";
            animatedGifOptionsGB.Size = new System.Drawing.Size(134, 167);
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
            groupBox5.Size = new System.Drawing.Size(118, 114);
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
            GifExportLossyNUD.Location = new System.Drawing.Point(55, 79);
            GifExportLossyNUD.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            GifExportLossyNUD.Name = "GifExportLossyNUD";
            GifExportLossyNUD.Size = new System.Drawing.Size(57, 23);
            GifExportLossyNUD.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 85);
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
            // GifSFDForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(296, 416);
            Controls.Add(panel1);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "GifSFDForm";
            Text = "GifSFDForm";
            panel1.ResumeLayout(false);
            framerateChangeGB.ResumeLayout(false);
            framerateChangeGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)newFramerateNUD).EndInit();
            framesAsFilesGB.ResumeLayout(false);
            framesAsFilesGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            animatedGifOptionsGB.ResumeLayout(false);
            animatedGifOptionsGB.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.GroupBox framesAsFilesGB;
        private System.Windows.Forms.NumericUpDown ImageExportJpegQualNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ImageExportFormatCBB;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox framerateChangeGB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox changeFramerateCB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label currentFramerateLabel;
        private System.Windows.Forms.NumericUpDown newFramerateNUD;
    }
}